﻿using System;
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
        //   AddMapMaster - Http GET Mehtod - Url : api/Login/GetLogin?Email=bhavesh@gmail.com&Password=xyz11234
        [HttpGet]
        [ActionName("GetLogin")]
        public HttpResponseMessage GetLogin(string Email, string Password)
        {
            
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    //  UserManagement objuser = new UserManagement();
                    var logininformation = (from objUserContactDets in DB.UserContactDets
                                            join objUserLoginDet in DB.UserLoginDets on objUserContactDets.UserId equals objUserLoginDet.UserId
                                            join objUserManagement in DB.UserManagements on objUserContactDets.UserId equals objUserManagement.UserId
                                            where objUserContactDets.EmailAddress == Email && objUserLoginDet.Password == Password
                                            select new
                                            {
                                                UserId=objUserManagement.UserId,
                                              Token =  String.Format("{0}{1}{2}", objUserManagement.UserId.ToString(),Convert.ToString(objUserManagement.AccountID),Convert.ToString(objUserManagement.RoleId)),
                                            }).FirstOrDefault();
                    AccessTokenCache objAccessTokenCache = new AccessTokenCache();
                    objAccessTokenCache.UserId = logininformation.UserId;
                    objAccessTokenCache.Token = logininformation.Token;
                    objAccessTokenCache.LastAccessDateTime = DateTime.Now;
                    DB.AccessTokenCaches.InsertOnSubmit(objAccessTokenCache);
                    DB.SubmitChanges();

                    strJson = "{\"Token\":\"" + logininformation.Token + "\"}";
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
