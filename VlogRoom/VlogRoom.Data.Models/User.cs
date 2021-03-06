﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models.Contracts;

namespace VlogRoom.Data.Models
{
    public class User : IdentityUser, IAuditable, IDeletable
    {
        private const int RoomNameMinLength = 5;
        private const int RoomNameMaxLength = 20;
        private const string RoomNameErrorMessage = "Invalid room name!";
        private const string RoomNamePattern = "^[a-zA-Z0-9 _]+$";

        public User() : base()
        {
            this.Videos = new HashSet<Video>();
            this.Subscribers = new HashSet<User>();
            this.Subscribtions = new HashSet<User>();
            this.RoomName = "New room";
        }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [Required]
        [StringLength(RoomNameMaxLength, MinimumLength = RoomNameMinLength)]
        [RegularExpression(RoomNamePattern, ErrorMessage = RoomNameErrorMessage)]
        public string RoomName { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public virtual ICollection<User> Subscribers { get; set; }

        public virtual ICollection<User> Subscribtions { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
