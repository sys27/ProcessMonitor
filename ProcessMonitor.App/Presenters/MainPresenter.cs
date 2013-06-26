using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using ProcessMonitor.App.MonitorService;
using ProcessMonitor.App.ViewModels;
using ProcessMonitor.App.Views;
using System.Xml.Linq;

namespace ProcessMonitor.App.Presenters
{

    public class MainPresenter : IPresenter
    {

        private IMonitor monitor;
        private MonitorClient monitorClient;
        private bool isConnected;

        private Timer timer;

        private IMainView view;

        public MainPresenter(IMainView view)
        {
            this.view = view;
            view.Presenter = this;
            monitor = new Monitor();
            timer = new Timer(500);
            timer.Elapsed += (o, args) => Update();

            Update();
        }

        private string ConvertMillisecondsToTime(uint milliseconds)
        {
            var seconds = milliseconds / 1000;
            var format = "{0} {1}";

            StringBuilder result = new StringBuilder();
            if (seconds >= 3600)
            {
                result.AppendFormat(format, seconds / 3600, "h");
                seconds %= 3600;
            }
            if (seconds >= 60)
            {
                if (result.Length > 0)
                    result.Append(' ');

                result.AppendFormat(format, seconds / 60, "min");
                seconds %= 60;
            }
            if (seconds >= 0)
            {
                if (result.Length > 0)
                    result.Append(' ');

                result.AppendFormat(format, seconds, "s");
            }

            return result.ToString();
        }

        public void Update()
        {
            try
            {
                UpdateWatchList();
                UpdateTotalTime();
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        private void UpdateTotalTime()
        {
            if (isConnected)
            {
                //4.5
                //monitorClient.GetTotalWorkTimeAsync().ContinueWith(t => view.SetTotalTime(ConvertMillisecondsToTime(t.Result)));
                //monitorClient.GetTotalWorkTimeAsync();
                view.SetTotalTime(ConvertMillisecondsToTime(monitorClient.GetTotalWorkTime()));
            }
            else
            {
                view.SetTotalTime(ConvertMillisecondsToTime(monitor.GetTotalWorkTime()));
            }
        }

        private void UpdateWatchList()
        {
            if (isConnected)
            {
                view.SetWatchList(monitorClient.GetWatchList().OrderBy(w => w.Process).Select(w => new WatchProcessViewModel(w.Process, ConvertMillisecondsToTime(w.Milliseconds))));
            }
            else
            {
                view.SetWatchList(monitor.GetWatchList().OrderBy(w => w.Process).Select(w => new WatchProcessViewModel(w.Process, ConvertMillisecondsToTime(w.Milliseconds))));
            }
        }

        public void Start()
        {
            try
            {
                if (isConnected)
                {
                    //monitorClient.StartAsync().ContinueWith(t => Update());
                    if (!monitorClient.IsStarted())
                    {
                        monitorClient.Start();
                        Update();
                    }
                }
                else
                {
                    if (!monitor.IsStarted())
                    {
                        monitor.Start();
                        Update();
                    }
                }

                timer.Start();
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void Stop()
        {
            try
            {
                if (isConnected)
                {
                    //monitorClient.StopAsync().ContinueWith(t => Update());
                    if (monitorClient.IsStarted())
                    {
                        monitorClient.Stop();
                        Update();
                    }
                }
                else
                {
                    if (monitor.IsStarted())
                    {
                        monitor.Stop();
                        Update();
                    }
                }

                timer.Stop();
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void Reset()
        {
            try
            {
                if (isConnected)
                {
                    //monitorClient.ResetAsync().ContinueWith(t => Update());
                    monitorClient.Reset();
                    Update();
                }
                else
                {
                    monitor.Reset();
                    Update();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void AddToWatch(string process)
        {
            try
            {
                if (isConnected)
                {
                    //monitorClient.AddToWatchAsync(process).ContinueWith(t => Update());
                    monitorClient.AddToWatch(process);
                    Update();
                }
                else
                {
                    monitor.AddToWatch(process);
                    Update();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void RemoveWatch(string process)
        {
            try
            {
                if (isConnected)
                {
                    //monitorClient.RemoveWatchAsync(process).ContinueWith(t => Update());
                    monitorClient.RemoveWatch(process);
                    Update();
                }
                else
                {
                    monitor.RemoveWatch(process);
                    Update();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void ClearWatchs()
        {
            try
            {
                if (isConnected)
                {
                    //monitorClient.ClearWatchsAsync().ContinueWith(t => Update());
                    monitorClient.ClearWatchs();
                    Update();
                }
                else
                {
                    monitor.ClearWatchs();
                    Update();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void LoadWatchs()
        {
            try
            {
                if (isConnected)
                {
                    monitorClient.LoadWatchs();
                    Update();
                }
                else
                {
                    monitor.LoadWatchs();
                    Update();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void SaveWatchs()
        {
            try
            {
                if (isConnected)
                {
                    monitorClient.SaveWatchs();
                    Update();
                }
                else
                {
                    monitor.SaveWatchs();
                    Update();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void Download(string path)
        {
            try
            {
                if (isConnected)
                {
                    using (var stream = monitorClient.GetStatistics())
                    {
                        using (var inStream = new StreamReader(stream))
                        {
                            using (var outStream = new StreamWriter(path))
                            {
                                outStream.Write(inStream.ReadToEnd());
                            }
                        }
                    }
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void SaveStatistics()
        {
            try
            {
                if (isConnected)
                {
                    monitorClient.SaveStatistics();
                }
                else
                {
                    monitor.SaveStatistics();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void SetInterval(uint interval)
        {
            try
            {
                if (isConnected)
                {
                    monitorClient.SetInterval(interval);
                }
                else
                {
                    monitor.SetInterval(interval);
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void Connect(string server)
        {
            try
            {
                if (!isConnected)
                {
                    monitorClient = new MonitorClient(new BasicHttpBinding()
                    {
                        OpenTimeout = TimeSpan.FromSeconds(5),
                        SendTimeout = TimeSpan.FromSeconds(10),
                        ReceiveTimeout = TimeSpan.FromSeconds(10),
                        CloseTimeout = TimeSpan.FromSeconds(5)
                    }, new EndpointAddress(string.Format(ConfigurationManager.AppSettings["connectionFormat"], server)));

                    //monitorClient.Open();
                    monitorClient.IsStarted();
                    //monitorClient.ChannelFactory.Open();

                    isConnected = true;
                    Update();
                    timer.Start();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public void Disconnect()
        {
            try
            {
                if (isConnected)
                {
                    monitorClient.SaveWatchs();
                    monitorClient.SaveStatistics();
                    monitorClient.Close();

                    isConnected = false;
                    Update();
                    timer.Stop();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }
        }

        public IEnumerable<string> GetAllProcesses()
        {
            IEnumerable<string> result = null;
            try
            {
                if (isConnected)
                {
                    result = monitorClient.GetAllProcesses();
                }
                else
                {
                    result = monitor.GetAllProcesses();
                }
            }
            catch (TimeoutException te)
            {
#if DEBUG
                view.ShowError(te.ToString());
#else
                view.ShowError(te.Message);
#endif
            }
            catch (EndpointNotFoundException enfe)
            {
#if DEBUG
                view.ShowError(enfe.ToString());
#else
                view.ShowError(enfe.Message);
#endif
            }

            return result;
        }

        public void Exit()
        {
            monitor.SaveWatchs();
            monitor.SaveStatistics();

            Application.Current.Shutdown();
        }

        public bool IsStarted
        {
            get
            {
                bool result = false;
                try
                {
                    if (isConnected)
                        result = monitorClient.IsStarted();
                    else
                        result = monitor.IsStarted();
                }
                catch (TimeoutException te)
                {
#if DEBUG
                view.ShowError(te.ToString());
#else
                    view.ShowError(te.Message);
#endif
                }
                catch (EndpointNotFoundException enfe)
                {
#if DEBUG
                    view.ShowError(enfe.ToString());
#else
                    view.ShowError(enfe.Message);
#endif
                }

                return result;
            }
        }

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }

    }

}
