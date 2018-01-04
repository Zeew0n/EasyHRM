using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.StoredProcedureVariable;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveEarnedService : ILeaveEarnedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IOfficeServices _officeService;

        public LeaveEarnedService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _officeService = new OfficeServices();
        }
        public List<LeaveEarnedDTO> GetAllLeaveEarnedList(int EmpCode)
        {
            List<LeaveEarned> Record = _unitOfWork.LeaveEarnedRepository.All().ToList();
            List<LeaveEarnedDTO> ReturnRecord = ResponseFormatters.LeaveEarnedResponseFormatter.LeaveEarnedListToDtoList(Record);
            return ReturnRecord;
        }
        public IEnumerable<SelectListItem> GetEarnedLeaveTypeList()
        {
            IEnumerable<LeaveType> Modellist = _unitOfWork.LeaveTypeRepository.Get(x => x.LeaveTypeAssignment == "Earned").ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Modellist)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.LeaveTypeName,
                    Value = row.LeaveTypeId.ToString()
                });
            }
            return ReturnRecord;
        }
        public void AddLeaveEarned(LeaveEarnedDTO Record)
        {
            LeaveEarned ReturnRecord = RequestFormatters.LeaveEarnedRequestFormatter.LeaveEarnedDTOToDb(Record);
            ReturnRecord.ApproverStatus = 2;
            ReturnRecord.ApproverStatusDate = DateTime.Now;
            ReturnRecord.LeaveTotalEanrnedDays = Convert.ToDateTime(Record.WorkedEndDate).Date.Subtract((Convert.ToDateTime(Record.WorkedStartDate)).Date).Duration().Days + 1;
            ReturnRecord.LeaveEarnRequestedDate = DateTime.Now;
            _unitOfWork.LeaveEarnedRepository.Create(ReturnRecord);
        }

        public LeaveEarnedDTO GetOneLeaveEarned(int id)
        {
            LeaveEarned Record = _unitOfWork.LeaveEarnedRepository.GetById(id);
            LeaveEarnedDTO ReturnRecord = LeaveEarnedResponseFormatter.LeaveEarnedToDto(Record);
            return ReturnRecord;
        }

        public void UpdateLeaveEarned(LeaveEarnedDTO Record)
        {
            LeaveEarned ReturnRecord = RequestFormatters.LeaveEarnedRequestFormatter.LeaveEarnedDTOToDb(Record);
            ReturnRecord.ApproverStatus = 2;
            ReturnRecord.ApproverStatusDate = DateTime.Now;
            ReturnRecord.LeaveTotalEanrnedDays = Convert.ToDateTime(Record.WorkedEndDate).Date.Subtract((Convert.ToDateTime(Record.WorkedStartDate)).Date).Duration().Days + 1;
            ReturnRecord.LeaveEarnRequestedDate = DateTime.Now;
            ReturnRecord.EmpCode = Record.EmpId;
            _unitOfWork.LeaveEarnedRepository.Update(ReturnRecord);
        }

        public void Delete(int id)
        {
            _unitOfWork.LeaveEarnedRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<SelectListItem> GetBrancheEmployeeSelectList(int EmpCode)
        {


            SqlParameter[] parameter = { new SqlParameter("@EmpCode", EmpCode) };
            List<BranchEmployee> RecordList = _unitOfWork.BranchEmployeeRepository.ExecuteSPwithParameterForList("sp_EmployeeListByEmpCode @EmpCode", parameter);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in RecordList)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpCode + " " + row.EmpName,
                    Value = row.EmpCode.ToString()
                });
            }
            return ReturnRecord;
        }

        public List<LeaveEarnedDTO> GetAllLeaveEarnedListByYearIdAndEmpCode(LeaveEarnedInfomationDTO Record)
        {
            List<LeaveEarned> RecordList = new List<LeaveEarned>();

            List<int> OfficeFilterList = _officeService.MyAccessOfficeList();

            RecordList = _unitOfWork.LeaveEarnedRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

            if (Record.EmpId != null)
            {
                RecordList = RecordList.Where(x => x.EmpCode == Record.EmpId).ToList();

            }

            if (Record.MonthId > 0)
            {
                int monthId = (int)Record.MonthId;

                RecordList = RecordList.Where(x => x.WorkedStartDate.Month == Record.MonthId || x.WorkedEndDate.Month == Record.MonthId).ToList();

            }

            //if (Record.MonthId == 0)
            //{
            //    if (Record.EmpCode == null)
            //    {
            //        RecordList = _unitOfWork.LeaveEarnedRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();
            //    }
            //    else
            //    {
            //        RecordList = _unitOfWork.LeaveEarnedRepository.Get(x => x.EmpCode == Record.EmpId && x.LeaveYearId == Record.LeaveYearId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

            //    }
            //}
            //else
            //{
            //    if (Record.EmpCode == null)
            //    {
            //        RecordList = _unitOfWork.LeaveEarnedRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && (x.WorkedStartDate.Month == Record.MonthId || x.WorkedEndDate.Month == Record.MonthId) && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();
            //    }
            //    else
            //    {
            //        RecordList = _unitOfWork.LeaveEarnedRepository.Get(x => x.LeaveYearId == Record.LeaveYearId && (x.WorkedStartDate.Month == Record.MonthId || x.WorkedEndDate.Month == Record.MonthId) && x.EmpCode == Record.EmpId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

            //    }

            //}
            List<LeaveEarnedDTO> ReturnRecord = ResponseFormatters.LeaveEarnedResponseFormatter.LeaveEarnedListToDtoList(RecordList);
            return ReturnRecord;
        }


        public IEnumerable<SelectListItem> GetSelectedBranchEmployeeSelectList(int curentemp, int EmpCode)
        {
            SqlParameter[] parameter = { new SqlParameter("@EmpCode", EmpCode) };
            List<BranchEmployee> RecordList = _unitOfWork.BranchEmployeeRepository.ExecuteSPwithParameterForList("sp_EmployeeListByEmpCode @EmpCode", parameter);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in RecordList)
            {

                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpCode + " " + row.EmpName,
                    Value = row.EmpCode.ToString(),

                });


            }
            return ReturnRecord;
        }


        public IEnumerable<SelectListItem> GetSelectedBranchApproveEmployeeSelectList(int curentemp, int ApproverEmpCode)
        {
            int OfficeId = _unitOfWork.EmployeeRepository.Get(x => x.EmpCode == curentemp).Select(x => x.EmpOfficeId).SingleOrDefault();
            SqlParameter[] parameter = { new SqlParameter("@officeid", OfficeId) };
            List<BranchEmployee> RecordList = _unitOfWork.BranchEmployeeRepository.ExecuteSPwithParameterForList("sp_getEmployeelistByOfficeId @officeid", parameter);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in RecordList)
            {
                bool Selected = false;
                if (row.EmpCode == ApproverEmpCode)
                {
                    Selected = true;
                }
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpCode + " " + row.EmpName,
                    Value = row.EmpCode.ToString(),
                    Selected = Selected
                });


            }
            return ReturnRecord;
        }
        public IEnumerable<SelectListItem> GetRecommederSelectList(int curentemp, int selectedEmpCode)
        {

            SqlParameter[] parameter = { new SqlParameter("@EmpCode", curentemp) };
            List<BranchEmployee> RecordList = _unitOfWork.BranchEmployeeRepository.ExecuteSPwithParameterForList("sp_Myrecommender @EmpCode", parameter);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in RecordList)
            {
                bool Selected = false;
                if (row.EmpCode == selectedEmpCode)
                {
                    Selected = true;
                }
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpCode + " " + row.EmpName,
                    Value = row.EmpCode.ToString(),
                    Selected = Selected
                });


            }
            return ReturnRecord;
        }

        public decimal YearlyEarnedLeave(int empCode, int leaveYearId, int leaveTypeId)
        {
            var days = _unitOfWork.LeaveEarnedRepository.All()
                .Where(x => x.LeaveYearId == leaveYearId && x.EmpCode == empCode && x.LeaveTypeId == leaveTypeId)
                .Sum(x => x.LeaveTotalEanrnedDays);
            return Convert.ToDecimal(days);
        }

    }
}

