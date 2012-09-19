using System;
using System.Net;
using System.Windows.Input;

namespace SimpleMVVM.ViewModels
{
    /// <summary>
    /// Generic command for implementing all commands. Requires an action 
    /// and optionally a predicate as parameters.
    /// </summary>
    public class ViewModelCommand : ICommand
    {
        /// <summary>
        /// Creates a ViewModelCommand instance with specified execute action
        /// and canexecute predicate.
        /// </summary>
        /// <param name="executeAction">Action that must be executed for the command.</param>
        /// <param name="canExecute">
        /// Optional predicate that must be checked for the command. If not specified action is always available.
        /// </param>
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecute = null)
        {
            if (executeAction == null)
                throw new ArgumentNullException("Action cannot be null.");

            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        #region ICommand implementation

        private Predicate<object> _canExecute;
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }


        private Action<object> _executeAction;
        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        #endregion
    }
}
