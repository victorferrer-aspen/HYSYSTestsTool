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
    public class GasPlantParallel
    {
        static string OneMassFlow = "Document.0/FlowSht.1/StreamObject.400(1):MassFlow.501.0";
        static string SalesGasTemperature = "Document.0/FlowSht.1/StreamObject.400(Sales Gas):Temperature.501.0";
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

            Parallel.ForEach(hysysSimulators, simulator =>
            {
                SimulationCase simCase = (SimulationCase)simulator.GetActiveSimulationCase();
                dynamic oneMassFlow = simulator.GetCaseVariable(OneMassFlow);
                dynamic salesGasTemperature = simulator.GetCaseVariable(SalesGasTemperature);
                double oneMassFlowValue = oneMassFlow.Value;
                double salesGasTemperatureValue = salesGasTemperature.Value;
                for (int i = 1; i <= 3; i++)
                {
                    oneMassFlowValue *= 1.1;
                    salesGasTemperatureValue *= 1.1;
                    simCase.Solver.CanSolve = false;
                    oneMassFlow.Value = oneMassFlowValue;
                    salesGasTemperature.Value = salesGasTemperatureValue;
                    simCase.Solver.CanSolve = true;
                }
                Console.WriteLine($"Test for {simCase.name} completed");
            });

            foreach (var tempFilename in tempFiles)
            {
                File.Delete(Path.Combine(filePath, tempFilename));
            }


            Console.WriteLine("test finished");
            Console.ReadKey();
        }
    }
}
