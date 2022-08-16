using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class ChangeInputList
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            Dictionary<string, (double,string)> inputVariables = new Dictionary<string, (double,string)>()
            {
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):LiqVolFlow.501.0"] = (190, "kbpd"),
                ["Document.0/FlowSht.1/UnitOpObject.400(E-1B/D/F)/ExchPerf.500/ExchangerDesign.300/SimulationObject.300:ExtraData.803.0(FoulResCS)"] = (0, "F-hr-ft2/Btu"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 water-3+):LiqVolFlow.501.0"] = (2, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Pressure.501.0"] = (6, "psig"),
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):Temperature.501.0"] = (79, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Temperature.501.0"] = (130, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 Nap-3):LiqVolFlow.501.0"] = (45, "kbpd")
            };

            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();

            simCase.Solver.CanSolve = false;

            foreach (var variable in inputVariables)
            {
                dynamic hysysVariable = hysysSimulator.GetCaseVariable(variable.Key);
                hysysVariable.SetValue(variable.Value.Item1, variable.Value.Item2);
            }

            simCase.Solver.CanSolve = true;
        }
    }
}
