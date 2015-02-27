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
                        return "false";
                    }
                }
                else
                {
                    return "flase";
                }
            }
            //return "false";
        }

    }
}