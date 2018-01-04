using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ITaxSetupService
    {
        IEnumerable<TaxSetupDTO> GetAllTaxSetup();
        void InsertIntoTaxSetup(TaxSetupDTO Record);
        TaxSetupDTO GetTaxSetupById(int Id);
        int UpdateTaxSetup(TaxSetupDTO Record);
    }
}
