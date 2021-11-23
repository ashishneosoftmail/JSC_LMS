using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Circulars.Commands.CreateCircular
{

    public class CreateCircularCommandHandler : IRequestHandler<CreateCircularCommand, Response<CreateCircularDto>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;


        public CreateCircularCommandHandler(IMapper mapper, ICircularRepository circularRepository)
        {
            _mapper = mapper;
            _circularRepository = circularRepository;
        }

        public async Task<Response<CreateCircularDto>> Handle(CreateCircularCommand request, CancellationToken cancellationToken)
        {
            var createCircularDto = new Response<CreateCircularDto>();

            var knowledge = await _circularRepository.AddAsync(_mapper.Map<Circular>(request.createCircularDto));
            createCircularDto.Data = _mapper.Map<CreateCircularDto>(knowledge);
            createCircularDto.Succeeded = true;
            createCircularDto.Message = "success";
            return createCircularDto;
        }

    }
}
