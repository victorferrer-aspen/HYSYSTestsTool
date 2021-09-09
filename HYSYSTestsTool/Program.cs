using Simulators;
using Simulators.Tests;
using System;
using System.IO;
using TestWrapper.Tests;

namespace HYSYSTestsTool
{
    class Program
    {
        static void Main(string[] args)
        { 
            ITest test = new ParallelTest
            {
                FilePath = Directory.GetCurrentDirectory(),
                FileName = Path.Combine(Environment.CurrentDirectory, "FCC_GulfCoast_ROM_V5.hsc"),
                ProgId = HysysStrings.HysysEngineProgId,
                SimulatorVersion = "V12.1",
                NumberOfSimulators = 30,
                Test = RunParallelFcc.TestDefinition
            };

            //Console.ReadKey();

            test.OpenSimulator();
            //Console.ReadKey();
            test.StartTest();
            test.CloseSimulator();
        }
    }
}
