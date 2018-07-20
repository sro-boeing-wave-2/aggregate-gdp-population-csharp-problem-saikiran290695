using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AggregateGDPPopulation
{
    public class Class1
    {
        public static void Method(string FilePath)
        {
            Regex reg = new Regex("[\"]+");
            string line;
            StreamReader sample = new StreamReader(FilePath);
            JObject CountryVsContinent = JObject.Parse(File.ReadAllText("../../../../AggregateGDPPopulation/data/country-continent.json"));
            List<string> rows = new List<string>();
            
            while (!sample.EndOfStream)
            {
                line = reg.Replace(sample.ReadLine(), "");
                rows.Add(line);
            }
            string[] header;
            header = rows[0].Split(',');
            int CountryIndex = Array.IndexOf(header, "Country Name");
            int PopulationIndex = Array.IndexOf(header, "Population (Millions) - 2012");
            int GDPIndex = Array.IndexOf(header, "GDP Billions (US Dollar) - 2012");
            rows.RemoveAt(0);
            Dictionary<string, List<double>> resultset = new Dictionary<string, List<double>>();
            string[] rowdata;
            foreach (string row in rows)
            {
                rowdata = row.Split(',');
                try
                {
                    string continent = CountryVsContinent[rowdata[CountryIndex]].ToString();
                    if (resultset.ContainsKey(continent))
                    {
                        double GDPToAdd = Convert.ToDouble(rowdata[GDPIndex]);
                        double PopulationToAdd = Convert.ToDouble(rowdata[PopulationIndex]);
                        resultset[continent][0] = resultset[continent][0] + GDPToAdd;
                        resultset[continent][1] = resultset[continent][1] + PopulationToAdd;
                    }
                    else
                    {
                        List<double> GDPVsPopulation = new List<double>();
                        GDPVsPopulation.Add(Convert.ToDouble(rowdata[GDPIndex]));
                        GDPVsPopulation.Add(Convert.ToDouble(rowdata[PopulationIndex]));
                        resultset.Add(continent, GDPVsPopulation);
                    }
                }
                catch { }
            }
            string Json = "{ ";
            foreach (string continent in resultset.Keys)
            {
                Json += "\"" + continent + "\": {";
                Json += "  \"GDP_2012\": " + resultset[continent][0];
                Json += ",  \"POPULATION_2012\": " + resultset[continent][1] + " }, ";
            }
            Json = Json.Substring(0, Json.LastIndexOf(",")) + "}";          
            StreamWriter outputdata = new StreamWriter("../../../../AggregateGDPPopulation/data/outputfilenew.json");
            outputdata.WriteLine(Json);
            outputdata.Close();
        }
    }
}
