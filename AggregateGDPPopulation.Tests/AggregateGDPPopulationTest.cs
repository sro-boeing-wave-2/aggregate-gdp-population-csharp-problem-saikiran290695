using System;
using Xunit;
using AggregateGDPPopulation;
using System.IO;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1.Method("../../../../AggregateGDPPopulation/data/datafile.csv");
            StreamReader sample = new StreamReader("../../../expected-output.json");
            StreamReader sample2 = new StreamReader("../../../../AggregateGDPPopulation/data/outputfilenew.json");
            string Actual = "";
            string Expected = "";
            while (!sample.EndOfStream)
                Expected += sample.ReadLine();
            while (!sample2.EndOfStream)
                Actual += sample2.ReadLine();
            Assert.Equal(Expected, Actual);           
        }
    }
}
