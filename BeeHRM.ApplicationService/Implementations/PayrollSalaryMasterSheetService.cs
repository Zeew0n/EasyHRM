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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollSalaryMasterSheetService: IPayrollSalaryMasterSheetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayrollSalaryMasterSheetService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public void DeletePayrollSalaryMasterSheet(int id)
        {
            _unitOfWork.PayrollSalaryMasterSheetRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<PayrollSalaryMasterSheetDTO> GetAllPayrollSalaryMasterSheet(string id)
        {
            int employeeCode = Convert.ToInt32(id);
            return PayrollSalaryMasterSheetResponseFormatter.GetAllTPayrollMasterSheet(_unitOfWork.PayrollSalaryMasterSheetRepository.Get(m => m.EmployeeCode == employeeCode));
        }
        public IEnumerable<PayrollSalaryMasterSheetDTO> GetAllPayrollSalaryMasterSheetFilter(string id, string yearValue)
        {
            int employeeCode = Convert.ToInt32(id);
            int fiscalYear = Convert.ToInt32(yearValue);
            return PayrollSalaryMasterSheetResponseFormatter.GetAllTPayrollMasterSheet(_unitOfWork.PayrollSalaryMasterSheetRepository.Get(m=>m.EmployeeCode == employeeCode && m.PayrollSalaryTable.PayrollMonthDescription.FyId==fiscalYear));
        }
        public PayrollSalaryMasterSheetDTO GetPayrollSalaryMasterSheetById(int id)
        {
            return PayrollSalaryMasterSheetRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.PayrollSalaryMasterSheetRepository.GetById(id));
        }

        public PayrollSalaryMasterSheetDTO InsertPayrollSalaryMasterSheet(PayrollSalaryMasterSheetDTO data)
        {
            PayrollSalaryMasterSheet dataToInsert = new PayrollSalaryMasterSheet();
            dataToInsert = PayrollSalaryMasterSheetRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return PayrollSalaryMasterSheetRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.PayrollSalaryMasterSheetRepository.Create(dataToInsert));
        }

        public int UpdatePayrollSalaryMasterSheet(PayrollSalaryMasterSheetDTO data)
        {
            PayrollSalaryMasterSheet dataToUpdate = PayrollSalaryMasterSheetRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.PayrollSalaryMasterSheetRepository.Update(dataToUpdate);
            return res;
        }
        public IEnumerable<SelectListItem> GetFiscalYear()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Fiscal> fiscalYearList = _unitOfWork.FiscalRepository.Get().ToList();
            foreach (Fiscal item in fiscalYearList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.FyName.ToString();
                selectData.Value = item.FyId.ToString();
                Record.Add(selectData);
            }
            return Record;
        }
        public IEnumerable<PayrollSalaryDetailSheetDTO> GetAllPayrollSalaryDetail(string id)
        {
            int salaryId = Convert.ToInt32(id);
            return PayrollSalaryDetailSheetResponseFormatter.GetAllPayrollDetailSheet(_unitOfWork.PayrollSalaryDetailSheet.Get(x=>x.AllowanceId>4 && x.PayrollSalaryMasterId==salaryId));
        }
        public DataTable GetPayrollEmployeeTaxDetails(string id)
        {
            int employeeCode = Convert.ToInt32(id);
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[sp_PayrollEmployeeTaxPivot]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empCode", employeeCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return dt;
            //return PayrollEmployeeTaxDetailResponseFormatter.GetAllTaxDetailSheet(_unitOfWork.PayrollEmployeeTaxDetail.Get(x => x.EmployeeCode == employeeCode));
        }
    }
}
