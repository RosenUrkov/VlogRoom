using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Contracts;
using System.ComponentModel.DataAnnotations;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Web.Areas.Administration.Models
{
    public class VideoManageViewModel : IMap<Video>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.VideoTitleMaxLength, ErrorMessage = GlobalConstants.VideoTitleErrorMessage, MinimumLength = GlobalConstants.VideoTitleMinLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string Title { get; set; }

        [Required]
        public string ServiceVideoId { get; set; }

        [Required]
        [Range(GlobalConstants.ViewsMinRange, GlobalConstants.ViewsMaxRange, ErrorMessage = GlobalConstants.ViewsErrorMessage)]
        public int Views { get; set; }

        [Required]
        [StringLength(GlobalConstants.UserNameMaxLength, ErrorMessage = GlobalConstants.UserNameErrorMessage, MinimumLength = GlobalConstants.UserNameMinLength)]
        public string OwnerUsername { get; set; }

        [Required]
        [StringLength(GlobalConstants.RoomNameMaxLength, ErrorMessage = GlobalConstants.RoomNameErrorMessage, MinimumLength = GlobalConstants.RoomNameMinLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string OwnerRoomName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime? CreatedOn { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime? ModifiedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Video, VideoManageViewModel>()
                .ForMember(x => x.OwnerUsername, c => c.MapFrom(y => y.User.UserName))
                .ForMember(x => x.OwnerRoomName, c => c.MapFrom(y => y.User.RoomName));
        }
    }
}