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


                        var objVehicleMasters = DB.VehicleMasters.Where(x => x.AccountId == AccountId).ToList();
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

        //GetVehicleLocationforAccount
        //   VehicleList - Http POST Mehtod - Url : api/VehicleInfo/GetVehicleLocationforAccount?AccountId=123&Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpGet]
        [ActionName("GetVehicleLocationforAccount")]
        public HttpResponseMessage VehicleLocationforAccount(string AccountId, string Token, string AppKey)
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
                        List<VehicleLocation> listObjVh = new List<Controllers.VehicleLocation>();
                        VehicleLocation objVh;


                        var objDevicecurrentlocations = (from objvehicalmap in DB.VehicleDeviceMapDets
                                                         join objDevicecurrentlocation in DB.DeviceCurrentLocations on objvehicalmap.DeviceId equals objDevicecurrentlocation.DeviceId
                                                         join  objvehicles in DB.VehicleMasters on AccountId equals objvehicles.AccountId
                                                         where (objvehicalmap.vehicleId == objvehicles.VehicleID)
                                                         select objDevicecurrentlocation).ToList();
                        foreach (DeviceCurrentLocation cdeviceurrentloc in objDevicecurrentlocations)
                        {
                            objVh = new Controllers.VehicleLocation();
                            objVh.deviceCurrentLocatio = cdeviceurrentloc;
                            objVh.vehicleId = (from objvehicalmap in DB.VehicleDeviceMapDets where (objvehicalmap.DeviceId == cdeviceurrentloc.DeviceId) select objvehicalmap.vehicleId).FirstOrDefault();
                            listObjVh.Add(objVh);
                        }

                       if (listObjVh != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(listObjVh), Encoding.UTF8, "application/json");
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


    }

    public class VehicleLocation
    {
        private DeviceCurrentLocation _deviceCurrentLocation;
        private string _vehicleId;

        public DeviceCurrentLocation deviceCurrentLocatio
        {
            get
            {
                return _deviceCurrentLocation;
            }
            set
            {
                this._deviceCurrentLocation = new DeviceCurrentLocation();
                this._deviceCurrentLocation._id = value._id;
                this._deviceCurrentLocation.CurrentLat = value.CurrentLat;
                this._deviceCurrentLocation.CurrentLong =  value.CurrentLong;
                this._deviceCurrentLocation.DeviceId = value.DeviceId;
                this._deviceCurrentLocation.UpatedDateTime = value.UpatedDateTime;
            }
        }

        public string vehicleId
        {
            get
            {
                return _vehicleId;
            }
            set
            {
                _vehicleId = value;
            }
        }
    }
}
