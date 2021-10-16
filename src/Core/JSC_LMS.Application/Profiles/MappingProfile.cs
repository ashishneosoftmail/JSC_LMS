using AutoMapper;
using JSC_LMS.Application.Features.Categories.Commands.CreateCateogry;
using JSC_LMS.Application.Features.Categories.Queries.GetCategoriesList;
using JSC_LMS.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using JSC_LMS.Application.Features.Events.Commands.CreateEvent;
using JSC_LMS.Application.Features.Events.Commands.UpdateEvent;
using JSC_LMS.Application.Features.Events.Queries.GetEventDetail;
using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using JSC_LMS.Application.Features.Events.Queries.GetEventsList;
using JSC_LMS.Application.Features.Orders.Queries.GetOrdersForMonth;
using JSC_LMS.Domain.Entities;

namespace JSC_LMS.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();

            CreateMap<Order, OrdersForMonthDto>();

            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();
        }
    }
}
