using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Common
{
    public class DateService : IDateService
    {
        public static IDateService Provider { get; set; } = new DateService();

        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
