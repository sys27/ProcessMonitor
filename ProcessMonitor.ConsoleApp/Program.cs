using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.ConsoleApp
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Run:");
            Console.WriteLine("1 - Local Monitor");
            Console.WriteLine("2 - Service");

            var input = Console.ReadLine();
            if (input == "1")
            {
                Monitor monitor = new Monitor();
                monitor.AddToWatch("explorer");
                monitor.AddToWatch("calc");
                monitor.AddToWatch("chrome");
                monitor.Start();

                Console.ReadLine();
            }
            else if (input == "2")
            {
                string address = string.Empty; ;
                foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    //if (!item.IsIPv6LinkLocal)
                    //    if (!item.IsIPv6Multicast)
                    //        if (!item.IsIPv6SiteLocal)
                    //            if (!item.IsIPv6Teredo)
                    //                address = item.ToString();
                    if (!item.IsIPv6LinkLocal && !item.IsIPv6Multicast && !item.IsIPv6SiteLocal && !item.IsIPv6Teredo)
                        address = item.ToString();
                }

                Monitor monitor = new Monitor();
                using (ServiceHost host = new ServiceHost(monitor, new Uri(string.Format("http://{0}:4325/", address))))
                {
                    host.Open();
                    monitor.Start();
                    Console.WriteLine("Opened...");

                    Console.ReadLine();
                }
            }
        }

    }

}
