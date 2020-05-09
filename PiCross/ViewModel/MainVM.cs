using PiCross;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {

        public MainVM()
        {
            
            this.activeWindow = new GameVM(this);
            this.facade = new PiCrossFacade();
        }
        public PiCrossFacade facade { get; }
        public object ActiveWindow
        {
            get
            {
                return activeWindow;
            }
            private set
            {
                activeWindow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveWindow)));
            }
        }
        private object activeWindow;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
