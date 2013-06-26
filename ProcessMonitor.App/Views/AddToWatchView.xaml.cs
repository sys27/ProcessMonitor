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

    public partial class AddToWatchView : Window, IAddToWatchView
    {

        private AddToWatchPresenter presenter;

        private RelayCommand addCommand;
        private RelayCommand updateListCommand;

        public AddToWatchView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void SetAllProcesses(IEnumerable<string> processes)
        {
            processBox.ItemsSource = processes;
        }

        public IPresenter Presenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter = (AddToWatchPresenter)value;
            }
        }

        public string Process
        {
            get
            {
                return processBox.Text;
            }
            set
            {
                processBox.Text = value;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                    addCommand = new RelayCommand(o =>
                    {
                        this.DialogResult = true;
                        this.Close();
                    }, o => !string.IsNullOrWhiteSpace(processBox.Text));

                return addCommand;
            }
        }

        public ICommand UpdateListCommand
        {
            get
            {
                if (updateListCommand == null)
                    updateListCommand = new RelayCommand(o => presenter.UpdateList());

                return updateListCommand;
            }
        }

    }

}
