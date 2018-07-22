using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AggregateGDPPopulation;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AggregateGDPPopulation.Tests
{
    public class AggregateGDPPopulationTesting
    {
        [Fact]
        public void SyncTesting()
        {
            Sync.CaculateGDPPopulation("../../../../AggregateGDPPopulation/data/datafile.csv");
            JObject Actual = JObject.Parse(File.ReadAllText("../../../../AggregateGDPPopulation/data/outputfilenew.json"));
            JObject Expected = JObject.Parse(File.ReadAllText("../../../expected-output.json"));
            Assert.Equal(Expected, Actual);           
        }
        [Fact]
        public async void AsyncTesting()
        {
            Task process1 =  Async.CalculateGDPPopulation("../../../../AggregateGDPPopulation/data/datafile.csv");
            await process1;
            JObject Actual = JObject.Parse(File.ReadAllText("../../../../AggregateGDPPopulation/data/outputfileAsync.json"));
            JObject Expected = JObject.Parse(File.ReadAllText("../../../expected-output.json"));
            Assert.Equal(Expected, Actual);
            Assert.Equal(Expected, Actual);
        }
    }
}
