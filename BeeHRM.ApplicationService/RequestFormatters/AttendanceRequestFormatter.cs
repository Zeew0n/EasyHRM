using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class AttendanceRequestFormatter
    {
        public static AttendanceRequest ConvertRespondentInfoFromDTO(AttendanceRequestDTO AttDTO)
        {
            Mapper.CreateMap<AttendanceRequestDTO, AttendanceRequest>().ConvertUsing(
                m => {
                    return new AttendanceRequest
                    {
                        RequestId = m.RequestId,
                        RequestEmpCode = m.RequestEmpCode,
                        ApproverEmpCode = m.ApproverEmpCode,
                        ApproveStatus = m.ApproveStatus,
                        ApproveStatusDate = m.ApproveStatusDate,
                        RecommendarEmpCode = m.RecommendarEmpCode,
                        RecommendStatus = m.RecommendStatus,
                        RecommendStatusDate = m.RecommendStatusDate,
                        CheckInDate = m.CheckInDate,
                        CheckOutDate = m.CheckOutDate,
                        RequestDescription = m.RequestDescription,
                        RequestedDate = m.RequestedDate,
                        RequestType = m.RequestType,
                        RecommedarMessage = m.RecommedarMessage,
                        ApproverMessage = m.ApproverMessage,
                        RequestIPAddress = m.IpAddress,
                        RequestCreatedDate = m.RequestCreatedDate
                    };
                });
            return Mapper.Map<AttendanceRequestDTO, AttendanceRequest>(AttDTO);
        }

        public static AttendanceRequestDTO ConvertRespondentToDTO(AttendanceRequest AttDTO)
        {

            Mapper.CreateMap<AttendanceRequest, AttendanceRequestDTO>().ConvertUsing(
                m =>
                {

                    return new AttendanceRequestDTO
                    {
                        RequestId = m.RequestId,
                        RequestEmpCode = m.RequestEmpCode,
                        ApproverEmpCode = m.ApproverEmpCode,
                        ApproveStatus = m.ApproveStatus,
                        ApproveStatusDate = m.ApproveStatusDate,
                        RecommendarEmpCode = m.RecommendarEmpCode,
                        RecommendStatus = m.RecommendStatus,
                        RecommendStatusDate = m.RecommendStatusDate,
                        CheckInDate = m.CheckInDate,
                        CheckOutDate = m.CheckOutDate,
                        RequestDescription = m.RequestDescription,
                        RequestedDate = m.RequestedDate,
                        RequestType = m.RequestType,
                        RecommedarMessage = m.RecommedarMessage,
                        ApproverMessage = m.ApproverMessage,
                        IpAddress = m.RequestIPAddress,
                        RequestCreatedDate=m.RequestCreatedDate
                    };
                });
            return Mapper.Map<AttendanceRequest, AttendanceRequestDTO>(AttDTO);
        }
    }
}
