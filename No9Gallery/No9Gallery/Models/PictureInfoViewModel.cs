using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace No9Gallery.Models
{
    public class PictureInfoViewModel
    {
        public List<WorkItem> Items { set; get; }
        public List<CommentsNeededItem> Comments { set; get; }
        public List<LikesItem> Likes { set; get; }
        public List<Work_typeItem> Work_Types { set; get; }
        public List<String> Types { set; get; }
    }
}
