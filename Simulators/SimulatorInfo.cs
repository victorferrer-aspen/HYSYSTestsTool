using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators
{
    public class SimulatorInfo
    {
        private readonly string shortVersion;
        private readonly string longVersion;
        private readonly int processId;
        public SimulatorInfo(string shortVersion, string longVersion, int processId)
        {
            this.shortVersion = shortVersion;
            this.longVersion = longVersion;
            this.processId = processId;
        }
        public string ShortVersion => shortVersion;
        public string LongVersion => longVersion;
        public int ProcessId => processId;
    }
}
