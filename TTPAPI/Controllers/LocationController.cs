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
        //   PostLocation - Http POST Mehtod - Url : api/Location?Token=&DeviceId=1&Lat=23&Long=345&Time=
        [System.Web.Http.HttpGet]
        public HttpResponseMessage PostLocation(string Token, Int32 DeviceId, string Lat, string Long,DateTime Time)
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
        //   GetLocation - Http POST Mehtod - Url : api/Location?Token=0f8fad5b-d9cb-469f-a165-70867728950e&DeviceId=12345678
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLocation(string Token, Int32 DeviceId)
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
    }
}
