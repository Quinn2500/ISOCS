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
        public DateTime BeginDateTime { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public OccurenceEnum Occurence { get; set; }
        public bool EnableNotifications { get; set; }
        public string CertificateName { get; set; }
    }
}
