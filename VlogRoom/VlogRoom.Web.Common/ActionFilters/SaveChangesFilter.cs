using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VlogRoom.Data.UnitOfWork;

namespace VlogRoom.Web.Common.ActionFilters
{
    public class SaveChangesFilter : IActionFilter
    {
        private readonly IUnitOfWork unitOfWork;

        public SaveChangesFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.unitOfWork.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Just to satisfy the interface. Cannot decouple from it.
        }
    }
}
