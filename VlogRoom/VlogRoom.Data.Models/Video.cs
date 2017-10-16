using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models.Abstractions;
using VlogRoom.Data.Models.Contracts;

namespace VlogRoom.Data.Models
{
    public class Video : BaseModel, IAuditable, IDeletable
    {
        private const int VideoTitleMinLength = 3;
        private const int VideoTitleMaxLength = 50;
        private const string VideoTitlePattern = "^[a-zA-Z0-9 _!.]+$";
        private const string VideoTitleErrorMessage = "Incorrect video title!";

        private const int VideoDesctiptionMinLength = 3;
        private const int VideoDescriptionMaxLength = 70;
        private const string VideoDescriptionPattern = "^[a-zA-Z0-9 _!.]+$";
        private const string VideoDescriptionErrorMessage = "Incorrect video description!";

        private const int ViewsMinRange = 0;
        private const int ViewsMaxRange = int.MaxValue;
        private const string ViewsErrorMessage = "Views value must be positive integer!";

        [Required]
        public string ServiceVideoId { get; set; }

        [Required]
        public string ServiceListItemId { get; set; }

        [Required]
        [StringLength(VideoTitleMaxLength, MinimumLength = VideoTitleMinLength, ErrorMessage = VideoTitleErrorMessage)]
        [RegularExpression(VideoTitlePattern, ErrorMessage = VideoTitleErrorMessage)]
        public string Title { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = ViewsErrorMessage)]
        public int Views { get; set; }

        [Required]
        [StringLength(VideoDescriptionMaxLength, MinimumLength = VideoDesctiptionMinLength, ErrorMessage = VideoDescriptionErrorMessage)]
        [RegularExpression(VideoDescriptionPattern, ErrorMessage = VideoDescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual User User { get; set; }
    }
}
