using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessMonitor.App.Presenters;

namespace ProcessMonitor.App.Views
{

    public interface IView
    {

        IPresenter Presenter { get; set; }

    }

}
