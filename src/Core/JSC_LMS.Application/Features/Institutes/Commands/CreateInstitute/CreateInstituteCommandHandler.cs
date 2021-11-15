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
    #region- Command Handler for creating institute : by Shivani Goswami
    public class CreateInstituteCommandHandler : IRequestHandler<CreateInstituteCommand, Response<CreateInstituteDto>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// constructor for CreateInstituteCommandHandler : by Shivani Goswami
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="intituteRepository"></param>
        /// <param name="authenticationService"></param>
        public CreateInstituteCommandHandler(IMapper mapper, IInstituteRepository intituteRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _instituteRepository = intituteRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Method for creating new instiute data on API side : by Shivani Goswami
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
                var UserData = new RegistrationRequest() { FirstName = request.createInstituteDto.ContactPerson, LastName = "I", UserName = request.createInstituteDto.Username, Email = request.createInstituteDto.Email, Password = request.createInstituteDto.Password, RoleName = request.createInstituteDto.RoleName };
                var User = await _authenticationService.RegisterAsync(UserData);
                if (User.UserId == null)
                {

                    createInstituteCommandResponse.Succeeded = false;
                    createInstituteCommandResponse.Message = "Email and/or Username already exists.";
                }
                else
                {

                    var data = new Institute()
                    {
                        UserId = User.UserId,
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
            }
            return createInstituteCommandResponse;
        }

    }
    #endregion
}
