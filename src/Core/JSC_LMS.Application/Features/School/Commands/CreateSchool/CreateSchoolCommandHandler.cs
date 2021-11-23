using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.School.Commands.CreateSchool
{
    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, Response<CreateSchoolDto>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public CreateSchoolCommandHandler(IMapper mapper, ISchoolRepository schoolRepository)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
        }

        public async Task<Response<CreateSchoolDto>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            var createSchoolCommandResponse = new Response<CreateSchoolDto>();
            var validator = new CreateSchoolCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createSchoolCommandResponse.Succeeded = false;
                createSchoolCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createSchoolCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var data = _mapper.Map<JSC_LMS.Domain.Entities.School>(request.createSchoolDto);
                var school = await _schoolRepository.AddAsync(data);
                createSchoolCommandResponse.Data = _mapper.Map<CreateSchoolDto>(school);
                createSchoolCommandResponse.Succeeded = true;
                createSchoolCommandResponse.Message = "success";
            }
            return createSchoolCommandResponse;
        }
    }
}
