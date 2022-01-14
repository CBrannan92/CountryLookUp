using Iso2CodeLookUp.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {        
        }

        [TestMethod]
        public async Task InvalidIso2Code()
        {
           
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent("[{\"message\":[{\"id\":\"120\",\"key\":\"Invalid value\",\"value\":\"The provided parameter value is not valid\"}]}]") })
                .Verifiable();

            var httpClient = new HttpClient(mockMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            CountryLogic countryLogic = new CountryLogic(mockFactory.Object);
             var countryInfo = await countryLogic.GetCountryInformationByIso2Code("ga");
             Assert.IsNull(countryInfo);
        }

        [TestMethod]
        public async Task ValidIso2Code()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent("[{\"page\":1,\"pages\":1,\"per_page\":\"50\",\"total\":1},[{\"id\":\"BRA\",\"iso2Code\":\"BR\",\"name\":\"Brazil\",\"region\":{\"id\":\"LCN\",\"iso2code\":\"ZJ\",\"value\":\"Latin America & Caribbean \"},\"adminregion\":{\"id\":\"LAC\",\"iso2code\":\"XJ\",\"value\":\"Latin America & Caribbean(excluding high income)\"},\"incomeLevel\":{\"id\":\"UMC\",\"iso2code\":\"XT\",\"value\":\"Upper middle income\"},\"lendingType\":{\"id\":\"IBD\",\"iso2code\":\"XF\",\"value\":\"IBRD\"},\"capitalCity\":\"Brasilia\",\"longitude\":\" - 47.9292\",\"latitude\":\" - 15.7801\"}]]") })
                .Verifiable();

            var httpClient = new HttpClient(mockMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            CountryLogic countryLogic = new CountryLogic(mockFactory.Object);
            var countryInfo = await countryLogic.GetCountryInformationByIso2Code("br");
            Assert.IsNotNull(countryInfo);
        }
    }
}
