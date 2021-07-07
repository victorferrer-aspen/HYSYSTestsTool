using Aspentech.HYSYS;
using Services;
using Simulators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TestWrapper.Tests
{
    public class ChangeSingleInput
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
            Flowsheet flowsheet = simCase?.Flowsheet;//?.Flowsheets[0] as Flowsheet;
            string flowsheetObject = "Crude_HCAMS";
            ProcessStream stream = flowsheet.MaterialStreams[flowsheetObject];
            List<double> timeList = new List<double>();

            double tempValue = stream.Temperature.Value;
            Stopwatch stopWatch = new Stopwatch();
            for (int i = 1; i <= 4; i++)
            {
                stopWatch.Start();
                if (i % 2 == 0)
                {
                    simCase.Solver.CanSolve = false;
                    stream.Temperature.Value = tempValue;
                    simCase.Solver.CanSolve = true;
                }
                else
                {
                    simCase.Solver.CanSolve = false;
                    stream.Temperature.Value = tempValue + 1;
                    simCase.Solver.CanSolve = true;
                }

                stopWatch.Stop();
                timeList.Add(stopWatch.Elapsed.TotalSeconds);
            }

            string data = string.Join(",", timeList);
            string csvFilePath = Path.Combine(filePath, $"CPUTime_{hysysSimulator.GetSimulatorVersion()}.csv");
            PersistenceManager persistanceManager = new PersistenceManager(csvFilePath);
            persistanceManager.WriteToFile(data, false);
            Console.WriteLine("test finished");

        }
    }
}
