using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Amedig_Panama_WPF.Views   
{
    /// <summary>
    /// Interaction logic for AfterLoginHome.xaml
    /// </summary>
    public partial class AfterLoginHome : Window
    {
        public AfterLoginHome()
        {
            InitializeComponent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void SetWhoLoggedIn(string message)
        {
            whoLoggedIn.Content = message;
        }
        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Login mainWindow = new Login();
            mainWindow.Show();
            this.Hide();
        }

    }
}
