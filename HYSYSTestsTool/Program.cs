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
            Console.WriteLine("Press Enter to start");

            ITest test = new ParallelTest
            {
                FilePath = Directory.GetCurrentDirectory(),
                FileName = Path.Combine(Environment.CurrentDirectory, "Heat Train V11.hsc"),
                ProgId = HysysStrings.HysysUIProgId,
                SimulatorVersion = "V14.0",
                NumberOfSimulators = 7,
                Test = RunParallelFcc.TestDefinition
            };

            //ITest test = new BasicTest
            //{
            //    FilePath = Directory.GetCurrentDirectory(),
            //    FileName = Path.Combine(Environment.CurrentDirectory, "AdjustRecycle.hsc"),
            //    ProgId = HysysStrings.HysysUIProgId,
            //    SimulatorVersion = "V14.0",
            //    Test = FlowsheetObjectStatus.TestDefinition
            //};
            //Console.ReadKey();

            test.OpenSimulator();
            //Console.ReadKey();
            test.StartTest();
            test.CloseSimulator();
        }
    }
}
