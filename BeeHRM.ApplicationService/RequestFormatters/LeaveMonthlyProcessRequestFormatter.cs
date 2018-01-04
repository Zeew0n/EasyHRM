using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class LeaveMonthlyProcessRequestFormatter
    {
        public static List<LeaveMonthlyProcessDTO> DomainToModelList(List<LeaveMonthlyProcess> Record)
        {
            List<LeaveMonthlyProcessDTO> ReturnRecord = new List<LeaveMonthlyProcessDTO>();
            foreach (LeaveMonthlyProcess Item in Record)
            {
                LeaveMonthlyProcessDTO Singles = new LeaveMonthlyProcessDTO
                {
                    LeaveYearId = Item.LeaveYearId,
                    ProcessId = Item.ProcessId,
                    MonthNumber = Item.MonthNumber,
                    ProcessByEmpCode = Item.ProcessByEmpCode,
                    ProcessDate = Item.ProcessDate,
                    ProcessStatus = Item.ProcessStatus
                };
                ReturnRecord.Add(Singles);
            }          
            return ReturnRecord;
            
        }
    }
}
