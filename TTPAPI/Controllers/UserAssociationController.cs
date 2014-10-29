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
        //   AddDeviceTypeInformationData - Http POST Mehtod - Url : api/UserAssociation/PostAddUserAssociationData?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("PostAddUserAssociationData")]
        public HttpResponseMessage AddUserAssociationData(UserMapDet objUserMapDet, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    UserMapDet objUserMapDets = new UserMapDet();
                    objUserMapDets.PrimaryUserId = objUserMapDet.PrimaryUserId;
                    objUserMapDets.SecondaryUserId = objUserMapDet.SecondaryUserId;
                    objUserMapDets.MapId = objUserMapDet.MapId;
                    objUserMapDets.MappedDateTime = DateTime.Now;
                    objUserMapDets.MappedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());

                    DB.UserMapDets.InsertOnSubmit(objUserMapDets);
                    DB.SubmitChanges();
                }
                strJson = "{\"Result\":\"204\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }

        //   AddAssociateRoutewithUser - Http POST Mehtod - Url : api/UserAssociation/PostAddAssociateRoutewithUser?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("PostAddAssociateRoutewithUser")]
        public HttpResponseMessage AddAssociateRoutewithUser(UserRouteMapDet objUserRouteMapDet, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    UserRouteMapDet objUserRouteMapDets = new UserRouteMapDet();
                    objUserRouteMapDets.RouteId = objUserRouteMapDet.RouteId;
                    objUserRouteMapDets.UserId = objUserRouteMapDet.UserId;
                    objUserRouteMapDets.CreatedDateTime = DateTime.Now;
                    objUserRouteMapDets.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                    objUserRouteMapDets.UpdatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                    objUserRouteMapDets.UpdatedDateTime = DateTime.Now;
                    DB.UserRouteMapDets.InsertOnSubmit(objUserRouteMapDets);
                    DB.SubmitChanges();
                }
                strJson = "{\"Result\":\"204\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }

        //   AddAssociateDevicewithUser - Http POST Mehtod - Url : api/UserAssociation/PostAddAssociateDevicewithUser?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("PostAddAssociateDevicewithUser")]
        public HttpResponseMessage AddAssociateDevicewithUser(UserDeviceMapDet objUserDeviceMapDet, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    UserDeviceMapDet objUserDeviceMapDets = new UserDeviceMapDet();
                    objUserDeviceMapDets.DeviceId = objUserDeviceMapDet.DeviceId;
                    objUserDeviceMapDets.UserId = objUserDeviceMapDet.UserId;
                    objUserDeviceMapDets.MappedDateTime = DateTime.Now;
                    objUserDeviceMapDets.MappedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                    objUserDeviceMapDets.isActive = true;
                    DB.UserDeviceMapDets.InsertOnSubmit(objUserDeviceMapDets);
                    DB.SubmitChanges();
                }
                strJson = "{\"Result\":\"204\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }

        //   DisassociateDevicewithUser - Http POST Mehtod - Url : api/UserAssociation/PostDisassociateDevicewithUser?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("PostDisassociateDevicewithUser")]
        public HttpResponseMessage DisassociateDevicewithUser(UserDeviceMapDet objUserDeviceMapDet, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    UserDeviceMapDet objUserDeviceMapDets = new UserDeviceMapDet();
                    var objDisassociate = (from udm in DB.UserDeviceMapDets
                                           where objUserDeviceMapDet.UserId == udm.UserId
                                           select new { udm }).ToList();
                    if(objDisassociate!=null)
                    {
                        objUserDeviceMapDets.isActive = false;
                        objUserDeviceMapDets.MappedDateTime=DateTime.Now;
                        DB.SubmitChanges();
                    }
                    //objUserDeviceMapDets.DeviceId = objUserDeviceMapDet.DeviceId;
                    //objUserDeviceMapDets.UserId = objUserDeviceMapDet.UserId;
                    //objUserDeviceMapDets.MappedDateTime = DateTime.Now;
                    //objUserDeviceMapDets.MappedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToLongDateString());
                    //objUserDeviceMapDets.isActive = true;
                    //DB.UserDeviceMapDets.InsertOnSubmit(objUserDeviceMapDets);
                    //DB.SubmitChanges();
                }
                strJson = "{\"Result\":\"204\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }
        //   GetUserInformation - Http GET Mehtod - Url : api/UserAssociation/GetListAssociations?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=2322&UserId=
        [HttpGet]
        [ActionName("GetListAssociations")]
        public HttpResponseMessage GetListAssociations(string Token, string AppKey,string UserId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var ListAssociations = (from objuserdevicemap in DB.UserDeviceMapDets
                                           join objUserRouteMap in DB.UserRouteMapDets on objuserdevicemap.UserId equals objUserRouteMap.UserId into g
                                           where objuserdevicemap.UserId==UserId
                                           select new
                                           {
                                               DeviceId=objuserdevicemap.DeviceId,
                                               RouteId=g.Select(x=>x.RouteId).ToList(),
                                              // objuserdevicemap,g,
                                           }).ToList();
                    if (ListAssociations != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(ListAssociations), Encoding.UTF8, "application/json");
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
