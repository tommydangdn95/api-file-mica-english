using Mica.File.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mica.File.Repository
{
    public interface IFileRepository
    {
        IEnumerable<FileUpload> GetFiles();
        FileUpload GetFile(Guid id);
        Task Create(FileUpload file);
        Task Update(FileUpload file);
        Task Delete(FileUpload file);
    }
}
