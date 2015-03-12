using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using KevPOS.ValueObjects;
using SendGrid;

namespace FirstOneTo.EmailSender
{
    public class SendGridMailer : IMailer
    {
        private readonly NetworkCredential _networkCredential;

        public SendGridMailer(string userName, string password)
        {
            _networkCredential = new NetworkCredential(userName, password);
        }

        public async Task Send(string toAddress, string fromAddress, string fromName, string subject, Text text)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            try
            {
                myMessage.AddTo(toAddress);
            }
            catch (Exception)
            {

            } myMessage.From = new MailAddress(fromAddress, fromName);
            myMessage.Subject = subject;
            myMessage.Text = text.ToString();
             Send(myMessage);     
        }

        public async Task Send(string toAddress, string fromAddress, string fromName, string subject, Html html)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            try
            {
                myMessage.AddTo(toAddress);
            }
            catch (Exception)
            {

            } myMessage.From = new MailAddress(fromAddress, fromName);
            myMessage.Subject = subject;
            myMessage.Html = html.ToString();
             Send(myMessage);     

        }

        public async Task Send(string toAddress, string fromAddress, string fromName, string subject, Text text, Html html)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            myMessage.Text = text.ToString();
            myMessage.Html = html.ToString();
            try
            {
                myMessage.AddTo(toAddress);
            }
            catch (Exception)
            {
                
            }
            myMessage.From = new MailAddress(fromAddress, fromName);
            myMessage.Subject = subject;
             Send(myMessage);     
        }

        private async Task Send(SendGridMessage message)
        {
            var transportWeb = new Web(_networkCredential);
            // Send the email.
            try
            {
                 transportWeb.DeliverAsync(message);
            }
            catch (Exception)
            {

            }
        }

    }
}