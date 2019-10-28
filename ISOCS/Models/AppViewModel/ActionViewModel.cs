using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DataModels;

namespace ISOCS.Models.AppViewModel
{
    public class ActionViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public OccurenceEnum Occurence { get; set; }
        public string ResponsibleUser { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableFileUpload { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
