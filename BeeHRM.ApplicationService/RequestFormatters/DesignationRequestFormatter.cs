using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public static class DesignationRequestFormatter
    {
        public static Designation ConvertRespondentInfoFromDTO(DesignationDTO desg)
        {
            AutoMapper.Mapper.CreateMap<DesignationDTO, Designation>().ConvertUsing(
                m =>
                {
                    return new Designation
                    {
                        DesgCode = m.DesgCode,
                        DsgName = m.DsgName,
                        DsgId = m.DsgId,
                        DsgParentId = m.DsgParentId,
                       
                        DesgLevelId = m.DesgLevelId,
                        DesgMaxPeriodDays = m.DesgMaxPeriodDays,
                        DesgOrder = m.DesgOrder,
                        LeaveApprove=m.LeaveApprove,
                        LeaveRecommendation=m.LeaveRecommendation,
                        AttendanceApprove=m.AttendanceApprove,
                        AttendanceRecommendation=m.AttendanceRecommendation,
                        DesgShortCode = m.DesgShortCode
                    };
                });
            return AutoMapper.Mapper.Map<DesignationDTO, Designation>(desg);
        }
        public static DesignationDTO ConvertRespondentInfoToDTO(Designation desg)
        {
            AutoMapper.Mapper.CreateMap<Designation, DesignationDTO>().ConvertUsing(
                m =>
                {
                    return new DesignationDTO
                    {
                        DesgCode = m.DesgCode,
                        DsgName = m.DsgName,
                        DsgId = m.DsgId,
                        DsgParentId = m.DsgParentId,
                       
                        DesgLevelId = m.DesgLevelId,
                        DesgMaxPeriodDays = m.DesgMaxPeriodDays,
                        DesgOrder = m.DesgOrder,
                        LeaveApprove = m.LeaveApprove,
                        LeaveRecommendation = m.LeaveRecommendation,
                        AttendanceApprove = m.AttendanceApprove,
                        AttendanceRecommendation = m.AttendanceRecommendation,
                        DesgShortCode = m.DesgShortCode
                    };

                });
            return AutoMapper.Mapper.Map<Designation, DesignationDTO>(desg);
        }
            
        }
    }

