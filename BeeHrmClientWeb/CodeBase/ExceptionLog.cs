using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace BeeHrmClientWeb.CodeBase
{
    public class ExceptionLog
    {
        public static void Log(Exception exception)
        {
            StringBuilder subExceptionMessage = new StringBuilder();

            do
            {
                subExceptionMessage.Append("Exception Types " + Environment.NewLine);
                subExceptionMessage.Append(exception.GetType().Name);
                subExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                subExceptionMessage.Append("Message " + Environment.NewLine);
                subExceptionMessage.Append(exception.Message + Environment.NewLine + Environment.NewLine);
                subExceptionMessage.Append("Stack Trace " + Environment.NewLine);
                subExceptionMessage.Append(exception.StackTrace + Environment.NewLine);

                Exception innerexception = exception.InnerException;
            } while (innerexception != null);




            


        }
    }
}