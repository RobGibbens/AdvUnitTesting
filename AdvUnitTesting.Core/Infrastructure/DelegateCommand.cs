using System;
using System.Windows.Input;

namespace AdvUnitTesting.Core
{
	public sealed class DelegateCommand : ICommand
	{
		readonly Action<object> command;
		readonly Func<object, bool> canExecute;

		public event EventHandler CanExecuteChanged = delegate { };

		public DelegateCommand (Action<object> command) : this (command, null)
		{
		}

		public DelegateCommand (Action command) : this (command, null)
		{
		}

		public DelegateCommand (Action command, Func<bool> test)
		{
			if (command == null)
				throw new ArgumentNullException ("command", "Command cannot be null.");
			this.command = delegate {
				command ();
			};
			if (test != null) {
				this.canExecute = delegate {
					return test ();
				};
			}
		}

		public DelegateCommand (Action<object> command, Func<object, bool> test)
		{
			if (command == null)
				throw new ArgumentNullException ("command", "Command cannot be null.");

			this.command = command;
			this.canExecute = test;
			;
		}

		public void RaiseCanExecuteChanged ()
		{
			this.CanExecuteChanged (this, EventArgs.Empty);
		}

		public bool CanExecute (object parameter)
		{
			return (this.canExecute == null) || this.canExecute (parameter);
		}

		public void Execute (object parameter)
		{
			this.command (parameter);
		}
	}

	public sealed class DelegateCommand<T> : ICommand
	{
		readonly Action<T> command;
		readonly Func<T, bool> canExecute;

		public event EventHandler CanExecuteChanged = delegate { };

		public DelegateCommand (Action<T> command) : this (command, null)
		{
		}

		public DelegateCommand (Action<T> command, Func<T, bool> test)
		{
			if (command == null)
				throw new ArgumentNullException ("command", "Command cannot be null.");

			this.command = command;
			this.canExecute = test;
		}

		public void RaiseCanExecuteChanged ()
		{
			this.CanExecuteChanged (this, EventArgs.Empty);
		}

		public bool CanExecute (object parameter)
		{
			return (this.canExecute == null) || this.canExecute ((T)parameter);
		}

		public void Execute (object parameter)
		{
			this.command ((T)parameter);
		}
	}
}

