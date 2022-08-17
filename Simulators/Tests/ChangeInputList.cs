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
            Dictionary<string, (double,string)> inputVariables1 = new Dictionary<string, (double,string)>()
            {
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):LiqVolFlow.501.0"] = (178.6351563, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Temperature.501.0"] = (127.7117188, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 water-3+):LiqVolFlow.501.0"] = (3.52734375, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Pressure.501.0"] = (7.958460938, "psig"),
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):Temperature.501.0"] = (76.77351563, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 Nap-3):LiqVolFlow.501.0"] = (41.6596875, "kbpd"),
                ["Document.0/FlowSht.1/UnitOpObject.400(E-1B/D/F)/ExchPerf.500/ExchangerDesign.300/SimulationObject.300:ExtraData.803.0(FoulResCS)"] = (0.003652301, "F-hr-ft2/Btu")
            };

            Dictionary<string, (double, string)> inputVariables2 = new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):LiqVolFlow.501.0"] = (146.4351562, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Temperature.501.0"] = (158.7117188, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 water-3+):LiqVolFlow.501.0"] = (2.92734375, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Pressure.501.0"] = (6.392460938, "psig"),
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):Temperature.501.0"] = (92.71351563, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 Nap-3):LiqVolFlow.501.0"] = (50.5396875, "kbpd"),
                ["Document.0/FlowSht.1/UnitOpObject.400(E-1B/D/F)/ExchPerf.500/ExchangerDesign.300/SimulationObject.300:ExtraData.803.0(FoulResCS)"] = (0.004401301, "F-hr-ft2/Btu")
            };

            Dictionary<string, (double, string)> inputVariables3 = new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):LiqVolFlow.501.0"] = (130.3351562, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Temperature.501.0"] = (143.2117188, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 water-3+):LiqVolFlow.501.0"] = (2.62734375, "kbpd"),
                ["Document.0/FlowSht.1/StreamObject.400(E1 Vap in -3):Pressure.501.0"] = (7.175460938, "psig"),
                ["Document.0/FlowSht.1/StreamObject.400(Bit in -3):Temperature.501.0"] = (68.80351563, "C"),
                ["Document.0/FlowSht.1/StreamObject.400(C11 Nap-3):LiqVolFlow.501.0"] = (37.2196875, "kbpd"),
                ["Document.0/FlowSht.1/UnitOpObject.400(E-1B/D/F)/ExchPerf.500/ExchangerDesign.300/SimulationObject.300:ExtraData.803.0(FoulResCS)"] = (0.004026801, "F-hr-ft2/Btu")
            };
            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();

            simCase.Solver.CanSolve = false;

            foreach (var variable in inputVariables1)
            {
                dynamic hysysVariable = hysysSimulator.GetCaseVariable(variable.Key);
                hysysVariable.SetValue(variable.Value.Item1, variable.Value.Item2);
            }

            simCase.Solver.CanSolve = true;

            simCase.Solver.CanSolve = false;

            foreach (var variable in inputVariables2)
            {
                dynamic hysysVariable = hysysSimulator.GetCaseVariable(variable.Key);
                hysysVariable.SetValue(variable.Value.Item1, variable.Value.Item2);
            }

            simCase.Solver.CanSolve = true;

            simCase.Solver.CanSolve = false;

            foreach (var variable in inputVariables3)
            {
                dynamic hysysVariable = hysysSimulator.GetCaseVariable(variable.Key);
                hysysVariable.SetValue(variable.Value.Item1, variable.Value.Item2);
            }

            simCase.Solver.CanSolve = true;
        }
    }
}
