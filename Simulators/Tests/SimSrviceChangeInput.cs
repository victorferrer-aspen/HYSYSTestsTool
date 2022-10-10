using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class SimSrviceChangeInput
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            IList<string> runs = new List<string>()
            {
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.5488,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.5792777777777778,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.6097555555555555,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.6402333333333333,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.6707111111111111,}",
                "{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7011888888888889,}",
                "{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7316666666666667,}",
                "{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7621444444444445,}",
                "{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7926222222222222,}",
                "{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.8231,}"
            };

            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
            IHysysVariableManager varManager = new HysysVariableManager(hysysSimulator);
            Flowsheet flowsheet = varManager.get_flowsheet(simCase.Flowsheet, "Main");
            ProcessStream feedGas = varManager.get_process_stream(flowsheet, "Feed Gas");

            //string[] components = feedGas.FluidPackage.Components.Names;
            Components components = feedGas.FluidPackage.Components;

            foreach (var run in runs)
            {
                simCase.Solver.CanSolve = false;
                dynamic methaneMassFrac = hysysSimulator.GetCaseVariable(":SpecifyInputVariables.0");
                methaneMassFrac.Value = run;
                simCase.Solver.CanSolve = true;
            }


        }
    }
}
