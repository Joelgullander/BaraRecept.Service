using BaraRecept.Recipe.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BaraRecept.Recipe.Api.Tests
{
    public class HealthCheckControllerTests
    {
        [Fact]
        public void Get_ShouldReturnStatusCode200WithBodyOK()
        {
            var controller = new HealthCheckController();

            var result = controller.Get() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("OK", result.Value);
        }
    }
}
