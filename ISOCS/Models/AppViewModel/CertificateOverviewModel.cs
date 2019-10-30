using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.AppViewModel
{
    public class CertificateOverviewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResponsibleUser { get; set; }
        public bool EnableNotifications { get; set; }
        public List<string> TaskNamesDaily { get; set; }
        public List<string> TaskNamesWeekly { get; set; }
        public List<string> TaskNamesMonthly { get; set; }
        public List<string> TaskNamesQuarterly { get; set; }
        public List<string> TaskNamesYearly { get; set; }
    }
}
