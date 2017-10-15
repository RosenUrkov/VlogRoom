using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VlogRoom.Data.Models;
using VlogRoom.Web.Contracts;
using VlogRoom.Web.Common.Constants;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace VlogRoom.Web.Areas.Administration.Models
{
    public class UserManageViewModel : IMap<User>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.UserNameMaxLength, ErrorMessage = GlobalConstants.UserNameErrorMessage, MinimumLength = GlobalConstants.UserNameMinLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.EmailErrorMessage)]
        public string Email { get; set; }

        [Required]
        [StringLength(GlobalConstants.RoomNameMaxLength, ErrorMessage = GlobalConstants.RoomNameErrorMessage, MinimumLength = GlobalConstants.RoomNameMinLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string RoomName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime? CreatedOn { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime? ModifiedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy HH:mm}")]
        public DateTime? DeletedOn { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}