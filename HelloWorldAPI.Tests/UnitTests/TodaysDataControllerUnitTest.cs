using System;
using NUnit.Framework;
using Moq;
using HelloWorldUtilities.Services;
using HelloWorldAPI.Controllers;
using HelloWorldUtilities.Models;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class TodaysDataControllerUnitTest
    {
        ///     The mocked data service
        
        private Mock<IDataService> dataServiceMock;

        ///     The implementation to test
        
        private TodaysDataController todaysDataController;

        ///     Initialize the test fixture (runs one time)
        
        [SetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.dataServiceMock = new Mock<IDataService>();

            // Create object to test
            this.todaysDataController = new TodaysDataController(this.dataServiceMock.Object);
        }

        ///     Tests the controller's get method for success
        
        [Test]
        public void UnitTestTodaysDataControllerGetSuccess()
        {
            // Create the expected result
            var expectedResult = GetSampleTodaysData();

            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetTodaysData()).Returns(expectedResult);

            // Call the method to test
            var result = this.todaysDataController.Get();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        ///     Gets a sample TodaysData model
        /// Returns A sample TodaysData model
        private static TodaysData GetSampleTodaysData()
        {
            return new TodaysData()
            {
                Data = "Hello World!"
            };
        }
       
    }
}
