using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.DocumentFeatures.Commands
{
    public class CreateDocumentCommand : IRequest<Guid>
    {
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int Size { get; set; }
        public string FileUrl { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Guid>
        {
            private readonly IStorageDbContext _context;

            public CreateDocumentCommandHandler(IStorageDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
            {
                var document = new Document()
                {
                    OriginalFileName = request.OriginalFileName,
                    FileName = request.FileName,
                    FileType = request.FileType,
                    Size = request.Size,
                    FileUrl = request.FileUrl,
                    Description = request.Description,
                    UserId = request.UserId
                };

                await _context.SaveChangesAsync();

                return document.Id;
            }
        }
    }
}
