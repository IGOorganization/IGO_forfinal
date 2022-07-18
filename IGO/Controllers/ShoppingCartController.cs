using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private DemoIgoContext _dbIgo;
		


		public ShoppingCartController(IWebHostEnvironment hostEnvironment, DemoIgoContext db)
        {

            webHostEnvironment = hostEnvironment;
            _dbIgo = db;
        }
        public IActionResult List()
        {
            List<CShoppingCartViewModel> lists = new List<CShoppingCartViewModel>();

            foreach (TShoppingCart data in _dbIgo.TShoppingCarts.ToList())
            {
                CShoppingCartViewModel shoppingCartViewModel = new CShoppingCartViewModel(_dbIgo);
                shoppingCartViewModel.shoppingCart = data;
                lists.Add(shoppingCartViewModel);
            };


            return View(lists);
        }


        public JsonResult Delete(int id)
        {
            try
            {
                var shoppingCartItem = _dbIgo.TShoppingCarts.FirstOrDefault(s => s.FShoppingCartId == id);
                _dbIgo.TShoppingCarts.Remove(shoppingCartItem);
                _dbIgo.SaveChanges();
                return Json(new { Flag = true, Description = "刪除成功" });
            }
            catch (Exception)
            {
                return Json(new { Flag = false, Description = "刪除失敗" });
            }
        }
        public JsonResult Checked([FromBody] List<int> id)
        {
            HttpContext.Session.SetString(CDictionary.SK_Selected_Item, JsonSerializer.Serialize(id));

            return Json(true);
        }

        public IActionResult Pay()
        {
            var json = HttpContext.Session.GetString(CDictionary.SK_Selected_Item);
            List<int> IDs = JsonSerializer.Deserialize<List<int>>(json);

            List<CShoppingCartViewModel> lists = new List<CShoppingCartViewModel>();
			
            
			foreach (TShoppingCart data in _dbIgo.TShoppingCarts.Where(c => IDs.Contains(c.FShoppingCartId)).ToList())
            {
                CShoppingCartViewModel shoppingCartViewModel = new CShoppingCartViewModel(_dbIgo);
                shoppingCartViewModel.shoppingCart = data;
                lists.Add(shoppingCartViewModel);
				
            };

            //=======================================預設金流變數=========================================
            IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            // 產生測試資訊
            ViewData["MerchantID"] = Config.GetSection("MerchantID").Value;
            ViewData["MerchantOrderNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");  //訂單編號
            ViewData["ExpireDate"] = DateTime.Now.AddDays(3).ToString("yyyyMMdd"); //繳費有效期限
            ViewData["ReturnURL"] = $"{Request.Scheme}://{Request.Host}{Request.Path}ShoppingCart/Finish"; //支付完成返回商店網址
            ViewData["CustomerURL"] = ""; //商店取號網址
            ViewData["NotifyURL"] = ""; //支付通知網址
                                        //ViewData["ClientBackURL"] = $"{Request.Scheme}://{Request.Host}{Request.Path}"; //返回商店網址
            ViewData["ClientBackURL"] = $"{Request.Scheme}://{Request.Host}/ShoppingCart/Finish"; //返回商店網址

            //=======================================預設金流變數=========================================

            return View(lists);
        }


		//==========================================藍新金流線上付款=====================================
		[ValidateAntiForgeryToken]
		public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
		{
			SendToNewebPayOut outModel = new SendToNewebPayOut();

			// 藍新金流線上付款

			//交易欄位
			List<KeyValuePair<string, string>> TradeInfo = new List<KeyValuePair<string, string>>();
			// 商店代號
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantID",inModel.MerchantID));
			// 回傳格式
			TradeInfo.Add(new KeyValuePair<string, string>("RespondType", "String"));
			// TimeStamp
			TradeInfo.Add(new KeyValuePair<string, string>("TimeStamp", DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0)).ToUnixTimeSeconds().ToString()));
			// 串接程式版本
			TradeInfo.Add(new KeyValuePair<string, string>("Version", "2.0"));
			// 商店訂單編號
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantOrderNo", inModel.MerchantOrderNo));
			// 訂單金額
			TradeInfo.Add(new KeyValuePair<string, string>("Amt", inModel.Amt));
			// 商品資訊
			TradeInfo.Add(new KeyValuePair<string, string>("ItemDesc", inModel.ItemDesc));
			// 繳費有效期限(適用於非即時交易)
			TradeInfo.Add(new KeyValuePair<string, string>("ExpireDate", inModel.ExpireDate));
			// 支付完成返回商店網址
			TradeInfo.Add(new KeyValuePair<string, string>("ReturnURL", inModel.ReturnURL));
			// 支付通知網址
			TradeInfo.Add(new KeyValuePair<string, string>("NotifyURL", inModel.NotifyURL));
			// 商店取號網址
			TradeInfo.Add(new KeyValuePair<string, string>("CustomerURL", inModel.CustomerURL));
			// 支付取消返回商店網址
			TradeInfo.Add(new KeyValuePair<string, string>("ClientBackURL", inModel.ClientBackURL));
			// 付款人電子信箱
			TradeInfo.Add(new KeyValuePair<string, string>("Email", inModel.Email));
			// 付款人電子信箱 是否開放修改(1=可修改 0=不可修改)
			TradeInfo.Add(new KeyValuePair<string, string>("EmailModify", "0"));

			//信用卡 付款
			if (inModel.ChannelID == "CREDIT")
			{
				TradeInfo.Add(new KeyValuePair<string, string>("CREDIT", "1"));
			}
			//ATM 付款
			if (inModel.ChannelID == "VACC")
			{
				TradeInfo.Add(new KeyValuePair<string, string>("VACC", "1"));
			}
			string TradeInfoParam = string.Join("&", TradeInfo.Select(x => $"{x.Key}={x.Value}"));

			// API 傳送欄位
			// 商店代號
			outModel.MerchantID = inModel.MerchantID;
			// 串接程式版本
			outModel.Version = "2.0";
			//交易資料 AES 加解密
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
			string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
			string TradeInfoEncrypt = EncryptAESHex(TradeInfoParam, HashKey, HashIV);
			outModel.TradeInfo = TradeInfoEncrypt;
			//交易資料 SHA256 加密
			outModel.TradeSha = EncryptSHA256($"HashKey={HashKey}&{TradeInfoEncrypt}&HashIV={HashIV}");

			return Json(outModel);
		}

		/// <summary>
		/// 加密後再轉 16 進制字串
		/// </summary>
		/// <param name="source">加密前字串</param>
		/// <param name="cryptoKey">加密金鑰</param>
		/// <param name="cryptoIV">cryptoIV</param>
		/// <returns>加密後的字串</returns>
		public string EncryptAESHex(string source, string cryptoKey, string cryptoIV)
		{
			string result = string.Empty;

			if (!string.IsNullOrEmpty(source))
			{
				var encryptValue = EncryptAES(Encoding.UTF8.GetBytes(source), cryptoKey, cryptoIV);

				if (encryptValue != null)
				{
					result = BitConverter.ToString(encryptValue)?.Replace("-", string.Empty)?.ToLower();
				}
			}

			return result;
		}

		/// <summary>
		/// 字串加密AES
		/// </summary>
		/// <param name="source">加密前字串</param>
		/// <param name="cryptoKey">加密金鑰</param>
		/// <param name="cryptoIV">cryptoIV</param>
		/// <returns>加密後字串</returns>
		public byte[] EncryptAES(byte[] source, string cryptoKey, string cryptoIV)
		{
			byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
			byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

			using (var aes = System.Security.Cryptography.Aes.Create())
			{
				aes.Mode = System.Security.Cryptography.CipherMode.CBC;
				aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
				aes.Key = dataKey;
				aes.IV = dataIV;

				using (var encryptor = aes.CreateEncryptor())
				{
					return encryptor.TransformFinalBlock(source, 0, source.Length);
				}
			}
		}

		/// <summary>
		/// 字串加密SHA256
		/// </summary>
		/// <param name="source">加密前字串</param>
		/// <returns>加密後字串</returns>
		public string EncryptSHA256(string source)
		{
			string result = string.Empty;

			using (System.Security.Cryptography.SHA256 algorithm = System.Security.Cryptography.SHA256.Create())
			{
				var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(source));

				if (hash != null)
				{
					result = BitConverter.ToString(hash)?.Replace("-", string.Empty)?.ToUpper();
				}

			}
			return result;
		}



		public IActionResult Finish()
        {
			//===============================取結帳清單資料=================================================
			var json = HttpContext.Session.GetString(CDictionary.SK_Selected_Item);
			List<int> IDs = JsonSerializer.Deserialize<List<int>>(json);

			List<CShoppingCartViewModel> lists = new List<CShoppingCartViewModel>();


			foreach (TShoppingCart data in _dbIgo.TShoppingCarts.Where(c => IDs.Contains(c.FShoppingCartId)).ToList())
			{
				CShoppingCartViewModel shoppingCartViewModel = new CShoppingCartViewModel(_dbIgo);
				shoppingCartViewModel.shoppingCart = data;
				lists.Add(shoppingCartViewModel);

			};
			//===============================產生QRCODE=================================================
			QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(lists[0].product.FProductName, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap icon = new Bitmap(@"wwwroot\ShoppingCartImgs\IGO.jpg");
            Bitmap icon = new Bitmap(webHostEnvironment.WebRootPath + "/ShoppingCartImgs/IGO.jpg");
            Bitmap qrCodeImage = qrCode.GetGraphic(5, Color.Black, Color.White, icon, 15, 0);

            string outputFileName = @"wwwroot\img\Code.jpg";
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create/*, FileAccess.ReadWrite*/))
                {
                    qrCodeImage.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }






            //----------------------------------------------------------寄信------------------------------------------



            MailMessage em = new MailMessage();
            em.From = new System.Net.Mail.MailAddress("zeroqazggc0504@gmail.com");
            em.To.Add("zeroqazggc0504@gmail.com");
            em.SubjectEncoding = System.Text.Encoding.UTF8;
            em.BodyEncoding = System.Text.Encoding.UTF8;
            em.Subject = "IGO訂單已成立";
            //em.Body = "<html><body><img src='~/ShoppingCartImgs/野柳海洋世界.jpg'></body></html>";
            em.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("zeroqazggc0504@gmail.com", "ccpapemvmokdjpjk");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            //------------------------------------------------------夾帶檔案
            //var res = new LinkedResource(@"wwwroot\img\Code.jpg", MediaTypeNames.Image.Jpeg);
            var res = new LinkedResource(webHostEnvironment.WebRootPath + "/img/Code.jpg", MediaTypeNames.Image.Jpeg);
            res.ContentId = "Pic1";  //每個檔案都需要有一個contentID

            var htmlBody = "<html><body><h1>你好</h1><img src='cid:Pic1'></body></html>";

            var altView = AlternateView.CreateAlternateViewFromString(
                htmlBody, null, MediaTypeNames.Text.Html);

            altView.LinkedResources.Add(res);
            em.AlternateViews.Add(altView);

            try
            {
                client.Send(em);
            }
            catch
            {

            }
            return View();
        }

    }
}
