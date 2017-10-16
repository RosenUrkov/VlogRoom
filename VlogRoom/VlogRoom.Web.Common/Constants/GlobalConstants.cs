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
        public const string AlphaNumericalPattern = "^[a-zA-Z0-9 _!.]+$";
               
        public const string AlreadyLoggedInMessage = "You are alredy logged in!";
        public const string LoggingTemplate = "Exception occured on route {0} with message '{1}' and stack trace {2}";

        public const string ProviderApplicationName = "VlogRoom";
        public const string ProviderApiKey = "AIzaSyCOpBHSZp8jqgImoRnY7ErzrsnMhibTGxU";
        public const string ProviderPlayListId = "PLuAZD7L_R_m20wOxJPjRRgjMAJSbXIoeL";
        
        public const string VideoCategoryId = "22";
        public const string VideoPrivacyStatus = "private";

        public const string EmailErrorMessage = "Invalid email!";

        public const int UserNameMinLength = 3;
        public const int UserNameMaxLength = 20;
        public const string UserNameErrorMessage = "Invalid username!";
        
        public const int RoomNameMinLength = 3;
        public const int RoomNameMaxLength = 20;
        public const string RoomNameErrorMessage = "Invalid room name!";

        public const int VideoTitleMinLength = 3;
        public const int VideoTitleMaxLength = 50;
        public const string VideoTitleErrorMessage = "Incorrect video title!";
        
        public const int VideoDesctiptionMinLength = 3;
        public const int VideoDescriptionMaxLength = 70;
        public const string VideoDescriptionErrorMessage = "Incorrect video description!";
        
        public const int ViewsMinRange = 0;
        public const int ViewsMaxRange = int.MaxValue;
        public const string ViewsErrorMessage = "Views value must be positive integer!";

        public const string InvalidSearchPatternMessage = "Search pattern must contain only valid characters!";
        public const string InvalidRoomMessage = "The room you want to enter does not exist!";
        public const string InvalidSubscriptionMessage = "The user that you want to subscribe/unsubscribe to does not exist!";
        public const string InvalidVideoMessage = "The video you want to see does not exist or is deleted!";
        public const string InvalidDeleteVideoMessage = "The video you want to delete does not exist or is deleted!";
        public const string InvalidUploadVideoMessage = "Invalid video for uploading!";
    }
}
