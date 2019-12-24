using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
