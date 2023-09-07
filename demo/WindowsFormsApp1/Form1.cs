using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Chuỗi kết nối đến SQL Server
          
           string connectionString1 = @"Data Source=localhost;Initial Catalog=ami_lt;Integrated Security=True";
            //  string connectionString2 = "Server=192.168.1.21;Database=ami_lt;User Id=ami;Password=protnc;";

            // Thực hiện kết nối và truy vấn
            string query = "SELECT judge, priority_defect_name, panelid, display_ins, start_time FROM product WHERE start_time >= @StartTime AND start_time <= @EndTime";
            using (SqlConnection connection1 = new SqlConnection(connectionString1))
          //  using (SqlConnection connection2 = new SqlConnection(connectionString2))
            {
                connection1.Open();
               // connection2.Open();

                SqlCommand command1 = new SqlCommand(query, connection1);
              //  SqlCommand command2 = new SqlCommand(query, connection2);

                // Đặt giá trị thời gian bắt đầu và kết thúc từ TextBox trên giao diện
                command1.Parameters.AddWithValue("@StartTime", dateTimePickerStart.Text);
                //command1.Parameters.AddWithValue("@StartTime", TextBox.text);
                command1.Parameters.AddWithValue("@EndTime", dateTimePickerEnd.Text);

               // command2.Parameters.AddWithValue("@StartTime", dateTimePickerStart.Text);
              //  command2.Parameters.AddWithValue("@EndTime", dateTimePickerEnd.Text);

                // Thực hiện truy vấn và ghi dữ liệu vào tệp tin
                using (StreamWriter sw = new StreamWriter("output.txt"))
                {
                    SqlDataReader reader1 = command1.ExecuteReader();
                    //SqlDataReader reader2 = command2.ExecuteReader();

                    while (reader1.Read())
                    {
                        sw.WriteLine("192.168.1.20 = *401: " + reader1["judge"] + ", " + reader1["priority_defect_name"] + ", " + reader1["panelid"] + ", " + reader1["display_ins"] + ", " + reader1["start_time"]);
                    }

                   // while (reader2.Read())
                  //  {
                      //  sw.WriteLine("192.168.1.20 = *402: " + reader2["judge"] + ", " + reader2["priority_defect_name"] + ", " + reader2["panelid"] + ", " + reader2["display_ins"] + ", " + reader2["start_time"]);
                  //  }
                
                    reader1.Close();
                 //   reader2.Close();
                }
            }

            MessageBox.Show("Xong! Dữ liệu đã được xuất ra file output.txt.");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
