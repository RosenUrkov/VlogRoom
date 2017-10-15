using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Web.Models
{
    public class UploadVideoViewModel
    {
        [Required]
        [UIHint("VideoFile")]
        [Common.Attributes.FileExtensions("mpg,mov,wmv,mp4,avi,flv,mpg,m4v", ErrorMessage = GlobalConstants.InvalidUploadVideoMessage)]
        public HttpPostedFileBase VideoFile { get; set; }

        [Required]
        [UIHint("Title")]
        [StringLength(GlobalConstants.VideoTitleMaxLength, MinimumLength = GlobalConstants.VideoTitleMinLength, ErrorMessage = GlobalConstants.VideoTitleErrorMessage)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern, ErrorMessage = GlobalConstants.VideoTitleErrorMessage)]
        public string VideoTitle { get; set; }

        [Required]
        [UIHint("Description")]
        [StringLength(GlobalConstants.VideoDescriptionMaxLength, MinimumLength = GlobalConstants.VideoDesctiptionMinLength, ErrorMessage = GlobalConstants.VideoDescriptionErrorMessage)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern, ErrorMessage = GlobalConstants.VideoDescriptionErrorMessage)]
        public string VideoDescription { get; set; }
    }
}