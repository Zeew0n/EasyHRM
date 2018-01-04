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
    public class PayrollSalaryTableRequestFormatter
    {
        public static PayrollSalaryTable ConvertRespondentInfoFromDTO(PayrollSalaryTableDTO PayrollSalaryTableDTO)
        {

            Mapper.CreateMap<PayrollSalaryTableDTO, PayrollSalaryTable>().ConvertUsing(
                      m =>
                      {
                          return new PayrollSalaryTable
                          {
                             Id = m.Id,
                             OfficeId = m.OfficeId,
                             PayrollMonthId = m.PayrollMonthId,
                             BgId = m.BgId,
                             CreatorId = m.CreatorId,
                             Details = m.Details,
                             SalaryConfirmed = m.SalaryConfirmed,
                             PayrollMonthDescription = new PayrollMonthDescription{
                                 Id = m.PayrollMonthDescription.Id,
                                 FyId = m.PayrollMonthDescription.FyId,
                                 MonthNameEnglish = m.PayrollMonthDescription.MonthNameEnglish,
                                 MonthNameNepali = m.PayrollMonthDescription.MonthNameNepali,
                                 StartDate = m.PayrollMonthDescription.StartDate,
                                 EndDate = m.PayrollMonthDescription.EndDate,
                                 WorkingDays = m.PayrollMonthDescription.WorkingDays
                             },
                             Office = new Office
                            {
                                OfficeId = m.Office.OfficeId,
                                OfficeName = m.Office.OfficeName,
                                OfficeAddress = m.Office.OfficeAddress,
                                OfficeCode = m.Office.OfficeCode,
                                OfficeGeoLocation = m.Office.OfficeGeoLocation,
                                OfficeParentId = m.Office.OfficeParentId,
                                OfficePhone = m.Office.OfficePhone,
                                OfficeStatus = m.Office.OfficeStatus
                            },
                             Fiscal = new Fiscal
                             {
                                 FyId = m.Fiscal.FyId,
                                 FyName = m.Fiscal.FyName
                                 
                             }
                          };

                      });
            return Mapper.Map<PayrollSalaryTableDTO, PayrollSalaryTable>(PayrollSalaryTableDTO);
        }

        public static PayrollSalaryTableDTO ConvertRespondentInfoToDTO(PayrollSalaryTable PayrollSalaryTable)
        {
            AutoMapper.Mapper.CreateMap<PayrollSalaryTable, PayrollSalaryTableDTO>().ConvertUsing(
                m =>
                {
                    return new PayrollSalaryTableDTO
                    {
                        Id = m.Id,
                        OfficeId = m.OfficeId,
                        PayrollMonthId = m.PayrollMonthId,
                        BgId = m.BgId,
                        CreatorId = m.CreatorId,
                        Details = m.Details,
                        SalaryConfirmed = m.SalaryConfirmed,
                        PayrollMonthDescription = new PayrollMonthDescriptionDTO
                        {
                            Id = m.PayrollMonthDescription.Id,
                            FyId = m.PayrollMonthDescription.FyId,
                            MonthNameEnglish = m.PayrollMonthDescription.MonthNameEnglish,
                            MonthNameNepali = m.PayrollMonthDescription.MonthNameNepali,
                            StartDate = m.PayrollMonthDescription.StartDate,
                            EndDate = m.PayrollMonthDescription.EndDate,
                            WorkingDays = m.PayrollMonthDescription.WorkingDays
                        },
                        Office = new OfficeDTOs
                        {
                            OfficeId = m.Office.OfficeId,
                            OfficeName = m.Office.OfficeName,
                            OfficeAddress = m.Office.OfficeAddress,
                            OfficeCode = m.Office.OfficeCode,
                            OfficeGeoLocation = m.Office.OfficeGeoLocation,
                            OfficeParentId = m.Office.OfficeParentId,
                            OfficePhone = m.Office.OfficePhone,
                            OfficeStatus = m.Office.OfficeStatus
                        },
                        Fiscal = new FiscalDTO
                        {
                            FyId = m.Fiscal.FyId,
                            FyName = m.Fiscal.FyName

                        }
                    };

                });
            return AutoMapper.Mapper.Map<PayrollSalaryTable, PayrollSalaryTableDTO>(PayrollSalaryTable);
        }
    }
}
