using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.UserFeatures.Commands
{
    public class VerifyEmailCommand : IRequest<string>
    {
        [Required]
        public string Token { get; set; }

        public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, string>
        {
            private readonly IStorageDbContext _context;
            public VerifyEmailCommandHandler(IStorageDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.VerificationToken == request.Token);
                if (user == null)
                {
                    return "Verification Failed";
                    //throw an exception
                    throw new AppException("Verification failed");
                }

                user.Verified = DateTime.UtcNow;
                user.VerificationToken = string.Empty;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return "Verification succeeded";
            }
        }
    }
}
