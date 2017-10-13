using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class UserDataViewModel : IMap<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string RoomName { get; set; }

        public IEnumerable<string> SubscribtionsNames { get; set; }

        public IEnumerable<VideoDataViewModel> Videos { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDataViewModel>()
                .ForMember(x => x.SubscribtionsNames, c => c.MapFrom(y => y.Subscribers.Select(z => z.UserName)))
                .ForMember(x => x.Videos, c => c.MapFrom(y => y.Videos.Where(z => !z.IsDeleted)));
        }
    }
}