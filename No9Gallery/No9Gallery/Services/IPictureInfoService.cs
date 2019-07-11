using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using No9Gallery.Models;
using Microsoft.AspNetCore.Mvc;


namespace No9Gallery.Services
{
    public interface IPictureInfoService
    {
      
        List<WorkItem> GetPictureInfo(String getwork_ID);
        bool AddLikes(String getwork_ID, String getUser_ID);
        bool AddCollections(String getwork_ID, String getUser_ID);
        bool ifEnoughPoints(String getwork_ID, String getuser_ID);
        bool DecreasePoints(String getwork_ID, String getuser_ID);
        bool AddReport(String getwork_ID, String getuser_ID);
        bool FollowAction(String getuser_ID, String getwork_ID);
        bool ifFollowed(String getuser_ID, String getwork_ID);
        bool ifCollected(String getuser_ID, String getwork_ID);
        bool ifLiked(String getuser_ID, String getwork_ID);
        void Delete(string workId, string authorId, bool isAdmin, string adminId);

        List<Comment> GetCommentInfo(String getwork_ID);
        String GetHead(String getwork_ID);
        List<String> GetTypes(String getwork_ID);
        bool AddComment(String getuser_ID, String getwork_ID, String words);
        //Task<List<CommentsNeededItem>> GetCommentInfo(String getwork_ID);

    }
}
