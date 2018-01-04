using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;
using BeeHRM.ApplicationService.ConnectDb;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EmployeeTrainingService : IEmployeeTrainingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeTrainingService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        //public void DeleteTrainingById(int id)
        //{
        //    _unitOfWork.EmployeeTrainingRepository.Delete(id);
        //    _unitOfWork.Save();
        //}

        //public IEnumerable<EmployeeTrainingDTO> GetAllTrainingOfEmployee(int id)
        //{
        //    var res = _unitOfWork.EmployeeTrainingRepository.All().Where(x => x.EmpCode == id);
        //    return EmployeeTrainingResponseFormatter.ModelData(res);
        //}

        //public EmployeeTrainingDTO GetTrainingById(int trainingId)
        //{
        //    EmployeeTraining data = _unitOfWork.EmployeeTrainingRepository.GetById(trainingId);
        //    return EmployeeTrainingRequestFormatter.ConvertRespondentInfoToDTO(data);
        //}

        //public void InsertEmployeeTraining(EmployeeTrainingDTO data)
        //{
        //    data.TrainingDays = (data.TrainingEndDate - data.TrainingStartDate).Value.Days;
        //    data.TrainingYear = data.TrainingStartDate.Value.Year;
        //    EmployeeTraining dataToInsert = EmployeeTrainingRequestFormatter.ConvertRespondentInfoFromDTO(data);
        //    _unitOfWork.EmployeeTrainingRepository.Create(dataToInsert);
        //}

        //public int UpdateTraining(EmployeeTrainingDTO data)
        //{
        //    EmployeeTraining dataToUpdate = EmployeeTrainingRequestFormatter.ConvertRespondentInfoFromDTO(data);
        //    return _unitOfWork.EmployeeTrainingRepository.Update(dataToUpdate);
        //}
        public List<EmpTrainingDTO> GetAllTrainingOfEmployeeWithId(int id)
        {
            List<EmpTraining> Domain = _unitOfWork.EmpTrainingRepository.Get(x => x.EmpCode == id).ToList();
            IEnumerable<EmpTrainingDTO> Record = EmpTrainingResponseFormatter.ModelListData(Domain);
            return Record.ToList();
        }

        public int CreateEmployeeTraining(EmpTrainingDTO Record)
        {
            Record.Id = 0;
            EmpTraining Domain = EmpTrainingRequestFormatter.ConvertRespondentInfoFromDTO(Record);

            using (TransactionScope Scope = new TransactionScope())
            {
                //for (DateTime Today = Convert.ToDateTime(Record.TrainingVisitStartDate).Date; Today.Date <= Convert.ToDateTime(Record.TrainingVisitEndDate).Date; Today = Today.AddDays(1))
                //{
                //    AttendaceDaily DailyRecord = _unitOfWork.AttendanceDailyRepository.Get(x => x.AttDate == Today && x.AttEmpCode == Record.EmpCode).FirstOrDefault();
                //    if(DailyRecord != null)
                //    {
                //        DailyRecord.IsTraining = true;
                //        _unitOfWork.AttendanceDailyRepository.Update(DailyRecord);
                //    }else
                //    {
                //        DailyRecord = new AttendaceDaily();
                //        DailyRecord.AttEmpCode = Record.EmpCode;
                //        DailyRecord.AttDate = Today;
                //        DailyRecord.IsTraining = true;
                //        _unitOfWork.AttendanceDailyRepository.Create(DailyRecord);
                //    }
                //}


                var newEntryId = _unitOfWork.EmpTrainingRepository.Create(Domain);
                Scope.Complete();
                return newEntryId.Id;
                //Calling stroprocedure for data in attendance daily table
                // exec Sp_TrainingAttendanceRecord newEntryId,'I'
            }

        }

        public void UpdateAttendaceonTraining(int id, string mode)
        {

            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Sp_TrainingAttendanceRecord", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainingId", id);
            cmd.Parameters.AddWithValue("@mode", mode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();


        }
        public EmpTrainingDTO GetEmployeeTrainingById(int id)
        {
            EmpTraining Record = _unitOfWork.EmpTrainingRepository.GetById(id);
            EmpTrainingDTO ReturnRecord = EmpTrainingResponseFormatter.ModelData(Record);
            return ReturnRecord;
        }
        public void UpdateEmployeeTraining(EmpTrainingDTO Record)
        {
            EmpTraining Domain = EmpTrainingRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.EmpTrainingRepository.Update(Domain);
        }
        public void DeleteTraining(int id)
        {
            _unitOfWork.EmpTrainingRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}