using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
    public class LeaveAssiginMapper
    {
        public static LeaveAssignedDTOs LaveAssignToLeaveAssignDTO(LeaveAssigned Record)
        {
            LeaveAssignedDTOs result = new LeaveAssignedDTOs()
            {
                AssignedId = Record.AssignedId,
                AssignedDays =Convert.ToDecimal( Record.AssignedDays),
                AssignedLeaveYearId =Convert.ToInt32(Record.AssignedLeaveYearId),
                AssignedRemarks = Record.AssignedRemarks,
                AssignEmpCode = Record.AssignEmpCode,
                AssignLeaveTypeId = Record.AssignLeaveTypeId,
                LeaveGainedMonth =Convert.ToInt32(Record.LeaveGainedMonth),
                LeaveType = new LeaveType
                {
                    IsPayable = Record.LeaveType.IsPayable,
                    Gender = Record.LeaveType.Gender,
                    HalfdayAllow = Record.LeaveType.HalfdayAllow,
                    IsCashable = Record.LeaveType.IsCashable,
                    IsTransferable = Record.LeaveType.IsTransferable,
                    LeaveApplyBefore = Record.LeaveType.LeaveApplyBefore,
                    LeaveCalculation = Record.LeaveType.LeaveCalculation,
                    LeaveDeductionPriority = Record.LeaveType.LeaveDeductionPriority,
                    LeaveRuleDetails = Record.LeaveType.LeaveRuleDetails,
                    LeaveTypeId = Record.LeaveType.LeaveTypeId,
                    LeaveTypeAssignment = Record.LeaveType.LeaveTypeAssignment,
                    LeaveTypeDescription = Record.LeaveType.LeaveTypeDescription,
                    LeaveTypeName = Record.LeaveType.LeaveTypeName,
                    MaritalStatus = Record.LeaveType.MaritalStatus,
                    MaxCashable = Record.LeaveType.MaxCashable,
                    MaxTransferable = Record.LeaveType.MaxTransferable,
                    NumberOfTime = Record.LeaveType.NumberOfTime,
                    LeaveType1 = Record.LeaveType.LeaveType1,
                    ProRataLeaveRatio = Record.LeaveType.ProRataLeaveRatio
                },
                LeaveYear = new LeaveYear
                {
                    YearId = Record.LeaveYear.YearId,
                    LeaveAssigneds = Record.LeaveYear.LeaveAssigneds,
                    YearCurrent = Record.LeaveYear.YearCurrent,
                    YearEndDate = Record.LeaveYear.YearEndDate,
                    YearEndDateNp = Record.LeaveYear.YearEndDateNp,
                    YearName = Record.LeaveYear.YearName,
                    YearStartDate = Record.LeaveYear.YearStartDate,
                    YearStartDateNp = Record.LeaveYear.YearStartDateNp,


                }
            };
            return result;
        }
        public static LeaveAssigned LeaveAsignedDTOtoLeaveAssign(LeaveAssignedDTOs Record)
        {
            LeaveAssigned result = new LeaveAssigned()
            {
                AssignedId = Record.AssignedId,
                AssignedDays = Record.AssignedDays,
                AssignedLeaveYearId = Record.AssignedLeaveYearId,
                AssignedRemarks = Record.AssignedRemarks,
                AssignEmpCode = Record.AssignEmpCode,
                AssignLeaveTypeId = Record.AssignLeaveTypeId,
                LeaveGainedMonth = Record.LeaveGainedMonth,
                
            };
            return result;
        }
        public static List<LeaveAssignedDTOs> LeaveAssignedLeaveAssignedDtoList(List<LeaveAssigned> Record)
        {
            List<LeaveAssignedDTOs> Result = new List<LeaveAssignedDTOs>();
            foreach(var Item in Record)
            {
                LeaveAssignedDTOs single = new LeaveAssignedDTOs()
                {
                    AssignedId = Item.AssignedId,
                    AssignedDays =Convert.ToDecimal(Item.AssignedDays),
                    AssignedLeaveYearId = Convert.ToInt32(Item.AssignedLeaveYearId),
                    AssignedRemarks = Item.AssignedRemarks,
                    AssignEmpCode = Item.AssignEmpCode,
                    AssignLeaveTypeId = Item.AssignLeaveTypeId,
                    LeaveGainedMonth =Convert.ToInt32(Item.LeaveGainedMonth),
                    LeaveType = new LeaveType
                    {
                        IsPayable = Item.LeaveType.IsPayable,
                        Gender = Item.LeaveType.Gender,
                        HalfdayAllow = Item.LeaveType.HalfdayAllow,
                        IsCashable = Item.LeaveType.IsCashable,
                        IsTransferable = Item.LeaveType.IsTransferable,
                        LeaveApplyBefore = Item.LeaveType.LeaveApplyBefore,
                        LeaveAssigneds = Item.LeaveType.LeaveAssigneds,
                        LeaveCalculation = Item.LeaveType.LeaveCalculation,
                        LeaveDeductionPriority = Item.LeaveType.LeaveDeductionPriority,
                        LeaveRuleDetails = Item.LeaveType.LeaveRuleDetails,
                        LeaveTypeId = Item.LeaveType.LeaveTypeId,
                        LeaveTypeAssignment = Item.LeaveType.LeaveTypeAssignment,
                        LeaveTypeDescription = Item.LeaveType.LeaveTypeDescription,
                        LeaveTypeName = Item.LeaveType.LeaveTypeName,
                        MaritalStatus = Item.LeaveType.MaritalStatus,
                        MaxCashable = Item.LeaveType.MaxCashable,
                        MaxTransferable = Item.LeaveType.MaxTransferable,
                        NumberOfTime = Item.LeaveType.NumberOfTime,
                        LeaveType1 = Item.LeaveType.LeaveType1,
                        ProRataLeaveRatio = Item.LeaveType.ProRataLeaveRatio
                    },
                    LeaveYear = new LeaveYear
                    {
                        YearId = Item.LeaveYear.YearId,
                        LeaveAssigneds = Item.LeaveYear.LeaveAssigneds,
                        YearCurrent = Item.LeaveYear.YearCurrent,
                        YearEndDate = Item.LeaveYear.YearEndDate,
                        YearEndDateNp = Item.LeaveYear.YearEndDateNp,
                        YearName = Item.LeaveYear.YearName,
                        YearStartDate = Item.LeaveYear.YearStartDate,
                        YearStartDateNp = Item.LeaveYear.YearStartDateNp,


                    }
                };

                Result.Add(single);
            }
            return Result;
        }

        
    }
}

