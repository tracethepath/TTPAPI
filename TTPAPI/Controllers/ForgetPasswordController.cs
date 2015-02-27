using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using TTPAPI.Models;

namespace TTPAPI.Controllers
{
    public class ForgetPasswordController : ApiController
    {
        //  ForgetPassword - Http GET Mehtod - Url : api/ForgetPassword/ForgetPassword?UserName=xyz&Email=bhavesh9252@gmail.com&Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0
        [HttpGet]
        [ActionName("ForgetPassword")]
        public HttpResponseMessage GetForgetPassword(string UserName, string Email, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                Accountmeg objaccountmegment = new Accountmeg();
                string result = objaccountmegment.Getresult(AppKey, Token);
                if (result == "true")
                {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {
                        var ForgetPassword = DB.UserManagements.Where(x => x.UserName == UserName).FirstOrDefault();
                        if (ForgetPassword != null)
                        {
                            UserLoginDet objUserLoginDet = new UserLoginDet();
                            var userdata = DB.UserLoginDets.Where(x => x.UserId == ForgetPassword.UserId).FirstOrDefault();
                            Random rng = new Random();
                            int Password = rng.Next(1000, 100000000);
                            //  string Password = rng.Next(10).ToString();
                            userdata.Password = Password.ToString();
                            DB.SubmitChanges();
                            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["PORT"]));
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());

                            string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                            string toEmail = Email;
                            MailMessage message = new MailMessage();
                            message.To.Add(toEmail);
                            message.From = new MailAddress(fromEmail, "TTPA");
                            message.IsBodyHtml = true;
                            message.Subject = "Send Password";
                            message.Body = "Hello, <br />";
                            message.Body += "Email Id : " + Email;
                            message.Body += "User Name: " + UserName;
                            message.Body += "<br />Your Password Is: " + Password;
                            message.Body += "<br />Thank You.";
                            smtp.Send(message);
                            strJson = "{\"Result\":\"Password is sent to your mail.\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }
                        else
                        {
                            strJson = "{\"Result\":\"AccountID is Already Use\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }

                    }
                }
                else
                {
                    strJson = "{\"Result\":\"Invalide AppKey\"}";
                    response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                    return response;
                }

            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }

    }
}
