using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace SparrowSMSTest
{
    class Program
    {
        static void Main(string[] args)
        {
             PostSendSMS("abhi@youngminds.com.np", "youngminds2016", "9849732625", "Hello Test Sms");
        }

        private static string PostSendSMS(string from, string token, string to, string text)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["from"] = from;
                values["token"] = token;
                values["to"] = to;
                values["text"] = text;
                var response = client.UploadValues("http://api.sparrowsms.com/v2/sms/", "Post", values);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }
    }
}
