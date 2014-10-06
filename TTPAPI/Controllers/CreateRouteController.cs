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
        //   AddRouteInformationData - Http POST Mehtod - Url : api/CreateRoute?Token=1f8fad5b-d9cb-469f-a165-70867728950e
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddRouteInformationData(RouteConfigMaster objRouteConfigMaster, string Token)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    RouteConfigMaster objrouteMaster = new RouteConfigMaster();
                    objrouteMaster.AccountId = Token.Substring(0, 1);
                    objrouteMaster.RouteDesc = objRouteConfigMaster.RouteDesc;
                    objrouteMaster.RouteName = objRouteConfigMaster.RouteName;
                    objrouteMaster.CreatedBy = String.Format("{0}{1}", Token.Substring(0, 15), DateTime.Now.ToString());
                    objrouteMaster.CreatedDateTime = DateTime.Now;
                    DB.RouteConfigMasters.InsertOnSubmit(objrouteMaster);
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
    }
}
