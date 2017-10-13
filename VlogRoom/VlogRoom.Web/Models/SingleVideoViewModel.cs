using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VlogRoom.Data.Models;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class SingleVideoViewModel : IMap<Video>, IHaveCustomMappings
    {
        public string ServiceVideoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        public DateTime? CreatedOn { get; set; }

        public string OwnerRoomName { get; set; }

        public string OwnerId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Video, VideoDataViewModel>()
                .ForMember(x => x.OwnerRoomName, c => c.MapFrom(y => y.User.RoomName))
                .ForMember(x => x.OwnerId, c => c.MapFrom(y => y.User.Id));
        }
    }
}