using Prism.Mvvm;
using Reactive.Bindings;

namespace ExampleApp.UI
{
    public class MainWindowViewModel : BindableBase
    {
        public int Count { get; set; } = 0;

        public ReactiveCommand TestCommand { get; set; }

        public MainWindowViewModel()
        {
            TestCommand = new ReactiveCommand();
            TestCommand.Subscribe(() => ++Count);
        }
    }
}
