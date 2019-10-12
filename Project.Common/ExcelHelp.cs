using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Project.Common
{
    public class ExcelHelp
    {

        public void Compare()
        {

            FileStream fs = new FileStream("../../../官网效果报表20190924 (1).xls", FileMode.Open, FileAccess.Read);
            IWorkbook workbook = WorkbookFactory.Create(fs);
            ISheet sheet666 = workbook.GetSheet("SheetName0");
            ISheet sheet400 = workbook.GetSheet("Sheet1");
            //var rowCount = sheet666.LastRowNum;
            for (int i = 0; i < 400; i++)
            {
                bool f = true;
                IRow row600 = sheet666.GetRow(i);
                for (int j = 0; j < 400; j++)
                {
                    IRow row400 = sheet400.GetRow(j);
                    if (row600.Cells[0].ToString().Trim() == row400.Cells[0].ToString().Trim())
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    Console.WriteLine($@"找不到关键词：" + row600.Cells[0]);
                }
            }
        }

        public  MemoryStream ExportDataTableToExcel(DataTable sourceTable, string sheetName)
        {
            const int maxSheetCount = 65536;
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            int Count = sourceTable.Rows.Count / maxSheetCount;
            for (int i = 0; i <= Count; i++)
            {
                ISheet sheet = workbook.CreateSheet(sheetName + i.ToString());
            }
            for (int i = 0; i <= Count; i++)
            {
                ISheet sheet = workbook.GetSheet(sheetName + i.ToString());
                IRow headerRow = sheet.CreateRow(0);
                // handling header.        
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }
                // handling value.        
                int rowIndex = 1;
                for (int j = maxSheetCount * i; j < maxSheetCount * (i + 1); j++)
                {
                    if (j >= sourceTable.Rows.Count || rowIndex >= maxSheetCount)
                    {
                        break;
                    }
                    DataRow row = sourceTable.Rows[j];
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    rowIndex++;
                }
            }
            workbook.Write(ms);
            return ms;
        }

    }
}
