namespace Application.Services.Abs
{
    public interface IEmailSender
    {
        public void SendEmail(string emailTo);
        public void SendEmail(string emailTo, string title, string htmlContent);
    }
}
