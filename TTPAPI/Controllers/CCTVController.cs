
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
    public class CCTVController : ApiController
    {
        //  AddAccountData - Http POST Mehtod - Url : api/AccountManagemet/CreateNewAccount?Token=0f8fad5b-d9cb-469f-a165-70867728950e
        [HttpPost]
        [ActionName("CreateIPDetails")]
        public HttpResponseMessage createIpDetails(CCTV_Detail objSt, string Token, string Appkey)
        {
            string strJson = string.Empty;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                Accountmeg objaccountmegment = new Accountmeg();
                string result = objaccountmegment.Getresult(Appkey, Token);
                if (result == "true")
                {
                    using (TTPAPIDataContext DB = new TTPAPIDataContext())
                    {
                        if (objSt != null)
                        {
                            var cctvmanagment = DB.CCTV_Details.Where(x => x.AccountID == objSt.AccountID).FirstOrDefault();
                            if (cctvmanagment == null)
                            {
                                CCTV_Detail objCCTVManagemet = new CCTV_Detail();
                                objCCTVManagemet.AccountID = objSt.AccountID;
                                objCCTVManagemet.IP = objSt.IP;
                                objCCTVManagemet.Port = objSt.Port;
                                objCCTVManagemet.Username = objSt.Username;
                                objCCTVManagemet.Password = objSt.Password;
                                DB.CCTV_Details.InsertOnSubmit(objCCTVManagemet);
                                DB.SubmitChanges();

                                strJson = "{\"Result\":\"204\"}";
                                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                                return response;
                            }
                            else
                            {
                                strJson = "{\"Result\":\"AccountID is Already Use\"}";
                                response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                                return response;
                            }
                        }
                        else
                        {
                            strJson = "{\"Result\":\"No Input request was passed in the body\"}";
                            response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                            return response;
                        }

                    }
                }
                else
                {
                    strJson = "{\"Result\":\"Invalid credentials\"}";
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

       
        //   GetCCTVInformation - Http GET Mehtod - Url : api/CCTV/CCTVDetailsbyAccountId?AccountId=1&Token=0&AppKey=0
        [HttpGet]
        [ActionName("CCTVDetailsbyAccountId")]
        public HttpResponseMessage GetCCTVDetailsbyAccountId(string AccountId, string Token, string AppKey)
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
                        var CCTVDetails = (from objcctvdetails in DB.CCTV_Details
                                           where objcctvdetails.AccountID == AccountId
                                               select new
                                               {
                                                   AccountID = objcctvdetails.AccountID,
                                                   IP = objcctvdetails.IP,
                                                   Port = objcctvdetails.Port,
                                                   Username = objcctvdetails.Username,
                                                   Password = objcctvdetails.Password,
                                                   //  LastLoginDateTime=  objUserLoginDets.LastLoginDateTime,
                                                   //    UpdatedDateTime=objUserLoginDets.UpdatedDateTime
                                               }).ToList();
                        if (CCTVDetails != null)
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(CCTVDetails), Encoding.UTF8, "application/json");
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
