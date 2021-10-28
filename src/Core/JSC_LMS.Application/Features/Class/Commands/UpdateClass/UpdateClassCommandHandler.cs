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

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
  public  class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, Response<int>>
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public UpdateClassCommandHandler(IMapper mapper, IClassRepository classRepository)
        {
            _mapper = mapper;
            _classRepository = classRepository;
        }

        public async Task<Response<int>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var classToUpdate = await _classRepository.GetByIdAsync(request.updateClassDto.Id);

            if (classToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Class), request.updateClassDto.Id);
            }

            var validator = new UpdateClassCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            classToUpdate.SchoolId = request.updateClassDto.SchoolId;
            classToUpdate.ClassName = request.updateClassDto.ClassName;
            classToUpdate.IsActive = request.updateClassDto.IsActive;
            
         
            await _classRepository.UpdateAsync(classToUpdate);

            return new Response<int>(request.updateClassDto.Id, "Updated successfully ");

        }
    }
}
