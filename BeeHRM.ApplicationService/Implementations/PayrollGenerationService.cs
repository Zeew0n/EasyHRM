using BeeHRM.ApplicationService.Common;
using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollGenerationService : IPayrollGenerationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IEmployeeService _employeeService;
        private IPayrollInsurancePremiumService _insuranceService;
        private IInterestGainService _intGainService;

        public PayrollGenerationService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _employeeService = new EmployeeService();
            _insuranceService = new PayrollInsurancePremiumService();
            _intGainService = new InterestGainService();
        }

        public PayrollSalaryTableDTO GetPayollGenerationForm(int fyid)
        {
            List<SelectListItem> OfficeSelectList = new List<SelectListItem>();
            int EmployeeCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MyRoleOfficesList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", EmployeeCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow row in dt.Rows)
            {
                OfficeSelectList.Add(new SelectListItem
                {
                    Text = row["ChildOfficeName"].ToString(),
                    Value = row["ChildOfficeId"].ToString()
                });

            }
            IEnumerable<PayrollMonthDescription> MonthDescriptrionList = _unitOfWork.PayrollMonthDescriptipnRepository.All().Where(x => x.FyId == fyid).ToList();
            List<SelectListItem> MonthDescription = new List<SelectListItem>();
            foreach (var str in MonthDescriptrionList)
            {
                MonthDescription.Add(new SelectListItem
                {
                    Text = str.MonthNameNepali,
                    Value = str.Id.ToString()
                });
            }
            PayrollSalaryTableDTO Record = new PayrollSalaryTableDTO();
            IEnumerable<Fiscal> FiscalRecord = _unitOfWork.FiscalRepository.All().ToList();
            List<SelectListItem> FiscalSelectList = new List<SelectListItem>();
            foreach (var Fiscalstr in FiscalRecord)
            {
                FiscalSelectList.Add(new SelectListItem
                {
                    Text = Fiscalstr.FyName,
                    Value = Fiscalstr.FyId.ToString()
                });
            }
            Record.CreatorId = EmployeeCode;
            Record.FiscalYearList = FiscalSelectList;
            Record.OfficeList = OfficeSelectList;
            Record.MonthSelectList = MonthDescription;
            return Record;
        }
        public PayrollSalaryTableDTO GeneratePayroll(PayrollSalaryTableDTO Record, out string Message, out bool Success, out bool UpdateExisting)
        {
            List<PayrollSalaryTable> CheckConfirmedSalary = _unitOfWork.PayrollSalaryTableRepository.All().Where(x => x.OfficeId == Record.OfficeId && x.SalaryConfirmed == false).ToList();


            if (CheckConfirmedSalary.Count != 0)
            {
                foreach (PayrollSalaryTable Rec in CheckConfirmedSalary)
                {
                    if (!(Rec.OfficeId == Record.OfficeId && Rec.PayrollMonthId == Record.PayrollMonthId && Rec.FyId == Record.FyId))
                    {
                        Message = "There is a previous unconfirmed salary for this office";
                        Success = false;
                        Record.SalaryConfirmed = true;
                        UpdateExisting = false;
                        return Record;
                    }
                }
            }
            PayrollSalaryTableDTO ReturnRecord = new PayrollSalaryTableDTO();
            PayrollSalaryTable Domain = _unitOfWork.PayrollSalaryTableRepository.All().Where(x => x.FyId == Record.FyId && x.OfficeId == Record.OfficeId && x.PayrollMonthId == Record.PayrollMonthId).FirstOrDefault();
            if (Domain == null)
            {
                SqlConnection conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GeneratePayrollNewVersion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FyId", Record.FyId);
                cmd.Parameters.AddWithValue("@MonthDescriptId", Record.PayrollMonthId);
                cmd.Parameters.AddWithValue("@CreatorId", Record.CreatorId);
                cmd.Parameters.AddWithValue("@BranchId", Record.OfficeId);
                cmd.Parameters.AddWithValue("@Details", Record.Details);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                Message = "Payroll generated successfully";
                Success = true;
                PayrollSalaryTable ReturnId = _unitOfWork.PayrollSalaryTableRepository.Get(x => x.FyId == Record.FyId && x.OfficeId == Record.OfficeId && x.PayrollMonthId == Record.PayrollMonthId).FirstOrDefault();
                UpdateExisting = false;
                return PayrollSalaryTableRequestFormatter.ConvertRespondentInfoToDTO(ReturnId);
            }
            else
            {
                if (!Domain.SalaryConfirmed)
                {
                    if (Record.UpdateExisting == true)
                    {
                        SqlConnection conn = DbConnectHelper.GetConnection();
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_GeneratePayrollNewVersion", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FyId", Record.FyId);
                        cmd.Parameters.AddWithValue("@MonthDescriptId", Record.PayrollMonthId);
                        cmd.Parameters.AddWithValue("@CreatorId", Record.CreatorId);
                        cmd.Parameters.AddWithValue("@BranchId", Record.OfficeId);
                        cmd.Parameters.AddWithValue("@Details", Record.Details);
                        cmd.Parameters.AddWithValue("@DeleteFlag", Record.UpdateExisting);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                        Message = "Payroll updated successfully";
                        Success = true;
                        UpdateExisting = false;
                        return Record;
                    }
                    else
                    {
                        UpdateExisting = true;
                        Message = "Payroll of this month for this Office already exists.";
                    }
                }
                else
                {
                    UpdateExisting = false;
                    Message = "Payroll for this month is already confirmed";
                }
            }

            ReturnRecord = PayrollSalaryTableRequestFormatter.ConvertRespondentInfoToDTO(Domain);
            Success = false;
            return ReturnRecord;
        }

        public IEnumerable<PayrollSalaryTableDTO> GetMyPayrollSalaryTable(int fyId, int officeId)
        {
            IEnumerable<PayrollSalaryTable> db = _unitOfWork.PayrollSalaryTableRepository.Get(x => x.FyId == fyId && x.OfficeId == officeId).ToList();

            IEnumerable<PayrollSalaryTableDTO> View = PayrollSalaryTableResponseFormatter.GetAllTPayrollSalaryTableDTO(db);
            return View;
        }

        public IEnumerable<PayrollSalaryTableDTO> GetPayrollSalaryTable(int fyId, int officeId)
        {
            IEnumerable<PayrollSalaryTable> db = _unitOfWork.PayrollSalaryTableRepository.Get(x => x.FyId == fyId && x.OfficeId == officeId).ToList();

            IEnumerable<PayrollSalaryTableDTO> View = PayrollSalaryTableResponseFormatter.GetAllTPayrollSalaryTableDTO(db);
            return View;
        }

        public IEnumerable<AllowanceViewModel> GetAllowanceData(int empcode, int fyId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select d.AllowanceId,sum(d.CalculatedValue)as totalValue from PayrollSalaryMasterSheet m, PayrollSalaryDetailSheet d, PayrollSalaryTable s where m.Id = d.PayrollSalaryMasterId and s.Id = m.PayrollSalaryTableId AND m.EmployeeCode ="+empcode+ " AND s.FyId = "+fyId+" AND d.AllowanceType <> 'R' GROUP BY d.AllowanceId", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@fiscalYearId", fyId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            IEnumerable<AllowanceViewModel> AllowanceData = new DataTableToEntityMapper().Map<AllowanceViewModel>(dt).ToList();
            return AllowanceData;
        }

        public IEnumerable<TaxViewModel> GetTaxData(int empcode, int fyId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select d.OrderNumber,sum(d.DeductedAmount)as totalValue from PayrollSalaryMasterSheet m,PayrollEmployeeTaxDetail d,PayrollSalaryTable s where m.Id=d.PayrollMasterSheetId and s.Id = m.PayrollSalaryTableId AND m.EmployeeCode = "+empcode+" AND s.FyId="+fyId+" GROUP BY d.OrderNumber", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@fiscalYearId", fyId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            IEnumerable<TaxViewModel> TaxData = new DataTableToEntityMapper().Map<TaxViewModel>(dt).ToList();
            return TaxData;
        }

        public IEnumerable<PayrollSalaryViewModel> GetYearlyPayrollSalaryTable(int fyId, int officeId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetPayrollSalaryTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fiscalYearId", fyId);
            cmd.Parameters.AddWithValue("@officeId", officeId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            IEnumerable<PayrollSalaryViewModel> payrollSalaryTableDTO = new DataTableToEntityMapper().Map<PayrollSalaryViewModel>(dt).ToList();
            foreach (var item in payrollSalaryTableDTO)
            {
                item.AllowanceViewModel = GetAllowanceData(item.EmployeeCode, fyId);
                item.TaxModel = GetTaxData(item.EmployeeCode, fyId);
                item.EmployeeName = _unitOfWork.EmployeeRepository.GetById(item.EmployeeCode).EmpName;
                item.OfficeName = _unitOfWork.OfficeRepository.GetById(officeId).OfficeName;
                item.FiscalYear = _unitOfWork.FiscalRepository.GetById(fyId).FyName;
                int dsgID = _unitOfWork.EmployeeRepository.GetById(item.EmployeeCode).EmpDesgId;
                item.EmpPost = _unitOfWork.DesignationRepository.GetById(dsgID).DsgName;
                item.GradeNo = (int)_unitOfWork.EmployeeRepository.GetById(item.EmployeeCode).CurrentGrade;
                item.PANNo = _employeeService.GetEmployeeByID(item.EmployeeCode).PANNumber;
                item.CitNo = _employeeService.GetEmployeeByID(item.EmployeeCode).CitNumber;
                item.InsurancePremium = _insuranceService.GetInsuranceInfobyEmpCode(item.EmployeeCode, fyId);
                item.LoanIntIncome = _intGainService.GetInterestGainByEmpCode(item.EmployeeCode).InterestGain;
                if (item.LoanIntIncome==null)
                {
                    item.LoanIntIncome = 0;
                }
                //foreach (var items in item.InsurancePremium)
                //{
                //    item.InsuranceID = items.IsuranceClaimId;
                //    item.InsuranceCompanyName = items.InsuranceCompany.CompanyName;
                //    item.PolicyNumber = items.InsurancePolicyNumber;
                //    item.InsuredAmount = (double)items.InsuredAmount;
                //    item.InsuranceEffectedDate = (DateTime)items.StartDate;
                //    item.MaturityDate = (DateTime)items.EndDate;
                //    item.AnnualPremium = (double)items.PremiumAmount;
                //}
            }
            return payrollSalaryTableDTO;

        }


        public IEnumerable<PayrollSalaryMasterSheetDTO> GetPayrollSalaryMasterSheet(int Id)
        {
            IEnumerable<PayrollSalaryMasterSheet> db = _unitOfWork.PayrollSalaryMasterSheetRepository.All().Where(x => x.PayrollSalaryTableId == Id).ToList();
            IEnumerable<PayrollSalaryMasterSheetDTO> view = PayrollSalaryMasterSheetResponseFormatter.GetAllTPayrollMasterSheet(db);
            return view;
        }
        public PayrollSalaryMasterSheetDTO GetIndividualSalarySheetDescription(int Id)
        {
            PayrollSalaryMasterSheet db = _unitOfWork.PayrollSalaryMasterSheetRepository.GetById(Id);
            PayrollSalaryMasterSheetDTO Record = PayrollSalaryMasterSheetRequestFormatter.ConvertRespondentInfoToDTO(db);
            Record.PayrollSalaryDetailSheets = PayrollSalaryDetailSheetResponseFormatter.GetAllPayrollDetailSheet(db.PayrollSalaryDetailSheets).Where(x => x.AllowanceType == "O");
            Record.PayrollEmployeeTaxDetails = PayrollEmployeeTaxDetailResponseFormatter.GetAllTaxDetailSheet(db.PayrollEmployeeTaxDetails);
            return Record;
        }
        public void ConfirmPayroll(int Id)
        {
            PayrollSalaryTable Record = _unitOfWork.PayrollSalaryTableRepository.GetById(Id);
            Record.SalaryConfirmed = true;
            _unitOfWork.PayrollSalaryTableRepository.Update(Record);
        }
    }
}
