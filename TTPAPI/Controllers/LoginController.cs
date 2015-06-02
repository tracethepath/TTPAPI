using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TTPAPI.Models;

namespace TTPAPI.Controllers
{
    public class LoginController : ApiController
    {
        //   AddMapMaster - Http GET Mehtod - Url : api/Login/Login?Email=bhavesh@gmail.com&Password=xyz1234&AppKey=F59BE8AB-2514-4F68-922D-A7E17604C0B1
        [HttpGet]
        [ActionName("Login")]
        public HttpResponseMessage GetLogin(string Email, string Password, Guid AppKey)
        {

            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                      UserManagement objuser = new UserManagement();
                     var appKeycheck = DB.AccountManagemets.Where(x => x.AppKey == AppKey).FirstOrDefault();
                     if (appKeycheck != null)
                     {
                         var logininformation = (from objUserContactDets in DB.UserContactDets
                                                 join objUserLoginDet in DB.UserLoginDets on objUserContactDets.UserId equals objUserLoginDet.UserId
                                                 join objUserManagement in DB.UserManagements on objUserContactDets.UserId equals objUserManagement.UserId
                                                 join objaccount in DB.AccountManagemets on objUserManagement.AccountID equals objaccount.AccountID
                                                 where objUserContactDets.EmailAddress == Email && objUserLoginDet.Password == Password && objaccount.AppKey == AppKey
                                                 select new
                                                 {
                                                     UserId = objUserManagement.UserId,
                                                     Token = String.Format("{0}{1}{2}", objUserManagement.UserId.ToString(), Convert.ToString(objUserManagement.AccountID), Convert.ToString(objUserManagement.RoleId)),
                                                 }).FirstOrDefault();
                         if (logininformation != null)
                         {
                             AccessTokenCache objAccessTokenCache = new AccessTokenCache();
                             var checkappkey = DB.AccessTokenCaches.Where(x => x.Token == logininformation.Token).FirstOrDefault();
                             if (checkappkey == null)
                             {
                                 objAccessTokenCache.UserId = logininformation.UserId;
                                 objAccessTokenCache.Token = logininformation.Token;
                                 objAccessTokenCache.LastAccessDateTime = DateTime.Now;
                                 DB.AccessTokenCaches.InsertOnSubmit(objAccessTokenCache);
                                 DB.SubmitChanges();
                             }
                             strJson = "{\"Token\":\"" + logininformation.Token + "\"}";
                             response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                             return response;
                         }
                         else
                         {
                             strJson = "{\"Result\":\"100\"}";
                             response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                             return response;
                         }
                     }
                     else
                     {
                         strJson = "{\"Result\":\"Invalide AppKey\"}";
                         response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                         return response;
                     }
                     
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
