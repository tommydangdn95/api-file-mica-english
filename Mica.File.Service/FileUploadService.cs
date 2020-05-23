using MediatR;
using Mica.File.Application.Command.Create;
using Mica.File.Domain;
using Mica.File.Model.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mica.File.Service
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IMediator _mediator;
        public FileUploadService(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public string GetPath(PathRequest pathRequest)
        {
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString();
            var date = DateTime.Now.Day.ToString();
            var pathDirectory = Path.Combine(pathRequest.ServerPath, pathRequest.FielType, year, month, date);
            if (string.IsNullOrEmpty(pathRequest.FileName))
            {
                return pathDirectory;
            }

            return Path.Combine(pathDirectory, pathRequest.FileName);
        }

        public async Task SaveFileOnDisk(MemoryStream file, string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    await file.FlushAsync();
                }
            }
        }

        public async Task SaveFilesOnDisk(IEnumerable<MemoryStream> files, string path)
        {
            foreach(var file in files)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    await file.FlushAsync();
                };
            }
        }

        public async Task SaveFileToDb(CreateFileCommand createFileCommand)
        {
            await this._mediator.Send(createFileCommand);
        }
    }
}
