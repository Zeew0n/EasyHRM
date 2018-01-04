using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class PayrollLeaveDeductionResponseFormatter
    {
        public static List<PayrollLeaveDeductionDTO> PayrollLeaveDeductionListToDtoList(List<PayrollLeaveDeduction> Data)
        {
            List<PayrollLeaveDeductionDTO> ReturnRecord = new List<PayrollLeaveDeductionDTO>();
            foreach (var Row in Data)
            {
                PayrollLeaveDeductionDTO Record = new PayrollLeaveDeductionDTO
                {
                 DeductionId=Row.DeductionId,
                 EmpCode=Row.EmpCode,
                 LeaveTypeId=Row.LeaveTypeId,
                 LeaveDays=Row.LeaveDays,
                 DeductionType=Row.DeductionType,
                 LeaveYearId=Row.LeaveYearId,
                 LeaveDate=Convert.ToDateTime(Row.LeaveDate),
                 Details=Row.Details,
                 isEncashed=Row.isEncashed,
        Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName
                    },
                
                 LeaveType = new LeaveTypeDTO
                    {
                        LeaveTypeId = Row.LeaveType.LeaveTypeId,
                        LeaveTypeName = Row.LeaveType.LeaveTypeName

                    },
                 LeaveYear =new LeaveYearDTO
                 {
                     YearName=Row.LeaveYear.YearName
                 }
               };
                 ReturnRecord.Add(Record);

            }
            return ReturnRecord;
        }

        public static PayrollLeaveDeductionDTO PayrollLeaveDeductionToDto(PayrollLeaveDeduction Data)
        {
            PayrollLeaveDeductionDTO Record = new PayrollLeaveDeductionDTO
            {
                 DeductionId = Data.DeductionId,
                 EmpCode = Data.EmpCode,
                 LeaveTypeId = Data.LeaveTypeId,
                 LeaveDays = Data.LeaveDays,
                 DeductionType = Data.DeductionType,
                 LeaveYearId = Data.LeaveYearId,
                 LeaveDate =Convert.ToDateTime( Data.LeaveDate),
                 Details = Data.Details,
                isEncashed = Data.isEncashed,
            };
             return Record;
        }
    }
}