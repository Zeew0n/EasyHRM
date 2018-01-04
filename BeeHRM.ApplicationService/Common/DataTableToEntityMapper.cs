using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Common
{
    public class DataTableToEntityMapper
    {

        /// <summary>
        /// Maps external column name in datatable with any data contract
        /// </summary>
        /// <typeparam name="TEntity">Any data contract class</typeparam>
        /// <param name="dt">list of table datas</param>
        /// <returns>returns list of data contract</returns>
        public List<TEntity> Map<TEntity>(DataTable dt) where TEntity : class
        {
            List<TEntity> obj = new List<TEntity>();
            Type t = typeof(TEntity);

            foreach (DataRow dr in dt.Rows)
            {
                TEntity item = (TEntity)Activator.CreateInstance<TEntity>();

                foreach (DataColumn dc in dr.Table.Columns)
                {
                    //// if value is not null, map respective propery
                    if (!(dr[dc.ToString()] is DBNull))
                    {
                        PropertyInfo p = item.GetType().GetProperty(dc.ColumnName);
                        if (p != null && dr[dc.ToString()] != DBNull.Value)
                        {
                            try
                            {
                                p.SetValue(item, dr[dc.ToString()], null);
                            }
                            catch
                            {
                                if (dc.DataType.ToString().Contains("DateTime"))
                                {
                                    string d = Convert.ToDateTime(dr[dc.ToString()]).ToString("dd-MM-yyyy");
                                    p.SetValue(item, d, null);
                                }
                                else
                                {
                                    p.SetValue(item, Convert.ChangeType(dr[dc.ToString()], p.PropertyType), null);
                                }
                            }
                        }

                    }
                }

                obj.Add(item);
            }

            return obj;
        }
    }
}
