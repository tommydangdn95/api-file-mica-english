using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using Mica.File.Application.Command.Create;
using Mica.File.Common;
using Mica.File.Model.Request;
using Mica.File.Models;
using Mica.File.Service;
using Mica.File.Service.OfficeFileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mica.File.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileUploadService _fileUploadService;
        private readonly IOfficeFileService _officeFileService;

        public FileController(IWebHostEnvironment env, IFileUploadService fileUploadService, IOfficeFileService officeFileService)
        {
            this._env = env;
            this._fileUploadService = fileUploadService;
            this._officeFileService = officeFileService;
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcelFile(IFormFile file)
        {
            if (file != null && file.Length >= 0)
            {
                using (var fileStreamMemory = new MemoryStream())
                {
                    await file.CopyToAsync(fileStreamMemory);
                    var id = Guid.NewGuid();

                    // get list word from excel file
                    var listWord = await this._officeFileService.GetListWordAsync(fileStreamMemory);

                    // save file to disk
                    PathRequest pathRequest = new PathRequest
                    {
                        ServerPath = this._env.ContentRootPath,
                        FielType = FileTypeExtension.OfficeExtension.TypeName,
                        FileName = id + System.IO.Path.GetExtension(file.FileName)
                    };
                    var path = this._fileUploadService.GetPath(pathRequest);
                    await this._fileUploadService.SaveFileOnDisk(fileStreamMemory, path);

                    // save file to db
                    CreateFileCommand createFileCommand = new CreateFileCommand
                    {
                        FileName = file.FileName,
                        FileType = (int)FileType.Office,
                        Id = id,
                        Path = path
                    };

                    await this._fileUploadService.SaveFileToDb(createFileCommand);

                    var reponse = new
                    {
                        id
                    };

                    return Ok(reponse);
                }
            }

            return BadRequest();
        }
    }
}