namespace UHP.Infrastructure.Configurations.Settings
{
    public class EmailConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
    }
}