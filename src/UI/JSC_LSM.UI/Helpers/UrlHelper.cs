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
    }
}
