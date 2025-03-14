﻿using System.Windows.Input;

namespace NineMansMorrisView.ViewModel
{
    /// <summary>
    /// Általános parancs típusa.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object?> _execute; // a tevékenységet végrehajtó lambda-kifejezés
        private readonly Func<object?, bool>? _canExecute; // a tevékenység feltételét ellenőrző lambda-kifejezés

        public DelegateCommand(Action<object?> execute) : this(null, execute)
        {
        }

        public DelegateCommand(Func<object?, bool>? canExecute, Action<object?> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}