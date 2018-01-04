using System;
using System.Collections.Generic;
using BeeHRM.Repository;
using System.Linq;
using BeeHRM.ApplicationService.Leave_Module.DTOs;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
   public class RecommenderListMapper
    {

        public static List<RecommenderDTO> RecommenderRepotoRecommenderDTO (IEnumerable<sp_Myrecommender_Result> Record)
        {
            List<RecommenderDTO> Result = new List<RecommenderDTO>();
            foreach(var item in Record)
            {
                RecommenderDTO T = new RecommenderDTO()
                {
                    DsgName = item.DsgName,
                    EmpCode = item.EmpCode,
                    EmpName = item.EmpName
                };


                Result.Add(T);
            }
            return Result;
        }
    }
}
