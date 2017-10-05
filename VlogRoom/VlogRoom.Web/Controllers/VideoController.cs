using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class VideoController : Controller
    {   
        public ActionResult Index()
        {
            return null;
        }
    }
}