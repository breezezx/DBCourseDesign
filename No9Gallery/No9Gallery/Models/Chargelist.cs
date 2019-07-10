using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace No9Gallery.Models
{
    public class Chargelist
    {
        [Display(Name = "Order no")]
        public string order_no { get; set; }
       
        public string user_ID { get; set; }

        [Display(Name = "Amount")]
        public int amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Time")]
        public DateTime time { get; set; }

        [Display(Name = "Content")]
        public string content { get; set; }

    }
}
