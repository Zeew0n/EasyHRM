using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IFiscalService
    {
        IEnumerable<FiscalDTO> GetAllFiscal();
        FiscalDTO InsertFiscal(FiscalDTO data);
        FiscalDTO GetFiscalById(int id);
        int UpdateFiscal(FiscalDTO data);
        void DeleteFiscal(int id);
        int GetCurrentFyId();
        IEnumerable<SelectListItem> GetFiscalDropDown();
    }
}
