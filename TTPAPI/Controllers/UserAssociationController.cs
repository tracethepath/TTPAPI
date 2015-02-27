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
    public class UserAssociationController : ApiController
    {
        //   AddDeviceTypeInformationData - Http POST Mehtod - Url : api/UserAssociation/CreateUserAssociation?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateUserAssociation")]
        public HttpResponseMessage AddUserAssociationData(UserMapDet objUserMapDet, string Token, string AppKey)
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
                        var CheckUserMapDetail = DB.UserMapDets.Where(x => x.PrimaryUserId == objUserMapDet.PrimaryUserId).FirstOrDefault();
                        if (CheckUserMapDetail == null)
                        {
                            UserMapDet objUserMapDets = new UserMapDet();
                            objUserMapDets.PrimaryUserId = objUserMapDet.PrimaryUserId;
                            objUserMapDets.SecondaryUserId = objUserMapDet.SecondaryUserId;
                            objUserMapDets.MapId = objUserMapDet.MapId;
                            objUserMapDets.MappedDateTime = DateTime.Now;
                            objUserMapDets.MappedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());

                            DB.UserMapDets.InsertOnSubmit(objUserMapDets);
                            DB.SubmitChanges();

                            strJson = "{\"Result\":\"204\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }

                        else
                        {
                            strJson = "{\"Result\":\"PrimaryUserId Is Already Use\"}";
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

        //   AddAssociateRoutewithUser - Http POST Mehtod - Url : api/UserAssociation/CreateAssociateRoutewithUser?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateAssociateRoutewithUser")]
        public HttpResponseMessage AddAssociateRoutewithUser(UserRouteMapDet objUserRouteMapDet, string Token, string AppKey)
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
                        UserRouteMapDet objUserRouteMapDets = new UserRouteMapDet();
                        objUserRouteMapDets.RouteId = objUserRouteMapDet.RouteId;
                        objUserRouteMapDets.UserId = objUserRouteMapDet.UserId;
                        objUserRouteMapDets.CreatedDateTime = DateTime.Now;
                        objUserRouteMapDets.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                        objUserRouteMapDets.UpdatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                        objUserRouteMapDets.UpdatedDateTime = DateTime.Now;
                        DB.UserRouteMapDets.InsertOnSubmit(objUserRouteMapDets);
                        DB.SubmitChanges();
                        strJson = "{\"Result\":\"204\"}";
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
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }

        //   AddAssociateDevicewithUser - Http POST Mehtod - Url : api/UserAssociation/CreateAssociateDevicewithUser?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateAssociateDevicewithUser")]
        public HttpResponseMessage AddAssociateDevicewithUser(UserDeviceMapDet objUserDeviceMapDet, string Token, string AppKey)
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
                        var checkUserDeviceMapDet = DB.UserDeviceMapDets.Where(x => x.UserId == objUserDeviceMapDet.UserId).FirstOrDefault();
                        if (checkUserDeviceMapDet == null)
                        {
                            UserDeviceMapDet objUserDeviceMapDets = new UserDeviceMapDet();
                            objUserDeviceMapDets.DeviceId = objUserDeviceMapDet.DeviceId;
                            objUserDeviceMapDets.UserId = objUserDeviceMapDet.UserId;
                            objUserDeviceMapDets.MappedDateTime = DateTime.Now;
                            objUserDeviceMapDets.MappedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                            objUserDeviceMapDets.isActive = true;
                            DB.UserDeviceMapDets.InsertOnSubmit(objUserDeviceMapDets);
                            DB.SubmitChanges();

                            strJson = "{\"Result\":\"204\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }
                        else
                        {
                            strJson = "{\"Result\":\"UserId Is Already Use\"}";
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

        //   DisassociateDevicewithUser - Http POST Mehtod - Url : api/UserAssociation/DisassociateDevicewithUser?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("DisassociateDevicewithUser")]
        public HttpResponseMessage DisassociateDevicewithUser(UserDeviceMapDet objUserDeviceMapDet, string Token, string AppKey)
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
                        UserDeviceMapDet objUserDeviceMapDets = new UserDeviceMapDet();
                        var objDisassociate = (from udm in DB.UserDeviceMapDets
                                               where objUserDeviceMapDet.UserId == udm.UserId
                                               select new { udm }).FirstOrDefault();
                        if (objDisassociate != null)
                        {
                            objDisassociate.udm.isActive = false;
                            objDisassociate.udm.MappedDateTime = DateTime.Now;
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
        //   GetUserInformation - Http GET Mehtod - Url : api/UserAssociation/ListAssociationsByUserId?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=2322&UserId=
        [HttpGet]
        [ActionName("ListAssociationsByUserId")]
        public HttpResponseMessage GetListAssociations(string Token, string AppKey, string UserId)
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
                        var UserList = (from objUserManagements in DB.UserManagements
                                        join objUserContactDets in DB.UserContactDets on objUserManagements.UserId equals objUserContactDets.UserId
                                        join objUserDeviceMapDets in DB.UserDeviceMapDets on objUserManagements.UserId equals objUserDeviceMapDets.UserId into g
                                        join objUserDeviceMapDets in DB.UserRouteMapDets on objUserManagements.UserId equals objUserDeviceMapDets.UserId into g1
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
                                            PreferredAlert = objUserContactDets.PreferredAlert,
                                            DeviceList = g.ToList(),
                                            RouteList = g1.ToList(),
                                            // Password = objUserLoginDets.Password,
                                            //  LastLoginDateTime=  objUserLoginDets.LastLoginDateTime,
                                            //   UpdatedDateTime=objUserLoginDets.UpdatedDateTime
                                        }).ToList();
                        ////   var userlist = DB.UserLoginDets.ToList();
                        //   var routelist = DB.UserRouteMapDets.ToList();
                        //   var userdevic = DB.UserDeviceMapDets.ToList();
                        if (UserList != null)
                        {
                            //response.Content = new StringContent("{\"UserList\":" +
                            //                                          JsonConvert.SerializeObject(UserList) + "," + "\"Routelist\":" + JsonConvert.SerializeObject(routelist) + "," + "\"Userdevice\":" + JsonConvert.SerializeObject(userdevic) + "}", Encoding.UTF8, "application/json");
                            //return response;


                            response.Content = new StringContent(JsonConvert.SerializeObject(UserList), Encoding.UTF8, "application/json");
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
