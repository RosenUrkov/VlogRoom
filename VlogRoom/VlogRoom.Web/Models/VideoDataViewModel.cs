﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class VideoDataViewModel : IMap<Video>, IHaveCustomMappings
    {
        public string ServiceVideoId { get; set; }

        public string Title { get; set; }

        public string Duration { get; set; }

        public int Views { get; set; }

        public string ImageUrl { get; set; }

        public string OwnerUsername { get; set; }

        public string OwnerId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Video, VideoDataViewModel>()
                .ForMember(x => x.OwnerUsername, c => c.MapFrom(y => y.User.UserName))
                .ForMember(x => x.OwnerId, c => c.MapFrom(y => y.User.Id));
        }
    }
}