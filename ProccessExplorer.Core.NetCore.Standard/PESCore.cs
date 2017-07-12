using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProccessExplorer.Core.NetCore
{
    public class PESCore
    {
        private long _minimumSize;
        public PESCore() {
            _minimumSize = 0;
        }

        public PESCore Init(IPESCoreConfiguration config)
        {
            _minimumSize = config.MinimumSize;
            return this;
        }

        public IEnumerable<MyProc> GetData()
        {
            var list = new Dictionary<string, MyProc>();
            var proccesses = Process.GetProcesses();
            foreach (var proc in proccesses)
            {
                MyProc tmp;
                try
                {
                    tmp = list[proc.ProcessName];
                }
                catch
                {
                    tmp = list[proc.ProcessName] = new MyProc();
                }

                tmp.Name = proc.ProcessName;
                tmp.PrivateMemorySize += proc.PrivateMemorySize64;
                tmp.VirtualMemorySize += proc.VirtualMemorySize64;
            }

            return list.Values.Where(item => item.PrivateMemorySize > _minimumSize).ToList();
        }
    }
}
