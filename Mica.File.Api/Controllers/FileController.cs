using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using Mica.File.Models;
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

        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length >= 0)
            {
                // save file
                await SaveFile(file);

                var listWord = await this.GetListWorkAsyn(file);
                if (listWord.Count > 0)
                {
                    return Ok(listWord);
                }
            }

            return BadRequest();
        }


        private async Task SaveFile(IFormFile file)
        {
            string serverPath = _env.ContentRootPath;
            string folderDataName = "Data";
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
            string pathData = System.IO.Path.Combine(serverPath, folderDataName, year, month, day);
            if (!Directory.Exists(pathData))
            {
                Directory.CreateDirectory(pathData);
                
            }

            var pathFileData = System.IO.Path.Combine(pathData, fileName);
            using (var fileStrem = new FileStream(pathFileData, FileMode.Create))
            {
                await file.CopyToAsync(fileStrem);
            }
        }

        private async Task<List<Word>> GetListWorkAsyn(IFormFile file)
        {
            return await Task.FromResult(GetListWord(file));
        }


        private List<Word> GetListWord(IFormFile file) 
        {
            List<Word> listWord = new List<Word>();
            using (var workbook = new XLWorkbook(file.OpenReadStream()))
            {
                var ws = workbook.Worksheet(1);
                var rows = ws.RangeUsed().RowsUsed().Skip(1).ToList();
                var columns = ws.RangeUsed().ColumnsUsed().ToList();
                foreach (var row in rows)
                {
                    Word word = new Word();
                    int count = 0;
                    foreach (var column in columns)
                    {
                        var columNumber = column.ColumnNumber();
                        var rowNumer = row.RowNumber();
                        var cell = ws.Cell(rowNumer, columNumber);
                        var data = cell.Value.ToString();
                        switch (count)
                        {
                            case 0:
                                word.Name = data;
                                break;
                            case 1:
                                word.Sysnonym = data;
                                break;
                            default:
                                word.Mean = data;
                                break;
                        }
                        count++;
                    }

                    listWord.Add(word);
                }
            }

            return listWord;
        }
    }
}