using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Web.Models
{
    public class UploadFileViewModel
    {
        public HttpPostedFileBase Video { get; set; }

        public string VideoTitle { get; set; }

        public string VideoDescription { get; set; }
    }
}