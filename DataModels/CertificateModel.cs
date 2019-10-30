using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class CertificateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserEmail { get; set; }
        public List<ActionModel> Actions { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CompanyName { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
