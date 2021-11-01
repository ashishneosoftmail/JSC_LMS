using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Responses;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.ZipCodes.Queries.GetZipcodeListWithCityId
{


    public class GetZipcodeListWithCityIdQueryHandler : IRequestHandler<GetZipcodeListWithCityIdQuery, Response<IEnumerable<Zip>>>
    {
        private readonly IZipRepository _zipRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetZipcodeListWithCityIdQueryHandler(IMapper mapper, ICityRepository cityRepository, IZipRepository zipRepository, ILogger<GetZipcodeListWithCityIdQueryHandler> logger)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _logger = logger;
            _zipRepository = zipRepository;
        }

        public async Task<Response<IEnumerable<Zip>>> Handle(GetZipcodeListWithCityIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var checkCity = await _cityRepository.GetByIdAsync(request.CityId);
            if (checkCity == null)
            {
                throw new NotFoundException(nameof(State), request.CityId);
            }
            var allZipcodesByCityId = (await _zipRepository.ListAllAsync()).Where<Zip>(e => e.CityId == request.CityId);
            List<Zip> zipList = new List<Zip>();
            foreach (var zip in allZipcodesByCityId)
            {
                zipList.Add(new Zip()
                {
                    Id = zip.Id,
                    Zipcode = zip.Zipcode
                });
            }
            //var cities = _mapper.Map<IEnumerable<City>>(allCitiesByStateId);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<Zip>>(zipList, "success");

        }
    }
}
