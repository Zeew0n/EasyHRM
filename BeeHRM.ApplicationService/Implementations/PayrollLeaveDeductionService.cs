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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BeeHRM.Repository.StoredProcedureVariable;


namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollLeaveDeductionService : IPayrollLeaveDeductionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IOfficeServices _officeService;

        
        public PayrollLeaveDeductionService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _officeService = new OfficeServices();
        }
        public List<PayrollLeaveDeductionDTO> GetAllPayrollLeaveDeductionList()
        {
            List<PayrollLeaveDeduction> Record = _unitOfWork.PayrollLeaveDeductionRepository.All().ToList();
            List<PayrollLeaveDeductionDTO> ReturnRecord = ResponseFormatters.PayrollLeaveDeductionResponseFormatter.PayrollLeaveDeductionListToDtoList(Record);
            return ReturnRecord;
        }

        public void AddPayrollLeaveDeduction(PayrollLeaveDeductionDTO Record)
        {
            PayrollLeaveDeduction ReturnRecord = RequestFormatters.PayrollLeaveDeductionRequestFormatter.PayrollLeaveDeductionDtoToDb(Record);
            ReturnRecord.DeductionType = "C";
            
            _unitOfWork.PayrollLeaveDeductionRepository.Create(ReturnRecord);
        }

        public void UpdatePayrollLeaveDeduction(PayrollLeaveDeductionDTO Record)
        {
            PayrollLeaveDeduction ReturnRecord = RequestFormatters.PayrollLeaveDeductionRequestFormatter.PayrollLeaveDeductionDtoToDb(Record);
            ReturnRecord.DeductionType = "C";
          
            ReturnRecord.EmpCode = Record.EmpId;
            _unitOfWork.PayrollLeaveDeductionRepository.Update(ReturnRecord);
        }

        public void Delete(int Id)
        {
            _unitOfWork.PayrollLeaveDeductionRepository.Delete(Id);
            _unitOfWork.Save();
        }


        public IEnumerable<SelectListItem> GetPayrollLeaveDeductionLeaveTypeSelectList()
        {
            IEnumerable<LeaveType> Modellist = _unitOfWork.LeaveTypeRepository.Get(x => x.IsCashable == true).ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Modellist)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.LeaveTypeName,
                    Value = row.LeaveTypeId.ToString(),

                });
            }
            return ReturnRecord;
        }

        public PayrollLeaveDeductionDTO GetOnePayrollLeaveDeduction(int id)
        {
            PayrollLeaveDeduction Record = _unitOfWork.PayrollLeaveDeductionRepository.Get(x => x.DeductionId == id).FirstOrDefault();
            PayrollLeaveDeductionDTO ReturnRecord = PayrollLeaveDeductionResponseFormatter.PayrollLeaveDeductionToDto(Record);
            return ReturnRecord;
        }
        public List<PayrollLeaveDeductionDTO> GetAllPayrollLeaveDeductionListByYearIdAndEmpCode(PayrollLeaveDeductionInformation Record)
        {
            List<PayrollLeaveDeduction> RecordList = new List<PayrollLeaveDeduction>();
            List<int> OfficeFilterList = _officeService.MyAccessOfficeList();
            if (Record.MonthId == 0)
            {
                if (Record.EmpId == null)
                {
                    RecordList = _unitOfWork.PayrollLeaveDeductionRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && x.DeductionType == "C"  && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

                }
                else
                {
                    RecordList = _unitOfWork.PayrollLeaveDeductionRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && x.EmpCode == Record.EmpId && x.DeductionType == "C" && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

                }
            }
            else
            {
                if (Record.EmpId == null)
                {
                    RecordList = _unitOfWork.PayrollLeaveDeductionRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && x.LeaveDate.Month == Record.MonthId && x.DeductionType == "C" && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();
                }
                else
                {
                    RecordList = _unitOfWork.PayrollLeaveDeductionRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && x.EmpCode == Record.EmpId && x.LeaveDate.Month == Record.MonthId && x.DeductionType == "C" && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

                }

            }

            List<PayrollLeaveDeductionDTO> ReturnRecord = ResponseFormatters.PayrollLeaveDeductionResponseFormatter.PayrollLeaveDeductionListToDtoList(RecordList);
            return ReturnRecord;
        }

       
        public decimal GetPayrollLeaveBalance(int leaveTypeId, int leaveYearId, int empCode)
        {
            SqlParameter[] parameter =
                            {
                            new SqlParameter("@leaveTypeId", leaveTypeId),
                            new SqlParameter("@leaveYearId",leaveYearId),
                            new SqlParameter("@empCode", empCode),
                           };
            PayrollLeaveBalance Record = _unitOfWork.PayrollLeaveBalanceRepository.ExecuteSPwithParameterForList("sp_LeaveApplicationEmployeeBalance @empCode,@leaveYearId,@leaveTypeId", parameter).FirstOrDefault();
           return Record.BalanceDays;
          
        }


        public int GetCurrentYear()
        {
          
          LeaveYear Record  = _unitOfWork.LeaveYearRepository.Get(x =>x.YearCurrent ==true).FirstOrDefault(); 
          return Record.YearId;

        }
    }
}

