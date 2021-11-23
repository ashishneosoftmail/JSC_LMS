using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Response<int>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IPrincipalRepository _principalRepository;
        private readonly IStudentRepository _studentRepository;

        private readonly ITeacherRepository _teacherRepository;
        private readonly IParentsRepository _parentsRepository;

        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public UpdateProfileCommandHandler(IMapper mapper, IInstituteRepository instituteRepository, IPrincipalRepository principalRepository, IStudentRepository studentRepository, IParentsRepository parentsRepository, ITeacherRepository teacherRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _principalRepository = principalRepository;
            _studentRepository = studentRepository;
            _parentsRepository = parentsRepository;
            _teacherRepository = teacherRepository;

            _authenticationService = authenticationService;
        }

        public async Task<Response<int>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.updateProfileInfoDto.roleName == "Institute Admin")
            {
                var instituteadminToUpdate = await _instituteRepository.GetByIdAsync(request.updateProfileInfoDto.Id);
                Response<int> UpdateInstituteadminCommandResponse = new Response<int>();
                if (instituteadminToUpdate == null)
                {
                    UpdateInstituteadminCommandResponse.Message = "No User Found";
                    return UpdateInstituteadminCommandResponse;
                }
                instituteadminToUpdate.ContactPerson = request.updateProfileInfoDto.Name;
                instituteadminToUpdate.Mobile = request.updateProfileInfoDto.Mobile;

                await _instituteRepository.UpdateAsync(instituteadminToUpdate);

                return new Response<int>(request.updateProfileInfoDto.Id, " Updated successfully ");
            }

            else if (request.updateProfileInfoDto.roleName == "Principal")
            {
                var principalToUpdate = await _principalRepository.GetByIdAsync(request.updateProfileInfoDto.Id);
                Response<int> UpdatePrincipalCommandResponse = new Response<int>();
                if (principalToUpdate == null)
                {
                    UpdatePrincipalCommandResponse.Message = "No User Found";
                    return UpdatePrincipalCommandResponse;
                }
                principalToUpdate.Name = request.updateProfileInfoDto.Name;
                principalToUpdate.Mobile = request.updateProfileInfoDto.Mobile;

                await _principalRepository.UpdateAsync(principalToUpdate);

                return new Response<int>(request.updateProfileInfoDto.Id, " Updated successfully ");
            }

            else if (request.updateProfileInfoDto.roleName == "Teacher")
            {
                var teacherToUpdate = await _teacherRepository.GetByIdAsync(request.updateProfileInfoDto.Id);
                Response<int> UpdateTeacherCommandResponse = new Response<int>();
                if (teacherToUpdate == null)
                {
                    UpdateTeacherCommandResponse.Message = "No User Found";
                    return UpdateTeacherCommandResponse;
                }
                teacherToUpdate.TeacherName = request.updateProfileInfoDto.Name;
                teacherToUpdate.Mobile = request.updateProfileInfoDto.Mobile;

                await _teacherRepository.UpdateAsync(teacherToUpdate);

                return new Response<int>(request.updateProfileInfoDto.Id, " Updated successfully ");
            }

            else if (request.updateProfileInfoDto.roleName == "Student")
            {
                var studentToUpdate = await _studentRepository.GetByIdAsync(request.updateProfileInfoDto.Id);
                Response<int> UpdateStudentCommandResponse = new Response<int>();
                if (studentToUpdate == null)
                {
                    UpdateStudentCommandResponse.Message = "No User Found";
                    return UpdateStudentCommandResponse;
                }
                studentToUpdate.StudentName = request.updateProfileInfoDto.Name;
                studentToUpdate.Mobile = request.updateProfileInfoDto.Mobile;

                await _studentRepository.UpdateAsync(studentToUpdate);

                return new Response<int>(request.updateProfileInfoDto.Id, " Updated successfully ");
            }


            else if (request.updateProfileInfoDto.roleName == "Parent")
            {
                var parentToUpdate = await _parentsRepository.GetByIdAsync(request.updateProfileInfoDto.Id);
                Response<int> UpdateParentCommandResponse = new Response<int>();
                if (parentToUpdate == null)
                {
                    UpdateParentCommandResponse.Message = "No User Found";
                    return UpdateParentCommandResponse;
                }
                parentToUpdate.ParentName = request.updateProfileInfoDto.Name;
                parentToUpdate.Mobile = request.updateProfileInfoDto.Mobile;

                await _parentsRepository.UpdateAsync(parentToUpdate);

                return new Response<int>(request.updateProfileInfoDto.Id, " Updated successfully ");
            }

            return null;
        }
    }
}
