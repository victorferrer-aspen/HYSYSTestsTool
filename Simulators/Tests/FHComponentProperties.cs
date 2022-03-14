using Aspentech.HYSYS;
using Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace Simulators.Tests
{
    public class FHComponentProperties
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));

            string csvFilePath = Path.Combine(filePath, $"Properties_results.csv");
            PersistenceManager persistanceManager = new PersistenceManager(csvFilePath);

            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
            IHysysVariableManager varManager = new HysysVariableManager(hysysSimulator);
            Flowsheet flowsheet = varManager.get_flowsheet(simCase.Flowsheet, "Main");
            ProcessStream radStream = varManager.get_process_stream(flowsheet, "50% H-101 Feed-2");
            ProcessStream steamStream = varManager.get_process_stream(flowsheet, "50% Injection Steam-2");
            ProcessStream productStream = varManager.get_process_stream(flowsheet, "50% H101 Oil Out-2");

            string[] components = radStream.FluidPackage.ComponentList.Components.Names;

            for (int i = 0; i < components.Length; i++)
            {
                simCase.Solver.CanSolve = false;
                radStream.ComponentMolarFraction.Erase();

                dynamic newMolarFractionVAlues = radStream.ComponentMolarFractionValue;
                for (int j = 0; j < components.Length; j++)
                {
                    if (j == i)
                    {
                        newMolarFractionVAlues[j] = 1;
                    }
                    else
                    {
                        newMolarFractionVAlues[j] = 0;
                    }

                }
                
                radStream.ComponentMolarFraction.Calculate(newMolarFractionVAlues);
                simCase.Solver.CanSolve = true;

                dynamic properties = hysysSimulator.GetCaseVariable("Document.0/FlowSht.1/StreamObject.400(50% H-101 Feed-2)/HysysCorrelation.300.55:ExtraData.560.[]");
                string rad_Stream_MW = "Document.0/FlowSht.1/StreamObject.400(50% H-101 Feed-2):MoleWeight.501.0";
                string steam_Stream_MW = "Document.0/FlowSht.1/StreamObject.400(50% Injection Steam-2):MoleWeight.501.0";
                string prod_Stream_MW = "Document.0/FlowSht.1/StreamObject.400(50% H101 Oil Out-2):MoleWeight.501.0";

                varManager.add_variable_through_moniker(radStream, rad_Stream_MW, "MW");
                varManager.add_variable_through_process_stream(radStream, "MolarFlow");
                varManager.add_variable_through_process_stream(radStream, "MassFlow");
                varManager.add_variable_through_moniker(steamStream, steam_Stream_MW, "MW");
                varManager.add_variable_through_process_stream(steamStream, "MolarFlow");
                varManager.add_variable_through_process_stream(steamStream, "MassFlow");
                varManager.add_variable_through_moniker(productStream, prod_Stream_MW, "MW");
                varManager.add_variable_through_process_stream(productStream, "MolarFlow");
                varManager.add_variable_through_process_stream(productStream, "MassFlow");

                string data = string.Empty;


                var header_names = new List<string>();
                var header_units = new List<string>();
                var values = new List<string>();

                if (i == 0)
                {
                    header_names.Add("Component");
                    header_units.Add(" ");
                }

                values.Add(components[i]);

                foreach (var variable in varManager.get_list())
                {
                    if (i == 0)
                    {
                        header_names.Add(variable.Name);
                        header_units.Add(variable.Uom);
                    }
                    values.Add(variable.value.ToString());
                }

                if (i == 0)
                {
                    data = string.Join(",", header_names);
                    persistanceManager.WriteToFile(data, false);
                    data = string.Join(",", header_units);
                    persistanceManager.WriteToFile(data, true);
                }

                data = string.Join(",", values);
                persistanceManager.WriteToFile(data, true);

                varManager.clear_list();
            }
        }
    }
}

