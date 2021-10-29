using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById
{
    
    public class GetInstituteQueryHandler : IRequestHandler<GetInstituteQuery, Response<GetInstituteListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IInstituteRepository _instituteRepository;

        public GetInstituteQueryHandler(IMapper mapper, IInstituteRepository instituteRepository)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
        }

        public async Task<Response<GetInstituteListVm>> Handle(GetInstituteQuery request, CancellationToken cancellationToken)
        {
            var list = await _instituteRepository.GetByIdAsync(request.Id);
            var instituteList = _mapper.Map<GetInstituteListVm>(list);
            return new Response<GetInstituteListVm>(instituteList, "success");
        }


    }
}
