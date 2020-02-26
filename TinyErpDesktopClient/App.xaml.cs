using Autofac;
using Localization;
using Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using TinyErpDesktopClient.Startup;

namespace TinyErpDesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string PathToAppSettings = "appsettings.json";
        private static readonly IContainer _container = new Bootstrapper().Bootstrap();
        
        private static AppProperties _appProperties;

        public static IContainer Container => _container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                LoadSettings();

                Localize.Load("translations.xml", _appProperties.SelectedLanguage);

                var mainWindow = Container.Resolve<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                var message = Localize.Translate("##MAIN_WINDOW_NOT_CREATED", "MainWindow can't be created!");

                Logger.Error(message, exception: ex, Level.Fatal);
                MessageBox.Show(string.Format("{0}\n{1}\n{2}\n{3}",  message,
                    Localize.Translate("##APP_NOT_CREATED", "Application not created!"),
                    Localize.Translate("##SEE_LOG", "See the log inforamtion."),
                    Localize.Translate("##STARTUP_ERROR", "Startup error!")));

                Current.Shutdown();
            }
        }

        private void LoadSettings()
        {
            if (!File.Exists(PathToAppSettings))
            {
                return;
            }


            _appProperties = JsonConvert.DeserializeObject<AppProperties>(File.ReadAllText(PathToAppSettings));
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var message = string.Format(Localize.Translate("##ERROR_1", "Error occured:\n{0}"), e.Exception);
            MessageBox.Show(message, Localize.Translate("##SEE_LOG", "See the log inforamtion."));

            Logger.Error(message, logLevel: Level.Error);
        }
    }
}
