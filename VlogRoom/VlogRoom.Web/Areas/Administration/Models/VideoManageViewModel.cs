using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Areas.Administration.Models
{
    public class VideoManageViewModel : IMap<Video>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ServiceVideoId { get; set; }

        public int Views { get; set; }

        public string OwnerUsername { get; set; }

        public string OwnerRoomName { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Video, VideoManageViewModel>()
                .ForMember(x => x.OwnerUsername, c => c.MapFrom(y => y.User.UserName))
                .ForMember(x => x.OwnerRoomName, c => c.MapFrom(y => y.User.RoomName));
        }
    }
}