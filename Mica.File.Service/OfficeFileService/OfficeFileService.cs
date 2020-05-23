using ClosedXML.Excel;
using Mica.File.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mica.File.Service.OfficeFileService
{
    public class OfficeFileService : IOfficeFileService
    {
        public async Task<List<Word>> GetListWordAsync(MemoryStream fileUpload)
        {

            return await Task.FromResult(GetListWord(fileUpload));
        }

        private List<Word> GetListWord(MemoryStream fileUpload)
        {
            List<Word> listWord = new List<Word>();
            using (var workbook = new XLWorkbook(fileUpload))
            {
                // get first worksheet
                var ws = workbook.Worksheet(1);

                // get row and column is used
                var rows = ws.RangeUsed().RowsUsed().ToList();
                var columns = ws.RangeUsed().ColumnsUsed().ToList();
                foreach (var row in rows)
                {
                    Word word = new Word();
                    int count = 0;
                    foreach (var column in columns)
                    {
                        // get column and row number
                        var columNumber = column.ColumnNumber();
                        var rowNumer = row.RowNumber();

                        // get cell data
                        var cell = ws.Cell(rowNumer, columNumber);
                        var data = cell.Value.ToString();

                        switch (count)
                        {
                            // for first cell of row is Name of Word
                            case 0:
                                word.Name = data;
                                break;

                            // for second cell of row is Sysnonyme of Word
                            case 1:
                                word.Sysnonym = data;
                                break;

                            // for third cell of row is Mean of Word
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
