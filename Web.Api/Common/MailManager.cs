using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DataAccess.BLL;
using System.Net.Mail;
using Service.Contracts;
using System.Net;

namespace Web.Api.Common
{
    public static class MailManager
    {
        public static void SendVerificationCode(User user, string body)
        {
            try
            {
                var Subject = "Verification Code";
                SendMail(user.Email, Subject, body);

            }
            catch (Exception ex)
            {

                //todo
            }
        }


        public static void SendVerificationSuccess(User user, string body)
        {
            try
            {
                var Subject = "Verification Success";
                SendMail(user.Email, Subject, body);
            }
            catch (Exception ex)
            {
                //todo
            }
        }

        public static void PinChangedSusseccful(User user, string newPin, string body)
        {
            try
            {
                var Subject = "Pin Reset";
                SendMail(user.Email, Subject, body);
            }
            catch (Exception ex)
            {
                // todo
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public static void SendWelcome(User user, string body)
        {
            try
            {
                var Subject = "Welcome to Smart Reports";
                SendMail(user.Email, Subject, body);
            }
            catch (Exception ex)
            {
                //todo
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendMail(string email, string subject, string body)
        {
            var server = new SmtpClient();
            try
            {
                var msg = new MailMessage();
                msg.To.Add(new MailAddress(email));
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                server.Send(msg);

            }
            catch (Exception ex)
            {
                string exs = ex.Message;

            }
        }
    }
}