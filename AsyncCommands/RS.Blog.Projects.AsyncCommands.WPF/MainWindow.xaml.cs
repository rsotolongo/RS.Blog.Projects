using System.Windows;

namespace RS.Blog.Projects.AsyncCommands.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;
        }

        public MainWindowViewModel ViewModel { get; set; }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Text = await ViewModel.ParameterlessReturnCommand.ExecuteAsync();
        }
    }
}
