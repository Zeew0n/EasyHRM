using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IJobHistoryService
    {
        //EmployeeJobHistoryDTO GetCurrentJobDetail(int empCode);
        int InsertJobHistoryForSaruwa(EmployeeJobHistoryDTO dataToInsert);
        int InsertJobHistoryForKaj(EmployeeJobHistoryDTO dataToInsert);

        int UpdateJobHistoryForKaj(EmployeeJobHistoryDTO data);
        int InsertJobHistoryForBadhuwa(EmployeeJobHistoryDTO dataToInsert);
        int InsertJobHistoryForAbakash(EmployeeJobHistoryDTO dataToInsert);
        int InsertJobHistoryForNiyukti(EmployeeJobHistoryDTO data);
        int InsertJobHistoryForPunishment(EmployeeJobHistoryDTO data);
        List<EmployeeJobHistoryDTO> GetAllHistoryOfEmployee(int empCode);
        List<EmployeeJobHistoryDTO> GetAllHistoryOfEmployeeForKaaz(int empCode);
        int GetJobHistoryOfEmployeeWIthCondition(int empCode, string type);

        EmployeeJobHistoryDTO GetJobHistoryById(int id);
        int UpdateTransfer(EmployeeJobHistoryDTO jobHistories);
        int UpdatePunishment(EmployeeJobHistoryDTO jobHistories);
        int UpdateBadhuwa(EmployeeJobHistoryDTO jobHistories);
        int UpdateAbakas(EmployeeJobHistoryDTO jobHistories);
        int UpdateKaj(EmployeeJobHistoryDTO jobHistories);
        void UpdateAsCurrent(EmployeeJobHistoryDTO jobHistories);
    }
}
