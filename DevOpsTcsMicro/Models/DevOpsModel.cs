namespace DevOpsTcsMicro.Models
{


    public class DevOpsModel
    {
        public string Message { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public int TimeToLifeSec { get; set; }
    }

    public class DevOpsResponse
    {
        public string Message { get; set; }
    }

    public class TokenRequest
    {
        public string Recipient { get; set; } // Recipient for whom to generate the token
    }
}
