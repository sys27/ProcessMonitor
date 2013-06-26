using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessMonitor.App.ViewModels;

namespace ProcessMonitor.App.Views
{

    public interface IMainView : IView
    {

        void ShowError(string message);
        void SetWatchList(IEnumerable<WatchProcessViewModel> watchList);
        void SetTotalTime(string time);

    }

}
