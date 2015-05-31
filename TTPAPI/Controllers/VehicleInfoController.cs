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
    public class VehicleInfoController : ApiController
    {
        //   AddVehicleInfo - Http POST Mehtod - Url : api/VehicleInfo/CreateVehicle?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateVehicle")]
        public HttpResponseMessage CreateVehicle(VehicleMaster objVehicleMaster, string Token, string AppKey)
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

                        VehicleMaster objVehicleMasters = new VehicleMaster();
                            objVehicleMasters.AccountId = objVehicleMaster.AccountId;
                            objVehicleMasters.VehicleID = objVehicleMaster.VehicleID;
                            objVehicleMasters.VehicleRegNo = objVehicleMaster.VehicleRegNo;
                            objVehicleMasters.Make = objVehicleMaster.Make;
                            objVehicleMasters.AccountId = objVehicleMaster.AccountId;
                            objVehicleMasters.IsActive = true;

                            DB.VehicleMasters.InsertOnSubmit(objVehicleMasters);
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

        //   AmendVehicleStatus - Http POST Mehtod - Url : api/VehicleInfo/AmendVehicleStatus?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("AmendVehicleStatus")]
        public HttpResponseMessage AmendVehicleStatus(VehicleMaster objVehicleMaster, string Token, string AppKey)
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


                        var objVehicleMasters = DB.VehicleMasters.Where(x => x.AccountId == objVehicleMaster.AccountId).FirstOrDefault();
                        if (objVehicleMasters != null)
                        {
                            VehicleMaster objvehicaldata = new VehicleMaster();
                            objVehicleMasters.IsActive = false;

                           // DB.VehicleMasters.InsertOnSubmit(objVehicleMasters);
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

        //   VehicleList - Http POST Mehtod - Url : api/VehicleInfo/VehicleList?AccountId=123&Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpGet]
        [ActionName("VehicleList")]
        public HttpResponseMessage VehicleList(string AccountId, string Token, string AppKey)
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


                        var objVehicleMasters = DB.VehicleMasters.Where(x => x.AccountId == AccountId).FirstOrDefault();
                        if (objVehicleMasters != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(objVehicleMasters), Encoding.UTF8, "application/json");
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

        //   VehicleList - Http POST Mehtod - Url : api/VehicleInfo/GetVehicleLocation?VehicleId=123&Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpGet]
        [ActionName("GetVehicleLocation")]
        public HttpResponseMessage VehicleLocation(string VehicleId, string Token, string AppKey)
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


                        var objDevicecurrentlocations = (from objvehicalmap in DB.VehicleDeviceMapDets
                                                 join objDevicecurrentlocation in DB.DeviceCurrentLocations on objvehicalmap.DeviceId equals objDevicecurrentlocation.DeviceId
                                                 where (objvehicalmap.vehicleId == VehicleId)
                                                 select objDevicecurrentlocation).ToList();


                        if (objDevicecurrentlocations != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(objDevicecurrentlocations), Encoding.UTF8, "application/json");
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

        //   AddVehicleInfo - Http POST Mehtod - Url : api/VehicleInfo/MapUsertoVehicle?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("MapUsertoVehicle")]
        public HttpResponseMessage MapUsertoVehicle(UservehicleMapDet objVehicleMaster, string Token, string AppKey)
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

                        UservehicleMapDet objVehicleMasters = new UservehicleMapDet();
                        objVehicleMasters.UserId = objVehicleMaster.UserId;
                        objVehicleMasters.vehicleId = objVehicleMaster.vehicleId;
                        DB.UservehicleMapDets.InsertOnSubmit(objVehicleMasters);
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


    }
}
