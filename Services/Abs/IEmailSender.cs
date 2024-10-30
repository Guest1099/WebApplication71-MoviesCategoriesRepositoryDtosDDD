using System.Threading.Tasks;

namespace Application.Services.Abs
{
    public interface IEmailSender
    {
        public void SendEmail(string email);
        public void SendEmail(string email, string title, string description);
    }
}
