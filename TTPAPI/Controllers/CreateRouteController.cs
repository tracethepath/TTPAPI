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
    public class CreateRouteController : ApiController
    {
        //   AddRouteInformationData - Http POST Mehtod - Url : api/CreateRoute/AddRouteInformationData?Token=1f8fad5b-d9cb-469f-a165-70867728950e&AppKey=0
        [HttpPost]
        [ActionName("AddRouteInformationData")]
        public HttpResponseMessage AddRouteInformationData(RouteConfigMaster objRouteConfigMaster, string Token, string AppKey)
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
                        RouteConfigMaster objrouteMaster = new RouteConfigMaster();
                        objrouteMaster.AccountId = Token.Substring(36, 1);
                        objrouteMaster.RouteDesc = objRouteConfigMaster.RouteDesc;
                        objrouteMaster.RouteName = objRouteConfigMaster.RouteName;
                        objrouteMaster.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 36), DateTime.Now.ToShortDateString());
                        objrouteMaster.CreatedDateTime = DateTime.Now;
                        DB.RouteConfigMasters.InsertOnSubmit(objrouteMaster);
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
    }
}
