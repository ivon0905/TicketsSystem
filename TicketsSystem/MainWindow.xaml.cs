using System.Windows;
using TicketsSystem.ViewModels;

namespace TicketsSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel vm = new MainViewModel();
            vm.SelectedViewModel = new LoginViewModel();
            DataContext = vm;
        }
    }
}
