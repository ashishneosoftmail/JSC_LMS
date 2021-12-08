using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Models.Mail;
using JSC_LMS.Domain.Entities;
using JSC_LMS.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITemporaryPasswordRepository _temporaryPasswordRepository;
        private readonly RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IdentityDbContext _context;
        private IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public SMTPEmailSettings _smtpEmailSettings { get; }
        public AuthenticationService(UserManager<ApplicationUser> userManager,
            RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager,
            IdentityDbContext context, IPasswordHasher<ApplicationUser> passwordHasher, IEmailService emailService, ITemporaryPasswordRepository temporaryPasswordRepository, IOptions<SMTPEmailSettings> smtpEmailSettings, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _context = context;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _temporaryPasswordRepository = temporaryPasswordRepository;
            _smtpEmailSettings = smtpEmailSettings.Value;
            _configuration = configuration;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            AuthenticationResponse response = new AuthenticationResponse();

            if (user == null)
            {
                response.IsAuthenticated = false;
                response.Message = $"Invalid email or password.";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.IsAuthenticated = false;
                response.Message = $"Invalid email or password.";
                return response;
            }
            else
            {
                var roleName = await _userManager.GetRolesAsync(user);
                var roleId = await _roleManager.FindByNameAsync(roleName[0]);
                if (roleId.Id.ToString() != request.Role)
                {
                    response.IsAuthenticated = false;
                    response.Message = $"Invalid email or password.";
                    return response;
                }

                JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);
                    response.RefreshToken = activeRefreshToken.Token;
                    response.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = GenerateRefreshToken();
                    response.RefreshToken = refreshToken.Token;
                    response.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _context.Update(user);
                    _context.SaveChanges();
                }

                response.Message = "succeeded";
                response.IsAuthenticated = true;
                response.UserDetails = new User()
                {
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Role = new Application.Models.Authentication.Role() { RoleId = roleId.Id.ToString(), RoleName = roleName[0] }
                };
                response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            return response;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            RegistrationResponse registrationResponse = new RegistrationResponse();
            if (existingUser != null)
            {
                registrationResponse.UserId = null;
                return registrationResponse;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.RoleName);
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    registrationResponse.UserId = null;
                    return registrationResponse;
                }
            }
            else
            {
                registrationResponse.UserId = null;
                return registrationResponse;
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var response = new RefreshTokenResponse();
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == request.Token));
            if (user == null)
            {
                response.IsAuthenticated = false;
                response.Message = $"Token did not match any users.";
                return response;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);

            if (!refreshToken.IsActive)
            {
                response.IsAuthenticated = false;
                response.Message = $"Token Not Active.";
                return response;
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            await _context.SaveChangesAsync();

            //Generates new jwt
            response.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.RefreshToken = newRefreshToken.Token;
            response.RefreshTokenExpiration = newRefreshToken.Expires;
            return response;
        }

        public async Task<RevokeTokenResponse> RevokeToken(RevokeTokenRequest request)
        {
            var response = new RevokeTokenResponse();
            if (string.IsNullOrEmpty(request.Token))
            {
                response.IsRevoked = false;
                response.Message = "Token is required";
                return response;
            }

            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == request.Token));

            if (user == null)
            {
                response.IsRevoked = false;
                response.Message = "Token did not match any users";
                return response;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            if (!refreshToken.IsActive)
            {
                response.IsRevoked = false;
                response.Message = "Token is not active";
                return response;
            }
            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            await _context.SaveChangesAsync();
            response.IsRevoked = true;
            response.Message = "Token revoked";
            return response;



        }
        public async Task<IEnumerable<RolesResponse>> GetAllRoles()
        {
            var rolesData = _roleManager.Roles;
            List<RolesResponse> response = new List<RolesResponse>();
            foreach (var role in rolesData)
            {
                response.Add(new RolesResponse()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                });
            }
            return response;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userData = _userManager.Users;
            List<User> response = new List<User>();
            foreach (var role in userData)
            {
                response.Add(new User()
                {
                    Id = role.Id,
                    UserName = role.UserName,
                    Email = role.Email
                });
            }
            return response;
        }
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var userExist = await _userManager.FindByIdAsync(request.UserId);
            if (userExist == null)
            {
                return new UpdateUserResponse() { Errors = null, Message = "User Not Found", Succeeded = false };
            }
            else
            {
                if (request.Email != userExist.Email || request.Username!=userExist.UserName)
                {
                    var userList = _userManager.Users;
                    foreach (var user in userList)
                    {
                        if (user.Email == request.Email)
                        {
                            UpdateUserResponse update = new UpdateUserResponse();
                            update.Errors = new List<string>();
                            update.Errors.Add("Email Is Already Taken");
                            return update;
                        }
                        if (user.UserName == request.Username)
                        {
                            UpdateUserResponse update = new UpdateUserResponse();
                            update.Errors = new List<string>();
                            update.Errors.Add("Username Is Already Taken");
                            return update;
                        }
                    }

                    /*if (await _userManager.FindByEmailAsync(request.Email))
                    {
                        UpdateUserResponse update = new UpdateUserResponse();
                        update.Errors = new List<string>();
                        update.Errors.Add("Email Is Already Taken");
                        return update;
                    }*/
                }

                userExist.Email = request.Email;
                userExist.UserName = request.Username;
                IdentityResult result = await _userManager.UpdateAsync(userExist);
                if (result.Succeeded) return new UpdateUserResponse() { UserId = userExist.Id, Errors = null, Message = "", Succeeded = true };
                else
                {
                    UpdateUserResponse update = new UpdateUserResponse();
                    update.Errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        update.Errors.Add(error.Description);
                    }
                    return update;
                }
            }
            return null;
        }
        public async Task<GetUserByIdResponse> GetUserById(string id)
        {
            var userExist = await _userManager.FindByIdAsync(id);
            if (userExist == null)
            {
                return null;
            }
            else
            {

                return new GetUserByIdResponse()
                {
                    Email = userExist.Email,
                    Pasword = userExist.PasswordHash,
                    Username = userExist.UserName
                };
            }
            return null;
        }

        public async Task<ChangeUserPasswordResponse> ChangeUserPassword(string userid, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userid);
            ChangeUserPasswordResponse changePassword = new ChangeUserPasswordResponse();
            if (user == null)
            {
                changePassword.Succeeded = false;
                changePassword.Errors = new List<string>();
                changePassword.Errors.Add("User Not Found");
                changePassword.UserId = null;
                return changePassword;
            }

            var verifyPassword = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
            if (verifyPassword != PasswordVerificationResult.Success)
            {
                changePassword.Succeeded = false;
                changePassword.Errors = new List<string>();
                changePassword.Errors.Add("Please enter correct password");
                changePassword.UserId = null;
                return changePassword;
            }
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                changePassword.Succeeded = false;
                changePassword.UserId = null;
                foreach (var error in result.Errors)
                {
                    changePassword.Errors = new List<string>();
                    changePassword.Errors.Add(error.Description);
                }
                return changePassword;
            }
            await _signInManager.RefreshSignInAsync(user);
            return new ChangeUserPasswordResponse() { Succeeded = true, Errors = null, Message = "Password Changed Successfully", UserId = userid };
        }
        public string GeneratePassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public async Task<TemporaryPasswordEmailValidateResponse> TempPasswordValidateEmail(string email)
        {
            TemporaryPasswordEmailValidateResponse temporaryPasswordEmailValidateResponse = new TemporaryPasswordEmailValidateResponse();
            var emailExist = await _userManager.FindByEmailAsync(email);
            if (emailExist == null)
            {
                temporaryPasswordEmailValidateResponse.Succeeded = false;
                temporaryPasswordEmailValidateResponse.message = "Email Doesn't Exist";
                return temporaryPasswordEmailValidateResponse;
            }
            var password = GeneratePassword();
            var user = await _userManager.FindByIdAsync(emailExist.Id);
            var passwordHash = _passwordHasher.HashPassword(user, password);
            var tempPasswordData = new TemporaryPassword()
            {
                Email = email,
                PasswordHash = passwordHash,
                IsActive = true,
                UserId = emailExist.Id
            };
            var response = await _temporaryPasswordRepository.AddAsync(tempPasswordData);
            /*  Email emailData = new Email();
              emailData.Body = $"Your Password is reset, Kindly use this Password : {password} to Log in on JSC_LMS Portal.";
              emailData.To = email;
              emailData.Subject = "JSC_LMS Forgot Password Recovery";
              var sendEmail = await _emailService.SendEmail(emailData);*/
            var fromEmail = "ashish.verma.neo01@gmail.com";
            var host = "smtp.gmail.com";
            var port = "587";
            var smtppassword = "Ashish12!@";
            var subject = "JSC_LMS Forgot Password Recovery";
            var body = $"Your Password is reset, Kindly use this Password : {password} to Log in on JSC_LMS Portal.";
            var sendEmail = _emailService.SendSmtpEmail(fromEmail, email, smtppassword, subject, body, host, port);
            if (!sendEmail)
            {
                temporaryPasswordEmailValidateResponse.Succeeded = true;
                temporaryPasswordEmailValidateResponse.message = "Email Sent UnSuccessfully";
                return temporaryPasswordEmailValidateResponse;
            }
            if (sendEmail)
            {
                temporaryPasswordEmailValidateResponse.Succeeded = true;
                temporaryPasswordEmailValidateResponse.message = "Email Sent Successfully";
                return temporaryPasswordEmailValidateResponse;

            }
            temporaryPasswordEmailValidateResponse.Succeeded = false;
            temporaryPasswordEmailValidateResponse.message = "User Doen't Exist";
            return temporaryPasswordEmailValidateResponse;
        }

        public async Task<VerifyTemporaryPasswordResponse> VerifyTemporaryPassword(VerfiyTemporaryPasswordRequest verfiyTemporaryPasswordRequest)
        {
            VerifyTemporaryPasswordResponse verifyTemporaryPasswordResponse = new VerifyTemporaryPasswordResponse();
            var getUserByEmail = (await _temporaryPasswordRepository.ListAllAsync()).LastOrDefault(x => x.Email == verfiyTemporaryPasswordRequest.Email && x.IsActive);
            if (getUserByEmail != null)
            {
                var emailExist = await _userManager.FindByEmailAsync(verfiyTemporaryPasswordRequest.Email);
                var user = await _userManager.FindByIdAsync(emailExist.Id);
                var passwordHash = _passwordHasher.VerifyHashedPassword(user, getUserByEmail.PasswordHash, verfiyTemporaryPasswordRequest.TemporaryPassword);

                if (passwordHash == PasswordVerificationResult.Success)
                {
                    if (getUserByEmail.IsActive)
                    {
                        verifyTemporaryPasswordResponse.Succeeded = true;
                        verifyTemporaryPasswordResponse.message = "";
                        getUserByEmail.IsActive = false;
                        await _temporaryPasswordRepository.UpdateAsync(getUserByEmail);
                    }
                }
                else
                {
                    verifyTemporaryPasswordResponse.Succeeded = false;
                    verifyTemporaryPasswordResponse.message = "Invalid Password";
                }
                return verifyTemporaryPasswordResponse;
            }
            verifyTemporaryPasswordResponse.Succeeded = false;
            verifyTemporaryPasswordResponse.message = "User Not Found";
            return verifyTemporaryPasswordResponse;

        }
        public async Task<UpdateResetPasswordResponse> UpdateForgotPasswordToNewPassword(UpdateResetPasswordRequest updateResetPasswordRequest)
        {
            UpdateResetPasswordResponse updateResetPasswordResponse = new UpdateResetPasswordResponse();
            var user = await _userManager.FindByEmailAsync(updateResetPasswordRequest.Email);
            if (user == null)
            {
                updateResetPasswordResponse.Succeeded = false;
                updateResetPasswordResponse.message = "User Not Found";
                return updateResetPasswordResponse;
            }
            var passwordHash = _passwordHasher.HashPassword(user, updateResetPasswordRequest.NewPassword);
            user.PasswordHash = passwordHash;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                updateResetPasswordResponse.Succeeded = false;
                updateResetPasswordResponse.message = "Password Not Updated Successfully";
                return updateResetPasswordResponse;
            }
            await _signInManager.RefreshSignInAsync(user);
            updateResetPasswordResponse.Succeeded = true;
            updateResetPasswordResponse.message = "Password Updated Successfully";
            return updateResetPasswordResponse;
        }
    }
}
