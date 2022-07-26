using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using Microsoft.Extensions.DependencyInjection;
using MimeKit.Utils;


namespace IGO.Models
{
    //public class Email : IGOEmail
    //{
    //    //寄件人
    //    public InternetAddressList From;

    //    //實際的寄件人(寄件人多於一個的時使用)
    //    public InternetAddressList Sender;

    //    //收件人
    //    public InternetAddressList To { get; set; }

    //    //副本
    //    public InternetAddressList Cc;

    //    //回覆的收件者 (預設為 From)
    //    public InternetAddressList ReplyTo;

    //    //信件標題
    //    public String Subject;

    //    //信件內容(文字、附件、 HTML...等)
    //    public MimeEntity Body;

    //    public static async Task Main(MimeMessage message)
    //    {
    //        IServiceCollection service = new ServiceCollection();

    //        service.AddSingleton<IGOEmail, Email>();

    //        var provider = service.BuildServiceProvider().GetRequiredService<IGOEmail>();

    //        var path = "";

    //        var builder = new BodyBuilder();

    //        var image = builder.LinkedResources.Add(path);
    //        image.ContentId = MimeUtils.GenerateMessageId();

    //        builder.HtmlBody = $"當前時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

    //        var message = new MimeMessage
    //        {
    //            Subject = "帶圖片的郵件推送",
    //            Body = builder.ToMessageBody()
    //        };

    //        await provider.SendEmailAsync(message);
    //    }
    //}
    public interface IGOEmail
    {
        /// <summary>
        /// 發送Email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(MimeMessage message);
    }
}

