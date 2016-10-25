using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    public class MailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}