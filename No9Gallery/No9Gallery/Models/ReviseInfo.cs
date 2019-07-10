using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace No9Gallery.Models
{
    public class ReviseInfo
    {
        public string UserID { get; set; }
        public string NewUserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewIntroduction { get; set; }
    }
}
