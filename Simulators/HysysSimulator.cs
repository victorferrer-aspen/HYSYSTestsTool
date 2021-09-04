using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators
{
    public partial class HysysSimulator : ISimulator
    {
        private Application hyApp;
        private SimulationCase simCase;
        public CaseInfo caseInfo { get; set; }
        public SimulatorInfo simInfo { get; set; }
        public event EventHandler<SimulatorEventArgs> SimulationCaseIsOpen;
        public bool CreateSimulator(string progId, string version)
        {
            if(string.IsNullOrWhiteSpace(version))
            {
                hyApp = new Application();
            }
            else
            {
                Type type = Type.GetTypeFromProgID($"{progId}.{version}");
                dynamic instance = Activator.CreateInstance(type);
                hyApp = instance;
            }
            
            if (hyApp == null)
            {
                return false;
            }
            else
            {
                hyApp.Visible = true;
                return true;
            }

        }
        public void CloseSimulator()
        {
            hyApp?.Quit();
            hyApp = null;
        }
        public bool OpenCase(CaseInfo caseInfo)
        {
            this.caseInfo = caseInfo;
            simCase = hyApp.SimulationCases.Open(caseInfo.FullPath);
            if (simCase == null)
            {
                return false;
            }
            else
            {
                //BackDoor bd = (BackDoor)simCase;
                //dynamic bd.get_BackDoorVariable(":MultiCaseProcessId.0").Variable;
                //simInfo = new SimulatorInfo(hyApp.Version, hyApp.LongVersion, Int32.Parse(processId.Value));
                simInfo = new SimulatorInfo(hyApp.Version, hyApp.LongVersion, 1000);
                //SimulatorEventArgs args = new SimulatorEventArgs { SimInfo = simInfo};
                //SimulatiionCaseOpened(args);
                simCase.Visible = true;
                return true;

            }
        }
        public void SaveCase()
        {
            simCase.Save();
        }
        public void CloseCase()
        {
            simCase.Close();
        }

        public int GetProcessId()
        {
            BackDoor bd = (BackDoor)simCase;
            dynamic processId = bd.get_BackDoorVariable(":MultiCaseProcessId.0").Variable;
            return Int32.Parse(processId.Value);
        }
        public string GetSimulatorVersion()
        {
            return simInfo.ShortVersion.Replace(HysysStrings.HysysKeyNotFound, "HysysEngine");
        }
        public void Dispose()
        {
            ((_SimulationCase)simCase)?.Close();
            simCase = null;
            hyApp.Quit();
            hyApp = null;
        }

        protected virtual void SimulatiionCaseOpened(SimulatorEventArgs e)
        {
            EventHandler<SimulatorEventArgs> handler = SimulationCaseIsOpen;
            handler?.Invoke(this, e);
        }

        public _SimulationCase GetActiveSimulationCase()
        {
            return simCase ?? hyApp.ActiveDocument;
        }
        public dynamic GetCaseVariable(string moniker)
        {
            BackDoor bd = (BackDoor)simCase;
            return bd?.get_BackDoorVariable(moniker).Variable ?? null;
        }
    }

    public class SimulatorEventArgs : EventArgs
    {
        public SimulatorInfo SimInfo { get; set; }
    }
}
