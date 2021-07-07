using Services.Connection;
using Services.Performance;
using System;

namespace Services.Monitor
{
    public class MonitorService
    {
        private PerformanceService PerformanceService;
        private ConnectionService Connection;
        public bool Connected { get; private set; }
        public MonitorService(int processId, int frequency, string filePath = "")
        {
            PerformanceService = new PerformanceService(filePath, processId);
            Connection = new ConnectionService(frequency);
            Connected = false;
        }
        public void Connect()
        {
            Connection.MessagedArrived += new EventHandler<ConnectionEventArgs>(PerformanceService.GeneratePerformanceInfo);
            Connection.Connect();
            Connected = true;
        }

        public void Disconect()
        {
            Connection.Disconect();
            Connection.MessagedArrived -= PerformanceService.GeneratePerformanceInfo;
            Connected = false;
        }
    }
}
