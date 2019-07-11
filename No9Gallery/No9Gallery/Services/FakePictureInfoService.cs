using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;
using No9Gallery.Models;
using Microsoft.AspNetCore.Mvc;


namespace No9Gallery.Services
{

    public class FakePictureInfoService : IPictureInfoService
    {

        public List<WorkItem> GetAll()
        {
            List<WorkItem> getitem = new List<WorkItem>();

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from work";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            var item = new WorkItem
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
                            getitem.Add(item);
                        }

                        reader.Dispose();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                    }

                }

            }

            return getitem;
        }

        public List<WorkItem> GetPictureInfo(String getwork_ID)
        {
            List<WorkItem> getitem = new List<WorkItem>();

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from work natural join users where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            var item = new WorkItem
                            {
                                work_ID = reader.GetString(1),
                                user_ID = reader.GetString(0),
                                headline = reader.GetString(2),
                                introduction = reader.GetString(3),
                                picture = reader.GetString(4),
                                likes_num = reader.GetInt32(5),
                                collect_num = reader.GetInt32(6),
                                upload_time = reader.GetDateTime(7),
                                points_need = reader.GetInt32(8),
                                user_name = reader.GetString(9)

                            };
                            getitem.Add(item);
                        }

                        reader.Dispose();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                    }
                }
            }
            return getitem;
        }

        public bool ifLiked(String getuser_ID, String getwork_ID)
        {

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select user_ID from likes where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            if (getuser_ID == reader.GetString(0))
                            {
                                return true;
                            }
                        }

                        reader.Dispose();
                        con.Close();
                        return false;

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }
        }

        public bool AddLikes(String getwork_ID, String getUser_ID)
        {

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "insert into likes values(" +
                            "'" + getwork_ID + "'" + "," +
                            "'" + getUser_ID + "'" + ")";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "update work set likes_num=likes_num+1 where work_id='" + getwork_ID + "'";
                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }
        public bool ifCollected(String getuser_ID, String getwork_ID)
        {



            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select user_ID from collection where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            if (getuser_ID == reader.GetString(0))
                            {
                                return true;
                            }
                        }

                        reader.Dispose();
                        con.Close();
                        return false;

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }

        public bool AddCollections(string getwork_ID, string getUser_ID)
        {
        

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {

                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "insert into collection values(" +
                            "'" + getwork_ID + "'" + "," +
                            "'" + getUser_ID + "'" + ")";
                        cmd.ExecuteNonQueryAsync();
                        cmd.CommandText = "update work set collect_num=collect_num+1 where work_id='" + getwork_ID + "'";
                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }

        public bool ifEnoughPoints(String getwork_ID, String getuser_ID)
        {

            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select points_need from work where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        int getpoints_need = reader.GetInt32(0);
                       
                        cmd.CommandText = "select membership_level from common_user where user_ID=" + "'" + getuser_ID + "'";
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        int level = reader.GetInt32(0);

                        cmd.CommandText = "select points from common_user where user_ID=" + "'" + getuser_ID + "'";
                        reader = cmd.ExecuteReader();
                        reader.Read();

                        if(level==0)
                        {
                            if (reader.GetInt32(0) >= getpoints_need)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if(level==1)
                        {
                            if (reader.GetInt32(0) >= getpoints_need*0.9)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if(level==2)
                        {
                            if (reader.GetInt32(0) >= getpoints_need*0.8)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (level == 3)
                        {
                            if (reader.GetInt32(0) >= getpoints_need * 0.7)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (level == 4)
                        {
                            if (reader.GetInt32(0) >= getpoints_need * 0.6)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (level == 5)
                        {
                            if (reader.GetInt32(0) >= getpoints_need * 0.5)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;

                        }
                            

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }

        public bool DecreasePoints(String getwork_ID, String getuser_ID)
        {

            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
            int getpoints_need = 0;
            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select points_need from work where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        reader.Read();

                        getpoints_need = reader.GetInt32(0);

                        cmd.CommandText = "select membership_level from common_user where user_ID=" + "'" + getuser_ID + "'";
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        int level = reader.GetInt32(0);

                        if (level == 0)
                        {
                            cmd.CommandText = " update common_user u set u.points = u.points -" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'";
                        }
                        else if (level == 1)
                        {
                            cmd.CommandText = " update common_user u set u.points = u.points -0.9*" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'";
                        }
                        else if (level == 2)
                        {
                            cmd.CommandText = " update common_user u set u.points = u.points -0.8*" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'";
                        }
                        else if (level == 3)
                        {
                            cmd.CommandText = " update common_user u set u.points = u.points -0.7*" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'";
                        }
                        else if (level == 4)
                        {
                            cmd.CommandText = " update common_user u set u.points = u.points -0.6*" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'";
                        }
                        else if (level == 5)
                        {
                            cmd.CommandText = " update common_user u set u.points = u.points -0.5*" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'";
                        }



                        cmd.ExecuteNonQueryAsync();


                        var randomnum = new Random();
                        var order_no = DateTime.Now.ToString("yyyyMMddHHmmss") + randomnum.Next(0, 1000).ToString();
                        cmd.CommandText = "INSERT INTO POINT_RECORD VALUES ('" + order_no + "','" + getuser_ID + "'," + 
                            getpoints_need * (10 - level) / 10 + ", '" + "Consume " + (getpoints_need * (10 - level) / 10).ToString() +
                            "',to_date('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS'))";

                        cmd.ExecuteNonQueryAsync();

                        con.Close();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }
        public bool AddReport(String getwork_ID, String getuser_ID)
        {

            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                         cmd.CommandText = "insert into report values(" +
                           "'" + getwork_ID + "'" + "," +
                            "'" + getuser_ID + "'" + ",to_date('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS') , 'User Report', " + 0 + ")";



                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }


        }

        public bool FollowAction(String getuser_ID, String getwork_ID)
        {

            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
            String getfollowed_userID = "000";

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select user_ID from work where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            getfollowed_userID = reader.GetString(0);
                        }

                        cmd.CommandText = "insert into follow values(" + "'" + getuser_ID + "'" + "," + "'" + getfollowed_userID + "'" + ")";

                        cmd.ExecuteNonQueryAsync();
                        con.Close();

                        return true;



                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }

        public bool ifFollowed(String getuser_ID, String getwork_ID)
        {

            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";


            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {

                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select user_ID from work where work_ID='" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();
                        reader.Read();

                        String getfollowed_userID = reader.GetString(0);


                        cmd.CommandText = "select followed_ID  from follow where follower_ID='" + getuser_ID + "'";

                        reader = cmd.ExecuteReader();
                        for (int i = 0; reader.Read() != false; i++)
                        {
                            if (getfollowed_userID == reader.GetString(0))
                            {
                                return true;
                            }
                        }
                        reader.Dispose();
                        con.Close();
                        return false;

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }

        }

        public List<Comment> GetCommentInfo(String getwork_ID)
        {
            List<Comment> getitem = new List<Comment>();
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from comments natural join users where work_ID=" + "'" + getwork_ID + "' order by time desc" ;

                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            var item = new Comment
                            {
                                commenterid = reader.GetString(0),
                                words = reader.GetString(3),
                                time = reader.GetDateTime(4),
                                avatar = reader.GetString(8),
                                name = reader.GetString(5)
                            };

                            getitem.Add(item);
                        }

                        reader.Dispose();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                    }
                }
            }
            return getitem;
        }

        public String GetHead(String getwork_ID)
        {

            

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {

                    try
                    {
                        con.Open();

                        cmd.BindByName = true;
                        cmd.CommandText = "select avatar from work natural join users where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.Read() != false)
                        {
                            return reader.GetString(0);
                        }
                        else
                        {
                            return "Default.png";
                        }

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return "Default.png";
                    }

                }

            }


        }

        public List<String> GetTypes(String getwork_ID)
        {
            List<String> getitem = new List<String>();
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select typename from work_type where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            var item = reader.GetString(0);
                            getitem.Add(item);
                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;

                    }
                }
            }
            return getitem;
        }

        public bool AddComment(String getuser_ID, String getwork_ID, String words)
        {

            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        Random random = new Random();
                        var randomnum = new Random();
                        var comment_no = DateTime.Now.ToString("yyyyMMddHHmmss") + "_+_" + randomnum.Next(0, 1000).ToString();
                        var order_no = DateTime.Now.ToString("yyyyMMddHHmmss") + "_+_" + randomnum.Next(0, 1000).ToString();

                         cmd.CommandText = "insert into comments values(" +
                            "'" + order_no + "'" + "," +
                        "'" + getwork_ID + "'" + "," +
                            "'" + getuser_ID + "'" + "," +
                            "'" + words + "'" + ",to_date('"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"','YYYY-MM-DD HH24:MI:SS'))";


                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return false;
                    }
                }
            }
            
        }


        public void Delete(string workId, string authorId, bool isAdmin, string AdminId)
        {
            using (OracleConnection con = new OracleConnection(ConString.conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "delete from collection where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "delete from comments where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "delete from download where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "delete from likes where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "delete from report where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "delete from work_type where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "delete from work where work_id = '" + workId + "'";
                        cmd.ExecuteNonQueryAsync();



                        if (isAdmin)
                        {
                            var randomnum = new Random();
                            var messageID = DateTime.Now.ToString("yyyyMMddHHmmss") + "_+_" + randomnum.Next(0, 1000).ToString();

                            cmd.CommandText = "INSERT INTO MESSAGE VALUES('" + messageID + "','" + AdminId + "','" + "Your work had been deleted" + "',to_date('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS'))";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "INSERT INTO RECEIVE VALUES('" + authorId + "','" + messageID + "'," + 0 + ")";
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;

                    }
                }
            }
        }

    }

}





