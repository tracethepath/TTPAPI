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
        //   AddDeviceMasterData - Http POST Mehtod - Url : api/DeviceDetail/CreateDeviceMaster?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateDeviceMaster")]
        public HttpResponseMessage AddDeviceMasterData(DeviceMaster objDeviceMaster, string Token, string AppKey)
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
                        var deviceDublicate = (from dm in DB.DeviceMasters
                                               where dm.DeviceUniqueId == objDeviceMaster.DeviceUniqueId
                                               select dm).FirstOrDefault();
                        if (deviceDublicate == null)
                        {
                            DeviceMaster objDeviceMasters = new DeviceMaster();
                            objDeviceMasters.DeviceId = objDeviceMaster.DeviceId;
                            objDeviceMasters.DeviceModel = objDeviceMaster.DeviceModel;
                            objDeviceMasters.DeviceTypeId = objDeviceMaster.DeviceTypeId;
                            objDeviceMasters.DeviceUniqueId = objDeviceMaster.DeviceUniqueId;
                            DB.DeviceMasters.InsertOnSubmit(objDeviceMasters);
                            DB.SubmitChanges();
                            strJson = "{\"Result\":\"204\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }
                        else
                        {
                            strJson = "{\"Result\":\"Device UniqId Is Already Use\"}";
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


        //   GetDeviceInformation - Http GET Mehtod - Url : api/DeviceDetail/DeviceInformationByDeviceId?DeviceId=1&Token=0&AppKey=0
        [HttpGet]
        [ActionName("DeviceInformationByDeviceId")]
        public HttpResponseMessage DeviceInformation(Int32 DeviceId, string Token, string AppKey)
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
        //   GetDeviceInformation - Http GET Mehtod - Url : api/DeviceDetail/AllDeviceInformation?Token=&AppKey=
        [HttpGet]
        [ActionName("AllDeviceInformation")]
        public HttpResponseMessage AllDeviceInformation(string Token, string AppKey)
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
