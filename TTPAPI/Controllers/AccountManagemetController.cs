﻿using Newtonsoft.Json;
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
    public class AccountManagemetController : ApiController
    {
        //  AddAccountData - Http POST Mehtod - Url : api/AccountManagemet/PostAddAccountManagemetData?Token=0f8fad5b-d9cb-469f-a165-70867728950e
        [HttpPost]
        [ActionName("PostAddAccountManagemetData")]
        public HttpResponseMessage AddAccountManagemetData(AccountManagemet objSt, string Token)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    AccountManagemet objAccountManagemet = new AccountManagemet();
                    objAccountManagemet.AccountID = objSt.AccountID;
                    objAccountManagemet.AccountName = objSt.AccountName;
                    objAccountManagemet.UpdatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                    objAccountManagemet.UpdatedDateTime = DateTime.Now;
                    objAccountManagemet.AccountDesc = objSt.AccountDesc;
                    DB.AccountManagemets.InsertOnSubmit(objAccountManagemet);
                    DB.SubmitChanges();

                    strJson = "{\"Result\":\"204\"}";
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

        //  UpdateAccount - Http POST Mehtod - Url : api/AccountManagemet/PostUpdateAccountManagement?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0&AccountId=1234
        [HttpPost]
        [ActionName("PostUpdateAccountManagement")]
        public HttpResponseMessage UpdateAccountManagement(AccountManagemet objaccount, string Token, string AppKey, string AccountId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var AccountInformation = DB.AccountManagemets.FirstOrDefault(x => x.AccountID == AccountId);
                    if (AccountInformation != null)
                    {
                        AccountInformation.AccountName = objaccount.AccountName;
                        AccountInformation.UpdatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                        AccountInformation.UpdatedDateTime = DateTime.Now;
                        AccountInformation.AccountDesc = objaccount.AccountDesc;
                        DB.SubmitChanges();

                        strJson = "{\"Result\":\"204\"}";
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
            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }

        //   GetUserInformation - Http GET Mehtod - Url : api/AccountManagemet/GetUserInformation?AccountId=1
        [HttpGet]
        [ActionName("GetUserInformation")]
        public HttpResponseMessage GetUserInformation(string AccountId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var UserInformation = (from objUserManagements in DB.UserManagements
                                           join objUserContactDets in DB.UserContactDets on objUserManagements.UserId equals objUserContactDets.UserId
                                           join objUserDeviceMapDets in DB.UserDeviceMapDets on objUserManagements.UserId equals objUserDeviceMapDets.UserId into g 
                                           //join objUserLoginDets in DB.UserLoginDets on objUserManagements.UserName equals objUserLoginDets.UserId
                                           where objUserManagements.AccountID == AccountId
                                           select new
                                           {
                                               UserId = objUserManagements.UserId,
                                               UserName = objUserManagements.UserName,
                                               RoleId = objUserManagements.RoleId,
                                               AccountID=objUserManagements.AccountID,
                                               PhoneNumber = objUserContactDets.PhoneNumber,
                                               PostalAddress = objUserContactDets.PostalAddress,
                                               EmailAddress = objUserContactDets.EmailAddress,
                                               DeviceId = (Int32?)g.Select(x => x.DeviceId).FirstOrDefault(),
                                               //  LastLoginDateTime=  objUserLoginDets.LastLoginDateTime,
                                               //    UpdatedDateTime=objUserLoginDets.UpdatedDateTime
                                           }).ToList();
                    if (UserInformation != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(UserInformation), Encoding.UTF8, "application/json");
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
