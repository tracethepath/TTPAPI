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
        //   PostLocation - Http Get Mehtod - Url : api/Location/CreatePostLocation?Lat=23&Long=345&DateTime=2014-10-05 18:47:59.927&Typeofnetwork=gsm&Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0&DeviceId=1
        [HttpGet]
        [ActionName("CreatePostLocation")]
        public HttpResponseMessage AddPostLocation(string Lat, string Long, DateTime DateTime, string Typeofnetwork, string Token, string AppKey, Int32 DeviceId)
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
                        
                            DeviceLocationHistory objDeviceLocationHistory = new DeviceLocationHistory();
                            DeviceCurrentLocation objDevicecurrentlocation = new DeviceCurrentLocation();
                            objDeviceLocationHistory.DeviceId = DeviceId;
                            objDeviceLocationHistory.Lat = Lat;
                            objDeviceLocationHistory.Long = Long;
                            objDeviceLocationHistory.DateTime = DateTime;
                            objDeviceLocationHistory.Typeofnetwork = Typeofnetwork;
                            DB.DeviceLocationHistories.InsertOnSubmit(objDeviceLocationHistory);
                            DB.SubmitChanges();
                            var objcurrentlocation = DB.DeviceCurrentLocations.Where(x => x.DeviceId == DeviceId).FirstOrDefault();
                            if(objcurrentlocation ==null)
                            {
                                objDevicecurrentlocation.CurrentLat = Lat;
                                objDevicecurrentlocation.CurrentLong = Long;
                                objDevicecurrentlocation.DeviceId = DeviceId;
                                objDevicecurrentlocation.UdatedDateTime = DateTime.UtcNow;
                                DB.DeviceCurrentLocations.InsertOnSubmit(objDevicecurrentlocation);
                                DB.SubmitChanges();

                            }
                            else
                            {
                                objcurrentlocation.CurrentLat = Lat;
                                objcurrentlocation.CurrentLong = Long;
                                objcurrentlocation.DeviceId = DeviceId;
                                objcurrentlocation.UdatedDateTime = DateTime.UtcNow;
                                DB.SubmitChanges();
                            }
                            

                            strJson = "{\"Result\":\"204\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                       
                    }
                }
                else
                {
                    strJson = result;
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
        //   GetLocationByDeviceId - Http GET Mehtod - Url : api/Location/DeviceLocationHistoryByDeviceId?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0&DeviceId=12345678
        [HttpGet]
        [ActionName("DeviceLocationHistoryByDeviceId")]
        public HttpResponseMessage GetDeviceLocationHistoryByDeviceId(string Token, string AppKey, Int32 DeviceId)
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
                        var PostLocationInfo = DB.DeviceLocationHistories.Where(x => x.DeviceId == DeviceId).ToList();
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

        //   GetLocationsbyUser - Http GET Mehtod - Url : api/Location/LocationHistorybyUserID?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0&UserId=12345678
       // [HttpGet]
       // [ActionName("LocationHistorybyUserID")]
        //public HttpResponseMessage GetLocationsbyUser(string Token, string AppKey, string UserId)
        //{
        //    string strJson = string.Empty;
        //    var response = this.Request.CreateResponse(HttpStatusCode.OK);
        //    try
        //    {
        //        Accountmeg objaccountmegment = new Accountmeg();
        //        string result = objaccountmegment.Getresult(AppKey, Token);
        //        if (result == "true")
        //        {
        //            using (TTPAPIDataContext DB = new TTPAPIDataContext())
        //            {
        //                var LocationByUser = (from objUserDevicMap in DB.VehicleDeviceMapDets
        //                                      join objdeviceHistories in DB.DeviceLocationHistories on objUserDevicMap.DeviceId equals objdeviceHistories.DeviceId
        //                                      where objUserDevicMap.UserId == UserId
        //                                      select new
        //                                      {
        //                                          Id = objdeviceHistories._id,
        //                                          DeviceId = objdeviceHistories.DeviceId,
        //                                          Lat = objdeviceHistories.Lat,
        //                                          Long = objdeviceHistories.Long,
        //                                          Typeofnetwork = objdeviceHistories.Typeofnetwork,
        //                                      }).ToList();

        //                if (LocationByUser != null)
        //                {
        //                    response.Content = new StringContent(JsonConvert.SerializeObject(LocationByUser), Encoding.UTF8, "application/json");
        //                    return response;
        //                }
        //                else
        //                {
        //                    strJson = "{\"Result\":\"100\"}";
        //                    response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
        //                    return response;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            strJson = "{\"Result\":\"Invalide AppKey\"}";
        //            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strJson = "{\"Result\":\"" + ex.Message + "\"}";
        //        response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
        //        return response;
        //    }
        //}

        //   GetLocationsbyRoute - Http GET Mehtod - Url : api/Location/LocationHistorybyRouteId?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0&RouteId=1234
       // [HttpGet]
       // [ActionName("LocationHistorybyRouteId")]
        //public HttpResponseMessage GetLocationsbyRoute(string Token, string AppKey, string RouteId)
        //{
        //    string strJson = string.Empty;
        //    var response = this.Request.CreateResponse(HttpStatusCode.OK);
        //    try
        //    {
        //        Accountmeg objaccountmegment = new Accountmeg();
        //        string result = objaccountmegment.Getresult(AppKey, Token);
        //        if (result == "true")
        //        {
        //            using (TTPAPIDataContext DB = new TTPAPIDataContext())
        //            {
        //                var LocationByUser = (from objUserRouteMapDets in DB.UserRouteMapDets
        //                                      join objUserDeviceMapDets in DB.VehicleDeviceMapDets on objUserRouteMapDets.UserId equals objUserDeviceMapDets.UserId
        //                                      join objdeviceHistories in DB.DeviceLocationHistories on objUserDeviceMapDets.DeviceId equals objdeviceHistories.DeviceId
        //                                      where objUserRouteMapDets.RouteId == RouteId
        //                                      select new
        //                                      {
        //                                          Id = objdeviceHistories._id,
        //                                          DeviceId = objdeviceHistories.DeviceId,
        //                                          Lat = objdeviceHistories.Lat,
        //                                          Long = objdeviceHistories.Long,
        //                                          Typeofnetwork = objdeviceHistories.Typeofnetwork,
        //                                      }).ToList();

        //                if (LocationByUser != null)
        //                {
        //                    response.Content = new StringContent(JsonConvert.SerializeObject(LocationByUser), Encoding.UTF8, "application/json");
        //                    return response;
        //                }
        //                else
        //                {
        //                    strJson = "{\"Result\":\"100\"}";
        //                    response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
        //                    return response;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            strJson = "{\"Result\":\"Invalide AppKey\"}";
        //            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strJson = "{\"Result\":\"" + ex.Message + "\"}";
        //        response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
        //        return response;
        //    }
        //}

        //   GetLocationsbyRoute - Http GET Mehtod - Url : api/Location/CurrentLocation?Token=0f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0&DeviceId=1234
        [HttpGet]
        [ActionName("CurrentLocation")]
        public HttpResponseMessage GetCurrentLocation(string Token, string AppKey, int DeviceId)
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
                        var getCurrentlocation = DB.DeviceLocationHistories.Where(x => x.DeviceId == DeviceId).OrderByDescending(x => x.DateTime).FirstOrDefault();

                        if (getCurrentlocation != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(getCurrentlocation), Encoding.UTF8, "application/json");
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
