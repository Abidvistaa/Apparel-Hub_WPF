using Amedig_Panama_WPF.Entity;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private string username;
        private string password;
        private string tabuserrole;
        private string department;

        private const string key = "cafjfg3123d565tdfg67dsofgsfgsda9";

        public Login()
        {
            InitializeComponent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            username = UserNameTextBox.Text.Trim();
            password = PasswordTextBox.Password;


            if (username.Contains("Manager") || username.Contains("Gerente"))
            {
                department = "ALL";
            }
            else if (username.Contains("PACA"))
            {
                department = "PACA ROPA";
            }

            AppUser appuser = CheckUser(department, password);

            if (appuser.Department == department && appuser.Password == password)
            {

                SetCredential(username,  department, appuser.TabUserRole);
                AfterLoginHome afterLoginHome = new AfterLoginHome();
                afterLoginHome.Show();
                this.Hide();

                string whologin = GetCredential_UserName();
                afterLoginHome.SetWhoLoggedIn(whologin);
            }
            else
            {
                string message = Application.Current.Resources["LoginFailed"] as string;
                MessageBox.Show(message);
            }
        }
        public string SystemSeletedServer()
        {
            if (File.Exists("C:\\PanamaApp folder\\MySelectedServer.txt"))
            {
                using (StreamReader sr = File.OpenText("C://PanamaApp folder\\MySelectedServer.txt"))
                {
                    return sr.ReadToEnd();
                }
            }
            else { return string.Empty; }
        }
        public AppUser CheckUser(string dept, string password)
        {

            var newAppUser = new AppUser();
            Login obj = new Login();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(SystemSeletedServer());
            //client.BaseAddress = new Uri("https://application-web.conveyor.cloud/");
            client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/GetTabUserList?token=26456dkj[i753(947kdl90rioe]utu9405u9]fg").Result;

            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsAsync<IEnumerable<AppUser>>().Result;

                foreach (var item in products)
                {
                    if (item.Department == dept && item.Password == password)
                    {
                        newAppUser = item;
                    }
                }
            }
            return newAppUser;
        }
        public void SetCredential(string userName, string userDepartment, string tabUserRole)
        {
            Directory.CreateDirectory("C:\\PanamaApp folder");
            using (StreamWriter sw = File.CreateText("C:\\PanamaApp folder\\FileUserName.txt"))
            {
                sw.Write(userName);
            }
            using (StreamWriter sw = File.CreateText("C:\\PanamaApp folder\\FileUserDepartment.txt"))
            {
                sw.Write(department);
            }
            using (StreamWriter sw = File.CreateText("C:\\PanamaApp folder\\FileTabUserRole.txt"))
            {
                sw.Write(tabuserrole);
            }
        }
        public string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
        public string GetCredential_UserName()
        {
            if (File.Exists("C:\\PanamaApp folder\\FileUserName.txt"))
            {
                using (StreamReader sr = File.OpenText("C:\\PanamaApp folder\\FileUserName.txt"))
                {
                    return sr.ReadToEnd();
                }
            }
            else { return ""; }
        }
        public string GetCredential_UserDepartment()
        {
            if (File.Exists("C:\\PanamaApp folder\\FileUserName.txt"))
            {
                using (StreamReader sr = File.OpenText("C:\\PanamaApp folder\\FileUserDepartment.txt"))
                {
                    return sr.ReadToEnd();
                }
            }
            else { return ""; }
        }
        public string GetCredential_TabUserRole()
        {
            if (File.Exists("C:\\PanamaApp folder\\FileUserName.txt"))
            {
                using (StreamReader sr = File.OpenText("C:\\PanamaApp folder\\FileTabUserRole.txt"))
                {
                    return sr.ReadToEnd();
                }
            }
            else { return ""; }
        }
        private void ButtonSettings(object sender, RoutedEventArgs e)
        {
            Setup obj = new Setup();
            obj.Show();
        }
        private void textBox1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.Password = string.Empty;
            KeyBoard kBoard = new KeyBoard(this, 340, 182, 0, "");

            if (kBoard.ShowDialog() == true)
                PasswordTextBox.Password = kBoard.Password;
        }

    }
}
