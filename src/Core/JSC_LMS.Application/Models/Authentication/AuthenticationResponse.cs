using System;
using System.Collections.Generic;

namespace JSC_LMS.Application.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        /* public string Id { get; set; }
         public string UserName { get; set; }
         public string Email { get; set; }*/
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public User UserDetails { get; set; }
    }
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public Role Role { get; set; }

    }
    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
