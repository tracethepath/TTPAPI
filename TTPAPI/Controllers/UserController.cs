using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TTPAPI.Models;

namespace TTPAPI.Controllers
{
    public class UserController : ApiController
    {
        //   AddUsersData - Http POST Mehtod - Url : api/User?Token=0f8fad5b-d9cb-469f-a165-70867728950e
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddUsersData(UserCreate objUser, string Token)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    UserManagement objUserManagement = new UserManagement();
                    Guid id = Guid.NewGuid();
                    objUserManagement.UserId = id.ToString();
                    objUserManagement.UserName = objUser.UserName;
                    objUserManagement.AccountID = objUser.AccountID;
                    objUserManagement.CreatedDateTime = DateTime.Now;
                    DB.UserManagements.InsertOnSubmit(objUserManagement);
                    DB.SubmitChanges();

                    UserContactDet objUserContactDet = new UserContactDet();
                    objUserContactDet.EmailAddress = objUser.Email;
                    objUserContactDet.PhoneNumber = objUser.PhoneNumber;
                    objUserContactDet.PostalAddress = objUser.Address;
                    objUserContactDet.UserId = id.ToString();
                    DB.UserContactDets.InsertOnSubmit(objUserContactDet);
                    DB.SubmitChanges();

                    UserLoginDet objUserLoginDet = new UserLoginDet();
                    objUserLoginDet.UpdatedDateTime = DateTime.Now;
                    objUserLoginDet.Password = objUser.Password;
                    objUserLoginDet.UserId = id.ToString();
                    objUserLoginDet.LastLoginDateTime = DateTime.Now;
                    DB.UserLoginDets.InsertOnSubmit(objUserLoginDet);
                    DB.SubmitChanges();

                    strJson = "{\"Result\":\"" + id + "\"}";
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
        //   GetRoleInformation - Http POST Mehtod - Url : api/User?UserId=1
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetRoleInformation(string UserId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var RoleInformation = (from objUserManagements in DB.UserManagements
                                           join objUserContactDets in DB.UserContactDets on objUserManagements.UserId equals objUserContactDets.UserId
                                           //join objUserLoginDets in DB.UserLoginDets on objUserManagements.UserName equals objUserLoginDets.UserId
                                           where objUserManagements.UserId == UserId
                                           select new
                                           {
                                               UserId = objUserManagements.UserId,
                                               UserName = objUserManagements.UserName,
                                               RoleId = objUserManagements.RoleId,
                                               AccountID = objUserManagements.AccountID,
                                               PhoneNumber = objUserContactDets.PhoneNumber,
                                               PostalAddress = objUserContactDets.PostalAddress,
                                               EmailAddress = objUserContactDets.EmailAddress,
                                               //  LastLoginDateTime=  objUserLoginDets.LastLoginDateTime,
                                               //    UpdatedDateTime=objUserLoginDets.UpdatedDateTime
                                           }).ToList();
                    if (RoleInformation != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(RoleInformation), Encoding.UTF8, "application/json");
                        return response;
                    }
                    else
                    {
                        strJson = "{\"Result\":\"100\"}";
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
