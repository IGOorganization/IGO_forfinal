using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class TestController : Controller
    {

        private IWebHostEnvironment _enviroment;
        public TestController(IWebHostEnvironment p)
        {
            _enviroment = p;
        }

        public IActionResult Index()
        {
            return View();
        }

        DemoIgoContext db = new DemoIgoContext();
        public ActionResult TestList()
        {


            var dates = from t in (new DemoIgoContext()).TTestQuestions
                        select t;

            return View(dates);
        }


        //public ActionResult ResultList()
        //{


        //    var dates = from t in (new DemoIgoContext()).TTestCustomerAnswers
        //                select t;

        //    return View(dates);
        //}


        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var Question = db.TTestQuestions.FirstOrDefault(x => x.FQuestionId == id.Value);
                var Answer = db.TTestAnswers.Where(x => x.FQuestionId == id.Value).ToList();
                CTestViewModel viewmodel = new CTestViewModel();
                viewmodel.testQuestion = Question;
                viewmodel.testAnswer = Answer;

                return View(viewmodel);
            }

            return RedirectToAction("Index");

        }







        public IActionResult nextquestion(int id)
        {
            if (id < 7)
            {

                id++;
                CQAndAViewModel item = new CQAndAViewModel(db);
                item.QuestionId = id;


                //string JData = JsonSerializer.Serialize(item);

                return Json(item);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ResultDetail(int id, int total, string type)
        {
            var result = resultList.First(x => x.Id == id);
            var result2 = Request.QueryString;
            TTestResult r = new TTestResult
            {

                FTestResultType = type,
                FCustomerId = 1,
                FTestResultTypeId = id,
                FPoint = total


            };

            db.TTestResults.Add(r);
            db.SaveChanges();
            return View(result);
        }





        public class result
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Contect { get; set; }
        }

        public List<result> resultList = new List<result>


    {
        new result
        {
            Id = 1,

            Name = "1~24分：悠哉悠哉型",
             Contect = "旅行就是要軟爛放空呀～行程那麼滿，整天在趕來趕去，那麼累幹嘛？這類人喜歡悠閒自在的旅行，即使安排好的行程大更動，只要能讓心情愉悅，有什麼不可以？比起喧囂的都市漫遊，更適合放鬆的緩慢的旅遊，像是古蹟之旅、到海島國家度假。"
        },
        new result
        {
            Id = 2,

            Name = "25~50分：文化體驗型",
             Contect = "對於旅行當地的文化和傳統特色有高度興趣，喜歡透過一些活動接觸不一樣的文化，適合去一些能夠了解人文風情的景點，像是古色古香的九份老街、到日本京都體驗和服、泰國水上市場等，適合預先安排好，做過功課有系統的深度旅行。"
        },
        new result
        {
            Id = 3,

            Name = "51分以上：滿腔熱血型",
            Contect = "旅行對你來說不只是充電，更有環島青年的熱血，會讓你活力滿滿！喜歡時尚快速的旅遊景點，但也喜歡像是泛舟、極限運動這類的活動，即使行程排得比較滿，有點累～還是會熱血地繼續行程，能玩到越多地方越好！可以說走就走，也可以預先安排，但就是要盡情玩啦～"
        }
    };


        public IActionResult List()
        {
            var results = resultList;

            return View(resultList);
        }


    }
}
