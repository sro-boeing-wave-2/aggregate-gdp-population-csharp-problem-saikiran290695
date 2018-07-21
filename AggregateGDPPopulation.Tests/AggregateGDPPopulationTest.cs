using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AggregateGDPPopulation;
using System.IO;
using AggregateGDPPopulationAsync;
using System.Threading.Tasks;
using Xunit;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1.Method("../../../../AggregateGDPPopulation/data/datafile.csv");
            JObject Actual = JObject.Parse(File.ReadAllText("../../../../AggregateGDPPopulation/data/outputfilenew.json"));
            JObject Expected = JObject.Parse(File.ReadAllText("../../../expected-output.json"));
            Assert.Equal(Expected, Actual);           
        }
        [Fact]
        public async void Test2()
        {
            Task process1 =  Class2.Process("../../../../AggregateGDPPopulation/data/datafile.csv");
            await process1;
            JObject Actual = JObject.Parse(File.ReadAllText("../../../../AggregateGDPPopulation/data/outputfileSync.json"));
            JObject Expected = JObject.Parse(File.ReadAllText("../../../expected-output.json"));
            Assert.Equal(Expected, Actual);
            Assert.Equal(Expected, Actual);
        }
    }
}
