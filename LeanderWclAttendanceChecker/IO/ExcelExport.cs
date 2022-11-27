using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using LeanderWclAttendanceChecker.Model;
using static LeanderWclAttendanceChecker.Resource.Constants;

namespace LeanderWclAttendanceChecker.IO
{
    public static class ExcelExport
    {
        public static void WriteOutPutFile(List<Player> players)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(OutputXls, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                    Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Attandance" };

                    sheets.Append(sheet);

                    foreach (Player player in players)
                    {
                        Row dataRow = new Row();
                        for (int i = 0; i < 3; i++)
                        {
                            string data;

                            if (i == 0)
                            {
                                data = player.Name;
                            }
                            else if (i == 1)
                            {
                                data = player.AttendanceCount.ToString();
                            }
                            else
                            {
                                data = player.AttendancePercent_String;
                            }

                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(data);
                            dataRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(dataRow);
                    }
                    workbookPart.Workbook.Save();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!");
            }
        }
    }
}
