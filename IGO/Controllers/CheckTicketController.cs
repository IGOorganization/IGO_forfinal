using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CheckTicketController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private DemoIgoContext _dbIgo;
     


        public CheckTicketController(IWebHostEnvironment hostEnvironment, DemoIgoContext db)
        {

            webHostEnvironment = hostEnvironment;
            _dbIgo = db;
        }
        public IActionResult ScanTicket(string id)
        {
            int OdId = 0;
             //OdId = int.Parse(CEncryptAndDecrypt.Decrypt(id));
            bool IsNum = int.TryParse(CEncryptAndDecrypt.Decrypt(id), out OdId);   //判斷是否解碼為數字
            if (IsNum)
            {
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))  //判斷使用者是否登入
                {

                    int UserId = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);

                    if (!string.IsNullOrEmpty((_dbIgo.TCustomers.FirstOrDefault(c => c.FCustomerId == UserId).FSupplierId.ToString())))  //判斷是否為驗票人員
                    {

                        if (_dbIgo.TOrderDetails.FirstOrDefault(c => c.FOrderDetailsId == OdId) != null) //判斷是否有此筆訂單
                        {
                            COrderDetailViewModel orderDetail = new COrderDetailViewModel(_dbIgo);
                            orderDetail.orderDetail = _dbIgo.TOrderDetails.FirstOrDefault(c => c.FOrderDetailsId == OdId);
                            List<COrderDetailViewModel> lists = new List<COrderDetailViewModel>();
                            lists.Add(orderDetail);
                            return View(lists);
                        }
                        else
                            return RedirectToAction("CheckFail");
                    }
                    else
                    {
                        return Redirect($"{Request.Scheme}://{Request.Host}/Coupon/List");
                    }
                }
                else
                {
                    return Redirect($"{Request.Scheme}://{Request.Host}/Coupon/List");
                }
            }
            else {
                return Redirect($"{Request.Scheme}://{Request.Host}/Home/Home");
            }
        }

        public IActionResult CheckFail()
        {

            return View();
        }

        public IActionResult Capture()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Capture(string name)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                byte[] imageBytes = null;

                if (files == null)
                {
                    return Json(false);
                }

                var file = files.FirstOrDefault();

                if (file.Length == 0)
                {
                    return Json(false);
                }

                using (MemoryStream ms = new MemoryStream()) //將檔案存成byte陣列
                {
                    file.CopyTo(ms);
                    imageBytes = ms.ToArray();
                }

                var fileName = file.FileName;
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var fileExtension = Path.GetExtension(fileName);
                var newFileName = string.Concat(myUniqueFileName, fileExtension);
                var filePath = webHostEnvironment.WebRootPath + "/CameraPhotos/" + $"{newFileName}";
                if (!string.IsNullOrEmpty(filePath))
                {
                    StoreInFolder(file, filePath);
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Prediction-Key", "a084a3a3f2bb4229b04870ec2ecd72db");
                var uri = "https://msit14106customervision-prediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/3b5e2451-d455-4390-b04d-7ccbadea94c6/classify/iterations/UserClassification/image";
                string text = "";
                using (ByteArrayContent ImageContent = new ByteArrayContent(imageBytes))
                {
                    ImageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"); //octet-stream 不需要指定二進位資料的格式
                    HttpResponseMessage response = await client.PostAsync(uri, ImageContent);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(content);
                    JObject jresult = result as JObject;
                     text = Convert.ToString(jresult["predictions"][0]["tagName"]);
                                   
                    return Json(text);
                }
            }
            catch (Exception)
            {
            }

            return View();
        }
   
        public IActionResult CheckCustomerOrderDetails(int id)
        {
            int UserId =(int) HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
            if (!String.IsNullOrEmpty((_dbIgo.TCustomers.FirstOrDefault(c => c.FCustomerId == UserId)).FSupplierId.ToString()) )
            {
                int SupplierId = (int)(_dbIgo.TCustomers.FirstOrDefault(c => c.FCustomerId == UserId)).FSupplierId;     //取出SupplierId
                int Productid = (_dbIgo.TProducts.FirstOrDefault(p => p.FSupplierId == SupplierId)).FProductId;//取出ProductId
                List<COrderDetailViewModel> lists = new List<COrderDetailViewModel>();
                List<int> orderdetailId = new List<int>();  //創造一個這個顧客有的orderId
                foreach (var data in _dbIgo.TOrders.Where(c => c.FCustomerId == id).ToList())
                {
                    orderdetailId.Add(data.FOrderId);
                }
                foreach (var data in _dbIgo.TOrderDetails.Where(c => orderdetailId.Contains(c.FOrderId) && c.FProductId == Productid))
                {
                    COrderDetailViewModel cOrderDetailViewModel = new COrderDetailViewModel(_dbIgo);
                    cOrderDetailViewModel.orderDetail = data;
                    lists.Add(cOrderDetailViewModel);
                }
                if (lists.Count == 0)
                {

                    return RedirectToAction("CheckFail");
                }


                return View(lists);
            }
            else { 
                 return Redirect($"{Request.Scheme}://{Request.Host}/Coupon/List");
            }
        }

        public JsonResult Checked([FromBody]List<int> id)
        {
           

            return Json(true);
        }
        private void StoreInFolder(IFormFile file, string fileName)
        {

            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
               
                file.CopyTo(stream);
                
            }
        }
        private void StoreInDatabase(byte[] imageBytes)
        {

        }
    }
}
