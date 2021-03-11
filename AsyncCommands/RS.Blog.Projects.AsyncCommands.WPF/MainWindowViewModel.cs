using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RS.Blog.Projects.AsyncCommands.WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            ParameterizedReturnCommand = new ParameterizedReturnAsyncCommand<string, string>(ParameterizedReturnMethod, parameter => OddLength(parameter));
            //// ParameterizedReturnCommand = new ParameterizedReturnAsyncCommand<string, string>(ParameterlessReturnMethod, parameter => OddLength(parameter));
            //// ParameterizedReturnCommand = new ParameterizedReturnAsyncCommand<string, string>(ParameterlessReturnMethod, () => OddLength("OddString"));
            ParameterizedVoidCommand = new ParameterizedVoidAsyncCommand<string>(ParameterizedVoidMethod, parameter => OddLength(parameter));
            //// ParameterizedVoidCommand = new ParameterizedVoidAsyncCommand<string>(ParameterlessVoidMethod, parameter => OddLength(parameter));
            //// ParameterizedVoidCommand = new ParameterizedVoidAsyncCommand<string>(ParameterlessVoidMethod, () => OddLength("OddString"));
            ParameterlessReturnCommand = new ParameterlessParameterizedAsyncCommand<string>(ParameterlessReturnMethod, () => OddLength("OddString"));
            ParameterlessVoidCommand = new ParameterlessVoidAsyncCommand(ParameterlessVoidMethod, () => OddLength("OddString"));
            Text = nameof(Text);
        }

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IParameterizedReturnAsyncCommand<string, string> ParameterizedReturnCommand { get; set; }

        public IParameterizedVoidAsyncCommand<string> ParameterizedVoidCommand { get; set; }

        public IParameterlessParameterizedAsyncCommand<string> ParameterlessReturnCommand { get; set; }

        public IParameterlessVoidAsyncCommand ParameterlessVoidCommand { get; set; }

        private string text;

        private async Task<string> ChangeText([CallerMemberName] string memberName = null)
        {
            string result = string.Empty;
            await Task.Run(() =>
            {
                result = memberName;
                Text = memberName;
            });
            return result;
        }

        private async Task<string> ParameterizedReturnMethod(string text)
        {
            return await ChangeText();
        }

        private async Task ParameterizedVoidMethod(string text)
        {
            await ChangeText();
        }

        private async Task<string> ParameterlessReturnMethod()
        {
            return await ChangeText();
        }

        private async Task ParameterlessVoidMethod()
        {
            await ChangeText();
        }

        private bool OddLength(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && (value.Length % 2 != 0);
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
