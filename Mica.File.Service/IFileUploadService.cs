using Mica.File.Application.Command.Create;
using Mica.File.Model.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Mica.File.Service
{
    public interface IFileUploadService
    {
        string GetPath(PathRequest pathRequest);
        Task SaveFileOnDisk(MemoryStream file, string path);
        Task SaveFilesOnDisk(IEnumerable<MemoryStream> files, string path);
        Task SaveFileToDb(CreateFileCommand file);
    }
}
