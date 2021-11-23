using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Authentication;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Class.Commands.CreateClass
{
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Response<CreateClassDto>>
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public CreateClassCommandHandler(IMapper mapper, IClassRepository classRepository)
        {
            _mapper = mapper;
            _classRepository = classRepository;
        }

        public async Task<Response<CreateClassDto>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var createClassCommandResponse = new Response<CreateClassDto>();
            var validator = new CreateClassCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createClassCommandResponse.Succeeded = false;
                createClassCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createClassCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var CheckExistingClass = await _classRepository.IsClassName(request.createClassDto.ClassName,request.createClassDto.SchoolId);
                if (CheckExistingClass)
                {
                    createClassCommandResponse.Succeeded = false;
                    //createClassCommandResponse.Errors.Add("Class Already Existed");
                    createClassCommandResponse.Message = "Class Already Existed";
                   
                    return createClassCommandResponse;
                }
                var data = _mapper.Map<JSC_LMS.Domain.Entities.Class>(request.createClassDto);
                var classData = await _classRepository.AddAsync(data);
                createClassCommandResponse.Data = _mapper.Map<CreateClassDto>(classData);
                createClassCommandResponse.Succeeded = true;
                createClassCommandResponse.Message = "success";
            }
            return createClassCommandResponse;
        }
    }
}
