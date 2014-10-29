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
    public class DeviceDetailController : ApiController
    {
        //   AddRouteInformationData - Http POST Mehtod - Url : api/DeviceDetail/PostAddDeviceMasterData?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("PostAddDeviceMasterData")]
        public HttpResponseMessage AddDeviceMasterData(DeviceMaster objDeviceMaster, string Token,string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    DeviceMaster objDeviceMasters = new DeviceMaster();
                    objDeviceMasters.DeviceId = 123;
                    objDeviceMasters.DeviceModel = objDeviceMaster.DeviceModel;
                    objDeviceMasters.DeviceTypeId =objDeviceMasters.DeviceTypeId;
                    objDeviceMasters.DeviceUniqueId = objDeviceMaster.DeviceUniqueId;
                    DB.DeviceMasters.InsertOnSubmit(objDeviceMasters);                   
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


        //   GetDeviceInformation - Http GET Mehtod - Url : api/DeviceDetail/GetDeviceInformation?DeviceId=1
        [HttpGet]
        [ActionName("GetDeviceInformation")]
        public HttpResponseMessage GetDeviceInformation(Int32 DeviceId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var DeviceInformation = DB.DeviceMasters.FirstOrDefault(x => x.DeviceId == DeviceId);
                    if (DeviceInformation != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(DeviceInformation), Encoding.UTF8, "application/json");
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
        //   GetDeviceInformation - Http GET Mehtod - Url : api/DeviceDetail/GetAllDeviceInformation?Token=&AppKey=
        [HttpGet]
        [ActionName("GetAllDeviceInformation")]
        public HttpResponseMessage GetAllDeviceInformation(string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var DeviceInformation = DB.DeviceMasters.ToList();
                    if (DeviceInformation != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(DeviceInformation), Encoding.UTF8, "application/json");
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
