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
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            this.Closing += Window2_Closing;
        }
        private void Window2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // See if the user really wants to shut down this window.
            string msg = "您确认离开?";
            MessageBoxResult result = MessageBox.Show(msg,
            "Younger提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                // If user doesn't want to close, cancel closure.
                e.Cancel = true;
            }
           
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow dialogWindow = new DialogWindow();
            dialogWindow.Show();
        }

        private void image2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
        protected void AddFriend_Click(object sender, RoutedEventArgs args)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.ShowDialog();
        }
        protected void AddGroup_Click(object sender, RoutedEventArgs args)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.ShowDialog();
            dockPanel2.Visibility = Visibility.Hidden;
        }
        protected void SearchFriend_Click(object sender, RoutedEventArgs args)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.ShowDialog();
        }
        protected void ChangeInfo_Click(object sender, RoutedEventArgs args)
        {
            ChangeInfoWindow ChangeInfo = new ChangeInfoWindow();
            ChangeInfo.ShowDialog();
        }
        protected void Quit_Click(object sender, RoutedEventArgs args)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.ShowDialog();
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

        private void ListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            dockPanel2.Visibility = Visibility.Visible;
            dockPanel3.Visibility = Visibility.Hidden;
            Point position = Mouse.GetPosition(button);
            dockPanel2.Margin = new Thickness(position.X + 20, position.Y + 30, 186 - position.X, 120 - position.Y);

        }

        private void ListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dockPanel2.Visibility = Visibility.Hidden;
            dockPanel3.Visibility = Visibility.Hidden;
            Point position = Mouse.GetPosition(button);
            dockPanel2.Margin = new Thickness(position.X + 20, position.Y + 30, 186 - position.X, 120 - position.Y);
        }

        private void ListView2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            dockPanel3.Visibility = Visibility.Visible;

        }

        private void ListView2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dockPanel3.Visibility = Visibility.Hidden;
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogWindow DialogWindow = new DialogWindow();
            DialogWindow.ShowDialog();
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            dockPanel3.Visibility = Visibility.Visible;
            Point position = Mouse.GetPosition(button1);
            dockPanel3.Margin = new Thickness(position.X + 20, position.Y + 30, 186 - position.X, 195 - position.Y);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dockPanel3.Visibility = Visibility.Hidden;
            Point position = Mouse.GetPosition(button1);
            dockPanel3.Margin = new Thickness(position.X + 20, position.Y + 30, 186 - position.X, 195 - position.Y);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow DialogWindow = new DialogWindow();
            DialogWindow.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CreateGroupWindow CreateGroupWindow = new CreateGroupWindow();
           CreateGroupWindow.ShowDialog();
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            GroupInfoUpdateWindow GroupInfoUpdateWindow = new GroupInfoUpdateWindow();
            GroupInfoUpdateWindow.ShowDialog();
        }
        private void GroupConversation_Click(object sender, RoutedEventArgs e)
        {
            GroupConversationWindow GroupConversationWindow = new GroupConversationWindow();
            GroupConversationWindow.ShowDialog();
        }









    }
}
