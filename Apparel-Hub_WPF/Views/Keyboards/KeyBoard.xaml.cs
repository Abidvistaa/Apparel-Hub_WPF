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
    /// Interaction logic for KeyBoard.xaml
    /// </summary>
    public partial class KeyBoard : Window
    {
        public string Password { get; internal set; }

        public KeyBoard(Window owner, int top, int left, int winStyle, string title)
        {
            InitializeComponent();
            this.Owner = owner;
            this.Top = top;
            this.Left = left;
            this.DataContext = this;

            if (winStyle == 1)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.Height = 160;
                this.Width = 420;
                this.Title = title;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.CommandParameter)
            {
                case "ESC":
                    this.DialogResult = false;
                    break;
                case "RETURN":
                    this.DialogResult = true;
                    break;

                case "BACK":
                    if (Password != null && Password.Length > 0)
                    {
                        passwordBox.Password = passwordBox.Password.Remove(passwordBox.Password.Length - 1);
                    }
                    else
                    {
                        passwordBox.Password = Password;
                    }
                    break;

                default:
                    passwordBox.Password += button.Content;
                    Password = passwordBox.Password;
                    break;
            }
        }
    }
}