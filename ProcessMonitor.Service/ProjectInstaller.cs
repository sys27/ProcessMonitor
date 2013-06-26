using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ProcessMonitor.Service
{

    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {

        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            serviceProcessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem
            };
            serviceInstaller = new ServiceInstaller()
            {
                DisplayName = "Process Monitor",
                ServiceName = "MonitorService",
                StartType = ServiceStartMode.Automatic
            };
            Installers.AddRange(new Installer[] { serviceProcessInstaller, serviceInstaller });
        }

    }

}
