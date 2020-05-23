using Mica.File.Common;
using System;

namespace Mica.File.Domain
{
    public class FileUpload
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
    }
}
