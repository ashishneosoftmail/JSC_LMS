using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Academics.Commands.CreateAcademic
{
    public class CreateAcademicCommandHandler : IRequestHandler<CreateAcademicCommand, Response<CreateAcademicDto>>
    {
        private readonly IAcademicRepository _academicRepository;
        private readonly IMapper _mapper;

        public CreateAcademicCommandHandler(IMapper mapper, IAcademicRepository academicRepository)
        {
            _mapper = mapper;
            _academicRepository = academicRepository;
        }

        public async Task<Response<CreateAcademicDto>> Handle(CreateAcademicCommand request, CancellationToken cancellationToken)
        {
            var createAcademicCommandResponse = new Response<CreateAcademicDto>();

            var validator = new CreateAcademicCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createAcademicCommandResponse.Succeeded = false;
                createAcademicCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createAcademicCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var data = new JSC_LMS.Domain.Entities.Academic()
                {
                    ClassId = request._createAcademicDto.ClassId,
                    SectionId = request._createAcademicDto.SectionId,

                    TeacherId = request._createAcademicDto.TeacherId,
                    SubjectId = request._createAcademicDto.SubjectId,
                    SchoolId = request._createAcademicDto.SchoolId,
                    Type = request._createAcademicDto.Type,
                    CutOff = request._createAcademicDto.CutOff,

                    IsActive = request._createAcademicDto.IsActive
                };
                var academic = await _academicRepository.AddAsync(data);
                createAcademicCommandResponse.Data = _mapper.Map<CreateAcademicDto>(academic);
                createAcademicCommandResponse.Succeeded = true;
                createAcademicCommandResponse.Message = "success";
            }

            return createAcademicCommandResponse;
        }
              
    }
}
