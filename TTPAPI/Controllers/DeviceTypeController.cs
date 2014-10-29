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
    public class DeviceTypeController : ApiController
    {
        //   AddDeviceTypeInformationData - Http POST Mehtod - Url : api/DeviceType/PostAddDeviceTypeInformationData?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("PostAddDeviceTypeInformationData")]
        public HttpResponseMessage AddDeviceTypeInformationData(DeviceTypeMaster objDeviceTypeMaster, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    DeviceTypeMaster objDeviceTypeMasters = new DeviceTypeMaster();
                    objDeviceTypeMasters.DeviceTypeDesc = objDeviceTypeMaster.DeviceTypeDesc;
                    DB.DeviceTypeMasters.InsertOnSubmit(objDeviceTypeMasters);
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

        //   GetDeviceTypeInformation - Http GET Mehtod - Url : api/DeviceType/GetDeviceTypeInformation?DeviceTypeId=1
        [HttpGet]
        [ActionName("GetDeviceTypeInformation")]
        public HttpResponseMessage GetDeviceTypeInformation(Int32 DeviceTypeId)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var DeviceTypeInformation = DB.DeviceTypeMasters.FirstOrDefault(x => x.DeviceTypeId == DeviceTypeId);
                    if (DeviceTypeInformation != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(DeviceTypeInformation), Encoding.UTF8, "application/json");
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
        //   GetAllDeviceTypeInformation - Http GET Mehtod - Url : api/DeviceType/GetAllDeviceTypeInformation?Token=&AppKey=
        [HttpGet]
        [ActionName("GetAllDeviceTypeInformation")]
        public HttpResponseMessage GetAllDeviceTypeInformation(string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    var DeviceTypeInformation = DB.DeviceTypeMasters.ToList();
                    if (DeviceTypeInformation != null)
                    {
                        response.Content = new StringContent(JsonConvert.SerializeObject(DeviceTypeInformation), Encoding.UTF8, "application/json");
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
