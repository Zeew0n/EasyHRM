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
    public class PayrollOvertimeResponseFormatter
    {
        public static List<PayrollOvertimeDTO> ModelData(List<PayrollOvertime> modelData)
        {

            Mapper.CreateMap<PayrollOvertime, PayrollOvertimeDTO>().ConvertUsing(

                m =>
                {
                    return new PayrollOvertimeDTO
                    {
                       OvertimeId = m.OvertimeId,
                       ApprovedById = m.ApprovedById,
                       ApproveStatus = m.ApproveStatus,
                       Subject = m.Subject,
                       OvertimeHours = m.OvertimeHours,
                       ApproveStatusDate = m.ApproveStatusDate,
                       Details = m.Details,
                       EmpCode = m.EmpCode,
                       OvertimeAmount = m.OvertimeAmount,
                       OvertimeAppliedDate = m.OvertimeAppliedDate,
                       OvertimeDate = m.OvertimeDate,
                        Employee = new EmployeeDTO
                        {
                            EmpName = m.Employee.EmpName
                        }
                    };

                }
                );
            return Mapper.Map<List<PayrollOvertime>, List<PayrollOvertimeDTO>>(modelData);
        }
    }
}
