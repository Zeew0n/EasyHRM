using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollArrearService
    {
        IEnumerable<PayrollArrearsDTO> GetPayrollArrears();
        PayrollArrearsDTO GetCreateForm();
        void InsertIntoArrears(PayrollArrearsDTO Record);
        PayrollArrearsDTO GetArrearById(int Id);
        void UpdateArrears(PayrollArrearsDTO Record);
    }
}
