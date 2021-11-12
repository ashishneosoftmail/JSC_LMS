using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Response<CreateSubjectDto>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public CreateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<Response<CreateSubjectDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var createSubjectCommandResponse = new Response<CreateSubjectDto>();
            var validator = new CreateSubjectCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createSubjectCommandResponse.Succeeded = false;
                createSubjectCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createSubjectCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var CheckExistingSubject = await _subjectRepository.IsSubjectName(request.createSubjectDto.SubjectName, request.createSubjectDto.SchoolId, request.createSubjectDto.ClassId,request.createSubjectDto.SectionId);
                if (CheckExistingSubject)
                {
                    createSubjectCommandResponse.Succeeded = false;
                    //createClassCommandResponse.Errors.Add("Class Already Existed");
                    createSubjectCommandResponse.Message = "Subject Already Existed";

                    return createSubjectCommandResponse;
                }
                var data = _mapper.Map<JSC_LMS.Domain.Entities.Subject>(request.createSubjectDto);
                var subjectData = await _subjectRepository.AddAsync(data);
                createSubjectCommandResponse.Data = _mapper.Map<CreateSubjectDto>(subjectData);
                createSubjectCommandResponse.Succeeded = true;
                createSubjectCommandResponse.Message = "success";
            }
            return createSubjectCommandResponse;
        }
    }
}
