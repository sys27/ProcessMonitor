﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17929
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProcessMonitor.App.MonitorService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MonitorService.IMonitor")]
    public interface IMonitor {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/Start", ReplyAction="http://tempuri.org/IMonitor/StartResponse")]
        void Start();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/Stop", ReplyAction="http://tempuri.org/IMonitor/StopResponse")]
        void Stop();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/AddToWatch", ReplyAction="http://tempuri.org/IMonitor/AddToWatchResponse")]
        void AddToWatch(string process);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/RemoveWatch", ReplyAction="http://tempuri.org/IMonitor/RemoveWatchResponse")]
        void RemoveWatch(string process);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/ClearWatchs", ReplyAction="http://tempuri.org/IMonitor/ClearWatchsResponse")]
        void ClearWatchs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/Reset", ReplyAction="http://tempuri.org/IMonitor/ResetResponse")]
        void Reset();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/GetTotalWorkTime", ReplyAction="http://tempuri.org/IMonitor/GetTotalWorkTimeResponse")]
        uint GetTotalWorkTime();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/GetWatchList", ReplyAction="http://tempuri.org/IMonitor/GetWatchListResponse")]
        ProcessMonitor.WatchProcess[] GetWatchList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/LoadWatchs", ReplyAction="http://tempuri.org/IMonitor/LoadWatchsResponse")]
        void LoadWatchs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/SaveWatchs", ReplyAction="http://tempuri.org/IMonitor/SaveWatchsResponse")]
        void SaveWatchs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/SaveStatistics", ReplyAction="http://tempuri.org/IMonitor/SaveStatisticsResponse")]
        void SaveStatistics();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/GetInterval", ReplyAction="http://tempuri.org/IMonitor/GetIntervalResponse")]
        uint GetInterval();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/SetInterval", ReplyAction="http://tempuri.org/IMonitor/SetIntervalResponse")]
        void SetInterval(uint interval);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/IsStarted", ReplyAction="http://tempuri.org/IMonitor/IsStartedResponse")]
        bool IsStarted();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/GetStatistics", ReplyAction="http://tempuri.org/IMonitor/GetStatisticsResponse")]
        System.IO.Stream GetStatistics();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitor/GetAllProcesses", ReplyAction="http://tempuri.org/IMonitor/GetAllProcessesResponse")]
        string[] GetAllProcesses();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMonitorChannel : ProcessMonitor.App.MonitorService.IMonitor, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MonitorClient : System.ServiceModel.ClientBase<ProcessMonitor.App.MonitorService.IMonitor>, ProcessMonitor.App.MonitorService.IMonitor {
        
        public MonitorClient() {
        }
        
        public MonitorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MonitorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Start() {
            base.Channel.Start();
        }
        
        public void Stop() {
            base.Channel.Stop();
        }
        
        public void AddToWatch(string process) {
            base.Channel.AddToWatch(process);
        }
        
        public void RemoveWatch(string process) {
            base.Channel.RemoveWatch(process);
        }
        
        public void ClearWatchs() {
            base.Channel.ClearWatchs();
        }
        
        public void Reset() {
            base.Channel.Reset();
        }
        
        public uint GetTotalWorkTime() {
            return base.Channel.GetTotalWorkTime();
        }
        
        public ProcessMonitor.WatchProcess[] GetWatchList() {
            return base.Channel.GetWatchList();
        }
        
        public void LoadWatchs() {
            base.Channel.LoadWatchs();
        }
        
        public void SaveWatchs() {
            base.Channel.SaveWatchs();
        }
        
        public void SaveStatistics() {
            base.Channel.SaveStatistics();
        }
        
        public uint GetInterval() {
            return base.Channel.GetInterval();
        }
        
        public void SetInterval(uint interval) {
            base.Channel.SetInterval(interval);
        }
        
        public bool IsStarted() {
            return base.Channel.IsStarted();
        }
        
        public System.IO.Stream GetStatistics() {
            return base.Channel.GetStatistics();
        }
        
        public string[] GetAllProcesses() {
            return base.Channel.GetAllProcesses();
        }
    }
}