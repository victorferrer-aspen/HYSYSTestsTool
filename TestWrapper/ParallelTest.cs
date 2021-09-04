using Simulators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWrapper.Tests;

namespace TestWrapper.Tests
{
    public class ParallelTest : ITest
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ProgId { get; set; }
        public string SimulatorVersion { get; set; }
        public int NumberOfSimulators { get; set; }
        public BlockingCollection<ISimulator> Simulators { get; set; }
        public Action<string, string, BlockingCollection<ISimulator>> Test { get; set; } = null;
        public bool OpenSimulator()
        {
            var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = NumberOfSimulators };
            Parallel.For(0, NumberOfSimulators, parallelOptions, i =>
            {
                Simulators = new BlockingCollection<ISimulator>();
                ISimulator Simulator = new HysysSimulator();
                Simulator.CreateSimulator(ProgId, SimulatorVersion);
                Simulators.Add(Simulator);
            });

            return true;
        }
        public void CloseSimulator()
        {
            Parallel.ForEach(Simulators, sim => sim.Dispose());
        }
        public void StartTest()
        {
            Test(FilePath, FileName, Simulators);
        }

        public ISimulator GetSimulator()
        {
            return Simulators.Take();
        }
        public void PutSimulator(ISimulator simulator)
        {
            Simulators.Add(simulator);
        }
    }
}
