using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<SelectListItem> GetCountryList();
        string GetCountryName(int id);
    }
}
