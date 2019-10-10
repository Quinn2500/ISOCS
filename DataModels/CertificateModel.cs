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
        public List<ActionModel> Actions { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public CompanyModel Company { get; set; }
    }
}
