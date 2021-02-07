using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using ConsoleDB;


namespace LoginPage
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        string strUsername;
        string strPassword;
        string strPassword2 ;
        string strEmail;
        string strGender;
        int Gender;
        string regex = "[A-Za-z0-9]{6,16}";
        public Window1()
        {
            InitializeComponent();
            

        }
        
        private void RadioChecked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            string msg = "You clicked " + li.Content.ToString() + ".";
            strGender = li.Content.ToString();
            if (strGender.Equals("Male")) 
            {
                Gender = 1;
            }
            else
            {
                Gender = 0;
            }
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {

           
            strUsername = Username.Text;
            strPassword = Password.Password;
            strPassword2 = Password2.Password;
            strEmail = Email.Text;
            if (string.IsNullOrEmpty(strUsername))
            {
                MessageBox.Show("请输入用户名！");
                Username.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("请输入密码！");
                Password.Focus();
                return;
            }
            else if (!strPassword.Equals(strPassword2))
            {
                MessageBox.Show("两次密码不一致！，请重输。");
                Password2.Focus();
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

                MessageBox.Show("恭喜您注册成功！" + '\n' + "您的信息如下：" + '\n' + strUsername + '\n' + strGender + '\n' + strPassword);

                this.Close();
            }

            
            
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string strPassword = Password.Password;
            string strPassword2 = Password2.Password;
            Username.Text = "";
            Username.Focus();
            Password.Password = null;
            Password2.Password=null;
            Email.Text = "";
        }

    }
}
