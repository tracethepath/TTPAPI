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
        //   AddDeviceTypeInformationData - Http POST Mehtod - Url : api/DeviceType/CreatDeviceTypeInformation?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreatDeviceTypeInformation")]
        public HttpResponseMessage AddDeviceTypeInformationData(DeviceTypeMaster objDeviceTypeMaster, string Token, string AppKey)
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
                        DeviceTypeMaster objDeviceTypeMasters = new DeviceTypeMaster();
                        objDeviceTypeMasters.DeviceTypeDesc = objDeviceTypeMaster.DeviceTypeDesc;
                        DB.DeviceTypeMasters.InsertOnSubmit(objDeviceTypeMasters);
                        DB.SubmitChanges();
                    }
                    strJson = "{\"Result\":\"204\"}";
                    response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                    return response;
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

        //   GetDeviceTypeInformation - Http GET Mehtod - Url : api/DeviceType/DeviceTypeInformationByDeviceTypeId?DeviceTypeId=1&Token=0&AppKey=0
        [HttpGet]
        [ActionName("DeviceTypeInformationByDeviceTypeId")]
        public HttpResponseMessage GetDeviceTypeInformation(string DeviceTypeId, string Token, string AppKey)
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
        //   GetAllDeviceTypeInformation - Http GET Mehtod - Url : api/DeviceType/listOfDeviceTypeInformation?Token=&AppKey=
        [HttpGet]
        [ActionName("listOfDeviceTypeInformation")]
        public HttpResponseMessage GetAllDeviceTypeInformation(string Token, string AppKey)
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
