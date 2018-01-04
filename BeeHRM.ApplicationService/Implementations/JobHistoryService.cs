using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BeeHRM.ApplicationService.Implementations
{
    public class JobHistoryService : IJobHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobHistoryService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public List<EmployeeJobHistoryDTO> GetAllHistoryOfEmployee(int empCode)
        {
            List<EmployeeJobHistory> res = _unitOfWork.EmployeeJobHistoryRepository.All().Where(x => x.EmpCode == empCode).OrderBy(x => x.EffectiveDate).ToList();
            res = res.Where(x => x.ServiceEventGroupId != 3).ToList();
            return EmployeeJobHistoryResponseFormatter.ModelData(res);
        }
        public List<EmployeeJobHistoryDTO> GetAllHistoryOfEmployeeForKaaz(int empCode)
        {
            List<EmployeeJobHistory> res = _unitOfWork.EmployeeJobHistoryRepository.All().Where(x => x.EmpCode == empCode).OrderBy(x => x.EffectiveDate).ToList();
            res = res.Where(x => x.ServiceEventGroupId == 3).ToList();
            return EmployeeJobHistoryResponseFormatter.ModelData(res);
        }

        public int GetJobHistoryOfEmployeeWIthCondition(int empCode, string type)
        {

            if (type == "current")
            {
                var res = _unitOfWork.EmployeeJobHistoryRepository.All().Where(x => x.EmpCode == empCode && x.ServiceEventGroupId != 3 && x.ServiceEventGroupId != 5).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
                if (res != null)
                {
                    return res.HistoryId;
                }
                else { return 0; }
            }
            else if (type == "appoint")
            {
                var res = _unitOfWork.EmployeeJobHistoryRepository.All().Where(x => x.EmpCode == empCode && x.ServiceEventGroupId == 1).FirstOrDefault();
                if (res != null)
                {
                    return res.HistoryId;
                }
                else { return 0; }
            }
            else
            {
                return 0;
            }

            return 1;
        }

        public EmployeeJobHistoryDTO GetJobHistoryById(int id)
        {
            EmployeeJobHistory res = _unitOfWork.EmployeeJobHistoryRepository.GetById(id);
            return EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoToDTO(res);
        }

        public int InsertJobHistoryForAbakash(EmployeeJobHistoryDTO data)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);
            Employee entityToUpdate = _unitOfWork.EmployeeRepository.GetById(dataToInsert.EmpCode);
            entityToUpdate.EmpStatus = false;
            dataToInsert.BusinessGroupId = entityToUpdate.EmpBgId;
            dataToInsert.DeptId = entityToUpdate.EmpDeptId;
            dataToInsert.DesgId = entityToUpdate.EmpDesgId;
            dataToInsert.JobTypeId = entityToUpdate.EmpJobTypeId;
            dataToInsert.LevelId = entityToUpdate.EmpLevelId;
            dataToInsert.OfficeId = entityToUpdate.EmpOfficeId;
            dataToInsert.RankId = entityToUpdate.EmpRankId;
            dataToInsert.SectionId = entityToUpdate.EmpSectionId;
            dataToInsert.ShiftId = entityToUpdate.EmpSectionId;

            int res = _unitOfWork.EmployeeRepository.Update(entityToUpdate);
            _unitOfWork.Save();
            return _unitOfWork.EmployeeJobHistoryRepository.Create(dataToInsert).HistoryId;
        }

        public int InsertJobHistoryForBadhuwa(EmployeeJobHistoryDTO data)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var conn = DbConnectHelper.GetConnection();
            conn.Open();


            SqlCommand cmd = new SqlCommand("sp_EmployeeTransferUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", data.EmpCode);
            cmd.Parameters.AddWithValue("@EmpOfficeId", data.OfficeId);
            cmd.Parameters.AddWithValue("@EmpLevelId", data.LevelId);
            cmd.Parameters.AddWithValue("@EmpShiftId", data.ShiftId);
            cmd.Parameters.AddWithValue("@EmpDeptId", data.DeptId);
            cmd.Parameters.AddWithValue("@EmpSectionId", data.SectionId);
            cmd.Parameters.AddWithValue("@EmpDesgId", data.DesgId);
            cmd.Parameters.AddWithValue("@EmpTypeId", 1);
            cmd.Parameters.AddWithValue("@EmpBgId", data.BusinessGroupId);
            cmd.Parameters.AddWithValue("@EmpJobTypeId", data.JobTypeId);
            cmd.Parameters.AddWithValue("@EmpRankId", data.RankId);
            cmd.Parameters.AddWithValue("@RemoteType", data.RemoteCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();



            return _unitOfWork.EmployeeJobHistoryRepository.Create(dataToInsert).HistoryId;
        }

        public int InsertJobHistoryForPunishment(EmployeeJobHistoryDTO data)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return _unitOfWork.EmployeeJobHistoryRepository.Create(dataToInsert).HistoryId;
        }
        public int InsertJobHistoryForKaj(EmployeeJobHistoryDTO data)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return _unitOfWork.EmployeeJobHistoryRepository.Create(dataToInsert).HistoryId;
        }
        public int UpdateJobHistoryForKaj(EmployeeJobHistoryDTO data)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return _unitOfWork.EmployeeJobHistoryRepository.Update(dataToInsert);
        }
        public int InsertJobHistoryForSaruwa(EmployeeJobHistoryDTO data)
        {

            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var conn = DbConnectHelper.GetConnection();
            conn.Open();


            SqlCommand cmd = new SqlCommand("sp_EmployeeTransferUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", data.EmpCode);
            cmd.Parameters.AddWithValue("@EmpOfficeId", data.OfficeId);
            cmd.Parameters.AddWithValue("@EmpLevelId", data.LevelId);
            cmd.Parameters.AddWithValue("@EmpShiftId", data.ShiftId);
            cmd.Parameters.AddWithValue("@EmpDeptId", data.DeptId);
            cmd.Parameters.AddWithValue("@EmpSectionId", data.SectionId);
            cmd.Parameters.AddWithValue("@EmpDesgId", data.DesgId);
            cmd.Parameters.AddWithValue("@EmpTypeId", 1);
            cmd.Parameters.AddWithValue("@EmpBgId", data.BusinessGroupId);
            cmd.Parameters.AddWithValue("@EmpJobTypeId", data.JobTypeId);
            cmd.Parameters.AddWithValue("@EmpRankId", data.RankId);
            cmd.Parameters.AddWithValue("@RemoteType", data.RemoteCode);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            EmployeeJobHistory result = _unitOfWork.EmployeeJobHistoryRepository.Create(dataToInsert);
            return result.HistoryId;
        }

        public int InsertJobHistoryForNiyukti(EmployeeJobHistoryDTO data)
        {

            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(data);


            EmployeeJobHistory result = _unitOfWork.EmployeeJobHistoryRepository.Create(dataToInsert);
            return result.HistoryId;
        }

        #region Updates 
        public int UpdateAbakas(EmployeeJobHistoryDTO jobHistories)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(jobHistories);
            dataToInsert.ServiceEventGroupId = 6;
            int result = _unitOfWork.EmployeeJobHistoryRepository.Update(dataToInsert);
            return result;
        }

        public void UpdateAsCurrent(EmployeeJobHistoryDTO data)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();


            SqlCommand cmd = new SqlCommand("sp_EmployeeTransferUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", data.EmpCode);
            cmd.Parameters.AddWithValue("@EmpOfficeId", data.OfficeId);
            cmd.Parameters.AddWithValue("@EmpLevelId", data.LevelId);
            cmd.Parameters.AddWithValue("@EmpShiftId", data.ShiftId);
            cmd.Parameters.AddWithValue("@EmpDeptId", data.DeptId);
            cmd.Parameters.AddWithValue("@EmpSectionId", data.SectionId);
            cmd.Parameters.AddWithValue("@EmpDesgId", data.DesgId);
            cmd.Parameters.AddWithValue("@EmpTypeId", 1);
            cmd.Parameters.AddWithValue("@EmpBgId", data.BusinessGroupId);
            cmd.Parameters.AddWithValue("@EmpJobTypeId", data.JobTypeId);
            cmd.Parameters.AddWithValue("@EmpRankId", data.RankId);
            cmd.Parameters.AddWithValue("@RemoteType", data.RemoteCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        public int UpdateBadhuwa(EmployeeJobHistoryDTO jobHistories)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(jobHistories);
            dataToInsert.ServiceEventGroupId = 7;
            int result = _unitOfWork.EmployeeJobHistoryRepository.Update(dataToInsert);
            return result;
        }

        public int UpdateKaj(EmployeeJobHistoryDTO jobHistories)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(jobHistories);
            dataToInsert.ServiceEventGroupId = 3;
            int result = _unitOfWork.EmployeeJobHistoryRepository.Update(dataToInsert);
            return result;
        }

        public int UpdatePunishment(EmployeeJobHistoryDTO jobHistories)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(jobHistories);
            dataToInsert.ServiceEventGroupId = 5;
            int result = _unitOfWork.EmployeeJobHistoryRepository.Update(dataToInsert);
            return result;
        }

        public int UpdateTransfer(EmployeeJobHistoryDTO jobHistories)
        {
            EmployeeJobHistory dataToInsert = EmployeeJobHistoryRequestFormatter.ConvertRespondentInfoFromDTO(jobHistories);
            //var conn = DbConnectHelper.GetConnection();
            //conn.Open();
            //SqlCommand cmd = new SqlCommand("sp_EmployeeTransferUpdate", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@EmpCode", jobHistories.EmpCode);
            //cmd.Parameters.AddWithValue("@EmpOfficeId", jobHistories.OfficeId);
            //cmd.Parameters.AddWithValue("@EmpLevelId", jobHistories.LevelId);
            //cmd.Parameters.AddWithValue("@EmpShiftId", jobHistories.ShiftId);
            //cmd.Parameters.AddWithValue("@EmpDeptId", jobHistories.DeptId);
            //cmd.Parameters.AddWithValue("@EmpSectionId", jobHistories.SectionId);
            //cmd.Parameters.AddWithValue("@EmpDesgId", jobHistories.DesgId);
            //cmd.Parameters.AddWithValue("@EmpTypeId", 1);
            //cmd.Parameters.AddWithValue("@EmpBgId", jobHistories.BusinessGroupId);
            //cmd.Parameters.AddWithValue("@EmpJobTypeId", jobHistories.JobTypeId);
            //cmd.Parameters.AddWithValue("@EmpRankId", jobHistories.RankId);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //cmd.Dispose();
            //conn.Close();
            //conn.Dispose();
            dataToInsert.ServiceEventGroupId = 2;
            int result = _unitOfWork.EmployeeJobHistoryRepository.Update(dataToInsert);
            return result;
        }
        #endregion
    }
}
