using JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents;
using JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents;
using JSC_LSM.UI.ResponseModels.EventsResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IEventsDetailsRepository
    {
        Task<AddEventsResponseModel> AddEventsData(CreateEventsDto createEventsDto);       
        Task<GetEventsListResponseModel> GetEventsList();
        Task<GetEventsByIdResponseModel> GetEventsById(int Id);
        Task<UpdateEventsResponseModel> UpdateEventsDetails(UpdateEventsDto updateEventsDto);    
        Task<GetAllEventsListBySchoolResponseModel> GetAllEventsBySchoolList(int schoolid);
       

    }
}
