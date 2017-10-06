using AutoMapper;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Common
{
    public class MappingService : IMappingService
    {
        public static IMappingService Provider { get; set; } = new MappingService();

        public T Map<T>(object from)
        {
            Guard.WhenArgument(from, "Object to map").IsNull().Throw();
            return Mapper.Map<T>(from);
        }
    }
}
