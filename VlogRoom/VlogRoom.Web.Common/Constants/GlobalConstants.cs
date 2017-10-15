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
        public const string AlphaNumericalPattern = "^[a-zA-Z0-9 _]+$";
               
        public const string AlreadyLoggedInMessage = "You are alredy logged in!";
        public const string LoggingTemplate = "Exception occured on route {0} with message '{1}' and stack trace {2}";

        public const string ProviderApplicationName = "VlogRoom";
        public const string ProviderApiKey = "AIzaSyCOpBHSZp8jqgImoRnY7ErzrsnMhibTGxU";
        public const string ProviderPlayListId = "PLuAZD7L_R_m20wOxJPjRRgjMAJSbXIoeL";
        
        public const string VideoCategoryId = "22";
        public const string VideoPrivacyStatus = "private";

        public const string EmailErrorMessage = "Invalid email!";

        public const int UserNameMinLength = 5;
        public const int UserNameMaxLength = 20;
        public const string UserNameErrorMessage = "Invalid username!";
        
        public const int RoomNameMinLength = 5;
        public const int RoomNameMaxLength = 20;
        public const string RoomNameErrorMessage = "Invalid room name!";

        public const int VideoTitleMinLength = 5;
        public const int VideoTitleMaxLength = 15;
        public const string VideoTitleErrorMessage = "Incorrect video title!";
        
        public const int VideoDesctiptionMinLength = 4;
        public const int VideoDescriptionMaxLength = 20;
        public const string VideoDescriptionErrorMessage = "Incorrect video description!";
        
        public const int ViewsMinRange = 0;
        public const int ViewsMaxRange = int.MaxValue;
        public const string ViewsErrorMessage = "Views value must be positive integer!";
    }
}
