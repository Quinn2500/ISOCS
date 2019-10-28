using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.AppViewModel
{
    public class AllActionsPartialViewModel
    {
        public List<string> TasksDaily { get; set; }
        public List<string> TasksWeekly { get; set; }
        public List<string> TasksMonthly { get; set; }
        public List<string> TasksQuarterly { get; set; }
        public List<string> TasksYearly { get; set; }
    }
}
