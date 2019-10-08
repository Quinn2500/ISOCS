using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    class CompletedAction
    {
        public ActionModel Action { get; set; }
        public UserModel CompletedBy { get; set; }
        public DateTime CompletedOn { get; set; }
    }
}
