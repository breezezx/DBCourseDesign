using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace No9Gallery.Models
{
    public class PersonInfo
    {
        public string ID { get; set; }
        public string VisitID { set; get; }
        public string HeadPortraitURL { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Introduction { get; set; }
        public string membershipLevel { get; set; }
        public string Integral { get; set; }
        public bool IsFollowed { get; set; }
        public int points { get; set; }
        public int Worknum { get; set; }
        public List<string> Workid{ get;set;}
        public List<string> WorkPath { get; set; }
        public List<string> WorkNames { get; set; }
    }
}
