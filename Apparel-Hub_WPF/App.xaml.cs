using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Amedig_Panama_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string savedLanguage = Amedig_Panama_WPF.Properties.Settings.Default.Language ?? "en";
            ChangeLanguage(savedLanguage);
        }

        public void ChangeLanguage(string languageCode)
        {
            var dict = new ResourceDictionary();
            switch (languageCode)
            {
                case "es":
                    dict.Source = new Uri("/Resources/es-Español.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("/Resources/en-English.xaml", UriKind.Relative);
                    break;
            }

            Resources.MergedDictionaries.Add(dict);
        }
    }
}
