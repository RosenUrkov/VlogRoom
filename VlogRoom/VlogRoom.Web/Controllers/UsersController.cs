using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Room(int id)
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Account()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Account(VideoDataViewModel video)
        {
            throw new NotImplementedException();
        }
    }
}