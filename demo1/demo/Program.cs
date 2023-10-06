using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Spire.Xls;

class Program
{
    static void Main()
    {
        Console.Write("Nhập thời gian bắt đầu (yyyy-MM-dd HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
        {
            Console.WriteLine("Thời gian không hợp lệ.");
            return;
        }

        Console.Write("Nhập thời gian kết thúc (yyyy-MM-dd HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
        {
            Console.WriteLine("Thời gian không hợp lệ.");
            return;
        }

        // Đường dẫn đến các file CSV
        string csvFilePath1 = @"D:\data\data.csv";
        string csvFilePath2 = @"D:\demo\demo.csv";

        // Đường dẫn đến file kết quả ex.txt
        string resultFilePath = @"D:\demo\ex.txt";

        // Đọc và xử lý data.csv
        var (okCount1, ngCount1, iqFailCount1) = ReadAndProcessCsvFile(csvFilePath1, startTime, endTime);

        // Đọc và xử lý demo.csv
        var (okCount2, ngCount2, iqFailCount2) = ReadAndProcessCsvFile(csvFilePath2, startTime, endTime);

        // Tổng số lần xuất hiện 'OK', 'NG', và 'iqFail' mỗi file
        int totalOkCount = okCount1 + ngCount1;
        int totalNgCount = okCount2 + ngCount2;
        int totalIqFailCount = iqFailCount2;

        // Tính tỷ lệ phần trăm 'OK' và 'NG' theo tổng (OK + NG)
        double okPercentage = (double)okCount1 / totalOkCount* 100;
        double ngPercentage = (double)ngCount1 / totalOkCount * 100;

        // Ghi kết quả vào file ex.txt
        WriteResultToFile(resultFilePath, startTime, endTime, totalOkCount, totalNgCount, totalIqFailCount, okPercentage, ngPercentage);

        Console.WriteLine($"Tổng số lần xuất hiện 'OK' mỗi file: {totalOkCount}");
        Console.WriteLine($"Tổng số lần xuất hiện 'NG' mỗi file: {totalNgCount}");
        Console.WriteLine($"Tổng số lần xuất hiện 'iqFail' mỗi file: {totalIqFailCount}");
        Console.WriteLine($"Tỷ lệ phần trăm 'OK' theo tổng (OK + NG): {okPercentage}%");
        Console.WriteLine($"Tỷ lệ phần trăm 'NG' theo tổng (OK + NG): {ngPercentage}%");
    }

    static (int okCount, int ngCount, int iqFailCount) ReadAndProcessCsvFile(string filePath, DateTime startTime, DateTime endTime)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<CSVRecord>();
                int okCount = 0;
                int ngCount = 0;
                int iqFailCount = 0;

                foreach (var record in records)
                {
                    if (record.StartDatetime >= startTime && record.StartDatetime <= endTime)
                    {
                        if (record.DefectName == "IQ_Fail_Gray127")
                        {
                            iqFailCount++;
                        }
                        if (record.Judge == "OK" )
                        {
                            okCount++;
                          
                            

                        }
                        else if (record.Judge == "NG")
                        {
                            ngCount++;
                        }
                        
                        // if (record.DefectName == "IQ_Fail_Gray127")
                        // {
                        
                      //  }
                    }
                }

                return (okCount, ngCount, iqFailCount);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Đã xảy ra lỗi khi đọc file CSV ({filePath}): {ex.Message}");
            return (0, 0, 0);
        }
    }

    static void WriteResultToFile(string resultFilePath, DateTime startTime, DateTime endTime, int okCount, int ngCount, int iqFailCount, double okPercentage, double ngPercentage)
    {
        using (StreamWriter writer = new StreamWriter(resultFilePath))
        {
            writer.WriteLine($"Thời gian bắt đầu: {startTime.ToString("yyyy-MM-dd HH:mm:ss")}");
            writer.WriteLine($"Thời gian kết thúc: {endTime.ToString("yyyy-MM-dd HH:mm:ss")}");
            writer.WriteLine($"Tổng số lần xuất hiện 'OK' mỗi file: {okCount}");
            writer.WriteLine($"Tổng số lần xuất hiện 'NG' mỗi file: {ngCount}");
            writer.WriteLine($"Tổng số lần xuất hiện 'iqFail' mỗi file: {iqFailCount}");
            writer.WriteLine($"Tỷ lệ phần trăm 'OK' theo tổng (OK + NG): {okPercentage}%");
            writer.WriteLine($"Tỷ lệ phần trăm 'NG' theo tổng (OK + NG): {ngPercentage}%");
        }
    }
}

public class CSVRecord
{
    [Name("start_datetime")]
    public DateTime StartDatetime { get; set; }

    [Name("judge")]
    public string Judge { get; set; }

    [Name("priority_defect_name")]
    public string DefectName { get; set; }
}
