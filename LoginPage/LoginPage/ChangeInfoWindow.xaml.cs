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
using System.Text.RegularExpressions;

namespace LoginPage
{
    /// <summary>
    /// ChangeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeInfoWindow : Window
    {
        public ChangeInfoWindow()
        {
            InitializeComponent();
        }
        string strGender;
        private void RadioChecked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            string msg = "You clicked " + li.Content.ToString() + ".";
            strGender = li.Content.ToString();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string strUsername = UserName.Text;
            string strPassword = Password.Password;
            string strPassword2 = NewPassword.Password;
            string strPassword3 = ConfirmPassword.Password;
            string regex = "[A-Za-z0-9]{6,16}";

            if (string.IsNullOrEmpty(strUsername))
            {
                MessageBox.Show("请输入用户名！");
                UserName.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("请输入密码！");
                Password.Focus();
                return;
            }
            else if (!strPassword2.Equals(strPassword3))
            {
                MessageBox.Show("两次密码不一致！，请重输。");
                NewPassword.Focus();
                return;
            }
            else if (!Regex.IsMatch(strPassword, regex))
            {
                MessageBox.Show("请输入规范的密码,密码长度6-20位。");
                Password.Focus();
                return;
            }
           
            else
            {

                MessageBox.Show("恭喜您修改成功！" + '\n' + "您的信息如下：" + '\n' + strUsername + '\n' + strGender + '\n' + strPassword2);
                this.Close();
            }

            
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
           UserName.Text = "";
           UserName.Focus();
           Password.Password = null;
           NewPassword.Password = null;
           ConfirmPassword.Password = null;
            
            
            
        }
        
    }
}
