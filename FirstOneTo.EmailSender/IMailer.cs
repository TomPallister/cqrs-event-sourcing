using System.Threading.Tasks;
using KevPOS.ValueObjects;

namespace FirstOneTo.EmailSender
{
    public interface IMailer
    {
        Task Send(string toAddress, string fromAddress, string fromName, string subject, Text text);
        Task Send(string toAddress, string fromAddress, string fromName, string subject, Html html);
        Task Send(string toAddress, string fromAddress, string fromName, string subject, Text text, Html html);
    }
}