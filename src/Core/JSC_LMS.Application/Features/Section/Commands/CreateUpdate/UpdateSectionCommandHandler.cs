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

namespace JSC_LMS.Application.Features.Section.Commands.CreateUpdate
{
   public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Response<int>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public UpdateSectionCommandHandler(IMapper mapper, ISectionRepository sectionRepository)
        {
            _mapper = mapper;
            _sectionRepository = sectionRepository;
        }

        public async Task<Response<int>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var sectionToUpdate = await _sectionRepository.GetByIdAsync(request.updateSectionDto.Id);

            if (sectionToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Section), request.updateSectionDto.Id);
            }

            var validator = new UpdateSectionCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            sectionToUpdate.SchoolId = request.updateSectionDto.SchoolId;

            sectionToUpdate.ClassId = request.updateSectionDto.ClassId;

            sectionToUpdate.SectionName = request.updateSectionDto.SectionName;
            sectionToUpdate.IsActive = request.updateSectionDto.IsActive;


            await _sectionRepository.UpdateAsync(sectionToUpdate);

            return new Response<int>(request.updateSectionDto.Id, "Updated successfully ");

        }
    }
}
