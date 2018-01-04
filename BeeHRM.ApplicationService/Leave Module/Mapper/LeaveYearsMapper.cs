using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
   public class LeaveYearsMapper
    {
        public static LeaveYearsDTOs LeaveYearsToLeaveYearsDtos(LeaveYear Record)
        {
            LeaveYearsDTOs Result = new LeaveYearsDTOs()
            {
                YearId=Record.YearId,
                YearCurrent= Record.YearCurrent,
                YearName=Convert.ToInt32(Record.YearName),
                YearStartDate= Record.YearStartDate,
                YearEndDate=Convert.ToDateTime(Record.YearEndDate),
                YearEndDateNp=Record.YearEndDateNp,
                YearStartDateNp=Record.YearStartDateNp

            };
            return Result;

        }
        public static LeaveYear  LeaveYearsDtoToLeaveYears(LeaveYearsDTOs Record)
        {
            LeaveYear Result = new LeaveYear()
            {
                YearId = Record.YearId,
                YearCurrent = Record.YearCurrent,
                YearName = Record.YearName,
                YearStartDate =Convert.ToDateTime(Record.YearStartDate),
                YearEndDate = Record.YearEndDate,
                YearEndDateNp = Record.YearEndDateNp,
                YearStartDateNp = Record.YearStartDateNp

            };
            return Result;

        }
        public static List<LeaveYearsDTOs> LeaveYearsListToLeaveYearsDtosList(List<LeaveYear> Record)
        {
            List<LeaveYearsDTOs> Result = new List<LeaveYearsDTOs>();
            foreach(var Item in Record)
            {
                LeaveYearsDTOs single = new LeaveYearsDTOs()
                {
                    YearId = Item.YearId,
                    YearCurrent = Item.YearCurrent,
                    YearName =Convert.ToInt32(Item.YearName),
                    YearStartDate = Item.YearStartDate,
                    YearEndDate = Convert.ToDateTime(Item.YearEndDate),
                    YearEndDateNp = Item.YearEndDateNp,
                    YearStartDateNp = Item.YearStartDateNp
                };
                Result.Add(single);
            }
            
            return Result;

        }
    }
}
