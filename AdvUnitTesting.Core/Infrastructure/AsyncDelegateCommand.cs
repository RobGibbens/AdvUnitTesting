using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdvUnitTesting.Core
{
	public class AsyncDelegateCommand : ICommand
	{
		protected readonly Predicate<object> _canExecute;
		protected Func<object, Task> _asyncExecute;

		public event EventHandler CanExecuteChanged = delegate { };

		public AsyncDelegateCommand(Func<object, Task> execute)
			: this(execute, null)
		{
		}

		public AsyncDelegateCommand(Func<object, Task> asyncExecute,
			Predicate<object> canExecute)
		{
			_asyncExecute = asyncExecute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute (parameter);

		}

		public async void Execute(object parameter)
		{
			await ExecuteAsync(parameter);
		}

		public async Task ExecuteAsync(object parameter)
		{
			await _asyncExecute(parameter);
		}
	}

	public class AsyncDelegateCommand<T> : ICommand
	{
		protected readonly Predicate<T> _canExecute;
		protected Func<T, Task> _asyncExecute;

		public event EventHandler CanExecuteChanged = delegate { };

		public AsyncDelegateCommand(Func<T, Task> execute)
			: this(execute, null)
		{
		}

		public AsyncDelegateCommand(Func<T, Task> asyncExecute,
			Predicate<T> canExecute)
		{
			_asyncExecute = asyncExecute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return (_canExecute == null) || _canExecute((T)parameter);
		}

		public async void Execute(object parameter)
		{
			await ExecuteAsync((T)parameter);
		}

		public async Task ExecuteAsync(T parameter)
		{
			await _asyncExecute(parameter);
		}
	}



}
