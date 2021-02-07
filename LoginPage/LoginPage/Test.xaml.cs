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
    /// Test.xaml 的交互逻辑
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();
        }

        protected void FileExit_Click(object sender, RoutedEventArgs args)
        {
            // Close this window.
            this.Close();
        }
        protected void ToolsSpellingHints_Click(object sender, RoutedEventArgs args)
        {
        }
        protected void MouseEnterExitArea(object sender, RoutedEventArgs args)
        {
        }
        protected void MouseEnterToolsHintsArea(object sender, RoutedEventArgs args)
        {
        }
        protected void MouseLeaveArea(object sender, RoutedEventArgs args)
        {
        }

    }
}
