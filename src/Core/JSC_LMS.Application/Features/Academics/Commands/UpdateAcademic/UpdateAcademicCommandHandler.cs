using AutoMapper;
using FluentValidation;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = JSC_LMS.Application.Exceptions.ValidationException;

namespace JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic
{
   public class UpdateAcademicCommandHandler : IRequestHandler<UpdateAcademicCommand, Response<int>>
    {
        private readonly IAcademicRepository _academicRepository;
        private readonly IMapper _mapper;

        public UpdateAcademicCommandHandler(IMapper mapper, IAcademicRepository academicRepository)
        {
            _mapper = mapper;
            _academicRepository = academicRepository;
        }

        public async Task<Response<int>> Handle(UpdateAcademicCommand request, CancellationToken cancellationToken)
        {
            var academicToUpdate = await _academicRepository.GetByIdAsync(request.updateAcademicDto.Id);

            if (academicToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Academic), request.updateAcademicDto.Id);
            }

            var validator = new UpdateAcademicCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            academicToUpdate.SchoolId = request.updateAcademicDto.SchoolId;
            academicToUpdate.ClassId = request.updateAcademicDto.ClassId;
            academicToUpdate.SectionId = request.updateAcademicDto.SectionId;
            academicToUpdate.SubjectId = request.updateAcademicDto.SubjectId;
            academicToUpdate.TeacherId = request.updateAcademicDto.TeacherId;
            academicToUpdate.Type = request.updateAcademicDto.Type;
            academicToUpdate.CutOff = request.updateAcademicDto.CutOff;
            academicToUpdate.IsActive = request.updateAcademicDto.IsActive;


            await _academicRepository.UpdateAsync(academicToUpdate);

            return new Response<int>(request.updateAcademicDto.Id, "Updated successfully ");

        }
    }
}
