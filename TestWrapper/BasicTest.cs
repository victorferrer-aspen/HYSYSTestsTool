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
        public string FilePath;
        public string FileName;
        public HysysSimulator Simulator;
        public Action<string, string, ISimulator> Test = null;

        public bool OpenSimulator(string version = "")
        {
            Simulator = new HysysSimulator();
            return Simulator.CreateSimulator(version);
        }
        public void CloseSimulator()
        {
            Simulator.Dispose();
        }
        public void RunTest()
        {
            Test(FilePath, FileName, Simulator);
        }

        public ISimulator GetSimulator()
        {
            return Simulator;
        }
    }
}
