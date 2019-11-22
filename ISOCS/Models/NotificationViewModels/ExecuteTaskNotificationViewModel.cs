using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.NotificationViewModels
{
    public class ExecuteTaskNotificationViewModel
    {
        public bool IsSucces { get; set; }
        public bool ExecutionSucces { get; set; }
        public string ActionName { get; set; }
        public string CertificateName { get; set; }
        public string FirstName { get; set; }

    }
}
