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
    public class RouteController : ApiController
    {
        //   AddRouteData - Http POST Mehtod - Url : api/Route?Token=0f8fad5b-d9cb-469f-a165-70867728950e
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddRouteData(Route objRouteDetail, string Token)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                foreach (var route in objRouteDetail.RouteDetail)
                {
                    RouteDetail objroute = new RouteDetail();
                    objroute.RouteId = objRouteDetail.routeId;
                    objroute.SeqNo = route.SeqNo;
                    objroute.Lat = route.Lat;
                    objroute.Long = route.Long;
                    DB.RouteDetails.InsertOnSubmit(objroute);
                    DB.SubmitChanges();
                }
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
        //   GetLocation - Http POST Mehtod - Url : api/Route?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AccountId=1
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLoction(string Token, string AccountId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var GetLocationInfo = DB.RouteConfigMasters.FirstOrDefault(x => x.AccountId == AccountId);
                    if (GetLocationInfo != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(GetLocationInfo), Encoding.UTF8, "application/json");
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
        //   GetLocationwithlatandlong - Http POST Mehtod - Url : api/Route?Token=0f8fad5b-d9cb-469f-a165-70867728950e&RouteId=123
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLocationwithlatandlong(string Token, long RouteId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var GetLocationInfo = DB.RouteDetails.FirstOrDefault(x => x.RouteId == RouteId);
                    var get = (from n in DB.RouteDetails
                               where n.RouteId == RouteId
                               select new
                               {
                                   SeqNo = n.SeqNo,
                                   Lat = n.Lat,
                                   Long = n.Long,
                               }).ToList();
                    if (GetLocationInfo != null)
                    {
                        response.Content = new StringContent("{\"RouteID\":" +
                                                                 JsonConvert.SerializeObject(GetLocationInfo.RouteId) + "," + "\"RouteDetails\":" + JsonConvert.SerializeObject(get) + "}", Encoding.UTF8, "application/json");
                        return response;
                    }
                    strJson = "{\"Result\":\"100\"}";
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
