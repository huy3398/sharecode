using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using  Excel= Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.ComponentModel;



namespace test
{

    internal class Program
    {
      

        static void Main(string[] args)
        {
           
            string excelFilePath = "D:\\mypro\\data.xlsx"; // Đường dẫn tới tệp Excel
            string outputFilePath = "D:\\mypro\\data.txt"; // Đường dẫn tới tệp văn bản đầu ra

            string sheetName = "Summary"; // Tên của sheet chứa dữ liệu

            var cellRanges = new List<string>
            {
                "A4:A22",
                "G4:G22",
                "M4:M22",
                "S4:S22",
                "Y4:Y22",
                "AF4:AH24"
                // Thêm các vùng ô khác vào đây nếu cần
            };
            
            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];

                if (worksheet != null)
                {
                    using (StreamWriter writer = new StreamWriter(outputFilePath))
                    {
                        foreach (var range in cellRanges)
                        {
                            var cellValues = worksheet.Cells[range].Value as object[,];
                            if (cellValues != null)
                            {
                                for (int row = 1; row <= cellValues.GetLength(0); row++)
                                {
                                    var cellValue = cellValues[row - 1, 0];
                                    writer.WriteLine(cellValue);
                                }
                            }
                        }
                    }

                    Console.WriteLine("Xuat du lieu thanh cong... ");
                }
            }
        }
        
    }
}
