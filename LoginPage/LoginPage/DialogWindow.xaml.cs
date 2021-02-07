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
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string msg = textBox1.Text;
            richTextBox1.AppendText(msg+'\n');
            textBox1.Clear();
        }

        
        private void textBox1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string msg = textBox1.Text;
                
            if (e.Key.Equals(Keys.Enter))//如果输入的是回车键
            {
                
                richTextBox1.AppendText(msg + '\n');
                textBox1.Clear();
            }
        }

       
    }
}
