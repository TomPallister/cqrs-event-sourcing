using System.Threading.Tasks;
using KevPOS.ValueObjects;
using NUnit.Framework;
using Text = KevPOS.ValueObjects.Text;

namespace FirstOneTo.EmailSender.Tests
{
    [TestFixture]
    public class SendGridMailerTests
    {
        [Test]
        public async Task can_send_html_mail()
        {
            const string html =
                "<html><head><title>FirstOneTo</title></head><body style=\"background-color: #3276b1;\"><div>Welcome To First One To</div></body></html>";
            IMailer mailer = new SendGridMailer("asdf@azure.com", "xxxx");
             mailer.Send("thomasgardham@googlemail.com", "donotreply@firstoneto.com", "FirstOneTo",
                "Welcome to FirstOneTo",
                new Html(html));
        }

        [Test]
        public async Task can_send_text_mail()
        {
            IMailer mailer = new SendGridMailer("asdf@azure.com", "xxx");
             mailer.Send("thomasgardham@googlemail.com", "donotreply@firstoneto.com", "FirstOneTo",
                "Welcome to FirstOneTo",
                new Text("Thanks for joining our game!")); 
        }
    }
}