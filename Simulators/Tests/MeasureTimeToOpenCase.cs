using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class MeasureTimeToOpenCase
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            Stopwatch stopWatch = new Stopwatch();
            for (int i = 0; i < 10; i++)
            {
                stopWatch.Start();
                if (hysysSimulator.OpenCase(new CaseInfo(filePath, fileName)))
                {
                    stopWatch.Stop();
                    Console.WriteLine($"Trial {i+1}: {stopWatch.Elapsed.TotalSeconds} ");
                    hysysSimulator.CloseCase();
                }
                    

            }
        }
    }
}
