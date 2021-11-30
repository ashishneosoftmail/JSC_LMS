using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.School.Commands.UpdateSchool
{
    public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand, Response<int>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public UpdateSchoolCommandHandler(IMapper mapper, ISchoolRepository schoolRepository)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
        }

        public async Task<Response<int>> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
        {
            var schoolToUpdate = await _schoolRepository.GetByIdAsync(request.updateSchoolDto.Id);

            if (schoolToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.School), request.updateSchoolDto.Id);
            }

            var validator = new UpdateSchoolCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);
            schoolToUpdate.Id = request.updateSchoolDto.Id;
            schoolToUpdate.InstituteId = request.updateSchoolDto.InstituteId;
            schoolToUpdate.SchoolName = request.updateSchoolDto.SchoolName;
            schoolToUpdate.Address1 = request.updateSchoolDto.Address1;
            schoolToUpdate.Address2 = request.updateSchoolDto.Address2;
            schoolToUpdate.ContactPerson = request.updateSchoolDto.ContactPerson;
            schoolToUpdate.Email = request.updateSchoolDto.Email;
            schoolToUpdate.CityId = request.updateSchoolDto.CityId;
            schoolToUpdate.StateId = request.updateSchoolDto.StateId;
            schoolToUpdate.ZipId = request.updateSchoolDto.ZipId;
            schoolToUpdate.Mobile = request.updateSchoolDto.Mobile;
            schoolToUpdate.IsActive = request.updateSchoolDto.IsActive;
            await _schoolRepository.UpdateAsync(schoolToUpdate);

            return new Response<int>(request.updateSchoolDto.Id, "Updated successfully ");

        }
    }
}
