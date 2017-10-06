using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public UsersController(IVideoDataService videoDataService)
        {
            this.videoDataService = videoDataService;
        }

        public ActionResult Room(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadVideo(HttpPostedFileBase video)
        {
            this.videoDataService.UploadVideo(video.InputStream);
            return this.Content("Pls work");
        }
    }
}