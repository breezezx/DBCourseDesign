using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace No9Gallery.Models
{
    public class MessageList
    {
        [Display(Name = "Work ID")]
        public string works_ID { get; set; }
        [Display(Name = "User ID")]
        public string user_ID { get; set; }
        [Display(Name = "Report Time")]
        public DateTime report_time { get; set; }
        [Display(Name = "Report Content")]
        public string report_content { get; set; }
        [Display(Name = "State")]
        public string state { get; set; }
    }
}
