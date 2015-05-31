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
    public class TrackInfoController : ApiController
    {
        //   AddDeviceTypeInformationData - Http POST Mehtod - Url : api/TrackInfo/InStudentTrack?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("InStudentTrack")]
        public HttpResponseMessage InStudentTrack(InStudentTrack obInStudentTrack, string Token, string AppKey)
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

                        InStudentTrack objInStudentTracks = new InStudentTrack();
                        objInStudentTracks.InDateTime = obInStudentTrack.InDateTime;
                        objInStudentTracks.InLat = obInStudentTrack.InLat;
                        objInStudentTracks.InLong = obInStudentTrack.InLong;
                        objInStudentTracks.StudentId = obInStudentTrack.StudentId;


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

        //   AddDeviceTypeInformationData - Http POST Mehtod - Url : api/TrackInfo/OutStudentTrack?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("OutStudentTrack")]
        public HttpResponseMessage OutStudentTrack(OutStudentTrack obOutStudentTrack, string Token, string AppKey)
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

                        OutStudentTrack objOutStudentTracks = new OutStudentTrack();
                        objOutStudentTracks.OutDateTime = obOutStudentTrack.OutDateTime;
                        objOutStudentTracks.OutLat = obOutStudentTrack.OutLat;
                        objOutStudentTracks.OutLong = obOutStudentTrack.OutLong;
                        objOutStudentTracks.StudentId = obOutStudentTrack.StudentId;


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
