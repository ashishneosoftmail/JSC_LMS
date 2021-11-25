using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Helpers
{
    public class UrlHelper
    {
        //User
        public static string UserAuthenticate = "/api/v1/Account/authenticate";
        public static string GetAllRole = "/api/v1/Common/GetAllRole";
        //Common
        public static string GetAllCity = "/api/v1/Common/";
        public static string GetAllState = "/api/v1/Common/GetAllState";
        public static string GetAllZip = "/Zip/";


        public static string CreateInstitute = "/api/v1/Institute";
        public static string GetAllSchool = "/api/v1/School/GetAllSchool";

        public static string AddNewPrincipal = "/api/v1/Principal";
        public static string UpdatePrincipal = "/api/v1/Principal/UpdatePrincipal";
        public static string PrincipalExportExcel = "/api/v1/Principal/exportToExcel";
        public static string AddNewClass = "/api/v1/Class";

        public static string CreateTeacher = "/api/v1/Teacher";

        public static string GetAllPrincipalDetails = "/api/v1/Principal/all";
        public static string GetAllInstituteDetails = "/api/v1/Institute/all";

        public static string GetPrincipalById = "/api/v1/Principal/";
        public static string GetInstituteById = "/api/v1/Institute/";


        public static string GetAllPrincipalByFilter = "​/api/v1/Principal/GetPrincipalByFilter";
        public static string GetAllClassDetails = "/api/v1/Class/all";
        public static string GetClassById = "/api/v1/Class/";
        public static string GetAllPrincipalByPagination = "/api/v1/Principal/Pagination";

        public static string AddNewSection = "/api/v1/Section";

        public static string GetAllClassByFilter = "/api/v1/Class/GetClassByFilter";
        public static string GetAllClassByPagination = "/api/v1/Class/Pagination";

        public static string GetAllInstituteByFilter = "​/api/v1/Institute/GetInstituteByFilter";
        public static string GetAllInstituteByPagination = "/api/v1/Institute/Pagination";

        public static string UpdateClass = "/api/v1/Class/UpdateClass";


        public static string UpdateInstitute = "/api/v1/Institute/UpdateInstitute";

        public static string UpdateSection = "/api/v1/Section/UpdateSection";
        public static string GetAllSectionByPagination = "/api/v1/Section/Pagination";
        public static string GetAllSectionByFilter = "/api/v1/Section/Filter";
        public static string GetAllSectionDetails = "/api/v1/Section/all";
        public static string GetSectionById = "/api/v1/Section/";
        public static string GetAllClass = "/api/v1/Class/GetAllClass";
        public static string UpdatePrincipalPersonalInformation = "/api/v1/Superadmin/UpdateSuperadmin";
        public static string GetSuperadminByUserId = "/api/v1/Superadmin/";


        public static string AddNewSubject = "/api/v1/Subject";
        public static string UpdateSubject = "/api/v1/Subject/UpdateSubject";
        public static string GetSubjectByPagination = "/api/v1/Subject/Pagination";
        public static string GetAllSubjectByFilter = "/api/v1/Subject/Filter";
        public static string GetAllSubjectDetails = "/api/v1/Subject/all";
        public static string GetSubjectById = "/api/v1/Subject/id";
        public static string GetAllSection = "/api/v1/Section/all";

        public static string SuperadminChangePassword = "/api/v1/Superadmin/UpdateSuperadminChangePassword";
        public static string UpdateSuperadminImage = "/api/v1/Superadmin/UpdateSuperadminImage";

        public static string GetTeacherById = "/api/v1/Teacher/";
        public static string GetAllTeacherDetails = "/api/v1/Teacher/all";
        public static string GetAllTeacherByPagination = "/api/v1/Teacher/Pagination";
        public static string GetInstituteByUserId = "/api/v1/Institute/GetInstituteAdminByUserId";

        public static string UpdateInstituteAdminProfileInformation = "/api/v1/Institute/UpdateInstituteAdminProfileInformation";
        public static string UpdateInstituteAdminChangePassword = "/api/v1/Institute/UpdateInstituteAdminChangePassword";

        public static string AddCategory = "/api/v1/Common/AddCategory";
        public static string GetAllCategory = "/api/v1/Common/GetAllCategory";

        public static string AddNewStudent = "/api/v1/Student/Add";
        public static string GetAllStudentDetails = "/api/v1/Student/all";
        public static string GetStudentById = "/api/v1/Student/id";
        public static string GetAllStudentByPagination = "/api/v1/Student/Pagination";
        public static string AddKnowledgeBase = "/api/v1/KnowledgeBase";
        public static string EditKnowledgeBase = "/api/v1/KnowledgeBase/UpdateKnowledgeBase";
        public static string GetKnowledgeBaseById = "/api/v1/KnowledgeBase/";
        public static string GetAllKnowledgeBase = "/api/v1/KnowledgeBase/all";
        public static string GetAllKnowledgeBasePagination = "/api/v1/KnowledgeBase/Pagination";
        public static string GetAllKnowledgeBaseByFilter = "/api/v1/KnowledgeBase/GetKnowledgeBaseByFilter";
        public static string DeleteKnowledgeBase = "/api/v1/KnowledgeBase/";


        public static string GetAcademicByFilters = "/api/v1/Academic/GetAcademicByFilter";
        public static string GetAllAcademicDetails = "/api/v1/Academic/all";
        public static string GetAcademicById = "/api/v1/Academic/id";
        public static string GetAllAcademicByPagination = "/api/v1/Academic/Pagination";
        public static string AddNewAcademic = "/api/v1/Academic";
        public static string UpdateAcademic = "/api/v1/Academic/UpdateAcademic";


        public static string UpdateChangePassword= "/api/v1/Account/UpdateChangePassword";
        public static string UpdateProfileInformation = "/api/v1/Account/UpdateProfileInformation";
        public static string GetPrincipalByUserId = "/api/v1/Principal/GetPrincipalByUserId";

        public static string ForgotPasswordValidateEmail = "/api/v1/Account/TemporaryPasswordValidateEmail";
        public static string VerifyTemporaryPassword = "/api/v1/Account/VerfiyTemporaryPassword";
        public static string UpdateForgotPasswordToNewPassword = "/api/v1/Account/UpdateForgotPasswordToNewPassword";

        public static string AddNewParents = "/api/v1/Parents/Add";
        public static string GetParentsById = "/api/v1/Parents/id";

        public static string GetTeacherByUserId = "/api/v1/Teacher/GetTeacherByUserId";

        public static string GetAllParentsDetails = "/api/v1/Parents/all";
        public static string GetAllParentsByPagination = "/api/v1/Parents/Pagination";
        public static string AddCircular = "/api/v1/Circular";
        public static string GetAllCircularByPagination = "/api/v1/Circular/Pagination";
        public static string GetAllCircularList = "/api/v1/Circular/all";
        public static string GetCircularById = "/api/v1/Circular/";
        public static string DeleteCircularById = "/api/v1/Circular/";
        public static string GetCircularByFilerInstituteAdmin = "/api/v1/Circular/GetCircularByFilter";

        public static string AddAnnouncement = "/api/v1/Announcement/Add";
        public static string GetAnnouncementByPagination = "/api/v1/Announcement/Pagination";
        public static string GetAnnouncementList = "/api/v1/Announcement/all";
        public static string AddFAQ = "/api/v1/FAQ";
        public static string EditFAQ = "/api/v1/FAQ/UpdateFAQBase";
        public static string GetFAQById = "/api/v1/FAQ/";
        public static string GetAllFAQ = "/api/v1/FAQ/all";
        public static string GetAllFAQByPagination = "/api/v1/FAQ/Pagination";
        public static string GetAllFAQByFilters = "/api/v1/FAQ/GetFAQByFilter";
        public static string DeleteFAQ = "/api/v1/FAQ/";

        public static string GetAnnouncementById = "/api/v1/Announcement/id";
    }
}
