using System;
using System.Windows.Input;

namespace Kinopolis
{
    public class PlayNextCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler ButtonClicked;

        public Serie Serie;

        public PlayNextCommand(Serie serie)
        {
            this.Serie = serie;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            EventHandler handler = ButtonClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}