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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using ConsoleDB;

namespace LoginPage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
          
            
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 windowRegister = new Window1();
            this.Hide();
            windowRegister.ShowDialog();
            this.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 windowLogin = new Window2();
            this.Close();
            windowLogin.ShowDialog();
        }
            
    }
}
