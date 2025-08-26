using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for IpAndPortSetting.xaml
    /// </summary>
    public partial class Setup : Window
    {
        string computerName = Environment.MachineName;

        string GetLocalIPAdress()
        {
            string localIP = "Not Available";
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }

        public Setup()
        {
            InitializeComponent();
            LoadSavedLanguage();
            this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
            TextBoxComputerName.Text = computerName;
            TextBoxComputerIp.Text = GetLocalIPAdress();
        }
        private void ButtonSettingsSave(object sender, RoutedEventArgs e)
        {

            try
            {

                Directory.CreateDirectory("C:\\PanamaApp folder");

                using (StreamWriter sw = File.CreateText("C:\\PanamaApp folder\\MyfileComputerName.txt"))
                {
                    sw.Write(TextBoxComputerName.Text);
                }

                using (StreamWriter sw = File.CreateText("C:\\PanamaApp folder\\MyfileComputerIP.txt"))
                {
                    sw.Write(TextBoxComputerIp.Text);
                }

                using (StreamWriter sw = File.CreateText("C:\\PanamaApp folder\\MySelectedServer.txt"))
                {
                    if (RadioButton3.IsChecked == true)
                    {
                        sw.Write("https://a.n-1.info/");
                    }

                    else if (RadioButton4.IsChecked == true)
                    {
                        sw.Write("https://qa.n-1.info/");
                    }
                }

                string selectedLanguage = rbSpanish.IsChecked == true ? "es" : "en";
                Properties.Settings.Default.Language = selectedLanguage;
                Properties.Settings.Default.Save();

                Login obj = new Login();
                obj.Show();
                this.Hide();
            }
            catch (Exception)
            {
                string message = Application.Current.Resources["Invalid"] as string;
                MessageBox.Show(message);
            }
        }

        private void LoadSavedLanguage()
        {
            string savedLanguage = Properties.Settings.Default.Language;
            if (savedLanguage == "es")
            {
                rbSpanish.IsChecked = true;
            }
            else
            {
                rbEnglish.IsChecked = true;
            }
        }

        private void LanguageChanged(object sender, RoutedEventArgs e)
        {
            if (rbSpanish != null)
            {
                string selectedLanguage = rbSpanish.IsChecked == true ? "es" : "en";
                ((App)Application.Current).ChangeLanguage(selectedLanguage);
            }
            else
            {
                string selectedLanguage = "en";
                ((App)Application.Current).ChangeLanguage(selectedLanguage);
            }
        }
    }
}