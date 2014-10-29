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
    public class RoleController : ApiController
    {
        //   AddRolesData - Http POST Mehtod - Url : api/Role/AddRolesData?Token=0f8fad5b-d9cb-469f-a165-70867728950e
        [HttpPost]
        [ActionName("PostAddRolesData")]
        public HttpResponseMessage AddRolesData(RoleMaster objRole, string Token)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    RoleMaster objRoleMaster = new RoleMaster();
                    objRoleMaster.RoleName = objRole.RoleName;
                    objRoleMaster.RoleDesc = objRole.RoleDesc;
                    objRoleMaster.CreatedDateTime = DateTime.Now;
                    objRoleMaster.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                    DB.RoleMasters.InsertOnSubmit(objRoleMaster);
                    DB.SubmitChanges();

                    strJson = "{\"Result\":\"" + objRoleMaster.RoleId + "\"}";
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

        //   GetRoleInformation - Http GET Mehtod - Url : api/Role/GetRoleInformation?RoleID=1
        [HttpGet]
        [ActionName("GetRoleInformation")]
        public HttpResponseMessage GetRoleInformation(long RoleID)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var RoleInformation = (from objUserManagements in DB.UserManagements
                                           join objUserContactDets in DB.UserContactDets on objUserManagements.UserId equals objUserContactDets.UserId
                                          // join objUserDeviceMapDets in DB.UserDeviceMapDets on objUserManagements.UserId equals objUserDeviceMapDets.UserId into g
                                           //join objUserLoginDets in DB.UserLoginDets on objUserManagements.UserName equals objUserLoginDets.UserId
                                           where objUserManagements.RoleId == RoleID
                                           select new
                                           {
                                              UserId= objUserManagements.UserId,
                                              UserName=objUserManagements.UserName,
                                              RoleId=objUserManagements.RoleId,
                                              AccountID= objUserManagements.AccountID,
                                              PhoneNumber= objUserContactDets.PhoneNumber,
                                              PostalAddress= objUserContactDets.PostalAddress,
                                              EmailAddress= objUserContactDets.EmailAddress,
                                           //   DeviceId = (Int32?)g.Select(x => x.DeviceId).FirstOrDefault(),
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
        //   GetRolewiseUserInformation - Http POST Mehtod - Url : api/Role/GetRolewiseUserInformation?Token=0f8fad5b-d9cb-469f-a165-70867728950e&Appkey=0&RoleID=1
        [HttpGet]
        [ActionName("GetRolewiseUserInformation")]
        public HttpResponseMessage GetRolewiseUserInformation(string Token, string Appkey, long RoleID)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var RoleInformation = DB.RoleMasters.SingleOrDefault(x => x.RoleId == RoleID);
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
