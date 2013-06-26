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

namespace ProcessMonitor.App.Views
{

    public partial class ConnectView : Window, IConnectView
    {

        private RelayCommand connectCommand;

        public ConnectView()
        {
            InitializeComponent();

            serverBox.Focus();
            this.DataContext = this;
        }

        IPresenter IView.Presenter
        {
            get
            {
                return null;
            }
            set { }
        }

        public string Server
        {
            get
            {
                return serverBox.Text;
            }
            set
            {
                serverBox.Text = value;
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                if (connectCommand == null)
                    connectCommand = new RelayCommand(o =>
                    {
                        this.DialogResult = true;
                        this.Close();
                    }, o => !string.IsNullOrWhiteSpace(serverBox.Text));

                return connectCommand;
            }
        }

    }

}
