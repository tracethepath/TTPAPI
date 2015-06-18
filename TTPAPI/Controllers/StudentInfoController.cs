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
    public class StudentInfoController : ApiController
    {
        //   CreateStudent - Http POST Mehtod - Url : api/StudentInfo/CreateStudent?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateStudent")]
        public HttpResponseMessage CreateStudent(StudentMaster objStudentMaster, string Token, string AppKey)
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

                        StudentMaster objStudentMasters = new StudentMaster();
                        objStudentMasters.AccountID = objStudentMaster.AccountID;
                        objStudentMasters.Class = objStudentMaster.Class;
                        objStudentMasters.QRCodeFileName = objStudentMaster.QRCodeFileName;
                        objStudentMasters.QRCodeFilePath = objStudentMaster.QRCodeFilePath;
                        objStudentMasters.RollNumber = objStudentMaster.RollNumber;
                        objStudentMasters.Division = objStudentMaster.Division;
                        objStudentMasters.Name = objStudentMaster.Name;
                        objStudentMasters.StudentID = objStudentMaster.StudentID;
                        objStudentMasters.IsDelete = true;

                        DB.StudentMasters.InsertOnSubmit(objStudentMasters);
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

        //   AmendStudent - Http POST Mehtod - Url : api/StudentInfo/AmendStudent?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("AmendStudent")]
        public HttpResponseMessage AmendStudent(StudentMaster objStudentMaster, string Token, string AppKey)
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
                        var objStudentMasters = DB.StudentMasters.Where(x => x.AccountID == objStudentMaster.AccountID && x.Class == objStudentMaster.Class && x.Name == objStudentMaster.Name).FirstOrDefault();
                        if (objStudentMasters != null)
                        {
                            StudentMaster objvehicaldata = new StudentMaster();
                            objStudentMasters.IsDelete = false;

                            // DB.StudentMasters.InsertOnSubmit(objStudentMasters);
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

        //   StudentList - Http POST Mehtod - Url : api/StudentInfo/StudentList?VehicleId=&AppKey=
        [HttpPost]
        [ActionName("StudentList")]
        public HttpResponseMessage StudentList(StudentMaster objstudentlist, string VehicleId, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
               // Accountmeg objaccountmegment = new Accountmeg();
               // string result = objaccountmegment.Getresult(AppKey);
               // if (result == "true")
               // {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {

                        if (objstudentlist.Division != null && objstudentlist.Division!="" && objstudentlist.Class != null)
                        {

                            var objStudentMasters = (from objstudentMaster in DB.StudentMasters
                                                     join objstudentVehiclemap in DB.StudentVehicleMapDetails on objstudentMaster.StudentID equals objstudentVehiclemap.StudentID
                                                     where objstudentVehiclemap.VehicleId == VehicleId && objstudentMaster.AccountID == objstudentlist.AccountID && objstudentMaster.Division == objstudentlist.Division && objstudentMaster.Class==objstudentlist.Class
                                                     select new { objstudentMaster }).ToList();
                                
                                
                            if (objStudentMasters != null)
                            {
                                response.Content = new StringContent(JsonConvert.SerializeObject(objStudentMasters), Encoding.UTF8, "application/json");
                                return response;
                            }
                            else
                            {
                                strJson = "{\"Result\":\"100\"}";
                                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                                return response;
                            }
                        }
                        else if (objstudentlist.Division != null && objstudentlist.Division != "")
                        {
                            var objStudentMasters = (from objstudentMaster in DB.StudentMasters
                                                     join objstudentVehiclemap in DB.StudentVehicleMapDetails on objstudentMaster.StudentID equals objstudentVehiclemap.StudentID
                                                     where objstudentVehiclemap.VehicleId == VehicleId && objstudentMaster.AccountID == objstudentlist.AccountID && objstudentMaster.Division == objstudentlist.Division
                                                     select new { objstudentMaster }).ToList();
                            if (objStudentMasters != null)
                            {
                                response.Content = new StringContent(JsonConvert.SerializeObject(objStudentMasters), Encoding.UTF8, "application/json");
                                return response;
                            }
                            else
                            {
                                strJson = "{\"Result\":\"100\"}";
                                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                                return response;
                            }
                        }
                        else if (objstudentlist.Class != null)
                        {
                            var objStudentMasters = (from objstudentMaster in DB.StudentMasters
                                                     join objstudentVehiclemap in DB.StudentVehicleMapDetails on objstudentMaster.StudentID equals objstudentVehiclemap.StudentID
                                                     where objstudentVehiclemap.VehicleId == VehicleId && objstudentMaster.AccountID == objstudentlist.AccountID && objstudentMaster.Class == objstudentlist.Class
                                                     select new { objstudentMaster }).ToList();
                            if (objStudentMasters != null)
                            {
                                response.Content = new StringContent(JsonConvert.SerializeObject(objStudentMasters), Encoding.UTF8, "application/json");
                                return response;
                            }
                            else
                            {
                                strJson = "{\"Result\":\"100\"}";
                                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                                return response;
                            }
                        }
                        else
                        {
                            var objStudentMasters = (from objstudentMaster in DB.StudentMasters
                                                     join objstudentVehiclemap in DB.StudentVehicleMapDetails on objstudentMaster.StudentID equals objstudentVehiclemap.StudentID
                                                     where objstudentVehiclemap.VehicleId == VehicleId && objstudentMaster.AccountID == objstudentlist.AccountID
                                                     select new { objstudentMaster }).ToList();
                                
                            if (objStudentMasters != null)
                            {
                                response.Content = new StringContent(JsonConvert.SerializeObject(objStudentMasters), Encoding.UTF8, "application/json");
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

              //  }
              ////  else
              //  {
              //      strJson = "{\"Result\":\"Invalide AppKey\"}";
              //      response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
              //      return response;
              //  }
            }
            catch (Exception ex)
            {
                strJson = "{\"Result\":\"" + ex.Message + "\"}";
                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                return response;
            }
        }


        //   SudentvehicalMap - Http POST Mehtod - Url : api/StudentInfo/SudentvehicalMap?VehicalId=3&Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
       [ActionName("SudentvehicalMap")]
        public HttpResponseMessage SudentvehicalMapdata(StudentList objstudentlist,string VehicalId, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                
                Accountmeg objaccountmegment = new Accountmeg();
                string result = objaccountmegment.Getresult(AppKey, Token);
                string[] StudentList = objstudentlist.StudentID.Split(',');
                if (result == "true")
                {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {

                        StudentMaster objStudentMasters = new StudentMaster();

                        foreach (var item in StudentList)
                        {
                            StudentVehicleMapDetail objStudentVehicleMapDetail = new StudentVehicleMapDetail();
                             objStudentVehicleMapDetail.StudentID = (Convert.ToString(item.ToString()));
                            objStudentVehicleMapDetail.VehicleId = VehicalId;
                            DB.StudentVehicleMapDetails.InsertOnSubmit(objStudentVehicleMapDetail);
                            DB.SubmitChanges();
                        }
                       
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

        //   GetStudentLocation - Http POST Mehtod - Url : api/StudentInfo/GetStudentLocation?studentId=123&AppKey=
        [HttpGet]
        [ActionName("GetStudentLocation")]
        public HttpResponseMessage StudentLocation(string studentId,string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
               // Accountmeg objaccountmegment = new Accountmeg();
               // string result = objaccountmegment.Getresult(AppKey, Token);
               // if (result == "true")
               // {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {


                        var objInStudentTracks = DB.InStudentTracks.Where(x => x.StudentId == studentId).ToList();
                        
                        if (objInStudentTracks != null)
                        {
                             
                            var objOutstudentTrack = DB.OutStudentTracks.Where(x => x.StudentId == studentId).ToList();
                            
                            if (objOutstudentTrack != null)
                            {
                                //response.Content = new StringContent("{\"InStudentTracks\":" +JsonConvert.SerializeObject(objInStudentTracks) + "," + "\"OutStudentTracks\":" + JsonConvert.SerializeObject(objOutstudentTrack)+"}", Encoding.UTF8, "application/json");
                                response.Content = new StringContent("{\"InStudentTracks\":" + JsonConvert.SerializeObject(objInStudentTracks.ToArray().Last()) + "," + "\"OutStudentTracks\":" + JsonConvert.SerializeObject(objOutstudentTrack.ToArray().Last()) + "}", Encoding.UTF8, "application/json");
                                return response;
                            }
                            else
                            {
                                response.Content = new StringContent(JsonConvert.SerializeObject(objInStudentTracks.ToArray().Last()), Encoding.UTF8, "application/json");
                                return response;
                            }
                        }
                        else
                        {
                            strJson = "{\"Result\":\"100\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }
                    }
              //  }
                //else
                //{
                //    strJson = "{\"Result\":\"Invalide AppKey\"}";
                //    response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                //    return response;
                //}
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
