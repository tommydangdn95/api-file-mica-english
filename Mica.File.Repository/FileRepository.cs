using Mica.File.Domain;
using Mica.File.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mica.File.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly FileDbContext _fileDbContext;
        public FileRepository(FileDbContext fileDbContext)
        {
            this._fileDbContext = fileDbContext;
        }
        public async Task Create(FileUpload file)
        {
            this._fileDbContext.FileUploads.Add(file);
            await this._fileDbContext.SaveChangesAsync();
        }

        public Task Delete(FileUpload file)
        {
            throw new NotImplementedException();
        }

        public FileUpload GetFile(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileUpload> GetFiles()
        {
            throw new NotImplementedException();
        }

        public Task Update(FileUpload file)
        {
            throw new NotImplementedException();
        }
    }
}
