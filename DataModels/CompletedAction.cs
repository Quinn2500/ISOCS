using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class CompletedAction
    {
        public int ActionHistoryId { get; set; }
        public ActionModel Action { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string CompletedByEmail { get; set; }
        public DateTime CompletedOn { get; set; }
        public bool ExecutionSucces { get; set; }
        public DateTime DateToExecute { get; set; }
    }
}
