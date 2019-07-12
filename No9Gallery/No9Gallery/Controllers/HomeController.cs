using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using No9Gallery.Models;
using No9Gallery.Services;


namespace No9Gallery.Controllers
{
    public class HomeController : Controller
    {
        private IRetrieveImages _retrieveImages;


        public HomeController(IRetrieveImages iretrieveImages)
        {
            _retrieveImages = iretrieveImages;
        }


        public async Task<IActionResult> Index()
        {
            var workitems = await _retrieveImages.GetWorkItemByRandomSelectionAsync(15);

            var homeviewmodel = new Models.ViewModel.HomeViewModel()
            {
                Works = workitems
            };
            ViewBag.part1 = workitems;


            return View(homeviewmodel);
        }

        [Authorize(Roles="Commom")]
        public async Task<IActionResult> Like()
        {
            var cur_user = new LoginUser
            {
                ID = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Avatar = User.FindFirst("Avatar").Value
            };

            ViewBag.imagelist = await _retrieveImages.GetWorkItemInUserLikesAsync(cur_user.ID);

            return View();
        }

        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> Collect()
        {
            var cur_user = new LoginUser
            {
                ID = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Avatar = User.FindFirst("Avatar").Value
            };

            ViewBag.imagelist = await _retrieveImages.GetWorkItemInUserCollectionAsync(cur_user.ID);

            return View();
        }

        [Authorize(Roles = "Commom")]
        public async Task<IActionResult> Follow()
        {
            var cur_user = new LoginUser
            {
                ID = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Avatar = User.FindFirst("Avatar").Value
            };

            ViewBag.imagelist = await _retrieveImages.GetWorkItemInUserFollowAsync(cur_user.ID);

            return View();
        }

        public async Task<IActionResult> Classification(int id = 0)            //分类界面
        {
            ViewBag.imagelist = new List<string>();
            if (id == 0)
            {
                ViewBag.classify = 1;
                ViewBag.sort = 1;
                //此处按illustration，like分类排序
                var workitems = await _retrieveImages.GetWorkItemByCategoryAsync("Illustration", "likes_num");

                var homeviewmodel = new Models.ViewModel.HomeViewModel()
                {
                    Works = workitems
                };
                ViewBag.imagelist = workitems;


                return View(homeviewmodel);
            }
            else
            {
                ViewBag.classify = id / 10;          //分类选项 1，2，3，4分别是illustration，phototype，animation，ui design
                ViewBag.sort = id % 10;                //排序选项  1，2，3分别是like，collect，time
                //此处分情况分类排序
                String morder = "";
                switch (ViewBag.sort)
                {
                    case 1:
                        morder = "likes_num";
                        break;
                    case 2:
                        morder = "collect_num";
                        break;
                    case 3:
                        morder = "upload_time";
                        break;
                    default:
                        break;
                }

                String mcate = "";
                if (ViewBag.classify == 1)
                    mcate = "Illustration";
                else if (ViewBag.classify == 2)
                    mcate = "Photograph";
                else if (ViewBag.classify == 3)
                    mcate = "Animation";
                else if (ViewBag.classify == 4)
                    mcate = "UI Design";
                else if (ViewBag.classify == 5)
                    mcate = "Painting";
                else { }

                var workitems = await _retrieveImages.GetWorkItemByCategoryAsync(mcate, morder);

                var homeviewmodel = new Models.ViewModel.HomeViewModel()
                {
                    Works = workitems
                };
                ViewBag.imagelist = workitems;



                return View(homeviewmodel);
            };
            return View();
        }

        public async Task<IActionResult> Search(string search_value)
        {

            var workitems = await _retrieveImages.GetWorkItemByIDAsync(search_value);

            var homeviewmodel = new Models.ViewModel.HomeViewModel()
            {
                Works = workitems
            };
            ViewBag.imagelist = workitems;

            if (workitems.Count == 0)
            {
                ViewBag.Error = "Not Found";
            }
            else
                ViewBag.Error = "Found";


            return View(homeviewmodel);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
