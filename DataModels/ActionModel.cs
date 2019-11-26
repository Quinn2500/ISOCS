using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class ActionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserEmail { get; set; }
        public DateTime StartDate { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public OccurenceEnum Occurence { get; set; }
        public bool EnableNotifications { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableFileUpload { get; set; }
        public string CertificateName { get; set; }
    }
}
