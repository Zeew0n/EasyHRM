using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollInsurancePremiumService
    {
        IEnumerable<PayrollInsurancePremiumDTO> GetAllPayrollInsurancePremium();
        PayrollInsurancePremiumDTO InsertPayrollInsurancePremium(PayrollInsurancePremiumDTO newPayrollInsurancePremium);
        PayrollInsurancePremiumDTO GetOnePayrollInsurancePremium(int Id);
        void UpdatePayrollInsurancePremium(PayrollInsurancePremiumDTO Record);
        int GetEmpCode(int Id);
        void DeletePayrollInsurancePremium(int id);
        //List<PayrollInsurancePremiumDTO> GetPayrollInsurancePremiumInformationByEmpCode(int? empcode);
        IEnumerable<SelectListItem> GetInsuranceCompanySelectList();
        //List<PayrollInsurancePremiumDTO> GetEmployeeByEmpCode(int empcode);
        IEnumerable<PayrollInsurancePremiumDTO> GetInsuranceInfobyEmpCode(int Empcode, int fyid);

    }
}
