using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
   public class LeaveMonthlyProcessMapper
    {
        public static LeaveMonthlyProcessDTOs LeaveMonthlyProcessToLeaveMonthlyProcessDTO(LeaveMonthlyProcess Record)
        {
            LeaveMonthlyProcessDTOs Result = new LeaveMonthlyProcessDTOs()
            {
                LeaveYear = new LeaveYear
                {
                    YearId = Record.LeaveYear.YearId,
                    LeaveAssigneds = Record.LeaveYear.LeaveAssigneds,
                    LeaveMonthlyProcesses = Record.LeaveYear.LeaveMonthlyProcesses,
                    YearCurrent = Record.LeaveYear.YearCurrent,
                    YearEndDate = Record.LeaveYear.YearEndDate,
                    YearEndDateNp = Record.LeaveYear.YearEndDateNp,
                    YearName = Record.LeaveYear.YearName,
                    YearStartDate = Record.LeaveYear.YearStartDate,
                    YearStartDateNp = Record.LeaveYear.YearStartDateNp,


                },
                LeaveYearId = Record.LeaveYearId,
                MonthNumber = Record.MonthNumber,
                ProcessByEmpCode = Record.ProcessByEmpCode,
                ProcessDate = Record.ProcessDate,
                ProcessId = Record.ProcessId,
                ProcessStatus = Record.ProcessStatus

            };
            return Result;
        }
        public static LeaveMonthlyProcess LeaveMonthlyDtoToMonthlyProcess(LeaveMonthlyProcessDTOs Record)
        {
            LeaveMonthlyProcess Result= new LeaveMonthlyProcess()
            {
                LeaveYear = new LeaveYear
                {
                    YearId = Record.LeaveYear.YearId,
                    LeaveAssigneds = Record.LeaveYear.LeaveAssigneds,
                    LeaveMonthlyProcesses = Record.LeaveYear.LeaveMonthlyProcesses,
                    YearCurrent = Record.LeaveYear.YearCurrent,
                    YearEndDate = Record.LeaveYear.YearEndDate,
                    YearEndDateNp = Record.LeaveYear.YearEndDateNp,
                    YearName = Record.LeaveYear.YearName,
                    YearStartDate = Record.LeaveYear.YearStartDate,
                    YearStartDateNp = Record.LeaveYear.YearStartDateNp,


                },
                LeaveYearId = Record.LeaveYearId,
                MonthNumber = Record.MonthNumber,
                ProcessByEmpCode = Record.ProcessByEmpCode,
                ProcessDate = Record.ProcessDate,
                ProcessId = Record.ProcessId,
                ProcessStatus = Record.ProcessStatus

            };
            return Result;
        }
        public static List<LeaveMonthlyProcessDTOs> ListLeaveMonthlyProcessToMonthlyProcessDTO(List<LeaveMonthlyProcess> Record)
        {
            List<LeaveMonthlyProcessDTOs> Result = new List<LeaveMonthlyProcessDTOs>();
            foreach(var Item in Record)
            {
                LeaveMonthlyProcessDTOs Singles = new LeaveMonthlyProcessDTOs()
                {
                    LeaveYear = new LeaveYear
                    {
                        YearId = Item.LeaveYear.YearId,
                        LeaveAssigneds = Item.LeaveYear.LeaveAssigneds,
                        LeaveMonthlyProcesses = Item.LeaveYear.LeaveMonthlyProcesses,
                        YearCurrent = Item.LeaveYear.YearCurrent,
                        YearEndDate = Item.LeaveYear.YearEndDate,
                        YearEndDateNp = Item.LeaveYear.YearEndDateNp,
                        YearName = Item.LeaveYear.YearName,
                        YearStartDate = Item.LeaveYear.YearStartDate,
                        YearStartDateNp = Item.LeaveYear.YearStartDateNp,


                    },
                    LeaveYearId = Item.LeaveYearId,
                    MonthNumber = Item.MonthNumber,
                    ProcessByEmpCode = Item.ProcessByEmpCode,
                    ProcessDate = Item.ProcessDate,
                    ProcessId = Item.ProcessId,
                    ProcessStatus = Item.ProcessStatus

                };
                Result.Add(Singles);
            }
            return Result;

        }
    }
}
