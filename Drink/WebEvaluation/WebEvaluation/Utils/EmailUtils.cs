using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace WebEvaluation.Utils
{
    public class EmailUtils
    {
        public static void SendMail(string controllerName, string actionName, string errorMessage, string stackTrace)
        {

            //SendMailAsync("jinrong@safeneeds.co.jp", errorMessage, stackTrace);
            //SendMailAsync("liaobaojun_safeneeds@hotmail.com", errorMessage, stackTrace);
            //SendMailAsync("safeneeds_sealovesky@hotmail.com",controllerName,actionName, errorMessage, stackTrace);
        }

        public static void SendMailAsync(
            List<string> sendTo
            ,List<string> ccTo
            , List<string> bccTo
            ,bool IsBodyHtml
            ,string Subject
            , string Body)
        {
            DateTime dateNow = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);
            dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateNow, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

            string logDate = dateNow.ToString("yyyy/MM/dd");
            string logTime = dateNow.ToString("yyyy/MM/dd HH:mm:ss");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("telerepo@yahoo.com", "テレフォンレポート");

            foreach(string sendToAddr in sendTo){
                message.To.Add(new MailAddress(sendToAddr));
            }

            foreach (string ccToAddr in ccTo)
            {
                message.CC.Add(new MailAddress(ccToAddr));
            }

            foreach (string bccToAddr in bccTo)
            {
                message.Bcc.Add(new MailAddress(bccToAddr));
            }

            message.IsBodyHtml = IsBodyHtml;

            message.BodyEncoding = Encoding.UTF8;

            message.Subject = Subject;
            message.Body = Body;

            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
            smtp.UseDefaultCredentials = false;
            var credentials = new System.Net.NetworkCredential("telerepo@yahoo.com", "safeneeds");
            smtp.Credentials = credentials;
            smtp.EnableSsl = true;
            smtp.Port = 25;
            string mailState = "State";
            smtp.SendAsync(message, mailState);
        }

        public static String SendMail(
            List<string> sendTo
            , List<string> ccTo
            , List<string> bccTo
            , bool IsBodyHtml
            , string Subject
            , string Body)
        {
            DateTime dateNow = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);
            dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateNow, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

            string logDate = dateNow.ToString("yyyy/MM/dd");
            string logTime = dateNow.ToString("yyyy/MM/dd HH:mm:ss");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("telerepo@yahoo.com", "テレフォンレポート");

            foreach (string sendToAddr in sendTo)
            {
                message.To.Add(new MailAddress(sendToAddr));
            }

            foreach (string ccToAddr in ccTo)
            {
                message.CC.Add(new MailAddress(ccToAddr));
            }

            foreach (string bccToAddr in bccTo)
            {
                message.Bcc.Add(new MailAddress(bccToAddr));
            }

            message.IsBodyHtml = IsBodyHtml;

            message.BodyEncoding = Encoding.UTF8;

            message.Subject = Subject;
            message.Body = Body;

            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
            smtp.UseDefaultCredentials = false;
            var credentials = new System.Net.NetworkCredential("telerepo@yahoo.com", "safeneeds");
            smtp.Credentials = credentials;
            smtp.EnableSsl = true;
            smtp.Port = 25;

            try
            {
                smtp.Send(message);

            }
            catch (Exception ee)
            {
                return ee.InnerException.Message;
            }

            return "OK";
        }
    }
}