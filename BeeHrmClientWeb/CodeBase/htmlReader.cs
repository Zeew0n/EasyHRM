

using System.IO;
using BeeHrmClientWeb.Models;


namespace BeeHrmClientWeb.CodeBase
{
    public class htmlReader:System.Web.UI.Page
    {
        public string GetHtmlBodyTemplate(EmailTemplateModel emailTemplateData,string msgType)
        {
            string body = string.Empty;
            string filePath = string.Empty;
            if (msgType =="Email")
            {
                filePath = Server.MapPath("~/Views/Shared/EmailTemplate.html");
                StreamReader reader = new StreamReader(filePath);
                body = reader.ReadToEnd();
                body = body.Replace("{UserName}", emailTemplateData.UserName);
                body = body.Replace("{Title}", emailTemplateData.Title);
                body = body.Replace("{Url}", emailTemplateData.Url);
                body = body.Replace("{Description}", emailTemplateData.Descriptions);

            }
            else
            {
                filePath = Server.MapPath("~/Views/Shared/EmailTemplate.html");
                StreamReader reader = new StreamReader(filePath);
                body = reader.ReadToEnd();
                body = body.Replace("{UserName}", emailTemplateData.UserName);
                body = body.Replace("{Title}", emailTemplateData.Title);
                body = body.Replace("{Url}", emailTemplateData.Url);
                body = body.Replace("{Description}", emailTemplateData.Descriptions); 

            }

            return body;
        }
        
    }
}