using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
   public class LeaveTypesMapper
    {
        public static LeaveTypesDTOs LeaveTypeToLeaveTypesDTO(LeaveType Record )
        {
            LeaveTypesDTOs Result = new LeaveTypesDTOs()
            {
                IsPayable = Record.IsPayable,
                Gender = Record.Gender,
                HalfdayAllow = Record.HalfdayAllow,
                IsCashable = Record.IsCashable,
                IsTransferable = Record.IsTransferable,
                LeaveApplyBefore = Record.LeaveApplyBefore,              
                LeaveCalculation = Record.LeaveCalculation,
                LeaveDeductionPriority = Record.LeaveDeductionPriority,                
                LeaveTypeId = Record.LeaveTypeId,
                LeaveTypeAssignment = Record.LeaveTypeAssignment,
                LeaveTypeDescription = Record.LeaveTypeDescription,
                LeaveTypeName = Record.LeaveTypeName,
                MaritalStatus = Record.MaritalStatus,
                MaxCashable = Record.MaxCashable,
                MaxTransferable = Record.MaxTransferable,
                NumberOfTime = Record.NumberOfTime,
                LeaveType = Record.LeaveType1,
                ProRataLeaveRatio = Record.ProRataLeaveRatio
            };
            return Result;
        }

        public static LeaveType LeaveTypeDTOToLeaveTypes(LeaveTypesDTOs Record)
        {
            LeaveType Result= new LeaveType()
            {
                IsPayable = Record.IsPayable,
                Gender = Record.Gender,
                HalfdayAllow =Convert.ToBoolean(Record.HalfdayAllow),
                IsCashable = Record.IsCashable,
                IsTransferable = Record.IsTransferable,
                LeaveApplyBefore = Record.LeaveApplyBefore,                
                LeaveCalculation = Record.LeaveCalculation,
                LeaveDeductionPriority = Record.LeaveDeductionPriority,               
                LeaveTypeId = Record.LeaveTypeId,
                LeaveTypeAssignment = Record.LeaveTypeAssignment,
                LeaveTypeDescription = Record.LeaveTypeDescription,
                LeaveTypeName = Record.LeaveTypeName,
                MaritalStatus = Record.MaritalStatus,
                MaxCashable = Record.MaxCashable,
                MaxTransferable = Record.MaxTransferable,
                NumberOfTime = Record.NumberOfTime,
                LeaveType1 = Record.LeaveType,
                ProRataLeaveRatio = Record.ProRataLeaveRatio
            };
            return Result;
        }
        public static List<LeaveTypesDTOs> LeaveTypeListToLeaveTypesDTOList(List<LeaveType> Record)
        {
            List<LeaveTypesDTOs> Result = new List<LeaveTypesDTOs>();
            foreach(var Item in Record)
            {
                LeaveTypesDTOs single = new LeaveTypesDTOs()
                {
                    IsPayable = Item.IsPayable,
                    Gender = Item.Gender,
                    HalfdayAllow = Item.HalfdayAllow,
                    IsCashable = Item.IsCashable,
                    IsTransferable = Item.IsTransferable,
                    LeaveApplyBefore = Item.LeaveApplyBefore,                   
                    LeaveCalculation = Item.LeaveCalculation,
                    LeaveDeductionPriority = Item.LeaveDeductionPriority,                   
                    LeaveTypeId = Item.LeaveTypeId,
                    LeaveTypeAssignment = Item.LeaveTypeAssignment,
                    LeaveTypeDescription = Item.LeaveTypeDescription,
                    LeaveTypeName = Item.LeaveTypeName,
                    MaritalStatus = Item.MaritalStatus,
                    MaxCashable = Item.MaxCashable,
                    MaxTransferable = Item.MaxTransferable,
                    NumberOfTime = Item.NumberOfTime,
                    LeaveType = Item.LeaveType1,
                    ProRataLeaveRatio = Item.ProRataLeaveRatio
                };
                 Result.Add(single);

            }
            return Result;
        }
    }
}
