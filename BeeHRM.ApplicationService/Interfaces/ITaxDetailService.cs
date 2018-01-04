using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ITaxDetailService
    {
        IEnumerable<TaxDetailDTO> GetAllTaxDetails();
        void InsertIntoTaxDetail(TaxDetailDTO Record);
        TaxDetailDTO GetTaxDetailById(int Id);
        int UpdateTaxDetail(TaxDetailDTO Record);
        IEnumerable<TaxDetailDTO> GetAllTaxDetailsByMasterId(int MasterId);
    }
}
