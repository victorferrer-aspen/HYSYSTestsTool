﻿using Simulators;
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
            //ITest test = new ParallelTest
            //{
            //    FilePath = Directory.GetCurrentDirectory(),
            //    FileName = Path.Combine(Environment.CurrentDirectory, "Heat Train V11.hsc"),
            //    ProgId = HysysStrings.HysysUIProgId,
            //    SimulatorVersion = "V14.0",
            //    NumberOfSimulators = 7,
            //    Test = RunParallelFcc.TestDefinition
            //};


            ITest test = new BasicTest
            {
                FilePath = Directory.GetCurrentDirectory(),
                FileName = Path.Combine(Environment.CurrentDirectory, "Samref FCC Model 24Noc2021 data Rev14.hsc"),
                ProgId = HysysStrings.HysysUIProgId,
                SimulatorVersion = "V14.0",
                Test = SimSrviceChangeInput.TestDefinition
            };
            //Console.ReadKey();

            test.OpenSimulator();
            //Console.ReadKey();
            test.StartTest();
            test.CloseSimulator();
        }
    }
}
