using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Services.Common.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}