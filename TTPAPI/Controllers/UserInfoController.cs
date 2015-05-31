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
    public class UserInfoController : ApiController
    {
        //   GetUserInfoByRoleInformation - Http GET Mehtod - Url : api/UserInfo/GetListOfUserByRoleId?RoleId=9968cc9a-1616-46c4-8a37-80d1b0f630f3&Token=0&AppKey=0
        [HttpGet]
        [ActionName("GetListOfUserByRoleId")]
        public HttpResponseMessage GetListOfUser(int RoleId, string Token, string AppKey)
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
                        var UserInfoByRole = DB.UserManagements.Where(x => x.RoleId == RoleId).ToList();
                        if (UserInfoByRole != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(UserInfoByRole), Encoding.UTF8, "application/json");
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
