using Xunit;
using Moq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using DevOpsTcsMicro.Services;

namespace DevOpsTcsMicro.Tests
{
    public class ApiManagerServiceTests
    {
        [Fact]
        public void ValidateApiKey_ReturnsTrue_WhenApiKeyIsValid()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["ApiKey"]).Returns("test");
            var apiManagerService = new ApiManagerService(configurationMock.Object);
            var apiKey = "test";

            // Act
            var result = apiManagerService.ValidateApiKey(apiKey);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateApiKey_ReturnsFalse_WhenApiKeyIsInvalid()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["ApiKey"]).Returns("test");
            var apiManagerService = new ApiManagerService(configurationMock.Object);
            var apiKey = "invalid";

            // Act
            var result = apiManagerService.ValidateApiKey(apiKey);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GenerateJwtToken_ReturnsToken_WhenRecipientIsValid()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["SecretKey"]).Returns("secret");
            var apiManagerService = new ApiManagerService(configurationMock.Object);
            var recipient = "test";

            // Act
            var token = apiManagerService.GenerateJwtToken(recipient);

            // Assert
            Assert.NotNull(token);
        }

        [Fact]
        public void ValidateJwtToken_ReturnsTrue_WhenTokenIsValid()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["SecretKey"]).Returns("secret");
            var apiManagerService = new ApiManagerService(configurationMock.Object);
            var token = apiManagerService.GenerateJwtToken("test");

            // Act
            var result = apiManagerService.ValidateJwtToken(token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateJwtToken_ReturnsFalse_WhenTokenIsInvalid()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["SecretKey"]).Returns("secret");
            var apiManagerService = new ApiManagerService(configurationMock.Object);
            var token = "invalid";

            // Act
            var result = apiManagerService.ValidateJwtToken(token);

            // Assert
            Assert.False(result);
        }
    }
}