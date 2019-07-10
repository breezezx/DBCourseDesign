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
        Task<List<WorkItem>> GetAll();
        Task<List<WorkItem>> GetPictureInfo(String getwork_ID);
        Task<bool> AddLikes(String getwork_ID, String getUser_ID);
        Task<bool> AddCollections(String getwork_ID, String getUser_ID);
        Task<bool> ifEnoughPoints(String getwork_ID, String getuser_ID);
        Task<bool> DecreasePoints(String getwork_ID, String getuser_ID);
        Task<bool> AddReport(String getwork_ID, String getuser_ID);
        Task<bool> FollowAction(String getuser_ID, String getwork_ID);
        Task<bool> ifFollowed(String getuser_ID, String getwork_ID);
        Task<bool> ifCollected(String getuser_ID, String getwork_ID);
        Task<bool> ifLiked(String getuser_ID, String getwork_ID);

        Task<List<CommentsNeededItem>> GetCommentInfo(String getwork_ID);
        Task<String> GetHead(String getwork_ID);
        Task<List<String>> GetTypes(String getwork_ID);
        Task<bool> AddComment(String getuser_ID, String getwork_ID, String words);
        //Task<List<CommentsNeededItem>> GetCommentInfo(String getwork_ID);

    }
}
