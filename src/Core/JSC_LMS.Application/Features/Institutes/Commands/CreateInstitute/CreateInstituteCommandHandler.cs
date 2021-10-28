using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Responses;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute
{

    public class CreateInstituteCommandHandler : IRequestHandler<CreateInstituteCommand, Response<CreateInstituteDto>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;


        public CreateInstituteCommandHandler(IMapper mapper, IInstituteRepository intituteRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _instituteRepository = intituteRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<CreateInstituteDto>> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
        {
            var createInstituteCommandResponse = new Response<CreateInstituteDto>();

            var validator = new CreateInstituteCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createInstituteCommandResponse.Succeeded = false;
                createInstituteCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createInstituteCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var UserData = new RegistrationRequest() { FirstName="",LastName="",UserName=request.createInstituteDto.Username,Email=request.createInstituteDto.Email,Password=request.createInstituteDto.Password,RoleName=request.createInstituteDto.RoleName};
                var User = await _authenticationService.RegisterAsync(UserData);
                var data = new Institute() {
                    UserId =  User.UserId,
                    InstituteName = request.createInstituteDto.InstituteName,
                    AddressLine1 = request.createInstituteDto.AddressLine1,
                    AddressLine2 = request.createInstituteDto.AddressLine2,
                    ContactPerson = request.createInstituteDto.ContactPerson,
                    Mobile = request.createInstituteDto.Mobile,
                    CityId = request.createInstituteDto.CityId,
                    StateId = request.createInstituteDto.StateId,
                    ZipId = request.createInstituteDto.ZipId,
                    LicenseExpiry = request.createInstituteDto.LicenseExpiry,
                    InstituteURL = request.createInstituteDto.InstituteURL,
                    IsActive = request.createInstituteDto.IsActive
                };
                var institute = await _instituteRepository.AddAsync(data);
                createInstituteCommandResponse.Data = _mapper.Map<CreateInstituteDto>(institute);
                createInstituteCommandResponse.Succeeded = true;
                createInstituteCommandResponse.Message = "success";
            }

            return createInstituteCommandResponse;
        }

    }
}
