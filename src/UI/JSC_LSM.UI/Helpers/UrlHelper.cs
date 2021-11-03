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

        public static string AddNewClass = "/api/v1/Class";



        public static string GetAllPrincipalDetails = "/api/v1/Principal/all";
        public static string GetAllInstituteDetails = "/api/v1/Institute/all";

        public static string GetPrincipalById = "/api/v1/Principal/";
        public static string GetInstituteById = "/api/v1/Institute/";


        public static string GetAllPrincipalByFilter = "​/api/v1/Principal/GetPrincipalByFilter";
        public static string GetAllClassDetails = "/api/v1/Class/all";
        public static string GetClassById = "/api/v1/Class/";


    }
}
