using System;
using System.Globalization;
using System.Windows;

using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using Gu.Localization;
using Unity;

using ExampleApp.UI;

namespace ExampleApp.App
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.Assembly.FullName;
                var viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override Window CreateShell()
        {
            try
            {
                Translator.Cultures.Clear();
                Translator.Cultures.Add(CultureInfo.GetCultureInfo("ja"));
                Translator.Cultures.Add(CultureInfo.GetCultureInfo("ja-JP"));
                Translator.Cultures.Add(CultureInfo.GetCultureInfo("en"));
                Translator.Cultures.Add(CultureInfo.GetCultureInfo("sw"));
            }
            catch (Exception)
            {
            }

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            shell.Show();
        }
    }
}
