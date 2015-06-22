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
    public class TrackeInfoController : ApiController
    {
        //   TrackeIn - Http POST Mehtod - Url : api/TrackeInfo/ScanIn?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("ScanIn")]
        public HttpResponseMessage TrackeIn(InStudentTrack objInStudentTrack, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                //Accountmeg objaccountmegment = new Accountmeg();
                //string result = objaccountmegment.Getresult(AppKey, Token);
                string result = "true";
                if (result == "true")
                {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {

                        InStudentTrack objInStudentTracks = new InStudentTrack();
                        objInStudentTracks.InDateTime = objInStudentTrack.InDateTime;
                        objInStudentTracks.InLat = objInStudentTrack.InLat;
                        objInStudentTracks.InLong = objInStudentTrack.InLong;
                        objInStudentTracks.StudentId = objInStudentTrack.StudentId;
                       
                        DB.InStudentTracks.InsertOnSubmit(objInStudentTracks);
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

        //   TrackeIn - Http POST Mehtod - Url : api/TrackeInfo/ScanOut?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("ScanOut")]
        public HttpResponseMessage TrackeOut(OutStudentTrack objOutStudentTrack, string Token, string AppKey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                //Accountmeg objaccountmegment = new Accountmeg();
                //string result = objaccountmegment.Getresult(AppKey, Token);
                string result = "true";
                if (result == "true")
                {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {

                        OutStudentTrack objOutStudentTracks = new OutStudentTrack();
                        objOutStudentTracks.OutDateTime = objOutStudentTrack.OutDateTime;
                        objOutStudentTracks.OutLat = objOutStudentTrack.OutLat;
                        objOutStudentTracks.OutLong = objOutStudentTrack.OutLong;
                        objOutStudentTracks.StudentId = objOutStudentTrack.StudentId;

                        DB.OutStudentTracks.InsertOnSubmit(objOutStudentTracks);
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
