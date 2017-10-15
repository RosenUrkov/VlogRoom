using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VlogRoom.Web.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileExtensionsAttribute : ValidationAttribute
    {
        public FileExtensionsAttribute(string fileExtensions)
        {
            this.AllowedExtensions = fileExtensions
                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .ToList();
        }

        private List<string> AllowedExtensions { get; set; }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file != null)
            {
                var fileName = file.FileName;
                return AllowedExtensions.Any(x => fileName.EndsWith(x));
            }

            return false;
        }
    }
}
