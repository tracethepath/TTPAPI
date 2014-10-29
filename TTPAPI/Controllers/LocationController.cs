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
    public class LocationController : ApiController
    {
        //   PostLocation - Http Get Mehtod - Url : api/Location/PostLocation?Lat=23&Long=345&Time=2014-10-05 18:47:59.927&Typeofnetwork=gsm&Token=0f8fad5b-d9cb-469f-a165-70867728950e&DeviceId=1
        [HttpGet]
        [ActionName("PostLocation")]
        public HttpResponseMessage PostLocation(string Lat, string Long, DateTime Time, string Typeofnetwork,string Token, Int32 DeviceId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var PostLocationInfo = DB.DeviceLocationHistories.FirstOrDefault(x => x.DeviceId == DeviceId);
                    if (PostLocationInfo != null)
                    {
                        PostLocationInfo.Lat = Lat;
                        PostLocationInfo.Long = Long;
                        PostLocationInfo.DateTime = Time;
                        PostLocationInfo.Typeofnetwork = Typeofnetwork;
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
        //   GetLocationByDeviceId - Http GET Mehtod - Url : api/Location/GetLocationByDeviceId?Token=0f8fad5b-d9cb-469f-a165-70867728950e&DeviceId=12345678
        [HttpGet]
        [ActionName("GetLocationByDeviceId")]
        public HttpResponseMessage GetLocationByDeviceId(string Token, Int32 DeviceId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var PostLocationInfo = DB.DeviceLocationHistories.FirstOrDefault(x => x.DeviceId == DeviceId);
                    if (PostLocationInfo != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(PostLocationInfo), Encoding.UTF8, "application/json");
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

        //   GetLocationsbyUser - Http POST Mehtod - Url : api/Location/GetLocationsbyUser?Token=0f8fad5b-d9cb-469f-a165-70867728950e&DeviceId=12345678
        [HttpGet]
        [ActionName("GetLocationsbyUser")]
        public HttpResponseMessage GetLocationsbyUser(string Token, string UserId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var LocationByUser = (from objUserDevicMap in DB.UserDeviceMapDets
                                          join objdeviceHistories in DB.DeviceLocationHistories on objUserDevicMap.DeviceId equals objdeviceHistories.DeviceId
                                          where objUserDevicMap.UserId == UserId
                                          select new
                                          {
                                              objdeviceHistories
                                          }).ToList();

                    if (LocationByUser != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(LocationByUser), Encoding.UTF8, "application/json");
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

        //   GetLocationsbyRoute - Http POST Mehtod - Url : api/Location/GetLocationsbyRoute?Token=0f8fad5b-d9cb-469f-a165-70867728950e&DeviceId=12345678
        [HttpGet]
        [ActionName("GetLocationsbyRoute")]
        public HttpResponseMessage GetLocationsbyRoute(string Token, string RouteId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var LocationByUser = (from objUserRouteMapDets in DB.UserRouteMapDets
                                          join objUserDeviceMapDets in DB.UserDeviceMapDets on objUserRouteMapDets.UserId equals objUserDeviceMapDets.UserId
                                          join objdeviceHistories in DB.DeviceLocationHistories on objUserDeviceMapDets.DeviceId equals objdeviceHistories.DeviceId
                                          where objUserRouteMapDets.RouteId == RouteId
                                          select new
                                          {
                                              objdeviceHistories
                                          }).ToList();

                    if (LocationByUser != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(LocationByUser), Encoding.UTF8, "application/json");
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
