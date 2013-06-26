using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace ProcessMonitor
{

    [ServiceContract]
    public interface IMonitor
    {

        [OperationContract]
        void Start();

        [OperationContract]
        void Stop();

        [OperationContract]
        void AddToWatch(string process);

        [OperationContract]
        void RemoveWatch(string process);

        [OperationContract]
        void ClearWatchs();

        [OperationContract]
        void Reset();

        [OperationContract]
        uint GetTotalWorkTime();

        [OperationContract]
        WatchProcess[] GetWatchList();

        [OperationContract]
        void LoadWatchs();

        [OperationContract]
        void SaveWatchs();

        [OperationContract]
        void SaveStatistics();

        [OperationContract]
        uint GetInterval();

        [OperationContract]
        void SetInterval(uint interval);

        [OperationContract]
        bool IsStarted();

        [OperationContract]
        Stream GetStatistics();

        [OperationContract]
        string[] GetAllProcesses();
        
    }

}
