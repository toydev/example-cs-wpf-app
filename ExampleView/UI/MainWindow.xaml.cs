using System.Globalization;
using System.Windows;

using Gu.Localization;

namespace ExampleApp.UI
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Translator.CurrentCulture = (CultureInfo)languageComboBox.SelectedItem;
        }
    }
}
