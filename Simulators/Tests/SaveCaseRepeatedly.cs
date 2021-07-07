using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class SaveCaseRepeatedly
    {
        public void TestDefinition(string fileName, string filePath, ISimulator hysysSimulator)
        {
            bool isOpen = hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            if (isOpen)
            {
                for (int i = 0; i < 10; i++)
                    hysysSimulator.SaveCase();
            }
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
