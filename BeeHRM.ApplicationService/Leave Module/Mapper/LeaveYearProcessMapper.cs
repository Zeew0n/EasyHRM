using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
   public class LeaveYearProcessMapper
    {
        public static LeaveYearlyProcessDTOs LeaveYearProcessToLeaveYearProcessDTO(LeaveYearlyProcess Record)
        {
            LeaveYearlyProcessDTOs Result = new LeaveYearlyProcessDTOs()
            {
                LeaveYearId=Record.LeaveYearId,
                PrevLeavYearId= Record.PrevLeavYearId,
                ProcessByEmpCode=Convert.ToInt32(Record.ProcessByEmpCode) ,
                ProcessDate=Record.ProcessDate,
                ProcessId=Record.ProcessId
            };
            return Result;
        }
        public static LeaveYearlyProcess LeaveYearProcessDTOToLeaveYearProcess(LeaveYearlyProcessDTOs Record)
        {
            LeaveYearlyProcess Result = new LeaveYearlyProcess()
            {
                LeaveYearId = Record.LeaveYearId,
                PrevLeavYearId = Record.PrevLeavYearId,
                ProcessByEmpCode = Record.ProcessByEmpCode,
                ProcessDate = Record.ProcessDate,
                ProcessId = Record.ProcessId
            };
            return Result;
        }
        public static List<LeaveYearlyProcessDTOs> LeaveYearProcessToLeaveYearProcessDTO(List<LeaveYearlyProcess> Record)
        {
            List<LeaveYearlyProcessDTOs> Result = new List<LeaveYearlyProcessDTOs>();
            foreach(var Item in Record)
            {
                LeaveYearlyProcessDTOs single = new LeaveYearlyProcessDTOs()
                {
                    LeaveYearId = Item.LeaveYearId,
                    PrevLeavYearId = Item.PrevLeavYearId,
                    ProcessByEmpCode = Convert.ToInt32(Item.ProcessByEmpCode),
                    ProcessDate = Item.ProcessDate,
                    ProcessId = Item.ProcessId
                };
                Result.Add(single);
            }            
            return Result;
        }
    }
}
