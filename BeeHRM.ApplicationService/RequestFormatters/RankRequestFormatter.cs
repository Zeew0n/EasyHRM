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
    public class RankRequestFormatter
    {
        public static Rank ConvertRespondentInfoFromDTO(RankDTO rankDTO)
        {

            Mapper.CreateMap<RankDTO, Rank>().ConvertUsing(
                      m =>
                      {
                          return new Rank
                          {
                              RankId = m.RankId,
                              RankName = m.RankName,
                              RankDescription=m.RankDescription,
                              RankSalary=m.RankSalary,
                              RankMaxGrade=m.RankMaxGrade,
                              RankPerGradeAmount=m.RankPerGradeAmount,
                              BankAllowance=m.BankAllowance,
                              Incentive=m.Incentive,
                              SiteInchargeship=m.SiteInchargeship,
                              UniformAllowance=m.UniformAllowance,
                              RankSpecialAllowance=m.RankSpecialAllowance,
                              RankInchargeShipAllowance=m.RankInchargeShipAllowance,
                              RankOtherAllowances=m.RankOtherAllowances,
                              OverTimeRate=m.OverTimeRate,

                          };

                      });
            return Mapper.Map<RankDTO, Rank>(rankDTO);
        }

        public static RankDTO ConvertRespondentInfoToDTO(Rank rnk)
        {

            Mapper.CreateMap<Rank, RankDTO>().ConvertUsing(
                      m =>
                      {
                          return new RankDTO
                          {
                              RankId = m.RankId,
                              RankName = m.RankName,
                              RankDescription = m.RankDescription,
                              RankSalary = m.RankSalary,
                              RankMaxGrade = m.RankMaxGrade,
                              RankPerGradeAmount = m.RankPerGradeAmount,
                              BankAllowance = m.BankAllowance,
                              Incentive = m.Incentive,
                              SiteInchargeship = m.SiteInchargeship,
                              UniformAllowance = m.UniformAllowance,
                              RankSpecialAllowance = m.RankSpecialAllowance,
                              RankInchargeShipAllowance = m.RankInchargeShipAllowance,
                              RankOtherAllowances = m.RankOtherAllowances,
                              OverTimeRate = m.OverTimeRate,

                          };

                      });
            return Mapper.Map<Rank, RankDTO>(rnk);
        }
    }
}
