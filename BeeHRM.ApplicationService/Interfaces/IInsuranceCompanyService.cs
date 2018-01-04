using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IInsuranceCompanyService
    {
        IEnumerable<InsuranceCompanyDTO> GetAllInsuranceCompany();
        InsuranceCompanyDTO InsertInsuranceCompany(InsuranceCompanyDTO data);
        InsuranceCompanyDTO GetInsuranceCompanyById(int id);
        int UpdateInsuranceCompany(InsuranceCompanyDTO data);
        void DeleteInsuranceCompany(int id);
    }
}
