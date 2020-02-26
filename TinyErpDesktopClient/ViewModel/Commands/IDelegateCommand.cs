using System.Windows.Input;

namespace TinyErpDesktopClient.ViewModel.Commands
{
    internal interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
