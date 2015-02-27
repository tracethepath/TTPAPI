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
    public class MapMasterController : ApiController
    {
        //   AddMapMaster - Http POST Mehtod - Url : api/MapMaster/CreateMapMaster?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpPost]
        [ActionName("CreateMapMaster")]
        public HttpResponseMessage AddMapMaster(MapMaster objMapMaster, string Token, string AppKey)
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
                        MapMaster objMapMasters = new MapMaster();
                        objMapMasters.MapName = objMapMaster.MapName;
                        objMapMasters.MapDesc = objMapMaster.MapDesc;
                        objMapMasters.CreatedDateTime = DateTime.Now;
                        objMapMasters.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                        DB.MapMasters.InsertOnSubmit(objMapMasters);
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

        //   GetMap - Http GET Mehtod - Url : api/MapMaster/ListOfMap?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=
        [HttpGet]
        [ActionName("ListOfMap")]
        public HttpResponseMessage GetMap(string Token, string AppKey)
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
                        var MapInformation = DB.MapMasters.ToList();
                        if (MapInformation != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(MapInformation), Encoding.UTF8, "application/json");
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
