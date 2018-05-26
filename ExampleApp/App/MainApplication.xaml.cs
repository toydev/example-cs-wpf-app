using System.Windows;

namespace ExampleApp.App
{
    /// <summary>
    /// MainApplication.xaml の相互作用ロジック
    /// </summary>
    public partial class MainApplication : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
