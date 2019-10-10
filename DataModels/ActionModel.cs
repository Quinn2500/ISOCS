using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class ActionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationUser> ResponsibleUsers { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<CommentModel> Comments { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
