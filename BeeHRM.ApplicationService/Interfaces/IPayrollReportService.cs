using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollReportService
    {
        IndividualYearlyTaxEstimationModel GetIndividualYearlyTaxEstimation(int Id);

        IndividualYearlyTaxEstimationModel GetIndividualYearlyTax(int Id);

        List<SelectListItem> GetOfficeSelectList();
    }
}
