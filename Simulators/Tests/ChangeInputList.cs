using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    class ChangeInputList
    {
        public Dictionary<string, double> inputVariables = new()
        { 
            ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):LiqVolFlow.501.0"]= 5.0 
        };

        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            string Bit_in_LqVolFlow = "Document.0/FlowSht.1/StreamObject.400(Bit in -3):LiqVolFlow.501.0";
            string LiquidDensity = "Document.0/FlowSht.1/UnitOpObject.400(Manipulator):ExtraData.553.3"; 

            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
        }
}
