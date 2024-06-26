using Rubyer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ReloadPanClinet
{
    internal class chucun
    {

        public class DataEntry
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string pass { get; set; }
            public string Time { get; set; }
            public string User { get; set; }



            // 构造函数，用于创建 DataEntry 对象时初始化属性  
            public DataEntry(string name, string url, string pass, string time, string user)
            {
                Name = name;
                Url = url;
                this.pass = pass;
                Time = time;
                User = user;
            }


        }



        // 解析 CSV 行并创建 DataEntry 对象
        public static DataEntry ParseEntry(ListView listView)
        {
            string Name = "", Url = "", pass = "", Time = "", User = "";

            var selectedItem = listView.SelectedItems[0];
            var itemType = selectedItem.GetType();

            // 假设你知道属性名  
            var NameP = itemType.GetProperty("Name");
            var UrlP = itemType.GetProperty("Url");
            var PassP = itemType.GetProperty("pass");
            var TimeP = itemType.GetProperty("Time");
            var UserP = itemType.GetProperty("User");

            if (NameP != null && UrlP != null)
            {
                // 获取属性的值  
                Name = NameP.GetValue(selectedItem) as string;
                Url = UrlP.GetValue(selectedItem) as string;
                pass = PassP.GetValue(selectedItem) as string;
                Time = TimeP.GetValue(selectedItem) as string;
                User = UserP.GetValue(selectedItem) as string;
            }


            // 创建 DataEntry 对象并设置属性值
            DataEntry entry = new DataEntry(Name, Url, pass, Time, User);

            return entry;
        }



        public static void SOUrl(ListView listView)
        {
            string Name = "", Url = "", pass = "", Time = "", User = "",uuid="";

            var selectedItem = listView.SelectedItems[0];
            var itemType = selectedItem.GetType();

            // 假设你知道属性名  
            var NameP = itemType.GetProperty("Name");
            var UrlP = itemType.GetProperty("Url");
            var PassP = itemType.GetProperty("pass");
            var TimeP = itemType.GetProperty("Time");
            var UserP = itemType.GetProperty("User");
            var Uuid = itemType.GetProperty("uuid");

            if (NameP != null && UrlP != null)
            {
                // 获取属性的值  
                Name = NameP.GetValue(selectedItem) as string;
                Url = UrlP.GetValue(selectedItem) as string;
                pass = PassP.GetValue(selectedItem) as string;
                Time = TimeP.GetValue(selectedItem) as string;
                User = UserP.GetValue(selectedItem) as string;
                uuid = Uuid.GetValue(selectedItem) as string;
            }


            Paso.OpenUrlInBrowser($"https://127.0.0.1:4540/id?q={uuid}");

        }


        // 添加一个 DataEntry 对象
        public static List<DataEntry> add(DataEntry DataEntry)
        {
            var entry = new List<DataEntry>();

            entry.Add(DataEntry);

            return entry;
        }

        //DataEntry 写出到文件
        public static void ExportToCsv(List<DataEntry> dataEntries, string filePath)
        {
            // 创建一个新的 StreamWriter，并设置编码为 UTF8
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入 CSV 文件头
                sw.WriteLine("Name,Url,Pass,Time,User");

                // 遍历 dataEntries 列表
                foreach (var entry in dataEntries)
                {
                    // 将每个 DataEntry 对象的属性写入文件
                    sw.WriteLine($"{entry.Name},{entry.Url},{entry.pass},{entry.Time},{entry.User}");
                }
            }
        }

        public static List<DataEntry> ReadFromCsv(string filePath)
        {
            List<DataEntry> dataEntries = new List<DataEntry>();

            // 确保文件存在  
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            // 创建一个新的 StreamReader 来读取文件  
            using (StreamReader sr = new StreamReader(filePath))
            {
                // 跳过文件头（如果有的话）  
                if (!sr.EndOfStream)
                {
                    sr.ReadLine(); // 读取并丢弃标题行  
                }

                // 逐行读取文件内容  
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(','); // 假设 CSV 文件中的字段由逗号分隔  

                    // 确保字段数量正确  
                    if (values.Length != 5)
                    {
                        throw new FormatException("Invalid number of fields in CSV line.");
                    }

                    // 创建一个新的 DataEntry 对象并设置其属性  
                    DataEntry entry = new DataEntry(values[0], values[1], values[2], values[3], values[4]);

                    // 将 DataEntry 对象添加到列表中  
                    dataEntries.Add(entry);
                }
            }

            // 返回包含所有 DataEntry 对象的列表  
            return dataEntries;
        }


        public static void CreateEmptyCsv(string filePath)
        {
            // 如果文件已经存在，则不执行任何操作  
            if (File.Exists(filePath))
            {
                return;
            }

            // 创建文件并写入标题行（如果需要的话）  
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("Name,Url,Pass,Time,User"); // 写入标题行  
                                                         // 在这里可以添加更多初始化数据，如果需要的话  
            }

            // 文件创建成功  
            Console.WriteLine("Empty CSV file created at: " + filePath);
        }




    }







}
