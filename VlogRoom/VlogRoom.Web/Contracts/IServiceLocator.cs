using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Web.Common
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}