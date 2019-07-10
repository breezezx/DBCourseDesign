using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace No9Gallery.Models.ObjectModel
{
    public class ObjectItem
    {
        public class UsersItem
        {
            public String ID { get; set; }
            public String user_name { get; set; }
            public String password { get; set; }
            public String avatar { get; set; }
        }

        public class AdministratorsItem
        {
            public String administrator_ID { get; set; }
        }

        public class CommonUserItem
        {

            public String user_ID { get; set; }
            public String introduction { get; set; }
            public int points { get; set; }
            public int membership_level;
        }

        public class FollowItem
        {
            public String follower_ID { get; set; }
            public String followed_ID { get; set; }
        }

        public class MessageItem
        {
            public String message_ID { get; set; }
            public String administrator_ID { get; set; }
            public String content { get; set; }
            DateTime time { get; set; }
        }

        public class ReceiveItem
        {
            public String user_ID { get; set; }
            public String message_ID { get; set; }
            public String is_read { get; set; }
        }

        public class PointRecordItem
        {
            public String order_no { get; set; }
            public String user_ID { get; set; }
            public int amount { get; set; }
            public String content { get; set; }
            public DateTime time { get; set; }
        }

        public class WorkItem
        {
            public String work_ID { get; set; }
            public String user_ID { get; set; }
            public String headline { get; set; }
            public String introduction { get; set; }
            public String picture { get; set; }
            public int likes_num { get; set; }
            public int collect_num { get; set; }
            public DateTime upload_time { get; set; }
            public int points_need { get; set; }
        }

        public class DownloadItem
        {
            public string work_ID { set; get; }
            public string user_ID { set; get; }
            public DateTime download_time { set; get; }
        }

        public class ReportItem
        {
            public string work_ID { set; get; }
            public string user_ID { set; get; }
            public DateTime report_time { set; get; }
            public string contents { set; get; }
            public char state { set; get; }
        }

        public class CollectionItem
        {
            public string work_ID { set; get; }
            public string user_ID { set; get; }

        }

        public class LikesItem
        {
            public string work_ID { set; get; }
            public string user_ID { set; get; }
        }

        public class CommentsItem
        {
            public string comment_ID { set; get; }
            public string work_ID { set; get; }
            public string user_ID { set; get; }
            public string content { set; get; }
            public DateTime time { set; get; }
        }

        public class WorktypeItem
        {
            public string typename { set; get; }
            public string work_ID { set; get; }
        }

        public class TypeItem
        {
            public string typename { set; get; }
        }



    }
}
