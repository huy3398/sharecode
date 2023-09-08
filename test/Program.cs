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
           
            string excelFilePath = "C:\\Users\\Administrator\\Desktop\\run.xlsx"; // Đường dẫn tới tệp Excel
            string outputFilePath = "C:\\Users\\Administrator\\Desktop\\data.txt"; // Đường dẫn tới tệp văn bản đầu ra

            string sheetName = "Summary"; // Tên của sheet chứa dữ liệu

            var cellRanges = new List<string>
            {
                "A4:A22",
                "G4:G22",
                "M4:M22",
                "S4:S22",
                "Y4:Y22","" +
                "AL4:AL24",
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
                        writer.WriteLine(" Dear Sir / All");
                        writer.WriteLine(" I share result LT AMI MP");
                        writer.WriteLine(" Time : 2023/9/7  18:00  -  2023/9/7  20:00");
                        writer.WriteLine("\n");
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
                            writer.WriteLine("***************************************************");
                        }
                    }

                    Console.WriteLine("Xuat du lieu thanh cong... ");
                }
            }
        }
        
    }
}
