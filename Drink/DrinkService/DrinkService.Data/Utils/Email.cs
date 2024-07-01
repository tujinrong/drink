using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Threading;
using DrinkService.Models;

namespace DrinkService.Utils
{
    public class Email
    {
        public static void SendMail(string controllerName,string actionName,string errorMessage, string stackTrace)
        {
         
            //SendMailAsync("jinrong@safeneeds.co.jp", errorMessage, stackTrace);
            //SendMailAsync("liaobaojun_safeneeds@hotmail.com", errorMessage, stackTrace);
            //SendMailAsync("safeneeds_sealovesky@hotmail.com",controllerName,actionName, errorMessage, stackTrace);
        }

        public static void SendMailAsync(string sendTo,string controllerName,string actionName, string errorMessage, string stackTrace)
        {
            string logDate = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd");
            string logTime = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");
            string testVersion = CommonLogic.IsTestVersion == "test" ? "【テスト環境】" : "";

            MailMessage message = new MailMessage();

            message.From = new MailAddress("drinkservicetest@sina.com");
            message.To.Add(new MailAddress(sendTo));

            message.IsBodyHtml = false;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = logDate + "[DrinkService]" + testVersion + "ErrorReport ";
            message.Body =  "errorTime: " + logTime +"\r\n"+
                            "controllerName: " + controllerName + "\r\n" +
                            "actionName: " + actionName + "\r\n" +
                            "errorMessage: " + errorMessage + "\r\n" +
                            "stackTrace:\r\n" +
                            stackTrace;

            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.sina.com");
            smtp.UseDefaultCredentials = false;
            var credentials = new System.Net.NetworkCredential("drinkservicetest@sina.com", "drinktest");
            smtp.Credentials = credentials;
            smtp.EnableSsl = true;
            smtp.Port = 25;
            string mailState = "State";
            smtp.SendAsync(message, mailState);
        }
    }
}
