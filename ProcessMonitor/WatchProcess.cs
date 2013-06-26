using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor
{

    [DataContract]
    public class WatchProcess
    {

        private string process;
        private uint milliseconds;

        public WatchProcess()
        {

        }

        public WatchProcess(string process)
        {
            this.process = process;
        }

        public override bool Equals(object obj)
        {
            var p = obj as WatchProcess;
            if (p == null)
                return false;

            return this.process == p.Process;
        }

        public override int GetHashCode()
        {
            return process.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", process, milliseconds);
        }

        [DataMember]
        public string Process
        {
            get
            {
                return process;
            }
            set
            {
                process = value;
            }
        }

        [DataMember]
        public uint Milliseconds
        {
            get
            {
                return milliseconds;
            }
            set
            {
                milliseconds = value;
            }
        }

    }

}
