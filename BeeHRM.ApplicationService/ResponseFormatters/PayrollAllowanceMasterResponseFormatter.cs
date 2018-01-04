using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class PayrollAllowanceMasterResponseFormatter
    {
        public static IEnumerable<PayrollAllowanceMasterDTO> GetAllTPayrollMasterDTO(IEnumerable<PayrollAllowanceMaster> Record)
        {
            Mapper.CreateMap<PayrollAllowanceMaster, PayrollAllowanceMasterDTO>().ConvertUsing(m =>
            {
                return new PayrollAllowanceMasterDTO
                {
                    AllowanceMasterId = m.AllowanceMasterId,
                    AllowanceName = m.AllowanceName,
                    AllowanceType = m.AllowanceType,
                    ApplicableFromDate = m.ApplicableFromDate,
                    ApplicableToDate = m.ApplicableToDate,
                    ApplyToAllEmployee = m.ApplyToAllEmployee,
                    CreatedBy = m.CreatedBy,
                    CreatedDate = m.CreatedDate,
                    EarningDeduction = m.EarningDeduction,
                    IsActive = m.IsActive,
                    IsAlwaysAplicable = m.IsAlwaysAplicable,
                    IsPerDayDeductable = m.IsPerDayDeductable,
                    SameForAllEmployee = m.SameForAllEmployee,
                    PercentageAmount = m.PercentageAmount,
                    Value = m.Value,
                    GivenOnlyIfPresent = m.GivenOnlyIfPresent,
                    IsTaxable = m.IsTaxable,
                    IsDefault = m.IsDefault,
                };
            });
            return Mapper.Map<IEnumerable<PayrollAllowanceMaster>, IEnumerable<PayrollAllowanceMasterDTO>>(Record);
        }
    }
}
