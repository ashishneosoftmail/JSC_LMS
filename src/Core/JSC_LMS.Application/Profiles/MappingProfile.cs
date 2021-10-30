using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Features.Categories.Commands.CreateCateogry;
using JSC_LMS.Application.Features.Categories.Queries.GetCategoriesList;
using JSC_LMS.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
using JSC_LMS.Application.Features.Common.Categories.Queries.GetCategoriesList;
using JSC_LMS.Application.Features.Common.Cities.Queries.GetCitiesListWithStateId;
using JSC_LMS.Application.Features.Common.Roles.Commands.CreateRole;
using JSC_LMS.Application.Features.Common.Roles.Commands.UpdateRole;
using JSC_LMS.Application.Features.Common.Roles.Queries.GetRoleById;
using JSC_LMS.Application.Features.Common.Roles.Queries.GetRolesList;
using JSC_LMS.Application.Features.Common.States.Queries.GetStatesList;
using JSC_LMS.Application.Features.Events.Commands.CreateEvent;
using JSC_LMS.Application.Features.Events.Commands.UpdateEvent;
using JSC_LMS.Application.Features.Events.Queries.GetEventDetail;
using JSC_LMS.Application.Features.Events.Queries.GetEventsExport;
using JSC_LMS.Application.Features.Events.Queries.GetEventsList;
using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Features.Orders.Queries.GetOrdersForMonth;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByFilter;
using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LMS.Domain.Entities;

namespace JSC_LMS.Application.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //Common Dto Mappers
            CreateMap<City, CityDto>();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<State, StateDto>();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<Zip, ZipDto>();
            CreateMap<Zip, ZipDto>().ReverseMap();


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

            //Roles Mapper
            //CreateMap<Role, RolesListVm>();
            // CreateMap<Role, RolesListVm>().ReverseMap();
            CreateMap<Role, CreateRoleCommand>().ReverseMap();
            CreateMap<Role, UpdateRoleCommand>().ReverseMap();
            CreateMap<Role, RoleListVm>().ReverseMap();

            /* //Categories Mapper
             CreateMap<Category, CategoriesListVm>();

             //State Mapper
             CreateMap<State, StatesListVm>();

             //Cities Mapper
             CreateMap<City, CitiesListVm>();*/

            //School Mapper
            CreateMap<School, CreateSchoolDto>();
            CreateMap<School, CreateSchoolDto>().ReverseMap();

            CreateMap<School, UpdateSchoolDto>();
            CreateMap<School, UpdateSchoolDto>().ReverseMap();

            //Institute Mapper
            CreateMap<Institute, CreateInstituteDto>();
            CreateMap<Institute, CreateInstituteDto>().ReverseMap();
            CreateMap<Institute, GetInstituteListVm>();

            //Class Mapper
            CreateMap<Class, CreateClassDto>();
            CreateMap<Class, CreateClassDto>().ReverseMap();

            CreateMap<Class, UpdateClassDto>();
            CreateMap<Class, UpdateClassDto>().ReverseMap();

            CreateMap<Teacher, CreateTeacherDto>();
            CreateMap<Teacher, CreateTeacherDto>().ReverseMap();

            //Principal Mapper
            CreateMap<Principal, CreatePrincipalDto>();
            CreateMap<Principal, CreatePrincipalDto>().ReverseMap();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList.GetPrincipalListDto>();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList.GetPrincipalListDto>().ReverseMap();
            CreateMap<School, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList.SchoolDto>();
            CreateMap<School, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById.SchoolDto>();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById.GetPrincipalByIdDto>();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById.GetPrincipalByIdDto>().ReverseMap();
            CreateMap<School, SchoolFilterDto>();
            CreateMap<Principal, GetPrincipalByFilterDto>();


        }
    }
}
