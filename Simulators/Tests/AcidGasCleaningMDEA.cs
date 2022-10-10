using Aspentech.HYSYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulators.Tests
{
    public class AcidGasCleaningMDEA
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            IList<IDictionary<string, double>> runs = new List<IDictionary<string, double>>()
            {
                new Dictionary<string, double>(){
                    ["CO2"] = 0.112334175176914,
                    ["H2S"] = 0.0434966743575132,
                    ["Methane"] = 0.690664785198439,
                    ["Ethane"] = 0.153504365267134
                },
                //new Dictionary<string, double>(){
                //    ["CO2"] = 0.107909675420095,
                //    ["H2S"] = 0.0417834733230624,
                //    ["Methane"] = 0.702848553677708,
                //    ["Ethane"] = 0.147458297579135
                //},
                //new Dictionary<string, double>(){
                //    ["CO2"] = 0.10186924768231,
                //    ["H2S"] = 0.0394445722907031,
                //    ["Methane"] = 0.7194821116205,
                //    ["Ethane"] = 0.139204068406487
                //},
                //new Dictionary<string, double>(){
                //    ["CO2"] = 0.0949263257397821,
                //    ["H2S"] = 0.036756218418445,
                //    ["Methane"] = 0.738600872648121,
                //    ["Ethane"] = 0.129716583193651
                //},
                new Dictionary<string, double>(){
                    ["CO2"] = 0.0875301079970184,
                    ["H2S"] = 0.033892344854344,
                    ["Methane"] = 0.758967876728347,
                    ["Ethane"] = 0.119609670420291
                }
            };

            
            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
            IHysysVariableManager varManager = new HysysVariableManager(hysysSimulator);
            Flowsheet flowsheet = varManager.get_flowsheet(simCase.Flowsheet, "Main");
            ProcessStream feedGas = varManager.get_process_stream(flowsheet, "Feed Gas");

            //string[] components = feedGas.FluidPackage.Components.Names;
            Components components = feedGas.FluidPackage.Components;

            foreach(var run in runs)
            {
                simCase.Solver.CanSolve = false;
                //feedGas.ComponentMassFraction.Erase();
                //feedGas.ComponentMolarFraction.Erase();
                int componentIndex = 0;
                dynamic newMassFractions = feedGas.ComponentMassFractionValue;
                foreach (Component component in components)
                {
                    componentIndex = components.index[component.name];
                    switch (component.name)
                    {
                        case "CO2":
                        case "H2S":
                        case "Methane":
                        case "Ethane":
                            newMassFractions[componentIndex] = run[component.name];
                            break;
                        default:
                            newMassFractions[componentIndex] = 0.0;
                            break;
                    }
                }
                feedGas.ComponentMassFraction.SetValues(newMassFractions);
                //dynamic newMoleFractions = feedGas.ComponentMolarFractionValue;
                //feedGas.ComponentMolarFraction.Calculate(newMassFractions);
                simCase.Solver.CanSolve = true;
            }


        }

    }
}
