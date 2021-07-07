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
                FilePath = Directory.GetCurrentDirectory(), //@"C:\Users\FERRERLV\Desktop\Gas Plant Performance",
                FileName = args[0],//@"PLANT 15 V1.4.2 no HCR V11.hsc",
                Test = ChangeSingleInput.TestDefinition
            };
            Console.ReadKey();
            test.OpenSimulator(args[1]);
            test.RunTest();
            test.CloseSimulator();
        }
    }
}
