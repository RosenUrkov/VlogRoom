using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VlogRoom.Web.Common.Constants
{
    public static class GlobalConstants
    {
        public const string SuccessMessage = "Success";
        public const string ErrorMessage = "Error";

        public const string AdministrationRoleName = "Admin";
        public const string LoggingTemplate = "Exception occured on route {0} with message '{1}' and stack trace {2}";

        public const string AlreadyLoggedInMessage = "You are alredy logged in!";
    }
}
