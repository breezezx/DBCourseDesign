using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using No9Gallery.Models.ObjectModel;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections;

namespace No9Gallery.Services
{

    public interface IRetrieveImages
    {
        Task<List<ObjectItem.WorkItem>> GetWorkItemByIDAsync(String work_ID);
        Task<List<ObjectItem.WorkItem>> GetWorkItemByCategoryAsync(String category, String order);
        Task<List<ObjectItem.WorkItem>> GetWorkItemByRandomSelectionAsync(int num);
        Task<List<ObjectItem.WorkItem>> GetWorkItemInUserCollectionAsync(string user_id);
        Task<List<ObjectItem.WorkItem>> GetWorkItemInUserLikesAsync(string user_id);
    }

    public interface IRetrieveUserInfo
    {

    }

    public class RetrieveImages : IRetrieveImages
    {
        public String connectString = "DATA SOURCE=localhost:1521/orcl1;PASSWORD=QAZPLMtgv123;" +
    "PERSIST SECURITY INFO=True;USER ID=c##eyisheng";

        public Task<List<ObjectItem.WorkItem>> GetWorkItemByCategoryAsync(String category, String order)
        {
            List<ObjectItem.WorkItem> paraWorks = new List<ObjectItem.WorkItem>();

            OracleConnection conn = new OracleConnection(ConString.conString);

            OracleCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.BindByName = true;

            cmd.CommandText = "select work_ID ,user_ID ,headline ,introduction,picture ,likes_num ,collect_num ,upload_time ,points_need " +
                "from work natural join work_type where typename= :category order by " + order + " desc";

            OracleParameter para_category = new OracleParameter("category", category);
            cmd.Parameters.Add(para_category);
            //OracleParameter para_order = new OracleParameter("morder", order);
            //cmd.Parameters.Add(para_order);

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ObjectItem.WorkItem workitem = new ObjectItem.WorkItem
                {
                    work_ID = reader.GetString(0),
                    user_ID = reader.GetString(1),
                    headline = reader.GetString(2),
                    introduction = reader.GetString(3),
                    picture = reader.GetString(4),
                    likes_num = reader.GetInt32(5),
                    collect_num = reader.GetInt32(6),
                    upload_time = reader.GetDateTime(7),
                    points_need = reader.GetInt32(8)
                };
                paraWorks.Add(workitem);
            }
            reader.Close();
            conn.Close();

            return Task.FromResult(paraWorks);
        }

        public Task<List<ObjectItem.WorkItem>> GetWorkItemByIDAsync(string name)
        {
            List<ObjectItem.WorkItem> paraWorks = new List<ObjectItem.WorkItem>();

            if (name is null)
                return Task.FromResult(paraWorks);

            OracleConnection conn = new OracleConnection(ConString.conString);

            OracleCommand cmd = conn.CreateCommand();


            conn.Open();
            cmd.BindByName = true;

            cmd.CommandText = "select * from work where headline like '%' || :para_headline || '%'";
            OracleParameter para = new OracleParameter("para_headline", name);
            cmd.Parameters.Add(para);

            OracleDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                ObjectItem.WorkItem workitem = new ObjectItem.WorkItem
                {
                    work_ID = reader.GetString(0),
                    user_ID = reader.GetString(1),
                    headline = reader.GetString(2),
                    introduction = reader.GetString(3),
                    picture = reader.GetString(4),
                    likes_num = reader.GetInt32(5),
                    collect_num = reader.GetInt32(6),
                    upload_time = reader.GetDateTime(7),
                    points_need = reader.GetInt32(8)
                };
                paraWorks.Add(workitem);
            }
            reader.Close();
            conn.Close();

            return Task.FromResult(paraWorks);
        }

        public Task<List<ObjectItem.WorkItem>> GetWorkItemByRandomSelectionAsync(int num)
        {

            List<ObjectItem.WorkItem> paraWorks = new List<ObjectItem.WorkItem>();

            OracleConnection conn = new OracleConnection(ConString.conString);

            OracleCommand cmd = conn.CreateCommand();


            conn.Open();
            cmd.BindByName = true;

            cmd.CommandText = "select * from work ";
            //OracleParameter para = new OracleParameter("category", category);
            //cmd.Parameters.Add(para);

            OracleDataReader reader = cmd.ExecuteReader();

            int count = 0;
            DateTime dt = DateTime.Now;
            dt.AddYears(-1);
            while (reader.Read() && count < num)
            {
                //Random rand = new Random();
                //if (rand.Next(1, 100) < 50)
                //{
                

                if (DateTime.Compare(dt, reader.GetDateTime(7)) > 0)
                {

                    ObjectItem.WorkItem workitem = new ObjectItem.WorkItem
                    {
                        work_ID = reader.GetString(0),
                        user_ID = reader.GetString(1),
                        headline = reader.GetString(2),
                        introduction = reader.GetString(3),
                        picture = reader.GetString(4),
                        likes_num = reader.GetInt32(5),
                        collect_num = reader.GetInt32(6),
                        upload_time = reader.GetDateTime(7),
                        points_need = reader.GetInt32(8)
                    };
                    count++;
                    paraWorks.Add(workitem);

                }
                //}
            }

            reader.Close();
            conn.Close();

            return Task.FromResult(paraWorks);
        }

        public Task<List<ObjectItem.WorkItem>> GetWorkItemInUserCollectionAsync(string user_id)
        {
            List<ObjectItem.WorkItem> paraWorks = new List<ObjectItem.WorkItem>();

            OracleConnection conn = new OracleConnection(ConString.conString);

            OracleCommand cmd = conn.CreateCommand();


            conn.Open();
            cmd.BindByName = true;

            cmd.CommandText = "select work_ID ,collection.user_ID ,headline ,introduction,picture ,likes_num ," +
                "collect_num ,upload_time ,points_need" +
                " from collection join work using (work_ID) where collection.user_ID = :userID ";
            OracleParameter para = new OracleParameter("userID", user_id);
            cmd.Parameters.Add(para);

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ObjectItem.WorkItem workitem = new ObjectItem.WorkItem
                {
                    work_ID = reader.GetString(0),
                    user_ID = reader.GetString(1),
                    headline = reader.GetString(2),
                    introduction = reader.GetString(3),
                    picture = reader.GetString(4),
                    likes_num = reader.GetInt32(5),
                    collect_num = reader.GetInt32(6),
                    upload_time = reader.GetDateTime(7),
                    points_need = reader.GetInt32(8)
                };
                paraWorks.Add(workitem);

            }

            reader.Close();
            conn.Close();

            return Task.FromResult(paraWorks);
        }

        public Task<List<ObjectItem.WorkItem>> GetWorkItemInUserLikesAsync(string user_id)
        {
            List<ObjectItem.WorkItem> paraWorks = new List<ObjectItem.WorkItem>();

            OracleConnection conn = new OracleConnection(ConString.conString);

            OracleCommand cmd = conn.CreateCommand();


            conn.Open();
            cmd.BindByName = true;

            cmd.CommandText = "select work_ID ,likes.user_ID ,headline ,introduction,picture ,likes_num ," +
                "collect_num ,upload_time ,points_need" +
                " from likes join work using(work_ID) where likes.user_ID = :userID ";
            OracleParameter para = new OracleParameter("userID", user_id);
            cmd.Parameters.Add(para);

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ObjectItem.WorkItem workitem = new ObjectItem.WorkItem
                {
                    work_ID = reader.GetString(0),
                    user_ID = reader.GetString(1),
                    headline = reader.GetString(2),
                    introduction = reader.GetString(3),
                    picture = reader.GetString(4),
                    likes_num = reader.GetInt32(5),
                    collect_num = reader.GetInt32(6),
                    upload_time = reader.GetDateTime(7),
                    points_need = reader.GetInt32(8)
                };
                paraWorks.Add(workitem);

            }

            reader.Close();
            conn.Close();

            return Task.FromResult(paraWorks);
        }
    }

}
