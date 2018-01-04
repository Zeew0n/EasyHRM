using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class LeaveTypeResponseFormatter
    {
        public static IEnumerable<LeaveTypeDTO> ModelData(IEnumerable<LeaveType> modelData)
        {

            Mapper.CreateMap<LeaveType, LeaveTypeDTO>().ConvertUsing(

                m =>
                {
                    return new LeaveTypeDTO
                    {
                        LeaveApplyBefore = m.LeaveApplyBefore,
                        LeaveTypeAssignment = m.LeaveTypeAssignment,
                        LeaveTypeDescription = m.LeaveTypeDescription,
                        LeaveTypeId = m.LeaveTypeId,
                        LeaveTypeName = m.LeaveTypeName,
                        ProRataLeaveRatio = m.ProRataLeaveRatio,
                        Gender = m.Gender,
                        IsCashable = m.IsCashable,
                        IsPayable = m.IsPayable,
                        IsTransferable = m.IsTransferable,
                        MaritalStatus = m.MaritalStatus,
                        MaxCashable = m.MaxCashable,
                        MaxTransferable = m.MaxTransferable,
                        MonthlyQty = m.MonthlyQty,
                        NumberOfTime = m.NumberOfTime
                    };

                }
                );
            return Mapper.Map<IEnumerable<LeaveType>, IEnumerable<LeaveTypeDTO>>(modelData);
        }
    }
}
