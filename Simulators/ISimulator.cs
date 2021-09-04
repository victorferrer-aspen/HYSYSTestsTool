using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators
{
    public interface ISimulator: IDisposable
    {
        bool CreateSimulator(string progId, string version);
        void CloseSimulator();
        bool OpenCase(CaseInfo caseInfo);
        void CloseCase();
        void SaveCase();
        int GetProcessId();
        dynamic GetCaseVariable(string moniker);
        _SimulationCase GetActiveSimulationCase();
        string GetSimulatorVersion();
    }
}
