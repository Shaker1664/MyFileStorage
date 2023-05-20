using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IStorageDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync();
    }
}
