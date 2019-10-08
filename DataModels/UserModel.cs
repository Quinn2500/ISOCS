using System;

namespace DataModels
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Preposition { get; set; }
        public CompanyModel Company { get; set; }
        public Role Role { get; set; }
    }
}
