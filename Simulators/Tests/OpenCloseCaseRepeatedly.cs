using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class OpenCloseCaseRepeatedly
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            for (int i = 0; i < 10; i++)
            {
                if (hysysSimulator.OpenCase(new CaseInfo(filePath, fileName)))
                    hysysSimulator.CloseCase();
            }
        }
    }
}
