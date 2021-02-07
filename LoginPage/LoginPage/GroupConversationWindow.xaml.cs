using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace LoginPage
{
    /// <summary>
    /// GroupConversationwindow.xaml 的交互逻辑
    /// </summary>
    public partial class GroupConversationWindow : Window
    {
        public GroupConversationWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
             string msg = GroupTextBox.Text;
            GroupRichTextBox.AppendText(msg+'\n');
            GroupTextBox.Clear();
        }
        private void GroupTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string msg = GroupTextBox.Text;

            if (e.Key.Equals(Keys.Enter))//如果输入的是回车键
            {

                GroupRichTextBox.AppendText(msg + '\n');
                GroupTextBox.Clear();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        

       
    }
}
