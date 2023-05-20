using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Document : BaseEntity
    {
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int Size { get; set; }
        public string FileUrl { get; set; }
        public DateTime Uploaded { get; set; }
        public DateTime? Modified { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
