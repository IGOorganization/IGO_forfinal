using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class AboutController : Controller
    {
        private DemoIgoContext _dbIgo;
        public AboutController(DemoIgoContext db)
        {
            _dbIgo = db;
        }


        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult HelpByAccount()
        {
            return View();
        }
        public IActionResult HelpByAccountDetail_1()
        {
            return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        public JsonResult AutoComplete(string prefix)
        {
            DemoIgoContext db= new DemoIgoContext();
            
            var questions = (from question in db.THelpers
                             where question.FHelperTitle.Contains(prefix)
                             select new
                             {
                                 label = question.FHelperTitle,
                                 val = question.FHelperId
                             }).ToList();
            


            //var JData = JsonSerializer.Serialize(questions);

            return Json(questions);
        }

       


        [HttpPost]

        public ActionResult Helper(string FHelperTitle, int FHelperId)
        {
            ViewBag.Message = "搜尋" + FHelperTitle + "ID:" + FHelperId;
           
            return View();
        }

        public IActionResult Helper()
        {
            return View();
        }


       


    }
}
