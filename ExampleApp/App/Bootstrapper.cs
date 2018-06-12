using System;
using System.Globalization;
using System.Threading;
using System.Windows;

using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity;
using Gu.Localization;

using ExampleApp.UI;

namespace ExampleApp.App
{
    public class Bootstrapper : UnityBootstrapper
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

        protected override DependencyObject CreateShell()
        {
            try
            {
                var culture = new CultureInfo("ja");
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
                Translator.Culture = culture;
            }
            catch (Exception)
            {
            }

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
