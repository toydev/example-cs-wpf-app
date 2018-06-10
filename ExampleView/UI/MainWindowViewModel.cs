using Prism.Commands;
using Prism.Mvvm;

namespace ExampleApp.UI
{
    public class MainWindowViewModel : BindableBase
    {
        public int Count { get; set; } = 0;

        public DelegateCommand CountUpCommand { get; set; }

        public MainWindowViewModel()
        {
            CountUpCommand = new DelegateCommand(CountUp);
        }

        public void CountUp()
        {
            ++Count;
        }
    }
}
