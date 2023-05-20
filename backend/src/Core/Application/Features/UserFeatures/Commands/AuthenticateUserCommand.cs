using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.UserFeatures.Commands
{
    public class AuthenticateUserCommand : IRequest<AuthenticationResponse>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticationResponse>
        {
            public Task<AuthenticationResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            bool isMatch = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            return isMatch;
        }
    }

    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
