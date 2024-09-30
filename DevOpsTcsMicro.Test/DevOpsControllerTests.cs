using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using DevOpsTcsMicro.Controllers;
using DevOpsTcsMicro.Models;
using DevOpsTcsMicro.Services;

namespace DevOpsTcsMicro.Tests
{
    public class DevOpsControllerTests
    {
        [Fact]
        public async Task GenerateToken_ReturnsOkResult()
        {
            // Arrange
            var apiManagerMock = new Mock<IApiManagerService>();
            apiManagerMock.Setup(x => x.GenerateJwtToken(It.IsAny<string>())).Returns("token");
            var controller = new DevOpsController(apiManagerMock.Object);

            // Act
            var result =  controller.GenerateToken(new TokenRequest { Recipient = "test" });

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


 

        [Fact]
        public async Task Post_ReturnsUnauthorizedResult_WhenJwtTokenIsInvalid()
        {
            // Arrange
            var apiManagerMock = new Mock<IApiManagerService>();
            apiManagerMock.Setup(x => x.ValidateApiKey(It.IsAny<string>())).Returns(true);
            apiManagerMock.Setup(x => x.ValidateJwtToken(It.IsAny<string>())).Returns(false);
            var controller = new DevOpsController(apiManagerMock.Object);

            // Act
            var result =  controller.Post(new DevOpsModel { To = "test", From = "test", TimeToLifeSec = 45 });

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}