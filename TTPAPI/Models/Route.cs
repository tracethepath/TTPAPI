using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTPAPI.Models
{
    public class Route
    {
        public long routeId{get;set;}
        public IList<RouteDetail> RouteDetail { get; set; }
    }
}