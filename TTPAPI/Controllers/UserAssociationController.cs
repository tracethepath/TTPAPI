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

        //   AddAssociateRoutewithUser - Http POST Mehtod - Url : api/UserAssociation/CreateAssociateRoutewithVehical?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateAssociateRoutewithVehical")]
        public HttpResponseMessage AddAssociateRoutewithVehical(vehicleRouteMapDet objUserRouteMapDet, string Token, string AppKey)
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
                        vehicleRouteMapDet objUserRouteMapDets = new vehicleRouteMapDet();
                        objUserRouteMapDets.RouteId = objUserRouteMapDet.RouteId;
                        objUserRouteMapDets.vehicleId = objUserRouteMapDet.vehicleId;
                        objUserRouteMapDets.CreatedDateTime = DateTime.Now;
                        objUserRouteMapDets.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                        objUserRouteMapDets.UpdatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                        objUserRouteMapDets.UpdatedDateTime = DateTime.Now;
                        DB.vehicleRouteMapDets.InsertOnSubmit(objUserRouteMapDets);
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

        //   AddAssociateDevicewithUser - Http POST Mehtod - Url : api/UserAssociation/CreateAssociateDevicewithVehical?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateAssociateDevicewithVehical")]
        public HttpResponseMessage AddAssociateDevicewithUser(VehicleDeviceMapDet objUserDeviceMapDet, string Token, string AppKey)
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
                        var checkUserDeviceMapDet = DB.VehicleDeviceMapDets.Where(x => x.vehicleId == objUserDeviceMapDet.vehicleId).FirstOrDefault();
                        if (checkUserDeviceMapDet == null)
                        {
                            VehicleDeviceMapDet objUserDeviceMapDets = new VehicleDeviceMapDet();
                            objUserDeviceMapDets.DeviceId = objUserDeviceMapDet.DeviceId;
                            objUserDeviceMapDets.vehicleId = objUserDeviceMapDet.vehicleId;
                          //  objUserDeviceMapDets.MappedDateTime = DateTime.Now;
                         //   objUserDeviceMapDets.MappedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                            objUserDeviceMapDets.IsActive = true;
                            DB.VehicleDeviceMapDets.InsertOnSubmit(objUserDeviceMapDets);
                            DB.SubmitChanges();

                            strJson = "{\"Result\":\"204\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }
                        else
                        {
                            strJson = "{\"Result\":\"VehicalId Is Already Use\"}";
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

        //   DisassociateDevicewithUser - Http POST Mehtod - Url : api/UserAssociation/DisassociateDevicewithVehicle?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("DisassociateDevicewithVehicle")]
        public HttpResponseMessage DisassociateDevicewithVehicle(VehicleDeviceMapDet objUserDeviceMapDet, string Token, string AppKey)
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
                        VehicleDeviceMapDet objUserDeviceMapDets = new VehicleDeviceMapDet();
                        var objDisassociate = (from udm in DB.VehicleDeviceMapDets
                                               where objUserDeviceMapDet.vehicleId == udm.vehicleId
                                               select new { udm }).FirstOrDefault();
                        if (objDisassociate != null)
                        {
                            objDisassociate.udm.IsActive = false;
                          //  objDisassociate.udm.MappedDateTime = DateTime.Now;
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
                                        //join objUserContactDets in DB.UserContactDets on objUserManagements.UserId equals objUserContactDets.UserId
                                        join objvehicalmaster in DB.VehicleMasters on objUserManagements.AccountID equals objvehicalmaster.AccountId 
                                        join objvehicaldetail in DB.VehicleDeviceMapDets on objvehicalmaster.VehicleID equals objvehicaldetail.vehicleId
                                        join objdeviceinfo in DB.DeviceMasters on objvehicaldetail.DeviceId equals objdeviceinfo.DeviceId into g
                                        join objRouteConfigMaster in DB.RouteConfigMasters on objvehicalmaster.AccountId equals objRouteConfigMaster.AccountId
                                        join objRoute in DB.RouteDetails on objRouteConfigMaster.RouteId equals objRoute.RouteId into routinelist
                                        join objUserLoginDets in DB.UserLoginDets on objUserManagements.UserId equals objUserLoginDets.UserId
                                        where objUserManagements.UserId == UserId
                                        select new
                                        {
                                            UserId = objUserManagements.UserId,
                                            UserName = objUserManagements.UserName,
                                            RoleId = objUserManagements.RoleId,
                                            AccountID = objUserManagements.AccountID,
                                           // PhoneNumber = objUserContactDets.PhoneNumber,
                                           // PostalAddress = objUserContactDets.PostalAddress,
                                           // EmailAddress = objUserContactDets.EmailAddress,
                                           // PreferredAlert = objUserContactDets.PreferredAlert,
                                            DeviceList = g.ToList(),
                                            RouteList = routinelist.ToList(),
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
