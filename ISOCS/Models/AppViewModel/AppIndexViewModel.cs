using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;

namespace ISOCS.Models.AppViewModel
{
    public class AppIndexViewModel
    {
        public string FirstName { get; set; }
        public List<ActionToComplete>ActionsToComplete { get; set; }
    }
}
