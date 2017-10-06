using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Common;

namespace VlogRoom.Web.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ToType> Map<FromType, ToType>(this IEnumerable<FromType> source)
        {
            return source.Select(x => MappingService.Provider.Map<ToType>(x));
        }
    }
}
