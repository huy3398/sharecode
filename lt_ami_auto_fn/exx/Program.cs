using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Globalization;
using System.Linq;
class Program
{
    static void Main()
    {
        // Địa chỉ IP và thông tin kết nối tương ứng cho từng máy chủ
        Dictionary<int, string> serverConnections = new Dictionary<int, string>
        {
           
            { 501, "Server=10.121.52.22;Database=ami_lt;User=ami;Password=protnc;" },
            { 502, "Server=10.121.52.23;Database=ami_lt;User=ami;Password=protnc;" },
            { 503, "Server=10.121.56.62;Database=ami_lt;User=ami;Password=protnc;" },
            { 504, "Server=10.121.56.30;Database=ami_lt;User=ami;Password=protnc;" },
            { 505, "Server=10.121.55.94;Database=ami_lt;User=ami;Password=protnc;" },
            { 406, "Server=10.121.41.104;Database=ami_lt;User=ami;Password=protnc;" },
        };

        Console.Write("Nhap ngay/gio bat dau (MM-dd-yyyy HH:mm): ");
        string inputStartDateTime = Console.ReadLine();

        Console.Write("Nhap ngay/gio ket thuc (MM-dd-yyyy HH:mm): ");
        string inputEndDateTime = Console.ReadLine();

        if (DateTime.TryParse(inputStartDateTime, out DateTime startDateTime) && DateTime.TryParse(inputEndDateTime, out DateTime endDateTime))
        {
            // Tạo tệp văn bản tổng hợp
            string summaryFilePath = "C:\\Users\\Administrator\\Desktop\\output_ti_le.txt";

            // Mở tệp văn bản tổng hợp để ghi dữ liệu
            using (StreamWriter summaryWriter = new StreamWriter(summaryFilePath))
            {
                summaryWriter.WriteLine("Dear Sir / All");
                summaryWriter.WriteLine("I share result LT AMI MP");
                summaryWriter.WriteLine("Time : 2023/9/10  18:0  -  2023/9/10  19:59");
                summaryWriter.WriteLine("");
                foreach (var serverConnection in serverConnections)
                {
                    int serverCode = serverConnection.Key;
                    string connectionString = serverConnection.Value;

                    string query = "SELECT panelid, judge, display_insp, priority_defect_name FROM product WHERE start_datetime BETWEEN @StartDateTime AND @EndDateTime";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@StartDateTime", startDateTime);
                            command.Parameters.AddWithValue("@EndDateTime", endDateTime);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                int totalPanels = dataTable.Rows.Count;
                                int okCount = 0;
                                int ngCount = 0;
                                int iqFailCount = 0;

                             //   Dictionary<int, (int okCount1, int ngCount1)> channelRatios = new Dictionary<int, (int, int)>();

                                Dictionary<string, int> priorityDefectCounts = new Dictionary<string, int>();
                                Dictionary<string, int> displayInspCounts = new Dictionary<string, int>();
                                Dictionary<string, (double okPercentage1, double ngPercentage1)> channelRatios = new Dictionary<string, (double, double)>();
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    string judge = row["judge"].ToString();
                                    string priorityDefectName = row["priority_defect_name"].ToString();

                                    if (judge == "OK")
                                    {
                                        if (priorityDefectName == "IQ_Fail_Gray127")
                                        {
                                            iqFailCount++;
                                        }
                                        okCount++;

                                    }
                                    else if (judge == "NG")
                                    {
                                        ngCount++;
                                        
                                    }
                                    
                                }


                                //_CH

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    string judge = row["judge"].ToString();
                                    string ch = row["display_insp"].ToString();
                                    string priorityDefectName = row["priority_defect_name"].ToString();


                                    // Tách lấy giá trị CH từ display_insp
                                    if (int.TryParse(ch, out int channel))
                                    {
                                        string channelKey = $"CH_{channel}";

                                        // Nếu kênh này chưa được thêm vào Dictionary, thêm nó vào
                                        if (!channelRatios.ContainsKey(channelKey))
                                        {
                                            channelRatios[channelKey] = (0, 0);
                                        }

                                        // Cập nhật tỷ lệ OK và NG dựa trên giá trị judge
                                        if (judge == "OK")
                                        {
                                            channelRatios[channelKey] = (
                                                channelRatios[channelKey].okPercentage1 + 1.0,
                                                channelRatios[channelKey].ngPercentage1
                                            );
                                           
                                        }
                                        else if (judge == "NG")
                                        {
                                            channelRatios[channelKey] = (
                                                channelRatios[channelKey].okPercentage1,
                                                channelRatios[channelKey].ngPercentage1 + 1.0
                                            );

                                            if (!string.IsNullOrEmpty(priorityDefectName))
                                            {
                                                if (!priorityDefectCounts.ContainsKey(priorityDefectName))
                                                {
                                                    priorityDefectCounts[priorityDefectName] = 0;
                                                }
                                                priorityDefectCounts[priorityDefectName]++;
                                            }

                                        }
                                        
                                    }
                                }










                                    //
                                    // Tính tỷ lệ IQ Fail so với OK
                                    double iqFailPercentage = (double)iqFailCount / totalPanels * 100;
                                double okPercentage = (double)okCount / totalPanels * 100;
                                double ngPercentage = (double)ngCount / totalPanels * 100;

                                // Ghi dữ liệu vào tệp văn bản tổng hợp
                                summaryWriter.WriteLine($"#{serverCode} Report");
                                summaryWriter.WriteLine($"Total Input: {totalPanels}ea");
                                summaryWriter.WriteLine("");
                                //xuat CH

                                foreach (var kvp1 in channelRatios)
                                {

                                    string channelKey = kvp1.Key;

                                    double okPercentage1 = kvp1.Value.okPercentage1;
                                    double ngPercentage1 = kvp1.Value.ngPercentage1;
                                    //tinh %ch
                                    //double ch1 = (double)chokCo / totalPanels * 100;


                                    //

                                    double chOK = okPercentage1 / (okPercentage1 + ngPercentage1) * 100;
                                    double chNG = ngPercentage1 / (okPercentage1 + ngPercentage1) * 100;

                                    summaryWriter.WriteLine($"{channelKey}: OK: {chOK:0.00}% - {okPercentage1}ea / NG: {chNG:0.00}% - {ngPercentage1}ea");
                                }



                                summaryWriter.WriteLine("");
                                summaryWriter.WriteLine($"OK Total: {okPercentage.ToString("0.00", CultureInfo.InvariantCulture)}% -{okCount}ea");
                                summaryWriter.WriteLine($" -IQ_Fail: {iqFailPercentage.ToString("0.00", CultureInfo.InvariantCulture)}% -{iqFailCount}ea");
                                summaryWriter.WriteLine($"NG Total: {ngPercentage.ToString("0.00", CultureInfo.InvariantCulture)}% -{ngCount}ea");
                               
                                foreach (var kvp in priorityDefectCounts)
                                {
                                    double priorityPercentage = (double)kvp.Value / totalPanels * 100;
                                    summaryWriter.WriteLine($" -{kvp.Key}: {priorityPercentage.ToString("0.00", CultureInfo.InvariantCulture)}% -{kvp.Value}ea");
                                }
                                summaryWriter.WriteLine("\n");






                                //

                                                                                                                                
                            }
                        }

                        connection.Close();
                    }
                }
            }

            Console.WriteLine($"Du lieu da xuat ra tep {summaryFilePath}.");
        }
        else
        {
            Console.WriteLine("Ngay/gio khong hop le.");
        }
    }
}
