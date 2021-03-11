using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RS.Blog.Projects.AsyncCommands.WPF
{
    public class Void
    {
    }

    public interface IErrorHandler
    {
        void HandleError(Exception exception);
    }

    public interface IGenericCommand<TInput> : ICommand
    {
        bool CanExecute(TInput parameter);

        void Execute(TInput parameter);

        void RaiseCanExecuteChanged();
    }

    public interface IAsyncCommand<TInput, TOutput> : IGenericCommand<TInput>
    {
        TOutput ExecuteAsync();

        TOutput ExecuteAsync(TInput parameter);
    }

    public interface IParameterizedReturnAsyncCommand<TInput, TOutput> : IAsyncCommand<TInput, Task<TOutput>>
    {
    }

    public interface IParameterizedVoidAsyncCommand<TInput> : IAsyncCommand<TInput, Task>
    {
    }

    public interface IParameterlessParameterizedAsyncCommand<TOutput> : IAsyncCommand<Void, Task<TOutput>>
    {
    }

    public interface IParameterlessVoidAsyncCommand : IAsyncCommand<Void, Task>
    {
    }

    public abstract class GenericCommand<TInput> : IGenericCommand<TInput>
    {
        protected GenericCommand(Func<TInput, bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            CanExecuteMethodParameterized = canExecute;
            ErrorHandler = errorHandler;
            Variant = 0;
        }

        protected GenericCommand(Func<bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            CanExecuteMethodParameterless = canExecute;
            ErrorHandler = errorHandler;
            Variant = 1;
        }

        public event EventHandler CanExecuteChanged;

        protected readonly Func<TInput, bool> CanExecuteMethodParameterized;

        protected readonly Func<bool> CanExecuteMethodParameterless;

        protected readonly IErrorHandler ErrorHandler;

        protected readonly int Variant;

        protected bool IsExecuting;

        public bool CanExecute(TInput parameter)
        {
            return !IsExecuting &&
                (((Variant == 0) && (CanExecuteMethodParameterized?.Invoke(parameter) ?? true)) ||
                 ((Variant == 1) && (CanExecuteMethodParameterless?.Invoke() ?? true)));
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((TInput)parameter);
        }

        public abstract void Execute(TInput parameter);

        public void Execute(object parameter)
        {
            Execute((TInput)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public abstract class AsyncCommand<TInput, TOutput> : GenericCommand<TInput>, IAsyncCommand<TInput, TOutput> where TOutput : Task
    {
        protected AsyncCommand(Func<TInput, bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
        }

        protected AsyncCommand(Func<bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
        }

        public override async void Execute(TInput parameter)
        {
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception exception)
            {
                ErrorHandler?.HandleError(exception);
            }
        }

        public abstract TOutput ExecuteAsync();

        public abstract TOutput ExecuteAsync(TInput parameter);
    }

    public class ParameterizedReturnAsyncCommand<TInput, TOutput> : AsyncCommand<TInput, Task<TOutput>>, IParameterizedReturnAsyncCommand<TInput, TOutput>
    {
        public ParameterizedReturnAsyncCommand(Func<TInput, Task<TOutput>> execute, Func<TInput, bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
            ExecuteMethodParameterized = execute;
        }

        public ParameterizedReturnAsyncCommand(Func<Task<TOutput>> execute, Func<TInput, bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
            ExecuteMethodParameterless = execute;
        }

        public ParameterizedReturnAsyncCommand(Func<Task<TOutput>> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
            ExecuteMethodParameterless = execute;
        }

        private readonly Func<TInput, Task<TOutput>> ExecuteMethodParameterized;

        private readonly Func<Task<TOutput>> ExecuteMethodParameterless;

        public override async Task<TOutput> ExecuteAsync()
        {
            return await ExecuteAsync(default);
        }

        public override async Task<TOutput> ExecuteAsync(TInput parameter)
        {
            TOutput result = default;
            if (CanExecute(parameter))
            {
                try
                {
                    IsExecuting = true;
                    result = (Variant == 0)
                        ? await ExecuteMethodParameterized(parameter)
                        : await ExecuteMethodParameterless();
                }
                finally
                {
                    IsExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
            return result;
        }
    }

    public class ParameterizedVoidAsyncCommand<TInput> : AsyncCommand<TInput, Task>, IParameterizedVoidAsyncCommand<TInput>
    {
        public ParameterizedVoidAsyncCommand(Func<TInput, Task> execute, Func<TInput, bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
            ExecuteMethodParameterized = execute;
        }

        public ParameterizedVoidAsyncCommand(Func<Task> execute, Func<TInput, bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
            ExecuteMethodParameterless = execute;
        }

        public ParameterizedVoidAsyncCommand(Func<Task> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(canExecute, errorHandler)
        {
            ExecuteMethodParameterless = execute;
        }

        private readonly Func<TInput, Task> ExecuteMethodParameterized;

        private readonly Func<Task> ExecuteMethodParameterless;

        public override async Task ExecuteAsync()
        {
            await ExecuteAsync(default);
        }

        public override async Task ExecuteAsync(TInput parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    IsExecuting = true;
                    if (Variant == 0)
                    {
                        await ExecuteMethodParameterized(parameter);
                    }
                    else
                    {
                        await ExecuteMethodParameterless();
                    }
                }
                finally
                {
                    IsExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }
    }

    public class ParameterlessParameterizedAsyncCommand<TOutput> : ParameterizedReturnAsyncCommand<Void, TOutput>, IParameterlessParameterizedAsyncCommand<TOutput>
    {
        public ParameterlessParameterizedAsyncCommand(Func<Task<TOutput>> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(execute, canExecute, errorHandler)
        {
        }
    }

    public class ParameterlessVoidAsyncCommand : ParameterizedVoidAsyncCommand<Void>, IParameterlessVoidAsyncCommand
    {
        public ParameterlessVoidAsyncCommand(Func<Task> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
            : base(execute, canExecute, errorHandler)
        {
        }
    }
}
