using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginPage
{
    /// <summary>
    /// ListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void GetUserID_Focus(object sender, RoutedEventArgs e)
        {
            UserIDSearchBox.Text=" ";
        }
        private void LoseUserID_Focus(object sender, RoutedEventArgs e)
        {
            UserIDSearchBox.Text = "请输入用户ID";
        }
        private void GetUserName_Focus(object sender, RoutedEventArgs e)
        {
            UserNameSearchBox.Text = " ";
        }
        private void LoseUserName_Focus(object sender, RoutedEventArgs e)
        {
            UserNameSearchBox.Text = "请输入用户昵称";
        }
        private void GetGroup_Focus(object sender, RoutedEventArgs e)
        {
            GroupSearchBox.Text = " ";
        }
        private void LoseGroup_Focus(object sender, RoutedEventArgs e)
        {
            GroupSearchBox.Text = "请输入群号";
        }
       
    }
}
