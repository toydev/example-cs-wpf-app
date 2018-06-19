using System.Windows;

namespace ExampleApp.App
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var resources = typeof(Properties.Resources);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
