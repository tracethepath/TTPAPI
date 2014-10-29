using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TTPAPI.Models
{
    public class MessageHandler : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                (task) =>
                {
                    Uri address = new Uri(request.RequestUri.ToString());
                    NameValueCollection nvcUri = address.ParseQueryString();
                    HttpResponseMessage response = task.Result;
                    return response;
                }
            );
        }
    }
}