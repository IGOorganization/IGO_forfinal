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


        public IActionResult HelpByAccount()
        {
            return View();
        }
        


        public IActionResult HelpByPayment()
        {
            return View();
        }


        public IActionResult HelpByBuyProcess()
        {
            return View();
        }

        public IActionResult HelpByTravel()
        {
            return View();
        }

        public IActionResult HelpByUsage()
        {
            return View();
        }



        public IActionResult HelpByCancel()
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

       


        


        public IActionResult HelpByAccountDetail_1()
        {
            return View();
        }

        public IActionResult HelpByAccountDetail_2()
        {
            return View();
        }

        public IActionResult HelpByAccountDetail_3()
        {
            return View();
        }

        public IActionResult HelpByAccountDetail_4()
        {
            return View();
        }

        public IActionResult HelpByAccountDetail_5()
        {
            return View();
        }


        public IActionResult HelpByPaymentDetail_1()
        {
            return View();
        }

        public IActionResult HelpByPaymentDetail_2()
        {
            return View();
        }


        public IActionResult HelpByPaymentDetail_3()
        {
            return View();
        }


        public IActionResult HelpByPaymentDetail_4()
        {
            return View();
        }


        public IActionResult HelpByBuyProcessDetail_1()
        {
            return View();
        }


        public IActionResult HelpByBuyProcessDetail_2()
        {
            return View();
        }

        public IActionResult HelpByBuyProcessDetail_3()
        {
            return View();
        }

        public IActionResult HelpByBuyProcessDetail_4()
        {
            return View();
        }

        public IActionResult HelpByBuyProcessDetail_5()
        {
            return View();
        }


        public IActionResult HelpByBuyProcessDetail_6()
        {
            return View();
        }


        public IActionResult HelpByBuyProcessDetail_7()
        {
            return View();
        }


        public IActionResult HelpByTravelDetail_1()
        {
            return View();
        }

        public IActionResult HelpByTravelDetail_2()
        {
            return View();
        }

        public IActionResult HelpByTravelDetail_3()
        {
            return View();
        }
        public IActionResult HelpByTravelDetail_4()
        {
            return View();
        }
        public IActionResult HelpByTravelDetail_5()
        {
            return View();
        }



        public IActionResult HelpByCancelDetail_1()
        {
            return View();
        }

        public IActionResult HelpByCancelDetail_2()
        {
            return View();
        }
        public IActionResult HelpByCancelDetail_3()
        {
            return View();
        }

    }
}
