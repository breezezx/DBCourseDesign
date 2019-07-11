using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using No9Gallery.Models;
using No9Gallery.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace /*WayKuratWeb.SimpleVueJs*/No9Gallery.Controllers
{
    public class thisWorkInfo
    {
        public static string work_id { get; set; }
        public static string user_id { get; set; }
        public static string workName { get; set; }
    }

    [Route("/api/[controller]/[action]/")]
    public class PictureInfoController : Controller
    {
        //string getuser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        private readonly IPictureInfoService _PictureInfoService;

        public PictureInfoController(IPictureInfoService PictureInfoService)
        {
            _PictureInfoService = PictureInfoService;
        }


        public IActionResult Index(string id)
        {

            var getitems = _PictureInfoService.GetPictureInfo(id);


            thisWorkInfo.work_id = id;
            thisWorkInfo.user_id = getitems.First().user_ID;
            thisWorkInfo.workName = getitems.First().picture;
            ViewBag.workuser_ID = getitems.First().user_ID;
            ViewBag.picture = getitems.First().picture;

            Comments comments = new Comments()
            {
                comments = _PictureInfoService.GetCommentInfo(id)
            };

            return View(comments);
        }


        

        [HttpGet]
        public  IActionResult GetPictureInfo()
        {
            List<WorkItem> getitems = _PictureInfoService.GetPictureInfo(thisWorkInfo.work_id);

            return Ok(getitems);
        }

        [HttpGet]
        public bool ifLiked()
        {
            var getitems = (User.FindFirst(ClaimTypes.NameIdentifier).Value == null) ? false:  _PictureInfoService.ifLiked(User.FindFirst(ClaimTypes.NameIdentifier).Value, thisWorkInfo.work_id);

            return getitems;
        }

        [HttpGet]
        [Authorize(Roles ="Commom")]
        public bool AddLikes()
        {
            var getitems = _PictureInfoService.AddLikes(thisWorkInfo.work_id, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return getitems;
        }

        [HttpGet]
        public bool ifCollected()
        {
            var getitems = (User.FindFirst(ClaimTypes.NameIdentifier).Value == null) ? false : _PictureInfoService.ifCollected(User.FindFirst(ClaimTypes.NameIdentifier).Value, thisWorkInfo.work_id);

            return getitems;
        }
        [HttpGet]
        [Authorize(Roles = "Commom")]
        public bool AddCollections()
        {
            var getitems = _PictureInfoService.AddCollections(thisWorkInfo.work_id, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        
        [Authorize(Roles = "Commom")]
        public bool ifEnoughPoints()
        {
            var getitems = _PictureInfoService.ifEnoughPoints(thisWorkInfo.work_id, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        [HttpGet]
        [Authorize(Roles = "Commom")]
        public bool DecreasePoints()
        {
            var getitems = _PictureInfoService.DecreasePoints(thisWorkInfo.work_id, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Downloadpt();
            return getitems;

        }

        public IActionResult Downloadpt()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image\\works", thisWorkInfo.workName);
            var mem = new MemoryStream();
            using (var streams = new FileStream(path, FileMode.Open))
            {
                streams.CopyTo(mem);
            }
            mem.Seek(0, SeekOrigin.Begin);

            //string[] strArry = path.Split('/');
            //string enco = System.Net.WebUtility.UrlEncode(thisWorkInfo.workName);
            //Response.Headers.Add("Content-Disposition", "attachment; filename = " + enco);
            return new FileStreamResult(mem, "application/octet-stream");
            var stream = System.IO.File.OpenRead(path);
            return File(stream, Path.GetFileName(path));
        }

        [HttpGet]
        [Authorize(Roles = "Commom")]
        public bool AddReport()
        {
            var getitems = _PictureInfoService.AddReport(thisWorkInfo.work_id, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return getitems;
        }

        [HttpGet]
        [Authorize(Roles = "Commom")]
        public bool FollowAction()
        {
            var getitems = _PictureInfoService.FollowAction(User.FindFirst(ClaimTypes.NameIdentifier).Value, thisWorkInfo.work_id);
            return getitems;
        }

        [HttpGet]
        public bool ifFollowed()
        {
            var getitems = (User.FindFirst(ClaimTypes.NameIdentifier).Value == null) ? false : _PictureInfoService.ifFollowed(User.FindFirst(ClaimTypes.NameIdentifier).Value, thisWorkInfo.work_id);
            return getitems;
        }
        [HttpGet]
        public String GetHead()
        {
            var getitems = _PictureInfoService.GetHead(thisWorkInfo.work_id);
            return getitems;
        }
        [HttpGet]
        public List<String> GetTypes()
        {
            var getitems = _PictureInfoService.GetTypes(thisWorkInfo.work_id);
            return getitems;
        }

        [HttpGet]
        public Task<bool> IfLogin()
        {
            if(User.Identity.Name == null)
            {
                return Task.FromResult(false);
            }
            else { return Task.FromResult(true); }
        }

        [HttpPost]
        [Authorize(Roles = "Commom")]
        public IActionResult AddComment(Comments newComment)
        {
            var getitems = _PictureInfoService.AddComment(User.FindFirst(ClaimTypes.NameIdentifier).Value, thisWorkInfo.work_id, newComment.addComment);
            return RedirectToAction("Index", new { id = thisWorkInfo.work_id });
        }


        public IActionResult DeleteWork()
        {
            _PictureInfoService.Delete(thisWorkInfo.work_id, thisWorkInfo.user_id, User.IsInRole("Admin"), User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index", "Home");
        }
    }
}



