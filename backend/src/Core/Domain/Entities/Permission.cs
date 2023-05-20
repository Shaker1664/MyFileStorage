using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Permission : BaseEntity
    {
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public bool CanRead { get; set; }
        public bool CarWrite { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual User User { get; set; }
        public virtual Document Document { get; set; }
    }
}
