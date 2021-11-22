using JSC_LMS.Application.Models.Mail;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
        bool SendSmtpEmail(string fromEmail, string toEmail, string password, string subject, string body, string host, string port);
    }
}
