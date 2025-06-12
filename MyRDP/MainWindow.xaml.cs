using Microsoft.UI.Xaml;
using MyRDP.ViewModels;

namespace MyRDP
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            Title = "MyRDP Manager";
            App.MainWindow = this;
        }
    }
}