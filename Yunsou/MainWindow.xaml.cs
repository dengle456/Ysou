using ReloadPanClinet.Views;
using Rubyer;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReloadPanClinet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml

    public partial class MainWindow : RubyerWindow

    {
        private UserControl paso = new Paso();
  



        public MainWindow()
        {

     
            InitializeComponent();
            User.Content = paso;
             
        }


        //网盘搜索按钮
        private void Button_UIpaso(object sender, RoutedEventArgs e)
        {

            User.Content = paso;
        }

        private void Button_Uishoucan(object sender, RoutedEventArgs e)
        {
            User.Content = new shoucan();
        }


        private void HamburgerMenu_HamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            if (C.Width.Value == 150)
            {
                C.Width = new GridLength(40);
            }
            else
            {
                C.Width = new GridLength(150);
            }
        }


    }
}