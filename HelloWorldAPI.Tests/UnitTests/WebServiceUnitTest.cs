using System;
using NUnit.Framework;
using System.Collections.Generic;
using HelloWorldUtilities.Services;
using Moq;
using HelloWorldApp.Services;
using RestSharp;
using HelloWorldUtilities.Wrappers;
using HelloWorldUtilities.Models;
using HelloWorldUtilities.Resources;
using System.Net;

namespace HelloWorldAPI.Tests.UnitTests
{
    ///     Unit tests for the Hello World Console App
    [TestFixture]
    public class WebServiceUnitTest
    {
        ///     The list of log messages set by calling classes
        private List<string> logMessageList;

        ///     The list of exceptions set by calling classes
        private List<Exception> exceptionList;

        ///     The list of other properties set by calling classes
        private List<object> otherPropertiesList;

        ///     The mocked application settings service
        private Mock<IAppSettings> appSettingsMock;

        ///     The test logger
        private ILogger testLogger;

        ///     The mocked Rest client
        private Mock<IRestClient> restClientMock;

        ///     The mocked Rest request
        private Mock<IRestRequest> restRequestMock;

        ///     The mocked wrapped Uri service
        private Mock<IUri> uriServiceMock;

        ///     The implementation to test
        private HelloWorldService helleHelloWorldWebService;

        ///     Initialize the test fixture (runs one time)
        [SetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);
            this.restClientMock = new Mock<IRestClient>();
            this.restRequestMock = new Mock<IRestRequest>();
            this.uriServiceMock = new Mock<IUri>();

            // Create object to test
            this.helleHelloWorldWebService = new HelloWorldService(
                this.restClientMock.Object,
                this.restRequestMock.Object,
                this.appSettingsMock.Object,
                this.uriServiceMock.Object,
                this.testLogger);
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

        
        ///     Tests the class's GetTodaysData method for success when normal data was found
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            // Create return models for dependencies
            const string Data = "Hello World!";
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            var todaysData = GetSampleTodaysData(Data);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.NotNull(response);
            Assert.AreEqual(response.Data, todaysData.Data);
        }

        ///     Tests the class's GetTodaysData method for success when there is a null response
        
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullResponse()
        {
            // Create return models for dependencies
            const string Data = "Hello World!";
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = (IRestResponse<TodaysData>)null;
            var todaysData = GetSampleTodaysData(Data);
            const string ErrorMessage = "Did not get any response from the Hello World Web Api for the Method: GET /todaysdata";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], ErrorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ErrorMessage);
        }

        ///     Tests the class's GetTodaysData method for success when there is null data in the response
       
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullData()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            TodaysData todaysData = null;
            const string ErrorMessage = "Error Message";
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            var errorException = new Exception("errorHere");
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, errorException.Message);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for success when there is null data in the response and a null error message
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullDataNullErrorMessage()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            TodaysData todaysData = null;
            const string ErrorMessage = null;
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            var errorException = new Exception("errorHere");
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ProfileContent);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for success when there is null data in the response and a null error exception
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullDataNullErrorException()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            TodaysData todaysData = null;
            const string ErrorMessage = "Error Message";
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            Exception errorException = null;
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKey.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ProfileContent);
        }
        
        ///     Gets a sample TodaysData model
        
        /// Returns A sample TodaysData model
        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData { Data = data };
        }
        
    }
}
