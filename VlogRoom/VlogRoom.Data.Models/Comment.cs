using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models.Abstractions;
using VlogRoom.Data.Models.Contracts;

namespace VlogRoom.Data.Models
{
    public class Comment : BaseModel, IAuditable, IDeletable
    {    
        public string OwnerUsername { get; set; }

        public string Content { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
