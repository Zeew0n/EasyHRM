using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollSalaryMasterSheetService
    {
        IEnumerable<PayrollSalaryMasterSheetDTO> GetAllPayrollSalaryMasterSheet(string id);
        IEnumerable<PayrollSalaryMasterSheetDTO> GetAllPayrollSalaryMasterSheetFilter(string id, string yearValue);
        PayrollSalaryMasterSheetDTO InsertPayrollSalaryMasterSheet(PayrollSalaryMasterSheetDTO data);
        PayrollSalaryMasterSheetDTO GetPayrollSalaryMasterSheetById(int id);
        int UpdatePayrollSalaryMasterSheet(PayrollSalaryMasterSheetDTO data);
        void DeletePayrollSalaryMasterSheet(int id);
        IEnumerable<SelectListItem> GetFiscalYear();
        IEnumerable<PayrollSalaryDetailSheetDTO> GetAllPayrollSalaryDetail(string id);
        DataTable GetPayrollEmployeeTaxDetails(string id);
    }
}
