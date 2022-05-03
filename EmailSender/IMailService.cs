using System.Threading.Tasks;

namespace TheHotel.EmailSender
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
