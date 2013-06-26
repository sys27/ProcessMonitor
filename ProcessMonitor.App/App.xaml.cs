using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProcessMonitor.App.Presenters;
using ProcessMonitor.App.Views;

namespace ProcessMonitor.App
{

    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += (o, args) =>
#if DEBUG
 MessageBox.Show(args.Exception.ToString());
#else
 MessageBox.Show(args.Exception.Message);
#endif

            MainView mainView = new MainView();
            MainPresenter presenter = new MainPresenter(mainView);
            //mainView.Presenter = presenter;
            mainView.Show();
            //base.OnStartup(e);
        }

    }

}
