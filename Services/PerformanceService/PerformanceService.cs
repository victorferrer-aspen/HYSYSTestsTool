using Services.Connection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Performance
{
    public class PerformanceService
    {
        private readonly PersistenceManager PersisntanceService;
        public string ProcessInstanceName { get; private set; }
        public string FullFilePath { get; set; }
        public int ProcessId { get; set; }
        public PerformanceService(string fullFilePath, int processId)
        {
            ProcessId = processId;
            ProcessInstanceName = GetProcessInstanceName(processId);
            PersisntanceService = new PersistenceManager(fullFilePath);
        }
        private string GetProcessInstanceName(int pid)
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {

                using (PerformanceCounter cnt = new PerformanceCounter("Process",
                     "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
                    if (val == pid)
                    {
                        return instance;
                    }
                }
            }
            throw new Exception("Could not find performance counter " +
                "instance name for current process. This is truly strange ...");
        }
        public void GeneratePerformanceInfo(object source, ConnectionEventArgs e)
        {
            string processName = Process.GetProcessById(ProcessId).ProcessName;
            PerformanceCounter bytesInAllHeaps = new PerformanceCounter(".NET CLR Memory", "# Gen 2 Collections", processName);

            TimeSpan hours = TimeSpan.FromMilliseconds(e.Counter * e.Frequency);
            long? totalMemorySize = Process.GetProcessById(ProcessId)?.PrivateMemorySize64;
            float? privateBytesSize = 0;// privateBytes.NextValue();
            float? bytesInAllHeapsSize = bytesInAllHeaps.NextValue();

            string data = $"{hours} , {totalMemorySize}, {bytesInAllHeapsSize}, {totalMemorySize - bytesInAllHeapsSize}";
            bool append = e.Counter > 0 ? true: false; 

            PersisntanceService.WriteToFile(data, append);
        }
    }
}
