namespace DevOpsTcsMicro.Services
{
    public interface IApiManagerService

    {
        bool ValidateApiKey(string apiKey);
        string GenerateJwtToken(string recipient);
        bool ValidateJwtToken(string token);
    }
}
