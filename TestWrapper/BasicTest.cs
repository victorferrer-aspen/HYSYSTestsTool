using Simulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWrapper.Tests
{
    public class BasicTest : ITest
    {
        public string FilePath { get; set; }
        public string FileName {get; set;}
        public string ProgId { get; set; }
        public string SimulatorVersion { get; set; }
        public HysysSimulator Simulator { get; set; }
        public Action<string, string, ISimulator> Test { get; set; } = null;
        public bool OpenSimulator()
        {
            Simulator = new HysysSimulator();
            return Simulator.CreateSimulator(ProgId, SimulatorVersion);
        }
        public void CloseSimulator()
        {
            Simulator.Dispose();
        }
        public void StartTest()
        {
            Test(FilePath, FileName, Simulator);
        }

        public ISimulator GetSimulator()
        {
            return Simulator;
        }
    }
}
