using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Application.Features.UserFeatures.Commands
{
    public class RegisterUserCommand : IRequest<string>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
        {
            private readonly IStorageDbContext _context;

            public RegisterUserCommandHandler(IStorageDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                string response;
                //validating with email
                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    response = "Already registered";
                    return response;
                }

                //validate the email
                if (!ValidEmail(request.Email))
                {
                    response = "Invalid email";
                    return response;
                }

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Created = DateTime.UtcNow,
                    UserName = request.Email,
                    IsEmailPreferredContactMethod = true,
                    IsEnabled = true,
                };

                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                user.SecurityStamp = salt;
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
                user.VerificationToken = GenerateVerificationToken();

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                //send an email to the registered email
                //SendVerificationEmail(user);
                response = "Registration Sucessfull, please check your email for verification";
                return response;

            }

            private string GenerateVerificationToken()
            {
                var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

                //check id the token is unique in the db
                var tokenIsUnique = !_context.Users.Any(u => u.VerificationToken == token);
                if (tokenIsUnique)
                {
                    return GenerateVerificationToken();
                }
                return token;
            }

            private static bool ValidEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                try
                {
                    // Normalize the domain
                    email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));

                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();

                        // Pull out and process domain name (throws ArgumentException on invalid)
                        string domainName = idn.GetAscii(match.Groups[2].Value);

                        return match.Groups[1].Value + domainName;
                    }
                }
                catch (RegexMatchTimeoutException e)
                {
                    var s = e.Data;
                    return false;
                }
                catch (ArgumentException e)
                {
                    var sr = e.Data;
                    return false;
                }

                try
                {
                    return Regex.IsMatch(email,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
        }
    }
}
