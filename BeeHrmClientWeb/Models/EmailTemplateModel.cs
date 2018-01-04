using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class EmailTemplateModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public string Descriptions { get; set; }



    }
}