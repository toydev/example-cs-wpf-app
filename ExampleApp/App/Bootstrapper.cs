﻿using System;
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

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
