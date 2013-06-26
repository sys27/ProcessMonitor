using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace ProcessMonitor
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Monitor : IMonitor, IDisposable
    {

        private bool isDisposed;

        private Timer timer;
        private uint interval = 1000;
        private HashSet<WatchProcess> watchList;
        private uint totalWorkTime = 0;
        private DateTime startTime;

        public Monitor()
        {
            startTime = DateTime.Now;
            watchList = new HashSet<WatchProcess>();
            LoadWatchs();

            timer = new Timer(interval);
            timer.Elapsed += WatchLoop;
        }

        ~Monitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    timer.Dispose();
                }

                isDisposed = true;
            }
        }

        private void WatchLoop(object o, ElapsedEventArgs args)
        {
            totalWorkTime += interval;

            var strings = watchList.Select(wp => wp.Process);
            var processes = Process.GetProcesses().Select(p => p.ProcessName).OrderBy(s => s).ToList();
            for (int i = 0; i < processes.Count; i++)
            {
                for (int j = i + 1; j < processes.Count; j++)
                {
                    if (processes[j] == processes[i])
                    {
                        processes.RemoveAt(j);
                        j--;
                    }
                }
            }
            processes = processes.Where(p => strings.Contains(p)).ToList();
            if (processes.Count() > 0)
            {
                foreach (var process in processes)
                {
                    var watch = watchList.FirstOrDefault(p => p.Process == process);
                    if (watch != null)
                    {
                        watch.Milliseconds += interval;
                    }
                }
            }
        }

        public void Start()
        {
            if (!timer.Enabled)
            {
                startTime = DateTime.Now;
                timer.Start();
            }
        }

        public void Stop()
        {
            if (timer.Enabled)
                timer.Stop();

            SaveWatchs();
            SaveStatistics();
        }

        public void AddToWatch(string process)
        {
            lock (watchList)
            {
                watchList.Add(new WatchProcess(process));
            }
        }

        public void RemoveWatch(string process)
        {
            lock (watchList)
            {
                watchList.Remove(new WatchProcess(process));
            }
        }

        public void ClearWatchs()
        {
            lock (watchList)
            {
                watchList.Clear();
            }
        }

        public void Reset()
        {
            foreach (var watch in watchList)
            {
                watch.Milliseconds = 0;
            }
            totalWorkTime = 0;
        }

        public WatchProcess[] GetWatchList()
        {
            return watchList.ToArray();
        }

        public string[] GetAllProcesses()
        {
            var processes = Process.GetProcesses().Select(p => p.ProcessName).OrderBy(s => s).ToList();
            for (int i = 0; i < processes.Count; i++)
            {
                for (int j = i + 1; j < processes.Count; j++)
                {
                    if (processes[i] == processes[j])
                    {
                        processes.RemoveAt(j);
                        j--;
                    }
                }
            }

            return processes.ToArray();
        }

        public void LoadWatchs()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["watchsPath"]))
                throw new ArgumentException("path");
            var path = Path.Combine(Path.GetTempPath(), ConfigurationManager.AppSettings["statisticsFolder"], ConfigurationManager.AppSettings["watchsPath"]);

            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                foreach (var watch in doc.Root.Elements("process"))
                {
                    watchList.Add(new WatchProcess(watch.Value));
                }
            }
        }

        public void SaveWatchs()
        {
            if (watchList.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["watchsPath"]))
                    throw new ArgumentException("path");
                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["statisticsFolder"]))
                    throw new ArgumentException("folder");

                var dir = Path.Combine(Path.GetTempPath(), ConfigurationManager.AppSettings["statisticsFolder"]);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var path = Path.Combine(dir, ConfigurationManager.AppSettings["watchsPath"]);

                XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
                XElement root = new XElement("watchs");
                foreach (var watch in watchList.OrderBy(w => w.Process))
                {
                    root.Add(new XElement("process", watch.Process));
                }
                doc.Add(root);
                doc.Save(path);
            }
        }

        public void SaveStatistics()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["statisticsPath"]))
                throw new ArgumentException("path");
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["statisticsFolder"]))
                throw new ArgumentException("folder");

            var dir = Path.Combine(Path.GetTempPath(), ConfigurationManager.AppSettings["statisticsFolder"]);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, ConfigurationManager.AppSettings["statisticsPath"]);

            XDocument doc;
            if (File.Exists(path))
            {
                doc = XDocument.Load(path);

                var currentEl = doc.Root.Elements("watchList").Where(el => el.Attribute("startTime").Value == startTime.ToString()).FirstOrDefault();
                if (currentEl != null)
                {
                    currentEl.Remove();
                }
            }
            else
            {
                doc = new XDocument(new XDeclaration("1.0", "UTF-8", null), new XElement("lists"));
            }

            XElement elWatch = new XElement("watchList", new XAttribute("startTime", startTime.ToString()), new XAttribute("stopTime", DateTime.Now.ToString()), new XAttribute("totalTime", totalWorkTime));
            if (totalWorkTime != 0)
            {
                lock (watchList)
                {
                    foreach (var watch in watchList)
                    {
                        if (watch.Milliseconds != 0)
                        {
                            elWatch.Add(new XElement("watch", new XElement("process", watch.Process), new XElement("time", watch.Milliseconds)));
                        }
                    }
                }
            }
            doc.Root.Add(elWatch);

            doc.Save(path);
        }

        public Stream GetStatistics()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["statisticsPath"]))
                throw new ArgumentException("path");
            var path = Path.Combine(Path.GetTempPath(), ConfigurationManager.AppSettings["statisticsFolder"], ConfigurationManager.AppSettings["statisticsPath"]);

            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public uint GetTotalWorkTime()
        {
            return totalWorkTime;
        }

        public uint GetInterval()
        {
            return interval;
        }

        public void SetInterval(uint interval)
        {
            this.interval = interval;
        }

        public bool IsStarted()
        {
            return timer.Enabled;
        }

    }

}
