using System;
using System.Reflection;
using System.Windows;

namespace ChesterfieldWpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            ExpressionDark.DarkMessageBox.Show(errorMessage, "Error", ExpressionDark.DarkMessageBox.Buttons.OK, ExpressionDark.DarkMessageBox.Icon.Error);
            // OR whatever you want like logging etc. MessageBox it's just example
            // for quick debugging etc.
            e.Handled = true;
        }
    }
}
