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
    public class AddAppKeyController : ApiController
    {
        //   AddAppKey - Http POST Mehtod - Url : api/AddAppKey/PostAddAppKey?Token=1f8fad5b-d9cb-469f-a165-70867728950e
        [HttpPost]
        [ActionName("PostAddAppKey")]
        public HttpResponseMessage AddAppKey(AccessAppKey objAccessAppKeys, string Token)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                using (TTPAPIDataContext DB = new TTPAPIDataContext())
                {
                    AccessAppKey objAccessAppKey = new AccessAppKey();
                    objAccessAppKey.AccountId = objAccessAppKeys.AccountId;
                    objAccessAppKey.AppKey = Guid.NewGuid();
                    objAccessAppKey.description = objAccessAppKeys.description;
                    DB.AccessAppKeys.InsertOnSubmit(objAccessAppKey);
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
