using Domain.Common;

namespace Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public bool IsEmailPreferredContactMethod { get; set; }
        public bool IsPhonePreferredContactMethod { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicturePath { get; set; }
        public string ServiceProvider { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Verified { get; set; }
        public string ResetToken { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public string SecurityStamp { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
