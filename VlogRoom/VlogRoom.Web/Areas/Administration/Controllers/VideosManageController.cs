using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministrationRoleName)]
    public class VideosManageController : Controller
    {

    }
}