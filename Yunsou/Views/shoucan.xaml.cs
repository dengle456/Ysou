using Rubyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReloadPanClinet.Views
{
    /// <summary>
    /// shoucan.xaml 的交互逻辑
    /// </summary>
    public partial class shoucan : UserControl
    {
        public shoucan()
        {
            InitializeComponent();
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory + "/ass/asd.csv";
            chucun.CreateEmptyCsv(currentDirectory);
            var sf = chucun.ReadFromCsv(currentDirectory);
            listView.ItemsSource = sf;
        }


        private async void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {



            string url = "", pass = "";
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
                  
                    return;
                }
            }


            Paso.OpenUrlInBrowser($"{url}?pwd={pass}");





            //如何切换到另一个页面

            //System.Diagnostics.Process.Start(gridControl.GetFocusedRowCellValue("Url").ToString());

        }



        private async void add_Click (object sender, EventArgs e)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory + "/ass/asd.csv";
            chucun.CreateEmptyCsv(currentDirectory);
            var sf = chucun.ReadFromCsv(currentDirectory);
            var data = chucun.ParseEntry(listView);
            sf.Add(data);
            chucun.ExportToCsv(sf, currentDirectory);
            listView.ItemsSource = sf;

        }

        private async void remove_Click(object sender, EventArgs e)
        {

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory + "/ass/asd.csv";
            chucun.CreateEmptyCsv(currentDirectory);
            var sf = chucun.ReadFromCsv(currentDirectory);  
            sf.Remove(sf[listView.SelectedIndex]);
            chucun.ExportToCsv(sf, currentDirectory);
            listView.ItemsSource = sf;

        }





    }




}
