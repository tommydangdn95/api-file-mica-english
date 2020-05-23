using Mica.File.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Mica.File.Entity
{
    public class FileDbContext : DbContext
    {
        public FileDbContext(DbContextOptions<FileDbContext> options): base(options) { }
        public DbSet<FileUpload> FileUploads { get; set; }
    }
}
