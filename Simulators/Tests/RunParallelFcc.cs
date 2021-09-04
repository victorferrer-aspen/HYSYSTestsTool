using Aspentech.HYSYS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class RunParallelFcc
    {
        static string RegenCatCoolerDuty = "Document.0/FlowSht.1/UnitOpObject.400(FCC)/FCCReactorSection.700/Regenerator.600.0:HeatFlow.3500.8";
        public static void TestDefinition(string filePath, string fileName, BlockingCollection<ISimulator> hysysSimulators)
        {
            var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
            ConcurrentBag<string> tempFiles = new ConcurrentBag<string>();
            Parallel.ForEach(hysysSimulators, simulator =>
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}.hsc";
                tempFiles.Add(newFileName);
                File.Copy(Path.Combine(filePath, fileName), Path.Combine(filePath, newFileName));
                simulator.OpenCase(new CaseInfo(filePath, newFileName));
            });

            //Parallel.ForEach(hysysSimulators, simulator =>
            //{
            //    SimulationCase simCase = (SimulationCase)simulator.GetActiveSimulationCase();
            //    dynamic regeneratorDuty = simulator.GetCaseVariable(RegenCatCoolerDuty);
            //    double valueSttatus1 = regeneratorDuty.Value;
            //    for (int i = 1; i <= 20; i++)
            //    {
            //        valueSttatus1 *= 1.02;
            //        regeneratorDuty.Value = valueSttatus1;
            //    }
            //});

            foreach(var tempFilename in tempFiles)
            {
                File.Delete(Path.Combine(filePath,tempFilename));
            }
            //hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            //SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
            //dynamic regeneratorDuty = hysysSimulator.GetCaseVariable(RegenCatCoolerDuty);
            //double valueSttatus1 = regeneratorDuty.Value;


            ////for(int i = 1; i <= 50; i++)
            ////{
            ////    valueSttatus1 *= 1.02;
            ////    regeneratorDuty.Value = valueSttatus1;
            ////}
            //Console.WriteLine("test finished");
            //hysysSimulators.Add(hysysSimulator);
        }
    }
}
