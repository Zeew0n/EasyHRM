using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class LeaveTypeRequestFormatter
    {
        public static LeaveType ConvertRespondentInfoFromDTO(LeaveTypeDTO leaveTypeDTO)
        {

            Mapper.CreateMap<LeaveTypeDTO, LeaveType>().ConvertUsing(
                      m =>
                      {
                          return new LeaveType
                          {
                              LeaveTypeAssignment = m.LeaveTypeAssignment,
                              Gender = m.Gender,
                              IsCashable = m.IsCashable,
                              IsPayable = m.IsPayable,
                              IsTransferable = m.IsTransferable,
                              LeaveTypeDescription = m.LeaveTypeDescription,
                              LeaveTypeName = m.LeaveTypeName,
                              LeaveTypeId = m.LeaveTypeId,
                              ProRataLeaveRatio = m.ProRataLeaveRatio,
                              LeaveApplyBefore = m.LeaveApplyBefore,
                              MaritalStatus = m.MaritalStatus,
                              MaxCashable = m.MaxCashable,
                              MaxTransferable = m.MaxTransferable,
                              MonthlyQty = m.MonthlyQty,
                              NumberOfTime = m.NumberOfTime,
                              LeaveType1=m.LeaveType1,
                              HalfdayAllow=m.HalfdayAllow,
                          };

                      });
            return Mapper.Map<LeaveTypeDTO, LeaveType>(leaveTypeDTO);
        }

        public static LeaveTypeDTO ConvertRespondentInfoToDTO(LeaveType emp)
        {

            Mapper.CreateMap<LeaveType, LeaveTypeDTO>().ConvertUsing(
                      m =>
                      {
                          return new LeaveTypeDTO
                          {
                              LeaveTypeAssignment = m.LeaveTypeAssignment,
                              Gender = m.Gender,
                              IsCashable = m.IsCashable,
                              IsPayable = m.IsPayable,
                              IsTransferable = m.IsTransferable,
                              LeaveTypeDescription = m.LeaveTypeDescription,
                              LeaveTypeName = m.LeaveTypeName,
                              LeaveTypeId = m.LeaveTypeId,
                              ProRataLeaveRatio = m.ProRataLeaveRatio,
                              LeaveApplyBefore = m.LeaveApplyBefore,
                              MaritalStatus = m.MaritalStatus,
                              MaxCashable = m.MaxCashable,
                              MaxTransferable = m.MaxTransferable,
                              MonthlyQty = m.MonthlyQty,
                              NumberOfTime = m.NumberOfTime,
                              LeaveType1 = m.LeaveType1,
                              HalfdayAllow = m.HalfdayAllow,
                          };
                      });
            return Mapper.Map<LeaveType, LeaveTypeDTO>(emp);
        }
    }
}
