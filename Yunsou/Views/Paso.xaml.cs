using ReloadPanClinet.Views;
using Rubyer;
using Rubyer.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static ReloadPanClinet.chucun;

namespace ReloadPanClinet
{
    /// <summary>
    /// Paso.xaml 的交互逻辑
    /// </summary>
    /// 

    

    public partial class Paso : UserControl
    {
        public Paso()
        {
            InitializeComponent();
        }
        int page { get; set; }
        ObservableCollection<Object> people = new ObservableCollection<Object>();

        //进度调调用
        public void loadschedule(string title, bool isLoading)
        {

            // 更新按钮文本  
            jdttile.Content = title;

            // 根据isLoading的值来设置进度条的状态  
            jdt.IsIndeterminate = isLoading;

            // 如果停止加载，你可能还想将进度条的值设置回0或其他默认值  
            if (!isLoading)
            {
                jdt.Value = 0; // 或者其他合适的默认值  
            }

        }


        //搜索按钮
        private async void Button_seach(object sender, RoutedEventArgs e)
        {

            JsonDocument json = null;
            JsonElement info;
            //初始化页码，表单结构体
            page = 0;
            people = new ObservableCollection<Object>();

            loadschedule("加载中...", true);
            string text = DPStext.Text;
            var DPS = new ClassPaso();
            string DPSdata = await Task.Run(() => DPS.seach(text, page));



            try
            {
                json = JsonDocument.Parse(DPSdata);
                json.RootElement.TryGetProperty("info",out info);
                json.RootElement.TryGetProperty("code", out var code);
                if (code.GetInt32() != 200) {
                    json.RootElement.TryGetProperty("code", out var msg);
                    Message.Error(msg);
                }

              
         
                Debug.WriteLine("JSON 格式正确");
                // 在这里可以对解析成功后的 JSON 进行处理
            }
            catch (JsonException ex)
            {
                //提示框显示msg数据
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Message.Error($"JSON序列化失败:{ex.Message}");
                });
                return;
            }

           
            var a = 0;



            Dispatcher.Invoke(() =>
            {
                var i = 0;
                foreach (var item in info.EnumerateArray())
                {
                    i++;
                    string disk_name = item.GetProperty("name").ToString();
                    string disk_url = item.GetProperty("url").ToString();
                    string disk_pass = item.GetProperty("pass").ToString();
                    string create_time = item.GetProperty("sharedtime").ToString();
                    string disk_user = item.GetProperty("user").ToString();
                    string disk_uuid = item.GetProperty("uuid").ToString();
                    // 调用UpdateProgress方法更新进度条的值 
                    var person = new
                    {
                        Id = i,
                        Name = disk_name,
                        Url = "https://pan.baidu.com/s/1" + disk_url,
                        pass = disk_pass,
                        Time = create_time,
                        User = disk_user,
                        uuid = disk_uuid,
                    };
                    people.Add(person);
                    //Dpslist.Items.Add(new { ID = Dpslist.Items.Count + 1, Name = disk_name, Url = $"https://pan.baidu.com/s/1{disk_url}", Time = create_time });
                }

                listView.ItemsSource = people;

            });

            loadschedule("已加载...", false);
            if (info.GetArrayLength() == 0)
            {
                loadschedule("已经被搜空了...", false);
            }

        }

        //清理页面
        private async void pageup_Click(object sender, RoutedEventArgs e)
        {
            people = new ObservableCollection<Object>();
            listView.ItemsSource = people;

        }

        //搜索下一页
        private async void pageDown_Cilick(object sender, RoutedEventArgs e)
        {

            JsonDocument json;
            JsonElement info;
            try
            {
            loadschedule("加载中...", true);
            string text = DPStext.Text;
            var DPS = new ClassPaso();
            string DPSdata = await Task.Run(() => DPS.seach(text, page + 1));
            json = JsonDocument.Parse(DPSdata);
            json.RootElement.TryGetProperty("info", out info);

            }
            catch (Exception ex)
            {
                Message.Error(ex.Message);
                return;
            }


            Dispatcher.Invoke(() =>
            {

                // 创建 ObservableCollection<Person>

                var i = page * 1;
                foreach (var item in info.EnumerateArray())
                {
                    i++;
                    string disk_name = item.GetProperty("name").ToString();
                    string disk_url = item.GetProperty("url").ToString();
                    string disk_pass = item.GetProperty("pass").ToString();
                    string create_time = item.GetProperty("sharedtime").ToString();
                    string disk_user = item.GetProperty("user").ToString();
                    string disk_uuid = item.GetProperty("uuid").ToString();
                    // 调用UpdateProgress方法更新进度条的值 
                    var person = new
                    {
                        Id = i,
                        Name = disk_name,
                        Url = "https://pan.baidu.com/s/1" + disk_url,
                        pass = disk_pass,
                        Time = create_time,
                        User = disk_user,
                        uuid = disk_uuid
                    };
                    people.Add(person);
                    //Dpslist.Items.Add(new { ID = Dpslist.Items.Count + 1, Name = disk_name, Url = $"https://pan.baidu.com/s/1{disk_url}", Time = create_time });
                }

                listView.ItemsSource = people;

            });
            //初始化页码，表单结构体
            page++;
            loadschedule("已加载...", false);

             if (info.GetArrayLength() == 0)
            {
                loadschedule("已经被搜空了...", false);
            }
        }

        //表单被点击
        private async void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


            string url="", pass="" ,uuid="";
            var listView = sender as ListView;
            if (listView != null && listView.SelectedItems.Count > 0)
            {
                var selectedItem = listView.SelectedItems[0];
                var itemType = selectedItem.GetType();

                // 假设你知道属性名  
                var urlProperty = itemType.GetProperty("Url");
                var passProperty = itemType.GetProperty("pass");

                if (urlProperty != null && passProperty != null)
                {
                    // 获取属性的值  
                     url = urlProperty.GetValue(selectedItem) as string;
                     pass = passProperty.GetValue(selectedItem) as string;

                    // 输出或使用url和pass变量  
                    Console.WriteLine("Selected URL: " + url);
                    Console.WriteLine("Selected Password: " + pass);
                }
                else
                {
                    loadschedule("数据出错", false);
                    return;
                }
            }


            OpenUrlInBrowser($"{url}?pwd={pass}");



            //如何切换到另一个页面

            //System.Diagnostics.Process.Start(gridControl.GetFocusedRowCellValue("Url").ToString());

        }

        public static void OpenUrlInBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Message.Error($"尝试打开URL时发生错误: {ex.Message}");
            }
        }

        //收藏被点击
        private async void shoucang_Click(object sender, EventArgs e)
        {

                  var data = ParseEntry(listView);
                  string currentDirectory = AppDomain.CurrentDomain.BaseDirectory + "/ass/asd.csv";
                  //检测是否有asd.csv文件
                  CreateEmptyCsv(currentDirectory);
                  //读取csv文件
                  var sd = ReadFromCsv(currentDirectory);
                  //将收藏的字符串添加到列表
                  sd.Add(data);
                  //将文件写出
                  ExportToCsv(sd, currentDirectory);

                  Message.Success("收藏成功");
         }



        //收藏被点击
        private async void SOopen_Click(object sender, EventArgs e)
        {

            SOUrl(listView);


        }

    }
}
