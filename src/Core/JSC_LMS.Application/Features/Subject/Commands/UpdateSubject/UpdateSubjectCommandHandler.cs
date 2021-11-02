using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Subject.Commands.UpdateSubject
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Response<int>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public UpdateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<Response<int>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectToUpdate = await _subjectRepository.GetByIdAsync(request.updateSubjectDto.Id);

            if (subjectToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Subject), request.updateSubjectDto.Id);
            }

            var validator = new UpdateSubjectCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            subjectToUpdate.SchoolId = request.updateSubjectDto.SchoolId;

            subjectToUpdate.ClassId = request.updateSubjectDto.ClassId;

            subjectToUpdate.SectionId = request.updateSubjectDto.SectionId;

            subjectToUpdate.SubjectName = request.updateSubjectDto.SubjectName;
            subjectToUpdate.IsActive = request.updateSubjectDto.IsActive;


            await _subjectRepository.UpdateAsync(subjectToUpdate);

            return new Response<int>(request.updateSubjectDto.Id, "Updated successfully ");

        }
    }
}
