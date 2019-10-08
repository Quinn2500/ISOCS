using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public UserModel Owner { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
