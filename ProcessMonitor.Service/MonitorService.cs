using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProcessMonitor.Service
{

    internal class MonitorService : ServiceBase
    {

        private Monitor monitor;
        private ServiceHost serviceHost;

        private Timer saveTimer;

        public MonitorService()
        {
            var time = double.Parse(ConfigurationManager.AppSettings["saveTimer"]);

            string address = string.Empty;
            foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (!item.IsIPv6LinkLocal && !item.IsIPv6Multicast && !item.IsIPv6SiteLocal && !item.IsIPv6Teredo)
                    address = item.ToString();
            }

            monitor = new Monitor();
            serviceHost = new ServiceHost(monitor, new Uri(string.Format("http://{0}:4325/", address)));
            saveTimer = new Timer(time);
            saveTimer.Elapsed += (o, args) =>
            {
                monitor.SaveWatchs();
                monitor.SaveStatistics();
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                saveTimer.Dispose();
                monitor.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            serviceHost.Open();
            monitor.Start();
            saveTimer.Start();
        }

        protected override void OnContinue()
        {
            monitor.Start();
            saveTimer.Start();
        }

        protected override void OnPause()
        {
            saveTimer.Stop();
            monitor.Stop();
        }

        protected override void OnStop()
        {
            monitor.Stop();
            saveTimer.Stop();
            serviceHost.Close();
        }

        protected override void OnShutdown()
        {
            monitor.Stop();
            saveTimer.Stop();
            serviceHost.Close();
        }

    }

}
