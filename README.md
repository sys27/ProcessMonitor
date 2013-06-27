ProcessMonitor
=====
ProcessMonitor is desktop app that allows you to watch processes. It has a service that can be installed on a remote computer and you can connect to this service via WCF and monitors running processes. Also it has installer that installs the service and the desktop app. All code is written on C#.

### Structure of projects

1. ProcessMonitor.App - a desktop app.
2. ProcessMonitor.ConsoleApp - a console app.
3. ProcessMonitor.Installer - a WiX installer.
4. ProcessMonitor.Service - a WCF service.
5. ProcessMonitor - a shared library with code.
