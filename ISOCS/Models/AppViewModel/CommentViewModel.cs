using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.AppViewModel
{
    public class CommentViewModel
    {
        public string Contents { get; set; }
        public string OwnerFullName { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
