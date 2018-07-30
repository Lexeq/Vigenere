using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VigenereBreaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Views.MainWindow window = new Views.MainWindow()
            {
                DataContext = new ViewModels.MainViewModel(Model.LanguagesList.Languages, new Model.MessageService())
            };
            MainWindow = window;
            window.Show();
        }
    }
}
