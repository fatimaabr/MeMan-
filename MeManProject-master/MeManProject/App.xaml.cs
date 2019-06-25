using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MeManProject
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var splashScreen = new Splashscreen();
            this.MainWindow = splashScreen;
            splashScreen.Show();
            

            Task.Factory.StartNew(() =>
            {
                double prct = 0.9;
                int j = 1;

                for (int i = 0; i <= 100; i++)
                {
                    prct = prct + (double)j * 0.1;
                    splashScreen.Dispatcher.Invoke(() => splashScreen.image.Opacity = prct);
                    if (prct == 1)
                        j = -1;
                    if (prct < 0.1)
                        j = 1;
                    System.Threading.Thread.Sleep(80);
                    i += 4;
                    splashScreen.Dispatcher.Invoke(() => splashScreen.Progress = i);
                }
                
                this.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                    splashScreen.Close();
                });
            });
        }
    }
}
