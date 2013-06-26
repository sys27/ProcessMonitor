using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.App.ViewModels
{

    public class WatchProcessViewModel : ViewModelBase
    {

        private string process;
        private string time;

        public WatchProcessViewModel(string process, string milliseconds)
        {
            this.process = process;
            this.time = milliseconds;
        }

        public string Process
        {
            get
            {
                return process;
            }
            set
            {
                process = value;
                OnPropertyChanged("Process");
            }
        }

        public string Milliseconds
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged("Milliseconds");
            }
        }

    }

}
