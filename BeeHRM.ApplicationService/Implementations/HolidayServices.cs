using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using System.Data.SqlClient;
using System.Data;
using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ViewModel;

namespace BeeHRM.ApplicationService.Implementations
{
    public class HolidayServices : IHolidayServices
    {

        private readonly IUnitOfWork _unitOfWork;
        
        public HolidayServices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void CreateHoliayUpdateAttendnace(HolidayDTOs adt)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateHolidayRecord", conn);
            cmd.Parameters.AddWithValue("@HolidayId", adt.HolidayId);
            cmd.Parameters.AddWithValue("@HOfficeId", adt.HolidayOfficeId);
            cmd.Parameters.AddWithValue("@HReligionId", adt.HolidayReligionId);
            cmd.Parameters.AddWithValue("@HEthnicityId", adt.HolidayEthnicityId);
            cmd.Parameters.AddWithValue("@HGender", adt.HolidayGender);
            cmd.Parameters.AddWithValue("@HolidayDate", adt.HolidayDate);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        public void UpdateHoliday(string date)
        {

            DateTime Holidaydate = Convert.ToDateTime(date);
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateHoliday", conn);          
            cmd.Parameters.AddWithValue("@HolidayDate", Holidaydate);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
        }

        public int CreateHolidays(HolidayDTOs htd)
        {
           
            Holiday dataToInsert = new Holiday();
            dataToInsert = HolidayRequestFormatter.ConvertRespondentInfoFromDTO(htd);
            HolidayDTOs dt= HolidayRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.HolidayRepository.Create(dataToInsert));
            int id = dt.HolidayId;
            return id;
        }

        public void DeleteHoliday(int id)
        {
            
                var conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteHoliday", conn);
                cmd.Parameters.AddWithValue("@HolidayId", id);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        public void DeleteHolidayOffice(int id)
        {            
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteOfficelist", conn);
            cmd.Parameters.AddWithValue("@HolidayId", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        public HolidayDetailsViewModel HolidayDetails(int id)
        {
            var data = _unitOfWork.HolidayRepository.Get().Where(x => x.HolidayId == id);
            HolidayDetailsViewModel hld = new HolidayDetailsViewModel();
            hld.HolidayDate = data.FirstOrDefault().HolidayDate;
            hld.HolidayTitle = data.FirstOrDefault().HolidayTitle;
            hld.HolidayStatus = data.FirstOrDefault().HolidayStatus;
            hld.HolidayYearlyOccourence = data.FirstOrDefault().HolidayYearlyOccourence;
            hld.HolidayOfficeId = data.FirstOrDefault().HolidayOfficeId;
            hld.HolidayEthnicityId = data.FirstOrDefault().HolidayEthnicityId;
            hld.HolidayDescription = data.FirstOrDefault().HolidayDescription;
            hld.HolidayReligionId = data.FirstOrDefault().HolidayReligionId;
            if(hld.HolidayOfficeId==0)
            {
                hld.HolidayOffices = "All";

            }
            else
            {
                hld.OfficesList = HolidayOfficeById(id);
            }
            if(hld.HolidayEthnicityId==0)
            {
                hld.EthnicityName = "All";
            }
            else
            {
                var ethname = _unitOfWork.EthnicityRepository.Get().Where(x => x.EthnicityId == hld.HolidayEthnicityId).Select(x => x.EthnicityName);
                hld.EthnicityName = ethname.FirstOrDefault();
            }
            if(hld.HolidayReligionId==0)
            {
                hld.ReligionName = "All";
            }
            else
            {
                var relg= _unitOfWork.ReligionRepository.Get().Where(x => x.ReligionId == hld.HolidayReligionId).Select(x => x.ReligionName);
                hld.ReligionName = relg.FirstOrDefault();
            }
            hld.HolidayGender = data.FirstOrDefault().HolidayGender;

            return hld;
            

        }

        public IEnumerable<HolidayDTOs> HolidayList()
        {
            IEnumerable<Holiday> modelDatas = _unitOfWork. HolidayRepository.All();
            IEnumerable<HolidayDTOs> val = from md in modelDatas                                          
                                           select new HolidayDTOs
                                           {
                                               HolidayId=md.HolidayId,
                                                HolidayDate=md.HolidayDate,                                                
                                                HolidayDescription=md.HolidayDescription,
                                                HolidayReligionId=md.HolidayReligionId,
                                                HolidayYearlyOccourence=md.HolidayYearlyOccourence,
                                                HolidayOfficeId=md.HolidayOfficeId,
                                                HolidayGender=md.HolidayGender,
                                                HolidayTitle=md.HolidayTitle,
                                                HolidayStatus=md.HolidayStatus,
                                                HolidayEthnicityId=md.HolidayEthnicityId,
                                               
                                                
                                           };
            List<HolidayDTOs> final = new List<HolidayDTOs>();
            foreach(var row in val)
            {
                HolidayDTOs holiday = new HolidayDTOs();
                string religion, ethnicity;
                if(row.HolidayReligionId==0)
                {
                    religion = "ALL";
                }
                else
                {
                    religion = _unitOfWork.ReligionRepository.Get().Where(x => x.ReligionId == row.HolidayReligionId).FirstOrDefault().ReligionName.ToString();
                }
               if(row.HolidayEthnicityId==0)
                {
                    ethnicity = "All";
                }
               else
                {
                    ethnicity = _unitOfWork.EthnicityRepository.Get().Where(x => x.EthnicityId == row.HolidayEthnicityId).FirstOrDefault().EthnicityName.ToString();
                }
              
                holiday.HolidayId = row.HolidayId;
                holiday.HolidayDate = row.HolidayDate;
                holiday.HolidayDescription = row.HolidayDescription;
                holiday.HolidayReligionId = row.HolidayReligionId;
                holiday.HolidayOfficeId = row.HolidayOfficeId;
                holiday.HolidayGender = row.HolidayGender;
                holiday.HolidayTitle = row.HolidayTitle;
                holiday.HolidayStatus = row.HolidayStatus;
                holiday.HolidayEthnicityId = row.HolidayEthnicityId;
                holiday.EthnicityName = ethnicity;
                holiday.ReligionName = religion;


                final.Add(holiday);
               
            }
                                            
            return final;
        }

        public HolidayDTOs HolidayListbyId(int id)
        {
            Holiday Record = _unitOfWork.HolidayRepository.GetById(id);
            HolidayDTOs Returnreocrd = HolidayRequestFormatter.ConvertRespondentInfoToDTO(Record);            

            return Returnreocrd;
        }

        public IEnumerable<HolidayOfficesViewModel> HolidayOfficeById(int id)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HolidayAssignedOficeList", conn);
            cmd.Parameters.AddWithValue("@HilidayId", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                yield return new HolidayOfficesViewModel
                {
                    HolidayId = Convert.ToInt32(row["HolidayId"].ToString()),
                    OfficeId = Convert.ToInt32(row["OfficeId"].ToString()),
                    OfficeName = row["OfficeName"].ToString()

                };

            }
        }
        public void InsertholidayOffices(int holidayId, int OfficeId)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_InsertHolidayAssignedOffice", conn);
            cmd.Parameters.AddWithValue("@HolidayId", holidayId);
            cmd.Parameters.AddWithValue("@OfficeId", OfficeId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

        }

        public int UpdateHoliday(HolidayDTOs htd)
        {

            Holiday att = HolidayRequestFormatter.ConvertRespondentInfoFromDTO(htd);

            return (_unitOfWork.HolidayRepository.Update(att));
       
        }
    }
}
