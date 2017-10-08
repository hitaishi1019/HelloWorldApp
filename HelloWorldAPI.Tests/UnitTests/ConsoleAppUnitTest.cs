using System;
using NUnit.Framework;
using System.Collections.Generic;
using HelloWorldApp.Services;
using Moq;
using HelloWorldApp.Application;
using HelloWorldUtilities.Services;
using HelloWorldUtilities.Models;

namespace HelloWorldAPI.Tests.UnitTests
{
    ///     Unit tests for the Hello World Console App
    [TestFixture]
    public class ConsoleAppUnitTest
    {
        ///     The list of log messages set by calling classes
        private List<string> logMessageList;

        ///     The list of exceptions set by calling classes
        private List<Exception> exceptionList;

        ///     The list of other properties set by calling classes
        private List<object> otherPropertiesList;
        ///     The mocked Hello World Web Service
        private Mock<IHelloWorldService> helloWorldWebServiceMock;

        ///     The test logger
        private ILogger testLogger;

        ///     The implementation to test
        private HelloWorldConsole helloWorldConsoleApp;

        ///     Initialize the test fixture (runs one time)

        [SetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.helloWorldWebServiceMock = new Mock<IHelloWorldService>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);

            // Create object to test
            this.helloWorldConsoleApp = new HelloWorldConsole(this.helloWorldWebServiceMock.Object, this.testLogger);
        }

        ///     Test tear down. (runs after each test)

        [TearDown]
        public void TearDown()
        {
            // Clear lists
            this.logMessageList.Clear();
            this.exceptionList.Clear();
            this.otherPropertiesList.Clear();
        }

        ///     Tests the class's Run method for success when normal data was found

        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            const string Data = "Hello World!";

            // Create return models for dependencies
            var todaysData = GetSampleTodaysData(Data);

            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetTodaysData()).Returns(todaysData);

            // Call the method to test
            this.helloWorldConsoleApp.Display(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], Data);
        }

        ///     Tests the class's Run method for success when null data was found
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNullDataSuccess()
        {
            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetTodaysData()).Returns((TodaysData)null);

            // Call the method to test
            this.helloWorldConsoleApp.Display(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], "No data was found!");
        }




        ///     Gets a sample TodaysData model

        /// Returns A sample TodaysData model
        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData { Data = data };
        }

    }
}
