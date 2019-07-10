using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace No9Gallery.Models
{
    public class MessageReceivelist
    {

        public string message_ID;
        public string user_ID;
        [Display(Name = "信息状态")]
        public string is_read { get; set; }

        [Display(Name = "发送日期")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Display(Name = "信息内容")]
        public string content { get; set; }
    }
}
