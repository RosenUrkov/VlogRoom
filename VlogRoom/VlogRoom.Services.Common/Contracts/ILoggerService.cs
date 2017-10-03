using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VlogRoom.Services.Common.Contracts
{
    public interface ILoggerService
    {
        void Log(string message);
    }
}
