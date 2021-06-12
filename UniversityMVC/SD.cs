using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44344/";
        public static string UniversityAPIPath = APIBaseUrl + "api/university/";
        public static string PathWayAPIPath = APIBaseUrl + "api/pathway/";
        public static string UserAPIPath = APIBaseUrl + "api/user/";

        public const string Role_Admin = "Admin";
        public const string Role_Guest = "Guest";
    }
}
