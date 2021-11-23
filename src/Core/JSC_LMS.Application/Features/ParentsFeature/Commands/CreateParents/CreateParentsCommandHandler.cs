using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents
{
    public class CreateParentsCommandHandler : IRequestHandler<CreateParentsCommand, Response<CreateParentsDto>>
    {
        private readonly IParentsRepository _parentsRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;


        public CreateParentsCommandHandler(IMapper mapper, IParentsRepository parentsRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _parentsRepository = parentsRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<CreateParentsDto>> Handle(CreateParentsCommand request, CancellationToken cancellationToken)
        {
            var createParentsCommandResponse = new Response<CreateParentsDto>();

                var UserData = new RegistrationRequest()
                {
                    FirstName = request.createParentDto.ParentName,
                    LastName = "P",
                    UserName = request.createParentDto.Username,
                    Email = request.createParentDto.Email,
                    Password = request.createParentDto.Password,
                    RoleName = "Parent"
                };

                var User = await _authenticationService.RegisterAsync(UserData);
                if (User.UserId == null)
                {

                createParentsCommandResponse.Succeeded = false;
                createParentsCommandResponse.Message = "Email Or Username already registered";
                }
                else
                {
                    var data = new JSC_LMS.Domain.Entities.Parents()
                    {
                        ClassId = request.createParentDto.ClassId,
                        SectionId = request.createParentDto.SectionId,
                        UserId = User.UserId,
                        AddressLine1 = request.createParentDto.AddressLine1,
                        AddressLine2 = request.createParentDto.AddressLine2,
                        ParentName = request.createParentDto.ParentName,
                        StudentId=request.createParentDto.StudentId,
                        UserType = request.createParentDto.UserType,
                        Mobile = request.createParentDto.Mobile,
                        CityId = request.createParentDto.CityId,
                        StateId = request.createParentDto.StateId,
                        ZipId = request.createParentDto.ZipId,
                        IsActive = request.createParentDto.IsActive
                    };
                    var parents = await _parentsRepository.AddAsync(data);
                createParentsCommandResponse.Data = _mapper.Map<CreateParentsDto>(parents);
                createParentsCommandResponse.Succeeded = true;
                createParentsCommandResponse.Message = "success";
                }
            

            return createParentsCommandResponse;
        }

    }
}
