using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProcessMonitor.App.Command;
using ProcessMonitor.App.Presenters;
using ProcessMonitor.App.ViewModels;

namespace ProcessMonitor.App.Views
{

    public partial class MainView : Window, IMainView
    {

        private MainPresenter presenter;

        private RelayCommand startMonitorCommand;
        private RelayCommand stopMonitorCommand;
        private RelayCommand resetMonitorCommand;
        private RelayCommand addWatchCommand;
        private RelayCommand removeWatchCommand;
        private RelayCommand clearWatchsCommand;
        private RelayCommand loadWatchCommand;
        private RelayCommand saveWatchCommand;
        private RelayCommand openStatisticsCommand;
        private RelayCommand downloadStatisticsCommand;
        private RelayCommand saveStatisticsCommand;
        private RelayCommand updateCommand;
        private RelayCommand connectCommand;
        private RelayCommand disconnectCommand;
        private RelayCommand aboutCommand;
        private RelayCommand exitCommand;

        public MainView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        protected override void OnClosed(EventArgs e)
        {
            presenter.Exit();
            //base.OnClosed(e);
        }

        public void ShowError(string message)
        {
            Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)));
        }

        public void SetWatchList(IEnumerable<WatchProcessViewModel> watchList)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var selectedIndex = watchListBox.SelectedIndex;
                watchListBox.ItemsSource = watchList;
                watchListBox.SelectedIndex = selectedIndex;
            }));
        }

        public void SetTotalTime(string time)
        {
            Dispatcher.BeginInvoke(new Action(() => totalTimeBox.Text = string.Format("Total time: {0}", time)));
            //totalTimeBox.Text = string.Format("Total time: {0}", time);
        }

        public IPresenter Presenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter = (MainPresenter)value;
            }
        }

        public ICommand StartMonitorCommand
        {
            get
            {
                if (startMonitorCommand == null)
                    startMonitorCommand = new RelayCommand(o => presenter.Start(), o => presenter != null && !presenter.IsStarted);

                return startMonitorCommand;
            }
        }

        public ICommand StopMonitorCommand
        {
            get
            {
                if (stopMonitorCommand == null)
                    stopMonitorCommand = new RelayCommand(o => presenter.Stop(), o => presenter != null && presenter.IsStarted);

                return stopMonitorCommand;
            }
        }

        public ICommand ResetMonitorCommand
        {
            get
            {
                if (resetMonitorCommand == null)
                    resetMonitorCommand = new RelayCommand(o => presenter.Reset());

                return resetMonitorCommand;
            }
        }

        public ICommand AddWatchCommand
        {
            get
            {
                if (addWatchCommand == null)
                    addWatchCommand = new RelayCommand(o =>
                    {
                        AddToWatchView addView = new AddToWatchView() { Owner = this };
                        AddToWatchPresenter addPresenter = new AddToWatchPresenter(addView, this.presenter);
                        if (addView.ShowDialog() == true)
                        {
                            presenter.AddToWatch(addPresenter.Process);
                        }
                    });

                return addWatchCommand;
            }
        }

        public ICommand RemoveWatchCommand
        {
            get
            {
                if (removeWatchCommand == null)
                    removeWatchCommand = new RelayCommand(o =>
                    {
                        var process = watchListBox.SelectedItem as WatchProcessViewModel;
                        if (process != null)
                        {
                            presenter.RemoveWatch(process.Process);
                        }
                    }, o => watchListBox.SelectedItem != null);

                return removeWatchCommand;
            }
        }

        public ICommand ClearWatchsCommand
        {
            get
            {
                if (clearWatchsCommand == null)
                    clearWatchsCommand = new RelayCommand(o => presenter.ClearWatchs());

                return clearWatchsCommand;
            }
        }

        public ICommand LoadWatchsCommand
        {
            get
            {
                if (loadWatchCommand == null)
                    loadWatchCommand = new RelayCommand(o => presenter.LoadWatchs());

                return loadWatchCommand;
            }
        }

        public ICommand SaveWatchsCommand
        {
            get
            {
                if (saveWatchCommand == null)
                    saveWatchCommand = new RelayCommand(o => presenter.SaveWatchs());

                return saveWatchCommand;
            }
        }

        public ICommand OpenStatisticsCommand
        {
            get
            {
                return openStatisticsCommand;
            }
        }

        public ICommand DownloadStatisticsCommand
        {
            get
            {
                if (downloadStatisticsCommand == null)
                    downloadStatisticsCommand = new RelayCommand(o =>
                    {
                        Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog()
                        {
                            FileName = "remoteStatistics.xml",
                            Filter = "XML|*.xml",
                            Title = "Save statistics"
                        };
                        if (sfd.ShowDialog(this) == true)
                        {
                            presenter.Download(sfd.FileName);
                        }
                    }, o => presenter != null && presenter.IsConnected);

                return downloadStatisticsCommand;
            }
        }

        public ICommand SaveStatisticsCommand
        {
            get
            {
                if (saveStatisticsCommand == null)
                    saveStatisticsCommand = new RelayCommand(o => presenter.SaveStatistics());

                return saveStatisticsCommand;
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                    updateCommand = new RelayCommand(o => presenter.Update());

                return updateCommand;
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                if (connectCommand == null)
                    connectCommand = new RelayCommand(o =>
                    {
                        ConnectView connectView = new ConnectView() { Owner = this };
                        //ConnectPresenter connectPresenter = new ConnectPresenter(connectView);
                        if (connectView.ShowDialog() == true)
                        {
                            //presenter.Connect(connectPresenter.Server);
                            presenter.Connect(connectView.Server);
                        }
                    }, o => presenter != null && !presenter.IsConnected);

                return connectCommand;
            }
        }

        public ICommand DisconnectCommand
        {
            get
            {
                if (disconnectCommand == null)
                    disconnectCommand = new RelayCommand(o => presenter.Disconnect(), o => presenter != null && presenter.IsConnected);

                return disconnectCommand;
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                    aboutCommand = new RelayCommand(o =>
                    {
                        AboutView aboutView = new AboutView() { Owner = this };
                        aboutView.ShowDialog();
                    });

                return aboutCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new RelayCommand(o => presenter.Exit());

                return exitCommand;
            }
        }

    }

}
