namespace UHP.Infrastructure.Interfaces
{
    public interface IMailService
    {
        public void Send(string subject, string body, string toRecipients, string qrcodeImage);
    }
}