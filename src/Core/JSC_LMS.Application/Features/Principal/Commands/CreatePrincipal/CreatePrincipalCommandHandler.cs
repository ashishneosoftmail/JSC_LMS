using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal
{

    public class CreatePrincipalCommandHandler : IRequestHandler<CreatePrincipalCommand, Response<CreatePrincipalDto>>
    {
        private readonly IPrincipalRepository _principalRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;


        public CreatePrincipalCommandHandler(IMapper mapper, IPrincipalRepository principalRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _principalRepository = principalRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<CreatePrincipalDto>> Handle(CreatePrincipalCommand request, CancellationToken cancellationToken)
        {
            var createPrincipalCommandResponse = new Response<CreatePrincipalDto>();

            var validator = new CreatePrincipalCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createPrincipalCommandResponse.Succeeded = false;
                createPrincipalCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createPrincipalCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var UserData = new RegistrationRequest() { FirstName = request.createPrincipalDto.Name, LastName = "I", UserName = request.createPrincipalDto.Username, Email = request.createPrincipalDto.Email, Password = request.createPrincipalDto.Password, RoleName = request.createPrincipalDto.RoleName };
                var User = await _authenticationService.RegisterAsync(UserData);
                if (User.UserId == null)
                {

                    createPrincipalCommandResponse.Succeeded = false;
                    createPrincipalCommandResponse.Message = "Email Or Username already registered";
                }
                else
                {
                    var data = new JSC_LMS.Domain.Entities.Principal()
                    {
                        SchoolId = request.createPrincipalDto.SchoolId,
                        UserId = User.UserId,
                        AddressLine1 = request.createPrincipalDto.AddressLine1,
                        AddressLine2 = request.createPrincipalDto.AddressLine2,
                        Name = request.createPrincipalDto.Name,
                        Mobile = request.createPrincipalDto.Mobile,
                        CityId = request.createPrincipalDto.CityId,
                        StateId = request.createPrincipalDto.StateId,
                        ZipId = request.createPrincipalDto.ZipId,
                        IsActive = request.createPrincipalDto.IsActive
                    };
                    var institute = await _principalRepository.AddAsync(data);
                    createPrincipalCommandResponse.Data = _mapper.Map<CreatePrincipalDto>(institute);
                    createPrincipalCommandResponse.Succeeded = true;
                    createPrincipalCommandResponse.Message = "success";
                }
            }

            return createPrincipalCommandResponse;
        }

    }
}
