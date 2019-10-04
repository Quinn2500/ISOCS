using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public static class Security
    {
        public static string HashPassword(string unhashedpassword)
        {
            int costParameter = 12;
            return BCrypt.Net.BCrypt.HashPassword(unhashedpassword, costParameter);
        }

        public static bool CheckPassword(string hashedpassword, string unhashedpassword)
        {
            return BCrypt.Net.BCrypt.Verify(unhashedpassword, hashedpassword);
        }
    }
}
