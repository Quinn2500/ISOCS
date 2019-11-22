using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.NotificationViewModels
{
    public class ExecuteTaskMailViewModel
    {
        public string FirstName { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CertificateName { get; set; }
        public string Token { get; set; }
        public int ActionId { get; set; }
    }
}
