using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class SMS
    {
        public string From { get; set; }
        public string  Token { get; set; }
        public string To { get; set; }
        public string MgsBody { get; set; }
    }
}