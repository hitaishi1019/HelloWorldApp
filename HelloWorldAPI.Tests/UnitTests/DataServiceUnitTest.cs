using System;
using NUnit.Framework;
using HelloWorldUtilities.Services;
using Moq;
using HelloWorldUtilities.Wrappers;
using HelloWorldUtilities.Mappers;
using HelloWorldUtilities.Resources;
using System.Configuration;
using System.IO;
using HelloWorldUtilities.Models;

namespace HelloWorldAPI.Tests.UnitTests
{
    [TestFixture]
    public class DataServiceUnitTest
    {
        
        ///     The mocked application settings service
        private Mock<IAppSettings> appSettingsMock;

        ///     The mocked DateTime wrapper
        private Mock<IDateTime> dateTimeWrapperMock;

        ///     The mocked File IO service
        private Mock<IFileIOService> fileIOServiceMock;

        ///     The mocked Hello World Mapper
        private Mock<IHelloWorldMapper> helloWorldMapperMock;

        ///     The implementation to test
        private HelloWorldDataService helloWorldDataService;

        ///     Initialize the test fixture (runs one time)
        
        [SetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.dateTimeWrapperMock = new Mock<IDateTime>();
            this.fileIOServiceMock = new Mock<IFileIOService>();
            this.helloWorldMapperMock = new Mock<IHelloWorldMapper>();

            // Create object to test
            this.helloWorldDataService = new HelloWorldDataService(
                this.appSettingsMock.Object,
                this.dateTimeWrapperMock.Object,
                this.fileIOServiceMock.Object,
                this.helloWorldMapperMock.Object);
        }

        ///     Tests the class's GetTodaysData method for success
        
        [Test]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSuccess()
        {
            // Create return models for dependencies
            const string DataFilePath = "some/path";
            const string FileContents = "Hello World!";
            var nowDate = DateTime.Now;
            var rawData = FileContents + " as of " + nowDate.ToString("F");

            // Create the expected result
            var expectedResult = GetSampleTodaysData(rawData);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.TodayDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Returns(FileContents);
            this.dateTimeWrapperMock.Setup(m => m.Now()).Returns(nowDate);
            this.helloWorldMapperMock.Setup(m => m.StringToTodaysData(rawData)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldDataService.GetTodaysData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        ///     Tests the class's GetTodaysData method for an IO Exception
        //[Test]
        //public void UnitTestHelloWorldDataServiceGetTodaysDataIOException()
        //{
        //    // Create return models for dependencies
        //    const string DataFilePath = "some/path";

        //    // Set up dependencies
        //    this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.TodayDataFileKey)).Returns(DataFilePath);

        //    Assert.Throws<IOException>(() => this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)));
        //    // Call the method to test
        //    this.helloWorldDataService.GetTodaysData();
        //}
        
        ///     Gets a sample TodaysData model
        
        /// Returns A sample TodaysData model
        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData { Data = data };
        }
        
    }
}
