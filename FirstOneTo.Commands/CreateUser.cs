using KevPOS.Messages;

namespace FirstOneTo.Commands
{
    public class CreateUser : AbstractCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}