using Simulators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWrapper.Tests;

namespace HYSYSTestsTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start");

            ITest test = new BasicTest
            {
                FilePath = Directory.GetCurrentDirectory(),
                FileName = args[0],
                ProgId = HysysStrings.HysysEngineProgId,
                SimulatorVersion = args[1],
                Test = ChangeSingleInput.TestDefinition
            };

            Console.ReadKey();

            test.OpenSimulator();
            test.StartTest();
            test.CloseSimulator();
        }
    }
}
