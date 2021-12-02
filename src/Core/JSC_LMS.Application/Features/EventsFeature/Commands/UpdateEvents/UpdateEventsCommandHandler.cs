using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents
{
    public class UpdateEventsCommandHandler : IRequestHandler<UpdateEventsCommand, Response<int>>
    {
        private readonly IEventsTableRepository _eventsRepository;
        private readonly IMapper _mapper;
        public UpdateEventsCommandHandler(IMapper mapper, IEventsTableRepository eventsRepository)
        {
            _mapper = mapper;
            _eventsRepository = eventsRepository;
        }

        public async Task<Response<int>> Handle(UpdateEventsCommand request, CancellationToken cancellationToken)
        {
            var eventsUpdate = await _eventsRepository.GetByIdAsync(request.updateEventsDto.Id);
            Response<int> UpdateEventsCommandResponse = new Response<int>();
            if (eventsUpdate == null)
            {
                UpdateEventsCommandResponse.Message = "No Data Found";
                return UpdateEventsCommandResponse;
            }
            eventsUpdate.EventTitle = request.updateEventsDto.EventTitle;
            eventsUpdate.CoordinatorNumber = request.updateEventsDto.CoordinatorNumber;
            eventsUpdate.EventCoordinator = request.updateEventsDto.EventCoordinator;
            eventsUpdate.EventDateTime = request.updateEventsDto.EventDateTime;
            eventsUpdate.Venue = request.updateEventsDto.Venue;
            eventsUpdate.Image = request.updateEventsDto.Image;           
            eventsUpdate.Description = request.updateEventsDto.Description;
            eventsUpdate.File = request.updateEventsDto.File;
            eventsUpdate.SchoolId = request.updateEventsDto.SchoolId;
            eventsUpdate.Status = request.updateEventsDto.Status;
            eventsUpdate.IsActive = request.updateEventsDto.IsActive;
            await _eventsRepository.UpdateAsync(eventsUpdate);

            return new Response<int>(request.updateEventsDto.Id, "Updated successfully ");
        }
    }
}
