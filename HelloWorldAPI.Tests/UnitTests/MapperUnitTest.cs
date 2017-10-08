using System;
using NUnit.Framework;
using HelloWorldUtilities.Mappers;
using HelloWorldUtilities.Models;

namespace HelloWorldAPI.Tests.UnitTests
{
    ///     Unit tests for the Hello World Mapper
    [TestFixture]
    public class MapperUnitTest
    {
        
        ///     The implementation to test
        private HelloWorldMapper helloWorldMapper;

        ///     Initialize the test fixture (runs one time)
        
        [SetUp]
        public void InitTestSuite()
        {
            // Create object to test
            this.helloWorldMapper = new HelloWorldMapper();
        }

        ///     Tests the class's StringToTodaysData method for success with a normal input value
        [Test]
        public void UnitTestHelloWorldMapperStringToTodaysDataNormalSuccess()
        {
            const string Data = "Hello World!";

            // Create the expected result
            var expectedResult = GetSampleTodaysData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToTodaysData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

       
        ///     Tests the StringToTodaysData method for success with a null input value
        
        [Test]
        public void UnitTestHelloWorldMapperStringToTodaysDataNullSuccess()
        {
            const string Data = null;

            // Create the expected result
            var expectedResult = GetSampleTodaysData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToTodaysData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }
        
        ///     Gets a sample TodaysData model
        
        /// Returns A sample TodaysData model
        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData()
            {
                Data = data
            };
        }
    }
}
