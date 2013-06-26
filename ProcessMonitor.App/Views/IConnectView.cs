using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.App.Views
{
    
    public interface IConnectView : IView
    {

        string Server { get; set; }

    }

}
