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

namespace JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents
{
    public class UpdateParentsCommandHandler : IRequestHandler<UpdateParentsCommand, Response<int>>
    {
        private readonly IParentsRepository _parentsRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdateParentsCommandHandler(IMapper mapper, IParentsRepository parentsRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _parentsRepository = parentsRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<int>> Handle(UpdateParentsCommand request, CancellationToken cancellationToken)
        {
            var parentsToUpdate = await _parentsRepository.GetByIdAsync(request.updateParentsDto.Id);
            Response<int> UpdateParentsCommandResponse = new Response<int>();
            if (parentsToUpdate == null)
            {
                UpdateParentsCommandResponse.Message = "No User Found";
                return UpdateParentsCommandResponse;
            }

           

            var userUpdate = new UpdateUserRequest()
            {
                UserId = request.updateParentsDto.UserId,
                Email = request.updateParentsDto.Email,
                Username = request.updateParentsDto.Username
            };

            var updateUser = await _authenticationService.UpdateUser(userUpdate);
            if (!updateUser.Succeeded)
            {
                UpdateParentsCommandResponse.Errors = updateUser.Errors;
                UpdateParentsCommandResponse.Succeeded = false;
                UpdateParentsCommandResponse.Message = "User Already Exist";
                return UpdateParentsCommandResponse;
            }

            parentsToUpdate.ClassId = request.updateParentsDto.ClassId;
            parentsToUpdate.SectionId = request.updateParentsDto.SectionId;
            parentsToUpdate.ParentName = request.updateParentsDto.ParentName;
            parentsToUpdate.StudentId = request.updateParentsDto.StudentId;
            parentsToUpdate.AddressLine1 = request.updateParentsDto.AddressLine1;
            parentsToUpdate.AddressLine2 = request.updateParentsDto.AddressLine2;

            parentsToUpdate.UserId = updateUser.UserId;
            parentsToUpdate.Mobile = request.updateParentsDto.Mobile;
            parentsToUpdate.CityId = request.updateParentsDto.CityId;
            parentsToUpdate.StateId = request.updateParentsDto.StateId;
            parentsToUpdate.ZipId = request.updateParentsDto.ZipId;
            parentsToUpdate.IsActive = request.updateParentsDto.IsActive;
            await _parentsRepository.UpdateAsync(parentsToUpdate);

            return new Response<int>(request.updateParentsDto.Id, "Updated successfully ");
        }
    }
}
