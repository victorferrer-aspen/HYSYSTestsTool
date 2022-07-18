using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class FlowsheetObjectStatus
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();

            dynamic safety = hysysSimulator.GetCaseVariable("Document.0/PSVSizingManager.300:Boolean.501");

            dynamic result = hysysSimulator.GetCaseVariable(":MulticaseCheckConvergenceForObjectTypes.0");
            string heaterStatusString = "Document.0/FlowSht.1/UnitOpObject.400(FLOW-1)/FlowSht.600/UnitOpObject.400(FLOW-11)/FlowSht.600/UnitOpObject.400(E-100):Status.1";
            //string adjustStatusString = "Document.0/FlowSht.1/UnitOpObject.400(ADJ-1):Status.1";
            string recycleStatusString = "Document.0/FlowSht.1/UnitOpObject.400(RCY-1):Status.1";

            dynamic adjustStatus = hysysSimulator.GetCaseVariable(heaterStatusString);
            dynamic recycleStatus = hysysSimulator.GetCaseVariable(recycleStatusString);

            ICollection<string> statusList = new List<string>();
            //statusList.Add(adjustStatusString);
            statusList.Add(heaterStatusString);

            dynamic statusResults = hysysSimulator.GetCaseVariables(statusList.ToArray());
            if(statusResults.Length > 0)
            {
                foreach (var statusResult in statusResults)
                {
                    Type type = statusResult.GetType();
                    string statusText = statusResult.Variable.Value;
                }
            }

            var recycleStatusValue = recycleStatus.Value;
            dynamic adjuststatus1 = hysysSimulator.GetCaseVariable("Document.0/FlowSht.1/UnitOpObject.400(ADJ-1):Attribute.501");
            
            dynamic adjuststatus2 = hysysSimulator.GetCaseVariable("Document.0/FlowSht.1/UnitOpObject.400(ADJ-1):Attribute.500.1");
            dynamic result1 = hysysSimulator.GetCaseVariable("Document.0/FlowSht.1/UnitOpObject.400(RCY-1):Boolean.500.1");
            dynamic result2 = hysysSimulator.GetCaseVariable("Document.0/FlowSht.1/UnitOpObject.400(RCY-1):Attribute.500.1");

            //double valueSttatus1 = result1.Value;
            //double valueSttatus2 = result2.Value;

            int objectStatus = simCase.GetFlowsheetStatus(Aspentech.HYSYS.FlowSheetObjStatusFlag_enum.flag_Error);

            //objectStatus = simCase.GetFlowsheetStatus(Aspentech.HYSYS.FlowSheetObjStatusFlag_enum.flag_NotSolved);


            //objectStatus = simCase.GetFlowsheetStatus(Aspentech.HYSYS.FlowSheetObjStatusFlag_enum.flag_UnderSpecified);

            var objectsWithWarning = simCase.GetFlowsheetObjectTypeAndName(Aspentech.HYSYS.FlowSheetObjStatusFlag_enum.flag_Warning); 
            var objectNotSolved = simCase.GetFlowsheetObjectTypeAndName(Aspentech.HYSYS.FlowSheetObjStatusFlag_enum.flag_NotSolved);
            var objectConverged = simCase.GetFlowsheetObjectTypeAndName(Aspentech.HYSYS.FlowSheetObjStatusFlag_enum.flag_OK);

            hysysSimulator.CloseSimulator();
        }
    }
}
