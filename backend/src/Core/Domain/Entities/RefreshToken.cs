using Domain.Common;

namespace Domain.Entities
{
    public partial class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public int TimeToLive { get; set; }
        public Guid UserId { get; set; }
        public string ReplacedByToken { get; set; }
        public DateTime? Revoked { get; set; }
        public string ReasonRevoked { get; set; }
        public virtual User User { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
