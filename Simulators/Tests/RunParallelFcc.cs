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
        static string LiquidDensity = "Document.0/FlowSht.1/UnitOpObject.400(Manipulator):ExtraData.553.3";
        public static void TestDefinition(string filePath, string fileName, BlockingCollection<ISimulator> hysysSimulators)
        {
            ConcurrentBag<string> tempFiles = new ConcurrentBag<string>();
            Parallel.ForEach(hysysSimulators, simulator =>
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}.hsc";
                tempFiles.Add(newFileName);
                File.Copy(Path.Combine(filePath, fileName), Path.Combine(filePath, newFileName));
                simulator.OpenCase(new CaseInfo(filePath, newFileName));
            });

                Console.WriteLine("Press enter to change inputs");
                Console.ReadKey();
                Console.WriteLine("Starting changing inputs");
            try
            { 
                Parallel.ForEach(hysysSimulators, simulator =>
                {
                    SimulationCase simCase = (SimulationCase)simulator.GetActiveSimulationCase();
                    dynamic regeneratorDuty = simulator.GetCaseVariable(RegenCatCoolerDuty);
                    dynamic liquidDentisy = simulator.GetCaseVariable(LiquidDensity);
                    double regeneratorDutyValue = regeneratorDuty.Value;
                    double liquidDentisyValue = liquidDentisy.Value;
                    for (int i = 1; i <= 20; i++)
                    {
                        if(simCase !=null & simCase.Solver != null)
                        {
                            regeneratorDutyValue *= 1.05;
                            liquidDentisyValue *= 1.001;
                            simCase.Solver.CanSolve = false;
                            regeneratorDuty.Value = regeneratorDutyValue;
                            liquidDentisy.Value = liquidDentisyValue;
                            simCase.Solver.CanSolve = true;
                        }
                    }
                    Console.WriteLine($"Test for {simCase.FullName} completed");
                });

                Console.WriteLine("test finished successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine("test finished wit errors");
            }
            finally
            {
                foreach (var tempFilename in tempFiles)
                {
                    File.Delete(Path.Combine(filePath, tempFilename));
                }
                Console.ReadKey();
            }
        }
    }
}
