using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;

namespace ISOCS.Models.AppViewModel
{
    public class ActionOverviewViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserEmail { get; set; }
        public DateTime DateToExecute { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public OccurenceEnum Occurence { get; set; }
        public string CertificateName { get; set; }
    }
}
