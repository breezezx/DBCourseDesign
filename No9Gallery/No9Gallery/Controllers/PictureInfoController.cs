using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using No9Gallery.Models;
using No9Gallery.Services;
using System.Security.Claims;

namespace /*WayKuratWeb.SimpleVueJs*/No9Gallery.Controllers
{
    [Route("/api/[controller]/[action]/")]
    public class PictureInfoController : Controller
    {
        string getwork_ID="001";
        //string getuser_ID = "10000";
        int woID = 100;
        //string getuser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        private readonly IPictureInfoService _PictureInfoService;

        public PictureInfoController(IPictureInfoService PictureInfoService)
        {
            _PictureInfoService = PictureInfoService;
        }


        public IActionResult Index(string wID)
        {
            //getwork_id = wID;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getitems = await _PictureInfoService.GetAll();

            return Ok(getitems);
        }

        [HttpGet]
        public async Task<IActionResult> GetPictureInfo()
        {
            var getitems = await _PictureInfoService.GetPictureInfo(getwork_ID);

            return Ok(getitems);
        }

        [HttpGet]
        public async Task<bool> ifLiked(String getuser_ID, String getwork_ID)
        {
            var getitems = await _PictureInfoService.ifLiked(User.FindFirst(ClaimTypes.NameIdentifier).Value,getwork_ID);

            return getitems;
        }

        [HttpGet]
        public Task<bool> AddLikes()
        {
            var getitems = _PictureInfoService.AddLikes(getwork_ID, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return getitems;
        }

        [HttpGet]
       public Task<bool> ifCollected()
        {
            var getitems = _PictureInfoService.ifCollected( User.FindFirst(ClaimTypes.NameIdentifier).Value, getwork_ID);

            return getitems;
        }
        [HttpGet]
        public Task<bool> AddCollections()
        {
            var getitems = _PictureInfoService.AddCollections(getwork_ID, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        [HttpGet]
        public Task<bool> ifEnoughPoints()
        {
            var getitems = _PictureInfoService.ifEnoughPoints(getwork_ID, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        [HttpGet]
        public Task<bool> DecreasePoints()
        {
            var getitems = _PictureInfoService.DecreasePoints(getwork_ID, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        [HttpGet]
        public Task<bool> AddReport()
        {
            var getitems = _PictureInfoService.AddReport(getwork_ID, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        [HttpGet]
        public Task<bool> FollowAction()
        {
            var getitems = _PictureInfoService.FollowAction(User.FindFirst(ClaimTypes.NameIdentifier).Value, getwork_ID);
            return getitems;
        }

        [HttpGet]
        public Task<bool> ifFollowed()
        {
            var getitems = _PictureInfoService.ifFollowed(User.FindFirst(ClaimTypes.NameIdentifier).Value, getwork_ID);
            return getitems;
        }
        [HttpGet]
        public async Task<List<CommentsNeededItem>> GetCommentInfo()
        {
            var getitems = await _PictureInfoService.GetCommentInfo(getwork_ID);
            return getitems;
        }
        [HttpGet]
        public Task<String> GetHead()
        {
            var getitems = _PictureInfoService.GetHead( getwork_ID);
            return  getitems;
        }
        [HttpGet]
        public Task<List<String>> GetTypes()
        {
            var getitems = _PictureInfoService.GetTypes( getwork_ID);
            return getitems;
        }
        [HttpPost]

        public IActionResult AddComment( String words)
        {
            var getitems = _PictureInfoService.AddComment(User.FindFirst(ClaimTypes.NameIdentifier).Value, getwork_ID,words);
            return View("Index");
        }

    }
}



