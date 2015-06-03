using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTPAPI.Models
{
    public class Accountmeg
    {
        public string tokenid { get; set; }
        public string appkey { get; set; }
        public string UserId { get; set; }

        public string Getresult(string appkey, string tokenid)
        {
            using (TTPAPIDataContext DB = new TTPAPIDataContext())
            {
                string strJson = string.Empty;
                Guid appkeyvalue = Guid.Parse(appkey);
                var checktoken = (from atc in DB.AccessTokenCaches
                                where atc.Token == tokenid
                                select atc.Token).ToList();
                if (checktoken != null && checktoken.Count() !=0)
                {
                    var appkeyvalu = (from am in DB.AccountManagemets
                                      where am.AppKey == appkeyvalue
                                      select am.AppKey).ToList();
                    if (appkeyvalu != null && appkeyvalu.Count() != 0)
                    {

                        return "true";
                    }
                    else
                    {
                        strJson = "{\"Result\":\"Invalide AppKey\"}";
                        // response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                        return strJson;
                    }
                }
                else
                {
                    strJson = "{\"Result\":\"Invalide Token\"}";
                   // response.Content = new StringContent(strJson, Encoding.UTF8, "application/json");
                    return strJson;
                }
            }
            //return "false";
        }

    }
}