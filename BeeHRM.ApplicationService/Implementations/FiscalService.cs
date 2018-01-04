using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class FiscalService : IFiscalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private dbBeeHRMEntities db = new dbBeeHRMEntities();


        public FiscalService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public void DeleteFiscal(int id)
        {
            _unitOfWork.FiscalRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<FiscalDTO> GetAllFiscal()
        {
            return FiscalResponseFormatter.ModelData(_unitOfWork.FiscalRepository.All());
        }

        public FiscalDTO GetFiscalById(int id)
        {
            return FiscalRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.FiscalRepository.GetById(id));
        }

        public FiscalDTO InsertFiscal(FiscalDTO data)
        {
            FiscalDTO res = new FiscalDTO();
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_FiscalAdd", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", data.FyName);
            cmd.Parameters.AddWithValue("@StartDate", data.FyStartDate);
            cmd.Parameters.AddWithValue("@EndDate", data.FyEndDate);
            cmd.Parameters.AddWithValue("@StartDateNep", data.FyStartDateNp);
            cmd.Parameters.AddWithValue("@EndDateNep", data.FyEndDateNp);
            cmd.Parameters.AddWithValue("@IsActive", data.FyCurrent);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                return new FiscalDTO
                {
                    FyName = row["FyName"].ToString(),
                    FyCurrent = Convert.ToBoolean(row["FyCurrent"]),
                    FyEndDateNp = row["FyEndDateNp"].ToString(),
                    FyStartDateNp = row["FyStartDateNp"].ToString(),
                    FyEndDate = Convert.ToDateTime(row["FyEndDate"]),
                    FyStartDate = Convert.ToDateTime(row["FyStartDate"])
                };
            }
            return res;
        }

        public int UpdateFiscal(FiscalDTO data)
        {
            FiscalDTO res = new FiscalDTO();
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_FiscalEdit", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", data.FyId);
            cmd.Parameters.AddWithValue("@Name", data.FyName);
            cmd.Parameters.AddWithValue("@StartDate", data.FyStartDate);
            cmd.Parameters.AddWithValue("@EndDate", data.FyEndDate);
            cmd.Parameters.AddWithValue("@StartDateNep", data.FyStartDateNp);
            cmd.Parameters.AddWithValue("@EndDateNep", data.FyEndDateNp);
            cmd.Parameters.AddWithValue("@IsActive", data.FyCurrent);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dt.Rows.Count;
        }
        public int GetCurrentFyId()
        {
            var data = _unitOfWork.FiscalRepository.All().Where(a => a.FyCurrent == true).FirstOrDefault(); return data.FyId;

        }
        public IEnumerable<SelectListItem> GetFiscalDropDown()
        {
            List<Fiscal> Fiscals = _unitOfWork.FiscalRepository.All().ToList();
            List<SelectListItem> FiscalList = new List<SelectListItem>();
            foreach (var row in Fiscals)
            {
                FiscalList.Add(new SelectListItem
                {
                    Text = row.FyName,
                    Value = row.FyId.ToString()
                });
            }
            return FiscalList;
        }

    }
}
