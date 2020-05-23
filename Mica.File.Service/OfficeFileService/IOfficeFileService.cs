using Mica.File.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mica.File.Service.OfficeFileService
{
    public interface IOfficeFileService
    {
        Task<List<Word>> GetListWordAsync(MemoryStream fileUpload);
    }
}
