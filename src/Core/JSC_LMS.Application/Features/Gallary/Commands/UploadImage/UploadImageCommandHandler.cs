using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JSC_LMS.Domain.Entities;

namespace JSC_LMS.Application.Features.Gallary.Commands.UploadImage
{

        public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Response<UploadImageDto>>
        {
            private readonly IGallaryRepository _gallaryRepository;
            private readonly IMapper _mapper;


            public UploadImageCommandHandler(IMapper mapper, IGallaryRepository gallaryRepository)
            {
                _mapper = mapper;
            _gallaryRepository = gallaryRepository;
            }

            public async Task<Response<UploadImageDto>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
            {
                var uploadImageDto = new Response<UploadImageDto>();

                var gallary = await _gallaryRepository.AddAsync(_mapper.Map<JSC_LMS.Domain.Entities.Gallary>(request.uploadImageDto));
            uploadImageDto.Data = _mapper.Map<UploadImageDto>(gallary);
            uploadImageDto.Succeeded = true;
            uploadImageDto.Message = "success";
                return uploadImageDto;
            }

        }
    }

