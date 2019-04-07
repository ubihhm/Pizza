using System.Threading.Tasks;

namespace Pizza.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string subject, string message);
    }
}