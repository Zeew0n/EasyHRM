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

namespace BeeHRM.ApplicationService.Implementations
{
    public class SpecialLeaveTypeService : ISpecialLeaveTypeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private ILeaveTypeService _leavetypeService;
        private IEmployeeService _employeeServices;
        private IOfficeServices _officeService;
        private IEmployeeService _employeeservice;

        public SpecialLeaveTypeService(IUnitOfWork unitOfWork = null, LeaveTypeService leavetypeService = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _leavetypeService = leavetypeService ?? new LeaveTypeService();
            _employeeServices = new EmployeeService();
            _officeService = new OfficeServices();
            _employeeservice = new EmployeeService();
        }

        #region Leave Type Special
        public IEnumerable<SelectListItem> GetYearSelectList()
        {
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            IEnumerable<LeaveYear> Modellist = _unitOfWork.LeaveYearRepository.All().ToList();

            foreach (var row in Modellist)
            {

                if (row.YearCurrent == true)
                {
                    ReturnRecord.Add(new SelectListItem
                    {
                        Text = (row.YearName).ToString() + " " + "(Current Year)",
                        Value = row.YearId.ToString()
                    });
                }
                else
                {
                    ReturnRecord.Add(new SelectListItem
                    {
                        Text = (row.YearName).ToString(),
                        Value = row.YearId.ToString()
                    });
                }

            }
            return ReturnRecord;
        }

        public IEnumerable<SelectListItem> EmployeeSelectList(int roleid)
        {
            IEnumerable<EmployeeDTO> Modellist = _employeeServices.GetEmployeeList(roleid);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Modellist)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpCode + " " + row.EmpName,
                    Value = row.EmpCode.ToString(),
                });
            }
            return ReturnRecord;
        }

        public IEnumerable<SelectListItem> GetLeaveTypeSelectList()
        {
            IEnumerable<LeaveType> Modellist = _unitOfWork.LeaveTypeRepository.All().ToList();
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
        public IEnumerable<SelectListItem> GetSpecialLeaveTypeSelectList()
        {
            IEnumerable<LeaveType> Record = _unitOfWork.LeaveTypeRepository.Get(x => x.LeaveType1 == "Special").ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Record)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.LeaveTypeName,
                    Value = row.LeaveTypeId.ToString()
                });
            }
            return ReturnRecord;
        }
        public IEnumerable<LeaveTypeDTO> GetSpecialLeaveType()
        {
            IEnumerable<LeaveType> Record = _unitOfWork.LeaveTypeRepository.Get(x => x.LeaveType1 == "Special").ToList();
            IEnumerable<LeaveTypeDTO> ReturnRecord = LeaveTypeResponseFormatter.ModelData(Record);
            return ReturnRecord;
        }
        public void AddSpecialLeaveType(LeaveApplicationDTO newLeave)
        {
            newLeave.LeaveDaysType = "F";
            newLeave.RecommededEmpCode = newLeave.RecommededEmpCode;
            newLeave.RecommendStatus = 2;
            newLeave.LeaveStatus = 2;
            newLeave.RecommenderMessage = newLeave.LeaveDetails;
            newLeave.RecommendStatusDate = DateTime.Now;
            newLeave.LeaveDays = newLeave.LeaveEndDate.Date.Subtract(newLeave.LeaveStartDate.Date).Duration().Days + 1;
            newLeave.LeaveAppliedDate = DateTime.Now;
            newLeave.LeaveGUICode = Guid.NewGuid().ToString();
            bool payable = Convert.ToBoolean(_unitOfWork.LeaveTypeRepository.Get(x => x.LeaveTypeId == newLeave.LeaveId).Select(x => x.IsPayable).SingleOrDefault());
            newLeave.PaidLeave = Convert.ToBoolean(payable);
            LeaveApplication Record = LeaveApplicationRequestFormatter.ConvertRespondentInfoFromDTO(newLeave);
            _unitOfWork.LeaveApplicationRepository.Create(Record);
            //call store proecudre for leave application attendance record
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveBalanceUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveId", Record.LeaveId);
            cmd.Parameters.AddWithValue("@LeaveStatus", Record.LeaveStatus);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

        }
        public List<LeaveApplicationDTO> GetAllSpecialApplicationLeaveList(int EmpCode)
        {
            List<LeaveApplication> Record = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveType.LeaveType1 == "Special" && x.LeaveEmpCode == EmpCode).ToList();
            List<LeaveApplicationDTO> ReturnRecord = ResponseFormatters.LeaveApplicationResponseFormatter.LeaveApplicationDbListToModelList(Record);
            return ReturnRecord;
        }
        public LeaveApplicationDTO GetLeaveApplicationsbyId(int id)
        {
            LeaveApplication Record = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveId == id).FirstOrDefault();
            LeaveApplicationDTO ReturnRecord = LeaveApplicationResponseFormatter.LeaveApplicationDbToModel(Record);
            return ReturnRecord;
        }

        public List<LeaveApplicationDTO> GetAllSpecialApplicationLeaveListByYearAndMonth(LeaveApplicationDTOInformation Record)
        {
           
           List<int> OfficeFilterList = _officeService.MyAccessOfficeList();

            List<LeaveApplication> RecordList = new List<LeaveApplication>();
            if (Record.MonthId == 0)
            {
                if (Record.LeaveEmpCode == null)
                {
                    RecordList = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveType.LeaveType1 == "Special" && x.LeaveYearId == Record.LeaveYearId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();
                }
                else
                {
                    RecordList = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveType.LeaveType1 == "Special" && x.LeaveEmpCode == Record.LeaveEmpCode && x.LeaveYearId == Record.LeaveYearId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

                }
            }
            else
            {
                if (Record.LeaveEmpCode == null)
                {
                    RecordList = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveType.LeaveType1 == "Special" && x.LeaveYearId == Record.LeaveYearId && (x.LeaveStartDate.Month == Record.MonthId || x.LeaveEndDate.Month == Record.MonthId) && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();
                }
                else
                {
                    RecordList = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveType.LeaveType1 == "Special" && x.LeaveEmpCode == Record.LeaveEmpCode && x.LeaveYearId == Record.LeaveYearId && (x.LeaveStartDate.Month == Record.MonthId || x.LeaveEndDate.Month == Record.MonthId) && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

                }

            }
            RecordList = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveType.LeaveType1 == "Special" && x.LeaveYearId == Record.LeaveYearId && OfficeFilterList.Contains(x.Employee.EmpOfficeId)).ToList();

            List<LeaveApplicationDTO> ReturnRecord = ResponseFormatters.LeaveApplicationResponseFormatter.LeaveApplicationDbListToModelList(RecordList);
            return ReturnRecord;

        }


       

        #endregion
        public void Reject(int id)
        {
          LeaveApplication Record=_unitOfWork.LeaveApplicationRepository.GetById(id);
            Record.LeaveStatus = 4;
            _unitOfWork.LeaveApplicationRepository.Update(Record);
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveBalanceUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveId", Record.LeaveId);
            cmd.Parameters.AddWithValue("@LeaveStatus", Record.LeaveStatus);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }


        public IEnumerable<SelectListItem> GetEmployeeApproverSelectList(int EmpCode,string approverType)
        {
             IEnumerable<EmployeeDTO> Record = _employeeservice.GetLeaveApprover(EmpCode, approverType);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Record)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpName,
                    Value = row.EmpCode.ToString()
                });
            }
            return ReturnRecord;
        }

        public IEnumerable<SelectListItem> GetEmployeeRecommenderSelectList(int EmpCode,string recommendType)
        {
            IEnumerable<EmployeeDTO> Record = _employeeservice.GetLeaveRecommander(EmpCode, recommendType);
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Record)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpName,
                    Value = row.EmpCode.ToString()
                });
            }
            return ReturnRecord;
        }
    }
}
