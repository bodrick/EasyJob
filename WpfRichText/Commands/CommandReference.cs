using System;
using System.Windows;
using System.Windows.Input;

namespace WpfRichText
{
    /// <summary>
    /// This class facilitates associating a key binding in XAML markup to a command
    /// defined in a View Model by exposing a Command dependency property.
    /// The class derives from Freezable to work around a limitation in WPF when data-binding from XAML.
    /// </summary>
    public class CommandReference : Freezable, ICommand
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandReference), new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));

        public event EventHandler CanExecuteChanged;

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool CanExecute(object parameter)
        {
            if (Command != null)
            {
                return Command.CanExecute(parameter);
            }

            return false;
        }

        public void Execute(object parameter) => Command.Execute(parameter);

        protected override Freezable CreateInstanceCore() => throw new NotImplementedException();

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var commandReference = d as CommandReference;

            if (e.OldValue is ICommand oldCommand)
            {
                oldCommand.CanExecuteChanged -= commandReference?.CanExecuteChanged;
            }
            if (e.NewValue is ICommand newCommand)
            {
                newCommand.CanExecuteChanged += commandReference?.CanExecuteChanged;
            }
        }
    }
}
