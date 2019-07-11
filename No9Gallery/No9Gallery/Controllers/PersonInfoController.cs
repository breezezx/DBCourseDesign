using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using No9Gallery.Models;
using No9Gallery.Services;

namespace No9Gallery.Controllers
{
    public class PersonInfoController : Controller
    {
        private readonly IPersonInfoService PersonInfoservice;

        public PersonInfoController(IPersonInfoService service)
        {
            PersonInfoservice = service;
        }


        //进入个人主界面
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> Index(string id)
        {
            
            var Info = await PersonInfoservice.GetPersonInfoAsync(id, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if(Info.Status == "Commom")
            {
                ViewBag.ID = Info.ID;
                ViewBag.VisitID = Info.VisitID;
                ViewBag.IsFollwed = Info.IsFollowed;
                ViewBag.Name = Info.Name;
                //ViewBag.Password = Info.Password;
                // ViewBag.Status = Info.Status;
                ViewBag.Introduction = Info.Introduction;
                ViewBag.HeadportraitURL = Info.HeadPortraitURL;
                ViewBag.Integral = Info.Integral;
                ViewBag.MembershipLevel = Info.membershipLevel;
                ViewBag.WorkNum = Info.Worknum;
                ViewBag.Workids = Info.Workid;
                ViewBag.Workpaths = Info.WorkPath;
                ViewBag.WorkHeadline = Info.WorkNames;
            }
            else
            {
                //导入管理员界面，现在先回主界面
               return Redirect("/PersonInfo/AdminView/?AdminName="+Info.VisitID+"&Avatar="+Info.HeadPortraitURL);

            }
            return View();
        }

        //进入管理员界面
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminView(string AdminName,string Avatar)
        {
            var id= User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.AdminID = id;
            if(AdminName!=null&&Avatar!=null)
            {
                ViewBag.HeadPortraitURL = Avatar;
                ViewBag.Name = AdminName;
            }
           else
            {
                var Info = await PersonInfoservice.GetPersonInfoAsync(id, id);
                ViewBag.HeadPortraitURL = Info.HeadPortraitURL;
                ViewBag.Name = Info.Name;
            }
            
            List<MessageList> result = await PersonInfoservice.GetReportAsync();
             string change=await PersonInfoservice.ChangeMessage();
            return View(result);
        }

        //管理员发送信息
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PostMessage()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var massage = Request.Form;
            string ReceiverID = Request.Form["ReceiverID"].ToString();
            string Content = Request.Form["Content"].ToString();
            if(id!=ReceiverID)
            {
                string result = await PersonInfoservice.PostMassage(id, ReceiverID, Content, time);
            }
            else
            {
                string result = await PersonInfoservice.PostAllMessage(id, Content, time);
            }
        
            return Redirect("/PersonInfo/AdminView");
        }

        //改变关注状态
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> Follow(string id)
        {
            var visitid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string result = await PersonInfoservice.ChangeFollow(visitid, id);
            return Redirect("/PersonInfo/Index/" + id);
        }


        //进入信箱界面
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> MessageReceiveView(string id)
        {
            List<MessageReceivelist> Messages = new List<MessageReceivelist>();


            Messages = await PersonInfoservice.GetMessageAsync(id);


            return View(Messages);
        }

        //进入信息修改界面
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> ReviseView(string id)
        {

            var Info = await PersonInfoservice.GetPersonInfoAsync(id, User.FindFirst(ClaimTypes.NameIdentifier).Value);
         
              ViewBag.ID = Info.ID;
            ViewBag.Name = Info.Name;
            ViewBag.Password = Info.Password;
            ViewBag.Status = Info.Status;
            ViewBag.Introduction = Info.Introduction;
            ViewBag.HeadportraitURL = Info.HeadPortraitURL;
            ViewBag.Integral = Info.Integral;
            ViewBag.MembershipLevel = Info.membershipLevel;
            
            return View("InfoRevise");
        }

        //进入作品上传界面
        [Authorize(Roles = "Commom")]
        public IActionResult UploadView(string id)
        {
            ViewBag.ID = id;
            return View("UploadView");
        }



        //进入升级界面
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> UpgradeView(string id,string flag,string result)
        {
            
            ViewBag.ID = id;
            var Info = await PersonInfoservice.GetPersonInfoAsync(id, id);
            ViewBag.UserName = Info.Name;
            ViewBag.MembershipLevel = Info.membershipLevel;
            ViewBag.Points = Info.Integral;
            ViewBag.Flag = flag;
            ViewBag.result = result;
       
            return View();
        }

        //进入充值界面
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult>ChargeView(string id,string flag,string result)
        {
            
            if (id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Redirect("/PersonInfo/Index/" +id);
            ViewBag.ID =id;
;            List<Chargelist> ChargeList=await PersonInfoservice.GetChargeListAsync(id);
            int points = await PersonInfoservice.GetPointsAsync(id);
            ViewBag.Points = points;
            ViewBag.Flag = flag;
            ViewBag.result = result;
            return View(ChargeList);
        }

        //提升等级
        [Authorize(Roles = "Commom")]
        public async Task<IActionResult>Upgrade(string id,string Level,string points)
        {
            string result;
            var upgradetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var randomnum = new Random();
            var order_no = DateTime.Now.ToString("yyyyMMddHHmmss") + "_-_" + randomnum.Next(0, 1000).ToString();
            if (Convert.ToInt32(points) >= 5000&&Level!="5")
            {
                 result = await PersonInfoservice.Upgrade(id,Level,points,order_no,upgradetime);
            }
            else if( Convert.ToInt32(points)<5000&&Level!="5")
            {
                 result = "false";
            }
            else
            {
                result = "limitlevel";
            }
            return Redirect("/PersonInfo/UpgradeView/?id="+id+"&flag=true&result=" + result);
        }

        //信息修改
        [Authorize(Roles = "Commom")]
        [HttpPost]
        public async Task<IActionResult> Revise(ReviseInfo reviseinfo)
        {
            reviseinfo.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string nowAvatar = User.FindFirstValue("Avatar");

            await HttpContext.SignOutAsync();
            var claimIdentity = new ClaimsIdentity("Cookie");
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, reviseinfo.UserID));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, reviseinfo.NewUserName));
            claimIdentity.AddClaim(new Claim("Avatar", nowAvatar));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Commom"));
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            string Result = await PersonInfoservice.ReviseInfo(reviseinfo);
            ViewBag.Result = Result;

            return Redirect("/PersonInfo/Index/"+reviseinfo.UserID);
        }

        //头像上传
        [Authorize(Roles = "Commom")]
        [HttpPost]
        public async Task<IActionResult> UploadAvatorAsync(List<IFormFile> files)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string nowName = User.FindFirstValue(ClaimTypes.Name);

            await HttpContext.SignOutAsync();
            var claimIdentity = new ClaimsIdentity("Cookie");
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, nowName));
            claimIdentity.AddClaim(new Claim("Avatar", "Default.png"));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Commom"));
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            string Result= await PersonInfoservice.Uploadavatar(id,files);                                    
            return Redirect("/PersonInfo/ReviseView/" +id);
        }

      
        //提交充值订单
        [Authorize(Roles = "Commom")]
        [HttpPost]
        public async Task<IActionResult> Chargesubmit(string id)
        {
            //yyyyMMddHHmmss
            var amount =Convert.ToInt32(Request.Form["ChargeAmount"]);
            var chargetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var randomnum = new Random();
            var order_no = DateTime.Now.ToString("yyyyMMddHHmmss") + "_+_" + randomnum.Next(0, 1000).ToString();
            var ConTent = "积分充值";
            int points = await PersonInfoservice.GetPointsAsync(id);
            points = points + amount;
            var result = await PersonInfoservice.ChargeSubmit(order_no, id, amount, ConTent, chargetime,points);
            return Redirect("/PersonInfo/ChargeView/?id=" + id+"&flag=true&result="+result);
        }

        //作品上传功能
        [Authorize(Roles = "Commom")]
        [HttpPost]
        public async Task<IActionResult> UploadWork(string id)
        {
            int i = 0;
            var table = Request.Form;
            IFormFile files =Request.Form.Files[0];
            int NewWorkPoint =Convert.ToInt32(Request.Form["NewWorkPoint"]);
            string Workname = Request.Form["NewWorkName"];
            string WorkType = Request.Form["Worktype"];
            string Introduction = Request.Form["WorkIntroduction"];
            if(files!=null&&Workname!=null&&WorkType!=null&&Introduction!=null)
            {
                string result = await PersonInfoservice.UploadWork(User.FindFirst(ClaimTypes.NameIdentifier).Value,files,Workname,WorkType,Introduction,NewWorkPoint);
            }
            
            return Redirect("/PersonInfo/Index/" + id);
        }



        //设置信息为已读
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> setRead()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var msgid= Request.Form["toSetRead"].ToString();
            string result = await PersonInfoservice.setRead(msgid);
            return Redirect("/PersonInfo/MessageReceiveView/" + id);
        }

    }
}