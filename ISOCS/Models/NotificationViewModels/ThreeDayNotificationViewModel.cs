using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.NotificationViewModels
{
    public class ThreeDayNotificationViewModel
    {
        public string ActionName { get; set; }
        public string CertificateName { get; set; }
        public string FirstName { get; set; }
        public string Description { get; set; }
        public DateTime DateToExecute { get; set; }
    }
}
