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

       
        public static string AddNewSubject = "/api/v1/Subject";
        public static string UpdateSubject = "/api/v1/Subject/UpdateSubject";
        public static string GetSubjectByPagination = "/api/v1/Subject/Pagination";
        public static string GetAllSubjectByFilter = "/api/v1/Subject/Filter";
        public static string GetAllSubjectDetails = "/api/v1/Subject/all";
        public static string GetSubjectById = "/api/v1/Subject/id";
        public static string GetAllSection = "/api/v1/Section/all";

    }
}
