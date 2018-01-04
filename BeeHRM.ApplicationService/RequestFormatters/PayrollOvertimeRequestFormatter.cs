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
   public class PayrollOvertimeRequestFormatter
    {
        public static PayrollOvertime ConvertRespondentInfoFromDTO(PayrollOvertimeDTO payrollOvertimeDTO)
        {

            Mapper.CreateMap<PayrollOvertimeDTO, PayrollOvertime>().ConvertUsing(
                      m =>
                      {
                          return new PayrollOvertime
                          {
                             OvertimeId = m.OvertimeId,
                             ApprovedById = m.ApprovedById,
                             ApproveStatus = m.ApproveStatus,
                             ApproveStatusDate = m.ApproveStatusDate,
                             Details = m.Details,
                             EmpCode = m.EmpCode,
                             OvertimeAmount = m.OvertimeAmount,
                             OvertimeAppliedDate = m.OvertimeAppliedDate,
                             OvertimeDate =Convert.ToDateTime(m.OvertimeDate),
                             OvertimeHours = m.OvertimeHours,
                             Subject = m.Subject,
                             //Employee = new Employee
                             //{
                             //    EmpName = m.Employee.EmpName
                             //}
                          };

                      });
            return Mapper.Map<PayrollOvertimeDTO, PayrollOvertime>(payrollOvertimeDTO);
        }

        public static PayrollOvertimeDTO ConvertRespondentInfoToDTO(PayrollOvertime skill)
        {

            Mapper.CreateMap<PayrollOvertime, PayrollOvertimeDTO>().ConvertUsing(
                      m =>
                      {
                          return new PayrollOvertimeDTO
                          {
                              OvertimeId = m.OvertimeId,
                              ApprovedById = m.ApprovedById,
                              ApproveStatus = m.ApproveStatus,
                              ApproveStatusDate = m.ApproveStatusDate,
                              Details = m.Details,
                              EmpCode = m.EmpCode,
                              OvertimeAmount = m.OvertimeAmount,
                              OvertimeAppliedDate = m.OvertimeAppliedDate,
                              OvertimeDate = m.OvertimeDate,
                              OvertimeHours = m.OvertimeHours,
                              Subject = m.Subject,
                              //Employee = new EmployeeDTO
                              //{
                              //    EmpName = m.Employee.EmpName
                              //}
                          };

                      });
            return Mapper.Map<PayrollOvertime, PayrollOvertimeDTO>(skill);
        }
    }
}
