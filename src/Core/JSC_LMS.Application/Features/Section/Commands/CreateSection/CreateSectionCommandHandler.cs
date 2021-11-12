using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Section.Commands.CreateSection
{
   public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Response<CreateSectionDto>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public CreateSectionCommandHandler(IMapper mapper, ISectionRepository sectionRepository)
        {
            _mapper = mapper;
            _sectionRepository = sectionRepository;
        }

        public async Task<Response<CreateSectionDto>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var createSectionCommandResponse = new Response<CreateSectionDto>();
            var validator = new CreateSectionCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createSectionCommandResponse.Succeeded = false;
                createSectionCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createSectionCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var CheckExistingSection = await _sectionRepository.IsSectionName(request.createSectionDto.SectionName, request.createSectionDto.SchoolId,request.createSectionDto.ClassId );
                if (CheckExistingSection)
                {
                    createSectionCommandResponse.Succeeded = false;
                    //createClassCommandResponse.Errors.Add("Class Already Existed");
                    createSectionCommandResponse.Message = "Section Already Existed";

                    return createSectionCommandResponse;
                }

                var data = _mapper.Map<JSC_LMS.Domain.Entities.Section>(request.createSectionDto);
                var sectionData = await _sectionRepository.AddAsync(data);
                createSectionCommandResponse.Data = _mapper.Map<CreateSectionDto>(sectionData);
                createSectionCommandResponse.Succeeded = true;
                createSectionCommandResponse.Message = "success";
            }
            return createSectionCommandResponse;
        }
    }
}

