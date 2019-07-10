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

        public Task<List<WorkItem>> GetAll()
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

            return Task.FromResult(getitem);
        }

        public Task<List<WorkItem>> GetPictureInfo(String getwork_ID)
        {
            List<WorkItem> getitem = new List<WorkItem>();
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
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
                                user_name= reader.GetString(9)

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
            return Task.FromResult(getitem);
        }
        public Task<bool> ifLiked(String getuser_ID, String getwork_ID)
        {
           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
           

            using (OracleConnection con = new OracleConnection(conString))
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
                                return Task.FromResult(false);
                            }
                        }
                       
                        reader.Dispose();
                        con.Close();
                        return Task.FromResult(true);

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
        }

        public Task<bool> AddLikes(String getwork_ID, String getUser_ID)
        {
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
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
                        return Task.FromResult(true);
                       
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
           
        }
        public Task<bool> ifCollected(String getuser_ID, String getwork_ID)
        {
           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
           

            using (OracleConnection con = new OracleConnection(conString))
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
                                return Task.FromResult(true);
                            }
                        }
                      
                        reader.Dispose();
                        con.Close();
                        return Task.FromResult(false);

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
           
        }

        public Task<bool> AddCollections(String getwork_ID, String getUser_ID)
        {
            
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
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
                        return Task.FromResult(true);
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
         
        }

        public Task<bool> ifEnoughPoints(String getwork_ID, String getuser_ID)
        {
           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
           
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        int getpoints_need = 0;
                        int getpoints_have = 0;
                        cmd.CommandText = "select points_need from work where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            getpoints_need = reader.GetInt32(0);
                        }

                        cmd.CommandText = "select points from common_user where user_ID=" + "'" + getuser_ID + "'";

                        reader = cmd.ExecuteReader();
                        for (int i = 0; reader.Read() != false; i++)
                        {
                            getpoints_have = reader.GetInt32(0);
                        }
                        reader.Dispose();
                        con.Close();
                        if (getpoints_have >= getpoints_need)
                        {
                            return Task.FromResult(true);
                        }
                        else
                        {
                            return Task.FromResult(false);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
           
        }

        public Task<bool> DecreasePoints(String getwork_ID, String getuser_ID)
        {
            
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
            int getpoints_need = 0;
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select points_need from work where work_ID=" + "'" + getwork_ID + "'";
                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            getpoints_need = reader.GetInt32(0);
                        }
                        reader.Dispose();

                        cmd.CommandText = " update common_user u set u.points = u.points -" + getpoints_need + "where u.user_id = " + "'" + getuser_ID + "'" ;

                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return Task.FromResult(true);

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
           
        }
        public Task<bool> AddReport(String getwork_ID, String getuser_ID)
        {
           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "insert into collection values(" +
                            "'" + getwork_ID + "'" + "," +
                            "'" + getuser_ID + "'" + "," +
                            "'" + DateTime.Now + "'" + "'" +
                            "'this is a report!'，'0'" + ")";
                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return Task.FromResult(true) ;
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
        

        }

        public Task<bool> FollowAction(String getuser_ID, String getwork_ID)
        {
            
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
            String getfollowed_userID = "000";

            using (OracleConnection con = new OracleConnection(conString))
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

                        cmd.CommandText = "insert into follow values：(" + "'" + getwork_ID + "'" + "," + "'" + getfollowed_userID + "'" + ")";

                        cmd.ExecuteNonQueryAsync();
                        con.Close();

                        return Task.FromResult(true);

                       

                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
        
        }

        public Task<bool> ifFollowed(String getuser_ID, String getwork_ID)
        {
           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";
            String getfollowed_userID = "000";

            using (OracleConnection con = new OracleConnection(conString))
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

                        cmd.CommandText = "select followed_ID  from follow where follwer_ID=" + "'" + getuser_ID + "'";

                        reader = cmd.ExecuteReader();


                        if (reader.Read() == false)
                        {
                        
                            return Task.FromResult(false);
                        }
                        else
                        {
                            for (int i = 0; reader.Read() != false; i++)
                            {
                                if (getfollowed_userID == reader.GetString(0))
                                {
                                    return Task.FromResult(true);
                                }
                            }
                            con.Close();
                            return Task.FromResult(false);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
            
        }

        public Task<List<CommentsNeededItem>> GetCommentInfo(String getwork_ID)
        {
            List<CommentsNeededItem> getitem = new List<CommentsNeededItem>();
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText = "select * from comments natural join users where work_ID=" + "'" + getwork_ID + "'" ;

                        OracleDataReader reader = cmd.ExecuteReader();

                        for (int i = 0; reader.Read() != false; i++)
                        {
                            var item = new CommentsNeededItem
                            {
                                comment_ID = reader.GetString(1),
                                work_ID = reader.GetString(2),
                                user_ID = reader.GetString(0),
                                content = reader.GetString(3),
                                time = reader.GetDateTime(4),
                                avatar = reader.GetString(8),
                                user_name = reader.GetString(5)
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
            return Task.FromResult(getitem);
        }

        public Task<String> GetHead(String getwork_ID)
        {

           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
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
                            return Task.FromResult(reader.GetString(0));
                        }
                       else
                        {
                            return Task.FromResult("000");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult("000");
                    }
                   
                }
               
            }
           

        }

        public Task<List<String>> GetTypes(String getwork_ID)
        {
            List<String> getitem = new List<String>();
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
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
            return Task.FromResult(getitem);
        }

        public Task<bool> AddComment(String getuser_ID,String getwork_ID, String words )
        {
           
            string conString = "User Id=C##DBCD;Password=12345678;Data Source=localhost:1521/orcl1";

            using (OracleConnection con = new OracleConnection(conString))
            {

                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        Random random = new Random();
                        cmd.CommandText = "insert into comments values(" +
                            "'" + Convert.ToString(random.Next(1, 10000))+"'"+","+
                        "'" + getwork_ID + "'" + "," +
                            "'" + getuser_ID + "'" + "," +
                            "'"+words+"'"+","+
                            "'" + DateTime.Now + "'"  
                            + ")";
                        cmd.ExecuteNonQueryAsync();
                        con.Close();
                        return Task.FromResult(true);
                    }
                    catch (Exception ex)
                    {
                        string e = ex.Message;
                        return Task.FromResult(false);
                    }
                }
            }
           
        }

        }
    
    }





