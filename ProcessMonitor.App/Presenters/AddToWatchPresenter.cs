using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProcessMonitor.App.Command;
using ProcessMonitor.App.MonitorService;
using ProcessMonitor.App.Views;

namespace ProcessMonitor.App.Presenters
{

    public class AddToWatchPresenter : IPresenter
    {

        private IAddToWatchView view;
        private MainPresenter presenter;

        public AddToWatchPresenter(IAddToWatchView view, MainPresenter presenter)
        {
            this.view = view;
            this.presenter = presenter;
            view.Presenter = this;
            UpdateList();
        }

        public void UpdateList()
        {
            view.SetAllProcesses(presenter.GetAllProcesses());
        }

        public string Process
        {
            get
            {
                return view.Process;
            }
            set
            {
                view.Process = value;
            }
        }

    }

}
