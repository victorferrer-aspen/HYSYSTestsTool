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
            List<Dictionary<string, (double, string)>> runs = new List<Dictionary<string, (double, string)>>();

            runs.Add(new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):MassFlow.501.0"] = (76346, "kg/h"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Temperature.501.0"] = (73.2, "C"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Pressure.501.0"] = (59.196, "barg*"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Column Spec):ExtraData.500.0.0"] = (108, "C")
            });

            runs.Add(new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):MassFlow.501.0"] = (81800, "kg/h"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Temperature.501.0"] = (73.2, "C"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Pressure.501.0"] = (59.196, "barg*"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Column Spec):ExtraData.500.0.0"] = (108, "C")
            });

            runs.Add(new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):MassFlow.501.0"] = (54530, "kg/h"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Temperature.501.0"] = (73.2, "C"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Pressure.501.0"] = (59.196, "barg*"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Column Spec):ExtraData.500.0.0"] = (118.8, "C")
            });

            runs.Add(new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):MassFlow.501.0"] = (70892, "kg/h"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Temperature.501.0"] = (73.2, "C"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Pressure.501.0"] = (68.3, "barg*"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Column Spec):ExtraData.500.0.0"] = (129.6, "C")
            });

            runs.Add(new Dictionary<string, (double, string)>()
            {
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):MassFlow.501.0"] = (76346, "kg/h"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Temperature.501.0"] = (73.2, "C"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Hydrotreating)/FlowSht.600/StreamObject.400(65):Pressure.501.0"] = (68.3, "barg*"),
                ["Document.0/FlowSht.1/UnitOpObject.400(Column Spec):ExtraData.500.0.0"] = (129.6, "C")
            });

            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();


            foreach(var run in runs)
            {
                simCase.Solver.CanSolve = false;

                foreach (var variable in run)
                {
                    dynamic hysysVariable = hysysSimulator.GetCaseVariable(variable.Key);
                    hysysVariable.SetValue(variable.Value.Item1, variable.Value.Item2);
                }

                simCase.Solver.CanSolve = true;
            }

        }
    }
}
