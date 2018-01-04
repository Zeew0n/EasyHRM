using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollOvertimeSerivce
    {
        List<PayrollOvertimeDTO> GetAllPayrollOvertime();
        PayrollOvertimeDTO InsertPayrollOvertime(PayrollOvertimeDTO data);
        PayrollOvertimeDTO GetPayrollOvertimeById(int id);
        int UpdatePayrollOvertime(PayrollOvertimeDTO data);
        void DeletePayrollOvertime(int id);
        IEnumerable<SelectListItem> GetEmployeeList();
        IEnumerable<SelectListItem> GetApproverList(int empCode);
    }
}
