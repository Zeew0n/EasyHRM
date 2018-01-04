using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class PayrollAllowanceMasterRequestFormatter
    {

        public static PayrollAllowanceMaster ConvertRespondentInfoFromDTO(PayrollAllowanceMasterDTO PayrollAllowanceMasterDTO)
        {

            Mapper.CreateMap<PayrollAllowanceMasterDTO, PayrollAllowanceMaster>().ConvertUsing(
                      m =>
                      {
                          return new PayrollAllowanceMaster
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
            return Mapper.Map<PayrollAllowanceMasterDTO, PayrollAllowanceMaster>(PayrollAllowanceMasterDTO);
        }

        public static PayrollAllowanceMasterDTO ConvertRespondentInfoToDTO(PayrollAllowanceMaster PayrollAllowanceMaster)
        {
            AutoMapper.Mapper.CreateMap<PayrollAllowanceMaster, PayrollAllowanceMasterDTO>().ConvertUsing(
                m =>
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
            return AutoMapper.Mapper.Map<PayrollAllowanceMaster, PayrollAllowanceMasterDTO>(PayrollAllowanceMaster);
        }
    }
}
