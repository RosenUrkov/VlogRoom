using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models.Abstractions;
using VlogRoom.Data.Models.Contracts;

namespace VlogRoom.Data.Models
{
    public class Video : BaseModel, IAuditable, IDeletable
    {
        public Video()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string ServiceVideoId { get; set; }

        public string ServiceListItemId { get; set; }

        public string Title { get; set; }

        public int Views { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
