using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class ActionToComplete
    {
        public ActionModel Action { get; set; }
        public DateTime DateToExecute { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
