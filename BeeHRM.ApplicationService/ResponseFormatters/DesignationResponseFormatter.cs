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
    public class DesignationResponseFormatter
    {
        public static IEnumerable<DesignationDTO> ModelData(IEnumerable<Designation> modelData)
        {

            Mapper.CreateMap<Designation, DesignationDTO>().ConvertUsing(

                m =>
                {
                    return new DesignationDTO
                    {
                        DesgCode = m.DesgCode,
                        DsgId = m.DsgId,
                        
                        DesgLevelId = m.DesgLevelId,
                        DesgMaxPeriodDays = m.DesgMaxPeriodDays,
                        DesgOrder = m.DesgOrder,
                        LeaveApprove = m.LeaveApprove,
                        LeaveRecommendation = m.LeaveRecommendation,
                        AttendanceApprove = m.AttendanceApprove,
                        AttendanceRecommendation = m.AttendanceRecommendation,
                        DesgShortCode = m.DesgShortCode,
                        DsgName = m.DsgName,
                        DsgParentId = m.DsgParentId
                    };

                }
                );
            return Mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDTO>>(modelData);
        }

    }
}
