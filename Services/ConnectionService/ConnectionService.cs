using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Services.Connection
{
    public class ConnectionEventArgs : EventArgs
    {
        public int Counter { get; set; }
        public int Frequency { get; set; }
        public ConnectionEventArgs(int counter, int frequency)
        {
            Counter = counter;
            Frequency = frequency;
        }
    }
    public class ConnectionService : IConnectionService
    {
        private Timer pollTimer;
        public event EventHandler<ConnectionEventArgs> MessagedArrived;
        public int Frequency { get; private set; }
        public int Counter { get; set; }

        public ConnectionService(int frequency)
        {
            Frequency = frequency;
            pollTimer = new Timer(frequency);
            pollTimer.Elapsed += new ElapsedEventHandler(OnMessageArrived);
        }

        public void Connect() => pollTimer.Start();
        public void Disconect() => pollTimer.Stop();

        private void OnMessageArrived(object source, EventArgs e)
        {
            ConnectionEventArgs eventArgs = new ConnectionEventArgs(Counter, Frequency);
            EventHandler<ConnectionEventArgs> handler = MessagedArrived;
            handler?.Invoke(this, eventArgs);
            this.Counter++;
        }
    }
}
