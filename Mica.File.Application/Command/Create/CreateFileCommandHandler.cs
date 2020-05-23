using Mapster;
using MediatR;
using Mica.File.Domain;
using Mica.File.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mica.File.Application.Command.Create
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand>
    {
        public readonly IFileRepository _fileRepository;
        public CreateFileCommandHandler(IFileRepository fileRepository)
        {
            this._fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.Adapt<FileUpload>();
            if (file != null)
            {
                await _fileRepository.Create(file);
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
