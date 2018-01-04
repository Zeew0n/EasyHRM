using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public static class EmployeeRequestFormatter
    {
        public static Employee ConvertRespondentInfoFromDTO(EmployeeDTO empDTO)
        {

            Mapper.CreateMap<EmployeeDTO, Employee>().ConvertUsing(
                      m =>
                      {
                          return new Employee
                          {
                              EmpCode = m.EmpCode,
                              CustomerId = m.CustomerId,
                              EmpAttendanceIgnore = m.EmpAttendanceIgnore,
                              EmpBgId = m.EmpBgId,
                              EmpContractExpiryDate = m.EmpContractExpiryDate,
                              EmpCreatedDate = m.EmpCreatedDate,
                              EmpDeptHead = m.EmpDeptHead,
                              EmpDeptId = m.EmpDeptId,
                              EmpDesgId = m.EmpDesgId,
                              EmpEmail = m.EmpEmail,
                              EmpGender = m.EmpGender,
                              EmpIncapacitated = m.EmpIncapacitated,
                              EmpJobHistoryId = m.EmpJobHistoryId,
                              EmpJobTypeId = m.EmpJobTypeId,
                              EmpLeaveRuleId = m.EmpLeaveRuleId,
                              EmpLevelId = m.EmpLevelId,
                              EmpName = m.EmpName,
                              EmpOfficeHead = m.EmpOfficeHead,
                              EmpOfficeId = m.EmpOfficeId,
                              EmpPassword = m.EmpPassword,
                              EmpPayroll = m.EmpPayroll,
                              EmpPhoto = m.EmpPhoto,
                              EmpRankId = m.EmpRankId,
                              EmpSectionId = m.EmpSectionId,
                              EmpShiftId = m.EmpShiftId,
                              EmpStatus = m.EmpStatus,
                              EmpRemoteType = m.EmpRemoteType,
                              EmpTypeId = m.EmpTypeId,
                              EmpUserName = m.EmpUserName,
                              EmpMaritalStatus = m.EmpMaritalStatus,
                              EmpAppointmentDate = m.EmpAppointmentDate,
                              EmpDisplayCode = m.EmpDisplayCode,
                              EmpRoleId = m.EmpRoleId,
                              EmpTaxSetupId = m.EmpTaxSetupId,

                          };

                      });
            return Mapper.Map<EmployeeDTO, Employee>(empDTO);
        }
        public static EmployeeDTO ConvertRespondentInfoToDTO(Employee emp)
        {

            Mapper.CreateMap<Employee, EmployeeDTO>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeDTO
                          {
                              EmpCode = m.EmpCode,
                              CustomerId = m.CustomerId,
                              EmpAttendanceIgnore = m.EmpAttendanceIgnore,
                              EmpBgId = m.EmpBgId,
                              EmpContractExpiryDate = m.EmpContractExpiryDate,
                              EmpCreatedDate = m.EmpCreatedDate,
                              EmpRemoteType = m.EmpRemoteType,
                              EmpDeptHead = m.EmpDeptHead,
                              EmpDeptId = m.EmpDeptId,
                              EmpDesgId = m.EmpDesgId,
                              EmpEmail = m.EmpEmail,
                              EmpGender = m.EmpGender,

                              EmpIncapacitated = m.EmpIncapacitated,
                              EmpJobHistoryId = m.EmpJobHistoryId,
                              EmpJobTypeId = m.EmpJobTypeId,
                              EmpLeaveRuleId = m.EmpLeaveRuleId,
                              EmpLevelId = m.EmpLevelId,
                              EmpRoleId = m.EmpRoleId,
                              EmpName = m.EmpName,
                              EmpOfficeHead = m.EmpOfficeHead,
                              EmpOfficeId = m.EmpOfficeId,
                              EmpPassword = m.EmpPassword,
                              EmpPayroll = m.EmpPayroll,
                              EmpPhoto = m.EmpPhoto,
                              EmpRankId = m.EmpRankId,

                              EmpSectionId = m.EmpSectionId,
                              EmpShiftId = m.EmpShiftId,
                              EmpStatus = m.EmpStatus,

                              EmpTypeId = m.EmpTypeId,
                              EmpUserName = m.EmpUserName,
                              EmpMaritalStatus = m.EmpMaritalStatus,
                              EmpAppointmentDate = m.EmpAppointmentDate,
                              EmpDisplayCode = m.EmpDisplayCode,


                          };

                      });
            return Mapper.Map<Employee, EmployeeDTO>(emp);
        }

        public static List<EmployeeDTO> CovertRespontFormatterListToDTO(List<Employee> Record)
        {
            List<EmployeeDTO> returnRecord = new List<EmployeeDTO>();
            foreach (var m in Record)
            {
                EmployeeDTO single = new EmployeeDTO()
                {
                    EmpCode = m.EmpCode,
                    CustomerId = m.CustomerId,
                    EmpAttendanceIgnore = m.EmpAttendanceIgnore,
                    EmpBgId = m.EmpBgId,
                    EmpContractExpiryDate = m.EmpContractExpiryDate,
                    EmpCreatedDate = m.EmpCreatedDate,
                    EmpRemoteType = m.EmpRemoteType,
                    EmpDeptHead = m.EmpDeptHead,
                    EmpDeptId = m.EmpDeptId,
                    EmpDesgId = m.EmpDesgId,
                    EmpEmail = m.EmpEmail,
                    EmpGender = m.EmpGender,

                    EmpIncapacitated = m.EmpIncapacitated,
                    EmpJobHistoryId = m.EmpJobHistoryId,
                    EmpJobTypeId = m.EmpJobTypeId,
                    EmpLeaveRuleId = m.EmpLeaveRuleId,
                    EmpLevelId = m.EmpLevelId,
                    EmpRoleId = m.EmpRoleId,
                    EmpName = m.EmpName,
                    EmpOfficeHead = m.EmpOfficeHead,
                    EmpOfficeId = m.EmpOfficeId,
                    EmpPassword = m.EmpPassword,
                    EmpPayroll = m.EmpPayroll,
                    EmpPhoto = m.EmpPhoto,
                    EmpRankId = m.EmpRankId,

                    EmpSectionId = m.EmpSectionId,
                    EmpShiftId = m.EmpShiftId,
                    EmpStatus = m.EmpStatus,

                    EmpTypeId = m.EmpTypeId,
                    EmpUserName = m.EmpUserName,
                    EmpMaritalStatus = m.EmpMaritalStatus,
                    EmpAppointmentDate = m.EmpAppointmentDate,
                    EmpDisplayCode = m.EmpDisplayCode,
                };
                returnRecord.Add(single);
            }
            return returnRecord;

        }


    }
}
