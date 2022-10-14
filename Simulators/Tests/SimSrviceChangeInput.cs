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
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7011888888888889,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7316666666666667,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7621444444444445,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.7926222222222222,}",
                //"{Document.0/FlowSht.1/StreamObject.400(Feed Gas):MassFraction.6666.4,0.8231,}"
                "{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.4,0.5,wt %}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.21,65.43,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.1,500,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.9,1.6,bar_g}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.6,505,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.10,70,%}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.15,155,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.14,0.8,bar_g}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.17,0.005,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.20,123.6,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.16,14,bar_g}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.12,168,C}{Document.0/FlowSht.1/UnitOpObject.400(Debutanizer)/Flowsht.600/ColumnSpec.500.8:ExtraData.200,0,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.8,0.35,%}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.3,100,ppmwt}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.13,420,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.5,420,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.2,0.93,}{Document.0/FlowSht.1/UnitOpObject.400(Debutanizer)/Flowsht.600/ColumnSpec.500.9:ExtraData.200,0,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.7,155,C}",
                "{Document.0/FlowSht.1/UnitOpObject.400(Debutanizer)/Flowsht.600/ColumnSpec.500.9(C4- in Gasoline):ExtraData.200,0,}{Document.0/FlowSht.1/UnitOpObject.400(Debutanizer)/Flowsht.600/ColumnSpec.500.8(C5+ in LPG):ExtraData.200,0,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.9,1.9,bar_g}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.10,72.5,%}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.12,192,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.13,435.5,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.14,0.825,bar_g}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.15,175,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.16,14.25,bar_g}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.17,0.0225,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.1,583.5,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.20,154.5,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.21,81.79,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.2,0.94,}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.3,160,ppmwt}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.4,0.85,wt %}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.5,452.5,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.6,525,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.7,167.5,C}{Document.0/FlowSht.1/UnitOpObject.400(Hybrid Indep):ExtraData.500.1.8,1,%}"
            };

            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();

            foreach (var run in runs)
            {
                //simCase.Solver.CanSolve = false;
                dynamic methaneMassFrac = hysysSimulator.GetCaseVariable(":SpecifyInputVariables.0");
                methaneMassFrac.Value = run;
                //simCase.Solver.CanSolve = true;
            }


        }
    }
}
