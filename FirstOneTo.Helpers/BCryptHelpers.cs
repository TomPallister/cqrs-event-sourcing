using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOneTo.Helpers
{
    public static class BCryptHelpers
    {
        public static string SetPassword(string userPassword)
        {
            string pwdToHash = userPassword + "^Y8~JJ"; // ^Y8~JJ is my hard-coded salt
            return BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public static bool DoesPasswordMatch(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword + "^Y8~JJ", hashedPwdFromDatabase);
        }
    }
}
