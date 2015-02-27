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
        //   AddUsersData - Http POST Mehtod - Url : api/User/NewRegistration?Token=0f8fad5b-d9cb-469f-a165-70867728950e
        [HttpPost]
        [ActionName("NewRegistration")]
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
                    objUserManagement.RoleId = objUser.RoleId;
                    objUserManagement.CreatedDateTime = DateTime.Now;
                    DB.UserManagements.InsertOnSubmit(objUserManagement);
                    DB.SubmitChanges();

                    UserContactDet objUserContactDet = new UserContactDet();
                    objUserContactDet.EmailAddress = objUser.Email;
                    objUserContactDet.PhoneNumber = objUser.PhoneNumber;
                    objUserContactDet.PostalAddress = objUser.Address;
                    objUserContactDet.UserId = id.ToString();
                    objUserContactDet.PreferredAlert = objUser.PreferredAlert;
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
        //   GetRoleInformation - Http GET Mehtod - Url : api/User/UserInformationByUserId?UserId=9968cc9a-1616-46c4-8a37-80d1b0f630f3&Token=0&AppKey=0
        [HttpGet]
        [ActionName("UserInformationByUserId")]
        public HttpResponseMessage GetRoleInformation(string UserId, string Token, string AppKey)
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
                        var RoleInformation = (from objUserManagements in DB.UserManagements
                                               join objUserContactDets in DB.UserContactDets on objUserManagements.UserId equals objUserContactDets.UserId
                                               join objUserDeviceMapDets in DB.UserDeviceMapDets on objUserManagements.UserId equals objUserDeviceMapDets.UserId into g
                                               join objUserLoginDets in DB.UserLoginDets on objUserManagements.UserId equals objUserLoginDets.UserId
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
                                                   DeviceId = (Int32?)g.Select(x => x.DeviceId).FirstOrDefault(),
                                                   Password = objUserLoginDets.Password,
                                                   //  LastLoginDateTime=  objUserLoginDets.LastLoginDateTime,
                                                   //   UpdatedDateTime=objUserLoginDets.UpdatedDateTime
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

        //   UpdateProfile - Http POST Mehtod - Url : api/User/UpdateProfile?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0
        [HttpPost]
        [ActionName("UpdateProfile")]
        public HttpResponseMessage UpdateProfile(UserCreate objUser, string Token, string AppKey)
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
                        UserManagement objUserManagement = new UserManagement();
                        var checkuser = DB.UserManagements.Where(x => x.UserId == objUser.UserId).FirstOrDefault();
                        if (checkuser != null)
                        {

                            checkuser.UserName = objUser.UserName;
                            checkuser.AccountID = objUser.AccountID;
                            checkuser.RoleId = objUser.RoleId;
                            checkuser.CreatedDateTime = DateTime.Now;
                            // DB.UserManagements.InsertOnSubmit(objUserManagement);
                            DB.SubmitChanges();

                            UserContactDet objUserContactDet = new UserContactDet();
                            var objusercontact = DB.UserContactDets.Where(x => x.UserId == objUser.UserId).FirstOrDefault();
                            objusercontact.EmailAddress = objUser.Email;
                            objusercontact.PhoneNumber = objUser.PhoneNumber;
                            objusercontact.PreferredAlert = objUser.PreferredAlert;
                            objusercontact.PostalAddress = objUser.Address;
                            DB.SubmitChanges();

                            UserLoginDet objUserLoginDet = new UserLoginDet();
                            var objloginDet = DB.UserLoginDets.Where(x => x.UserId == objUser.UserId).FirstOrDefault();
                            objloginDet.UpdatedDateTime = DateTime.Now;
                            objloginDet.Password = objUser.Password;
                            //objUserLoginDet.LastLoginDateTime = DateTime.Now;
                            //  DB.UserLoginDets.InsertOnSubmit(objUserLoginDet);
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
