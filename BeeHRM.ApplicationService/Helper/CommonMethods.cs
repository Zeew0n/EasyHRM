using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BeeHRM.ApplicationService.Common;
using BeeHRM.ApplicationService.Helper;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Helper
{
    public static class CommonMethods
    {
        /// <summary>
        /// Get Current culture
        /// </summary>
        public static CultureInfo culture = CultureInfo.CurrentCulture;

        /// <summary>
        /// DBContext connection
        /// </summary>
        //public static BeeHRM.Repository.dbBeeHRMEntities DBContext
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Session["DbContext"] == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return System.Web.HttpContext.Current.Session["DbContext"] as BeeHRM.Repository.dbBeeHRMEntities;
        //        }
        //    }

        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["DbContext"] = value;
        //    }
        //}

        /// <summary>
        /// Gets App setting string
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>returns string</returns>
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// Gets Connection string
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>returns connection string</returns>
        public static string GetConnectionString(string key)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[key].ToString();
        }

        /// <summary>
        /// Gets Login response
        /// </summary>yy
        /// <returns>returns login response model</returns>
        //public static LoginResponseModel GetLoginResponse()
        //{
        //    return (HttpContext.Current.Session["LoginResponse"] == null) ? null : (LoginResponseModel)HttpContext.Current.Session["LoginResponse"];
        //    ////return (LoginResponseModel)System.Web.HttpContext.Current.Session["LoginResponse"];
        //}

        /// <summary>
        /// Translates the labels
        /// </summary>
        /// <param name="label">label name</param>
        /// <returns>returns translated string</returns>
        public static string Translate(string label)
        {
            //// Get current culture
            //CultureInfo culture = CultureInfo.CurrentCulture;
            //if (System.Web.HttpContext.Current.Session["TranslationKeyValuePair"] == null)
            //{
            //    var loginResponse = GetLoginResponse();
            //    System.Web.HttpContext.Current.Session["TranslationKeyValuePair"] = new TranslationService(loginResponse.ConnectionString).LoadTranslations(1043);
            //}

            var collection = (Dictionary<string, string>)System.Web.HttpContext.Current.Session["TranslationKeyValuePair"];
            var translation = collection.Where(q => q.Key.ToLower(culture) == label.ToLower(culture)).Select(q => q.Value).SingleOrDefault();
            if (translation == null)
            {
                return label;
            }
            else
            {
                return translation;
            }
        }

        /// <summary>
        /// Inserts log info
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="errorMsg">Error message</param>
        //public static void LogInfo(string connectionString, string errorMsg)
        //{
        //    Log_InfoService logInfo = new Log_InfoService(connectionString);
        //    logInfo.Insert(errorMsg);
        //}

        /// <summary>
        /// Converts string to datetime format
        /// </summary>
        /// <param name="value">Date value</param>
        /// <returns>returns date time</returns>
        public static DateTime? CheckConvertStringToDateTime(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    var date = value.Replace("-", "/").Trim() + " 00:00:00";
                    return DateTime.ParseExact(date, "g", new CultureInfo("fr-FR"));
                }
                catch
                {
                    var date = Convert.ToDateTime(DateTime.ParseExact(value, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                    return date;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts string to date time
        /// </summary>
        /// <param name="value">Date string</param>
        /// <returns>returns datetime</returns>
        public static DateTime ConvertStringToDateTime(string value)
        {
            try
            {
                var date = value.Replace("-", "/").Trim() + " 00:00:00";
                return DateTime.ParseExact(date, "g", new CultureInfo("fr-FR"));
            }
            catch (Exception)
            {
                return Convert.ToDateTime(DateTime.ParseExact(value, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
            }
        }

        /// <summary>
        /// Creates Csv file
        /// </summary>
        /// <typeparam name="T">Entity Class</typeparam>
        /// <param name="list">List of entity</param>
        /// <param name="separator">Seperator field</param>
        /// <returns>Returns string format</returns>
        public static string CreateCsvFromGenericList<T>(List<T> list, string separator)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }

            ////get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;
            StringWriter sw = new StringWriter();

            ////make a new instance of the class name we figured out to get its props
            object o = Activator.CreateInstance(t);
            ////gets all properties
            PropertyInfo[] props = o.GetType().GetProperties();

            ////foreach of the properties in class above, write out properties
            ////this is the header row
            foreach (PropertyInfo pi in props)
            {
                sw.Write(pi.Name.ToUpper(culture) + separator);
            }

            sw.Write(newLine);

            ////this acts as datarow
            foreach (T item in list)
            {
                ////this acts as datacolumn
                foreach (PropertyInfo pi in props)
                {
                    ////this is the row+col intersection (the value)
                    /*string whatToWrite = Convert.ToString(item.GetType()
                                             .GetProperty(pi.Name)
                                             .GetValue(item, null))
                            .Replace(',', ' ') + ','; */
                    string whatToWrite = string.Empty;
                    if (!string.IsNullOrEmpty(Convert.ToString(item.GetType()
                                              .GetProperty(pi.Name)
                                              .GetValue(item, null))))
                    {
                        whatToWrite =
                        Convert.ToString(item.GetType()
                                             .GetProperty(pi.Name)
                                             .GetValue(item, null))
                            .Replace(',', ' ') + ',';
                    }
                    else
                    {
                        whatToWrite = "NULL" + ',';
                    }

                    sw.Write(whatToWrite);
                }

                sw.Write(newLine);
            }

            return sw.ToString();
        }

        /// <summary>
        /// Creates csv file of generic list
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="list">List of entity</param>
        /// <param name="separator">Seperator field</param>
        /// <param name="csvNameWithExt">Csv filename with extension</param>
        public static void CreateCsvFromGenericList<T>(List<T> list, string separator, string csvNameWithExt)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            ////get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            using (var sw = new StreamWriter(csvNameWithExt))
            {
                ////make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                ////gets all properties
                PropertyInfo[] props = o.GetType().GetProperties();

                ////foreach of the properties in class above, write out properties
                ////this is the header row
                foreach (PropertyInfo pi in props)
                {
                    sw.Write(pi.Name.ToUpper(culture) + separator);
                }

                sw.Write(newLine);

                ////this acts as datarow
                foreach (T item in list)
                {
                    ////this acts as datacolumn
                    foreach (PropertyInfo pi in props)
                    {
                        ////this is the row+col intersection (the value)
                        string whatToWrite =
                            Convert.ToString(item.GetType()
                                                 .GetProperty(pi.Name)
                                                 .GetValue(item, null))
                                .Replace(',', ' ') + ',';

                        sw.Write(whatToWrite);
                    }

                    sw.Write(newLine);
                }
            }
        }

        /// <summary>
        /// Method that sorts model ascending
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <param name="collection">Collection of entity</param>
        /// <param name="columnName">Column name for sorting</param>
        /// <returns>returns sorted list</returns>
        public static IEnumerable<TEntity> OrderModel<TEntity>(IEnumerable<TEntity> collection, string columnName) where TEntity : class
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");   // x
            Expression property = Expression.Property(param, columnName);       // x.ColumnName
            Func<TEntity, object> lambda = Expression.Lambda<Func<TEntity, object>>(        // x => x.ColumnName
                    Expression.Convert(property, typeof(object)),
                    param)
                .Compile();

            Func<IEnumerable<TEntity>, Func<TEntity, object>, IEnumerable<TEntity>> expression = (c, f) => c.OrderBy(f); // here you can use OrderByDescending basing on SortDirection

            IEnumerable<TEntity> sorted = expression(collection, lambda);
            return sorted;
        }

        /// <summary>
        /// Method that sorts model descending
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <param name="collection">Collection of entity</param>
        /// <param name="columnName">Column name for sorting</param>
        /// <returns>returns sorted list</returns>
        public static IEnumerable<TEntity> OrderModelByDescending<TEntity>(IEnumerable<TEntity> collection, string columnName) where TEntity : class
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");   //// x
            Expression property = Expression.Property(param, columnName);       //// x.ColumnName
            Func<TEntity, object> lambda = Expression.Lambda<Func<TEntity, object>>(        //// x => x.ColumnName
                    Expression.Convert(property, typeof(object)),
                    param)
                .Compile();

            Func<IEnumerable<TEntity>, Func<TEntity, object>, IEnumerable<TEntity>> expression = (c, f) => c.OrderByDescending(f); // here you can use OrderByDescending basing on SortDirection
            IEnumerable<TEntity> sorted = expression(collection, lambda);

            return sorted;
        }

        /// <summary>
        /// Converts date string from
        /// </summary>
        /// <param name="searchKey">Search key</param>
        /// <param name="dateFrom">From date</param>
        /// <param name="month">Month field</param>
        /// <param name="week">Week field</param>
        /// <returns>returns converted datetime</returns>
        public static DateTime GetConvertedDateFrom(string searchKey, string dateFrom, int month, int week)
        {
            DateTime convDateFrom = new DateTime();
            switch (searchKey)
            {
                case "rDiff":
                    convDateFrom = DateTime.ParseExact(dateFrom, "dd-MM-yyyy", null);
                    break;
                case "rMonth":
                    convDateFrom = new DateTime(DateTime.Now.Year, Convert.ToInt32(month), 1);
                    break;
                case "rWeek":
                    DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    DateTime firstDayInWeek = DateTime.Now;
                    if (week == 2)
                    {
                        firstDayInWeek = firstDayInWeek.AddDays(-7);
                    }

                    while (firstDayInWeek.DayOfWeek != firstDay)
                    {
                        firstDayInWeek = firstDayInWeek.AddDays(-1);
                    }

                    convDateFrom = firstDayInWeek;
                    break;
            }

            return convDateFrom;
        }

        /// <summary>
        /// Converts date string to
        /// </summary>
        /// <param name="searchKey">Search key</param>
        /// <param name="dateTo">To date</param>
        /// <param name="month">Month field</param>
        /// <param name="week">Week field</param>
        /// <returns>returns converted datetime</returns>
        public static DateTime GetConvertedDateTo(string searchKey, string dateTo, int month, int week)
        {
            DateTime convDateTo = new DateTime();

            switch (searchKey)
            {
                case "rDiff":
                    convDateTo = DateTime.ParseExact(dateTo, "dd-MM-yyyy", null);
                    break;
                case "rMonth":
                    DateTime firstDayOfTheMonth = new DateTime(DateTime.Now.Year, Convert.ToInt32(month), 1);
                    convDateTo = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                    break;
                case "rWeek":
                    DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    DateTime firstDayInWeek = DateTime.Now;
                    if (week == 2)
                    {
                        firstDayInWeek = firstDayInWeek.AddDays(-7);
                    }

                    while (firstDayInWeek.DayOfWeek != firstDay)
                    {
                        firstDayInWeek = firstDayInWeek.AddDays(-1);
                    }

                    convDateTo = firstDayInWeek.AddDays(6);
                    break;
            }

            return convDateTo;
        }

        /// <summary>
        /// Creates csv string
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="list">List of entity</param>
        /// <param name="separator">Seperator field</param>
        /// <returns>returns string</returns>
        public static string CsvString<T>(this IList<T> list, string separator)
        {
            ////Variables for build string
            string propStr = string.Empty;
            StringBuilder sb = new StringBuilder();

            ////Get property collection and set selected property list
            PropertyInfo[] props = typeof(T).GetProperties();

            string header = string.Empty;
            foreach (PropertyInfo pi in props)
            {
                header = header + propStr + pi.Name + separator;
            }

            sb.AppendLine(header);

            ////Iterate through data list collection
            foreach (var item in list)
            {
                propStr = string.Empty;
                ////Iterate through property collection
                foreach (var prop in props)
                {
                    ////Construct property name and value string
                    propStr = propStr + prop.GetValue(item, null) + separator;
                }

                sb.AppendLine(propStr);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates csv string
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="list">List of entity</param>
        /// <param name="separator">Seperator field</param>
        /// <param name="filePath">File path</param>
        public static void CsvServerFile<T>(this IList<T> list, string separator, string filePath)
        {
            ////Variables for build string
            ////string propStr = string.Empty;
            ////StringBuilder sb = new StringBuilder();
            ////get type from 0th member

            ////Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            using (var sw = new StreamWriter(filePath))
            {
                PropertyInfo[] props = typeof(T).GetProperties();

                foreach (PropertyInfo pi in props)
                {
                    //// Get current culture
                    CultureInfo culture = CultureInfo.CurrentCulture;
                    sw.Write(pi.Name.ToUpper(culture) + separator);
                }

                sw.Write(newLine);
                foreach (T item in list)
                {
                    foreach (PropertyInfo pi in props)
                    {
                        string whatToWrite =
                            Convert.ToString(item.GetType()
                                                 .GetProperty(pi.Name)
                                                 .GetValue(item, null))
                                .Replace(',', ' ') + ',';

                        sw.Write(whatToWrite);
                    }

                    sw.Write(newLine);
                }
            }
        }

        /// <summary>
        /// Method that converts dataTable to string
        /// </summary>
        /// <param name="list">List of datatablerows</param>
        /// <param name="query">query to be executed</param>
        /// <returns>returns html raw table string</returns>
        public static string DataTableToString(DataTable list, string query)
        {
            ////Variables for build string
            int i = 0;
            string topString = "<table width='100%' align='center' border='0' cellspacing='0'><thead>";
            topString += "<tr class='ListTableHeader'><th class='detail_data' align='left' width='100%' height='20px'>";
            topString += "Query result (Maximum 100, For all results click 'Export')</th></tr></thead>";
            topString += "<tr><td width='100%'>Total Count:" + list.Rows.Count + "</td></tr>";
            topString += "<tr><td width='100%'>Executed Query:" + query + " </td></tr> </table>";

            topString += "<div class='ListTableWrapper' style='border: 1px solid silver; margin: 10px auto 10px auto; width:1198px; overflow: auto; height: 420px;'>";
            string header = "<table  class='noSelect ListTable' border='0' cellpadding='2' cellspacing='0' width='100%'><thead class='noHover'><tr>";
            foreach (DataColumn pi in list.Columns)
            {
                //// Get current culture
                CultureInfo culture = CultureInfo.CurrentCulture;
                header = header + "<th>" + pi.ColumnName.ToUpper(culture) + "</th>";
            }

            header += "</tr></thead><tbody  class='noHover'>";
            foreach (DataRow row in list.Rows)
            {
                header += "<tr>";
                foreach (var item in row.ItemArray)
                {
                    header += "<td align='center'>";
                    header += item.ToString();
                    header += "</td>";
                }

                header += "</tr>";
                i++;
            }

            header += "</tbody></table></div>";
            if (i != 0)
            {
                return topString + header;
            }
            else
            {
                return topString + "No records were returned from this query." + "</div>";
            }
        }

        /// <summary>
        /// Converts csv string to datatable
        /// </summary>
        /// <param name="list">List of datatablerows</param>
        /// <param name="separator">Seperator field</param>
        /// <returns>returns string</returns>
        public static string CsvDataTableString(DataTable list, string separator)
        {
            ////Variables for build string
            string propStr = string.Empty;
            StringBuilder sb = new StringBuilder();

            string header = string.Empty;
            foreach (DataColumn pi in list.Columns)
            {
                header = header + propStr + pi.ColumnName + separator;
            }

            sb.AppendLine(header);

            ////Iterate through data list collection
            foreach (DataRow row in list.Rows)
            {
                propStr = string.Empty;
                ////Iterate through property collection
                foreach (var item in row.ItemArray)
                {
                    ////Construct property name and value string
                    propStr = propStr + item.ToString() + separator;
                }

                sb.AppendLine(propStr);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts csv string to datatable
        /// </summary>
        /// <param name="list">List of datatablerows</param>
        /// <param name="separator">Seperator field</param>
        /// <param name="filePath">File path</param>
        public static void CsvDataTableStringServerFile(DataTable list, string separator, string filePath)
        {
            ////get type from 0th member
            string newLine = Environment.NewLine;
            using (var sw = new StreamWriter(filePath))
            {
                foreach (DataColumn pi in list.Columns)
                {
                    //// Get current culture
                    sw.Write(pi.ColumnName.ToUpper(culture) + separator);
                }

                sw.Write(newLine);
                foreach (DataRow row in list.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        string whatToWrite = item.ToString().Replace(',', ' ') + ',';
                        sw.Write(whatToWrite);
                    }

                    sw.Write(newLine);
                }
            }
        }

        /// <summary>
        /// AutoFill dropdown
        /// </summary>
        /// <param name="helper">Html Helper</param>
        /// <param name="id">id value</param>
        /// <param name="actionUrl">Action url</param>
        /// <param name="selectedText">Selected text</param>
        /// <param name="selectedValue">Selected value</param>
        /// <param name="showSearchButton">Show search button</param>
        /// <returns>returns html string</returns>
        public static HtmlString AutoFill(this HtmlHelper helper, string id, string actionUrl, string selectedText, string selectedValue, bool showSearchButton)
        {
            return AutoFill(helper, id, actionUrl, selectedText, selectedValue, showSearchButton, string.Empty, new Dictionary<string, object>());
        }

        /// <summary>
        /// AutoFill dropdown
        /// </summary>
        /// <param name="helper">Html Helper</param>
        /// <param name="id">id value</param>
        /// <param name="actionUrl">Action url</param>
        /// <param name="selectedText">Selected text</param>
        /// <param name="selectedValue">Selected value</param>
        /// <param name="showSearchButton">Show search button</param>
        /// <param name="buttonImage">Button image</param>
        /// <param name="htmlAttributes">Html attributes</param>
        /// <returns>returns html string</returns>
        public static HtmlString AutoFill(this HtmlHelper helper, string id, string actionUrl, string selectedText, string selectedValue, bool showSearchButton, string buttonImage, IDictionary<string, object> htmlAttributes)
        {
            var attributes = string.Empty;
            foreach (var attribute in htmlAttributes)
            {
                attributes += " " + attribute.Key + "=\"" + attribute.Value + "\"";
            }

            var builder = new StringBuilder();
            builder = builder.AppendFormat("<div id=\"{0}_autofill_wrapper_div\" style=\"display:inline\">", id);
            builder = builder.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />", id, id, selectedValue);
            builder = builder.AppendFormat("<input type=\"text\" id=\"{0}_autofill_text_item\" name=\"{1}_autofill_text_item\" value=\"{2}\" class=\"autofill-text-item\" onkeyup=\"Autofill_TextBox_KeyUp_Event('{3}', '{4}', false);\" {5} />", id, id, selectedText, id, actionUrl, attributes);

            if (showSearchButton)
            {
                if (string.IsNullOrEmpty(buttonImage))
                {
                    builder = builder.AppendFormat("<img id=\"{0}_autofill-search-button\" name=\"{1}_autofill_search_buttom\" class=\"autofill-search-button\" src=\"/content/default/autofill_dropdown.gif\" onclick=\"Autofill_Search_Button_Click('{2}', '{3}');\" />", id, id, id, actionUrl);
                }
                else
                {
                    builder = builder.AppendFormat("<img id=\"{0}_autofill-search-button\" name=\"{1}_autofill_search_buttom\" class=\"autofill-search-button\" src=\"{2}\" onclick=\"Autofill_Search_Button_Click('{3}', '{4}');\" />", id, id, buttonImage, id, actionUrl);
                }
            }

            builder = builder.AppendFormat("<img id=\"{0}_autofill_text_item_loading\" name=\"{1}_autofill_text_item_loading\" class=\"autofill-text-item-loading\" src=\"/content/default/loading-small.gif\" />", id, id);
            builder = builder.Append("</div>");

            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Prepares Table String
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="list">List of entity</param>
        /// <param name="separator">Seperator field</param>
        /// <returns>returns string</returns>
        public static string PrepareTableString<T>(this IList<T> list, string separator)
        {
            ////Variables for build string
            ////string propStr = string.Empty;
            string tableString = "<table width='100%' align='center' border='1' cellspacing='0'><thead>";

            PropertyInfo[] props = typeof(T).GetProperties();

            foreach (PropertyInfo pi in props)
            {
                tableString = tableString + "<th>" + pi.Name.ToUpper(culture) + "</th>";
            }

            tableString += "</thead><tbody  class='noHover'>";

            ////Iterate through data list collection
            foreach (var item in list)
            {
                ////propStr = string.Empty;
                tableString += "<tr>";
                ////Iterate through property collection
                foreach (var prop in props)
                {
                    ////Construct property name and value string
                    tableString += "<td align='center'>";
                    tableString += prop.GetValue(item, null);
                    tableString += "</td>";
                }

                tableString += "</tr>";
            }

            tableString += "</tbody></table>";

            return tableString;
        }

        public static string ConvertDate(string date)
        {
            string strDate;
            int a;
            string[] arrDate;
            if (!string.IsNullOrEmpty(date))
            {
                a = date.IndexOf(" ");
                strDate = date.Substring(0, a).Replace("/", "-");
                arrDate = strDate.Split('-');
                if (a == 8)
                {
                    strDate = '0' + arrDate[0] + '-' + '0' + arrDate[1] + '-' + arrDate[2];
                }
                else if (a == 9)
                {
                    if (arrDate[0].Length == 2)
                    {
                        strDate = '0' + arrDate[1] + '-' + arrDate[0] + '-' + arrDate[2];
                    }
                    else
                    {
                        strDate = arrDate[1] + '-' + '0' + arrDate[0] + '-' + arrDate[2];
                    }
                }
                else
                {
                    strDate = arrDate[1] + '-' + arrDate[0] + '-' + arrDate[2];
                }
            }
            else
            {
                strDate = string.Empty;
            }

            return strDate.Trim();
        }

        /// <summary>
        /// Convert base64 parameter string into UrlParametersModel
        /// </summary>
        /// <param name="base64String">base64string of json parameter</param>
        /// <returns>UrlParametersModel</returns>
        //public static UrlParametersModel GetUrlParameterModel(string base64String)
        //{
        //    byte[] dataByte = Convert.FromBase64String(base64String);
        //    string jsonObj = Encoding.UTF8.GetString(dataByte, 0, dataByte.Length);
        //    return JsonConvert.DeserializeObject<UrlParametersModel>(jsonObj);
        //}

        /// <summary>
        /// private static PAFQTObjects SqlConnection
        /// </summary>
        private static SqlConnection qtObjects = new SqlConnection();

        /// <summary>
        /// Method that gets PAFQTObjects datatable
        /// </summary>
        /// <param name="sqlQuery">sql query to be executed</param>
        /// <returns>returns PAFQTObjects datatable</returns>
        //public static DataTable GetDWHDataTable(string sqlQuery)
        //{
        //    return GetDWHDataset(sqlQuery).Tables["table"];
        //}

        ///// <summary>
        ///// Method that gets PAFQTObjects dataSet
        ///// </summary>
        ///// <param name="sqlQuery">sql query to be executed</param>
        ///// <returns>returns PAFQTObjects dataSet</returns>
        //public static DataSet GetDWHDataset(string sqlQuery)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, GetDWVConnection());
        //    da.SelectCommand.CommandTimeout = 5000;
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "table");
        //    da.Dispose();
        //    return ds;
        //}

        //public static int GetDWHScalarValues(string sqlQuery)
        //{
        //    SqlCommand command = new SqlCommand(sqlQuery, GetDWVConnection());
        //    int value = Convert.ToInt32(command.ExecuteScalar());

        //    return value;
        //}

        /// <summary>
        /// Method that returns PAFQTObjects sql connection
        /// </summary>
        /// <returns>returns PAFQTObjects db connection</returns>
        //public static SqlConnection GetDWVConnection()
        //{
        //    var loginResponse = CommonMethods.GetLoginResponse();

        //    string userName = loginResponse.UserName;
        //    string password = loginResponse.Password;
        //    qtObjects.Close();
        //    qtObjects.Dispose();
        //    qtObjects.ConnectionString = GetConnectionString("SimimDWVConnectionString");
        //    qtObjects.ConnectionString = qtObjects.ConnectionString.Replace("{username}", userName).Replace("{password}", password);
        //    qtObjects.Open();
        //    return qtObjects;
        //}

        public static string CreateExcelFromGenericList<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }

            ////get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;
            StringWriter sw = new StringWriter();

            ////make a new instance of the class name we figured out to get its props
            object o = Activator.CreateInstance(t);
            ////gets all properties
            PropertyInfo[] props = o.GetType().GetProperties();

            ////foreach of the properties in class above, write out properties
            ////this is the header row
            sw.Write("<table border=1>");
            sw.Write(newLine);
            sw.Write("<tr>");
            foreach (PropertyInfo pi in props)
            {
                sw.Write("<td>" + pi.Name.ToUpper(culture) + "</td>");
            }
            sw.Write("</tr>");
            sw.Write(newLine);

            ////this acts as datarow
            foreach (T item in list)
            {
                sw.Write("<tr>");
                ////this acts as datacolumn
                foreach (PropertyInfo pi in props)
                {
                    string whatToWrite = string.Empty;
                    if (!string.IsNullOrEmpty(Convert.ToString(item.GetType()
                                              .GetProperty(pi.Name)
                                              .GetValue(item, null))))
                    {
                        whatToWrite =
                        Convert.ToString(item.GetType()
                                             .GetProperty(pi.Name)
                                             .GetValue(item, null))
                            .Replace(',', ' ');
                    }
                    else
                    {
                        whatToWrite = "NULL";
                    }
                    sw.Write("<td>" + whatToWrite + "</td>");
                }
                sw.Write("</tr>");
                sw.Write(newLine);
            }
            sw.Write("</table>");
            return sw.ToString();
        }
    }
}
