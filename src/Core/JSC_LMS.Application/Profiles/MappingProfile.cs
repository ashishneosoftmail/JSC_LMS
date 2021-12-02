using AutoMapper;
using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic;
using JSC_LMS.Application.Features.Academics.Queries.GetAcademicList;
using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LMS.Application.Features.Categories.Commands.CreateCateogry;
using JSC_LMS.Application.Features.Categories.Queries.GetCategoriesList;
using JSC_LMS.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularList;
using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
using JSC_LMS.Application.Features.Class.Queries.GetClassByFilter;
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
using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ;
using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;
using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter;
using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase;
using JSC_LMS.Application.Features.Orders.Queries.GetOrdersForMonth;
using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByFilter;
using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
using JSC_LMS.Application.Features.Section.Queries.GetSectionFilter;
using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Subject.Commands.CreateSubject;
using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectById;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter;
using JSC_LMS.Application.Features.Subject.Queries.GetSubjectList;
using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherById;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherFilter;
using JSC_LMS.Application.Features.Teachers.Queries.GetTeacherList;
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

            CreateMap<School, JSC_LMS.Application.Features.Class.Queries.GetClassById.SchoolDto>();
            CreateMap<Class, JSC_LMS.Application.Features.Class.Queries.GetClassById.GetClassByIdDto>();
            CreateMap<Class, JSC_LMS.Application.Features.Class.Queries.GetClassById.GetClassByIdDto>().ReverseMap();

            CreateMap<Teacher, CreateTeacherDto>();
            CreateMap<Teacher, CreateTeacherDto>().ReverseMap();
            CreateMap<Teacher, GetTeacherByIdVm>().ReverseMap();
            CreateMap<Teacher, GetTeacherListDto>();
            CreateMap<Teacher, GetTeacherByFilterDto>();


            //Principal Mapper
            CreateMap<Principal, CreatePrincipalDto>();
            CreateMap<Principal, CreatePrincipalDto>().ReverseMap();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList.GetPrincipalListDto>();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList.GetPrincipalListDto>().ReverseMap();
            CreateMap<School, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalList.SchoolDto>();
            CreateMap<School, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById.SchoolDto>();
            CreateMap<Principal, JSC_LMS.Application.Features.Principal.Queries.GetPrincipalById.GetPrincipalByIdDto>();
            CreateMap<Principal, Features.Principal.Queries.GetPrincipalById.GetPrincipalByIdDto>().ReverseMap();
            CreateMap<School, SchoolFilterDto>();
            CreateMap<Principal, GetPrincipalByFilterDto>();
            CreateMap<School, SchoolFilterDtoVms>();
            CreateMap<Class, GetClassByFilterDto>();
            CreateMap<Institute, GetInstituteFilterVm>();

            //Section Mapper
            CreateMap<Section, CreateSectionDto>();
            CreateMap<Section, CreateSectionDto>().ReverseMap();

            CreateMap<Section, UpdateSectionDto>();
            CreateMap<Section, UpdateSectionDto>().ReverseMap();
            CreateMap<School, JSC_LMS.Application.Features.Section.Queries.GetSectionById.SchoolDto>();
            CreateMap<Class, JSC_LMS.Application.Features.Section.Queries.GetSectionById.ClassDto>();
            CreateMap<Section, JSC_LMS.Application.Features.Section.Queries.GetSectionById.GetSectionByIdDto>();
            CreateMap<Section, Features.Section.Queries.GetSectionById.GetSectionByIdDto>().ReverseMap();

            CreateMap<School, JSC_LMS.Application.Features.Section.Queries.GetSectionList.SchoolDto>();
            CreateMap<Class, JSC_LMS.Application.Features.Section.Queries.GetSectionList.ClassDto>();
            CreateMap<Section, JSC_LMS.Application.Features.Section.Queries.GetSectionList.GetSectionListDto>();
            CreateMap<Section, Features.Section.Queries.GetSectionList.GetSectionListDto>().ReverseMap();

            CreateMap<Subject, CreateSubjectDto>();
            CreateMap<Subject, CreateSubjectDto>().ReverseMap();

            CreateMap<Subject, UpdateSubjectDto>();
            CreateMap<Subject, UpdateSubjectDto>().ReverseMap();
            CreateMap<Subject, GetSubjectByIdVm>().ReverseMap();
            CreateMap<Subject, GetSubjectListDto>();
            CreateMap<Subject, GetSubjectFilterDto>();
            CreateMap<Section, GetSectionFilterDto>();

            CreateMap<KnowledgeBase, CreateKnowledgeBaseDto>();
            CreateMap<KnowledgeBase, CreateKnowledgeBaseDto>().ReverseMap();
            CreateMap<KnowledgeBase, UpdateKnowledgeBaseDto>();
            CreateMap<KnowledgeBase, UpdateKnowledgeBaseDto>().ReverseMap();


            CreateMap<Academic, CreateAcademicDto>();
            CreateMap<Academic, CreateAcademicDto>().ReverseMap();

            CreateMap<Category, CategoriesDto>();
            CreateMap<Category, CategoriesDto>().ReverseMap();

            CreateMap<Students, CreateStudentDto>();
            CreateMap<Students, CreateStudentDto>().ReverseMap();


            CreateMap<Academic, UpdateAcademicDto>();
            CreateMap<Academic, UpdateAcademicDto>().ReverseMap();


            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Academic, GetAcademicListDto>();


            CreateMap<FAQ, CreateFAQDto>();
            CreateMap<FAQ, CreateFAQDto>().ReverseMap();

            CreateMap<FAQ, UpdateFAQDto>();
            CreateMap<FAQ, UpdateFAQDto>().ReverseMap();

            CreateMap<Parents, CreateParentsDto>();
            CreateMap<Parents, CreateParentsDto>().ReverseMap();
            CreateMap<Circular, CreateCircularDto>();
            CreateMap<Circular, CreateCircularDto>().ReverseMap();
            CreateMap<Circular, GetAllCircularListDto>();
            CreateMap<Circular, GetAllCircularListDto>().ReverseMap();

            CreateMap<Announcement, CreateAnnouncementDto>();
            CreateMap<Announcement, CreateAnnouncementDto>().ReverseMap();

            CreateMap<Gallary, UploadImageDto>();
            CreateMap<Gallary, UploadImageDto>().ReverseMap();

        }
    }
}
