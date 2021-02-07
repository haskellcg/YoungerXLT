using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    /// GroupInfoUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class GroupInfoUpdateWindow : Window
    {
        public GroupInfoUpdateWindow()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GroupName.Clear();
            textBox1.Clear();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string strGroupName = GroupName.Text;
            string strTextBox1 = textBox1.Text;
            if (string.IsNullOrEmpty(strGroupName))
            {
                MessageBox.Show("请输入群组名！");
                GroupName.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(strTextBox1))
            {
                MessageBox.Show("请输入群组信息！");
                textBox1.Focus();
                return;
            }
            else
            {
                MessageBox.Show("恭喜您修改成功 ");
            }
        }
    }
}
