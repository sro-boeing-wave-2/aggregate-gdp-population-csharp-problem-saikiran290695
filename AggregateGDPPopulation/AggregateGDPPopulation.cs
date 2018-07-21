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
            Dictionary<string, Dictionary<string, double>> resultset = new Dictionary<string, Dictionary<string, double>>();
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
                        resultset[continent]["GDP_2012"] = resultset[continent]["GDP_2012"] + GDPToAdd;
                        resultset[continent]["POPULATION_2012"] = resultset[continent]["POPULATION_2012"] + PopulationToAdd;
                    }
                    else
                    {
                        Dictionary<string, double> GDPVsPopulation = new Dictionary<string, double>();
                        GDPVsPopulation.Add("GDP_2012", Convert.ToDouble(rowdata[GDPIndex]));
                        GDPVsPopulation.Add("POPULATION_2012", Convert.ToDouble(rowdata[PopulationIndex]));
                        resultset.Add(continent, GDPVsPopulation);
                    }
                }
                catch { }
            }
            string Json = JsonConvert.SerializeObject(resultset, Formatting.Indented);    
            StreamWriter outputdata = new StreamWriter("../../../../AggregateGDPPopulation/data/outputfilenew.json");
            outputdata.WriteLine(Json);
            outputdata.Close();
        }
    }
}
