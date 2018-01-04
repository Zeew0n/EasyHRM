using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.StoredProcedureVariable;
using BeeHRM.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BeeHrmInterface.GlobalSelectLists
{
    public class DynamicSelectList : IDynamicSelectList
    {
        private readonly IUnitOfWork _unitOfWork;
        public DynamicSelectList(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public IEnumerable<SelectListItem> GetBranchSelectList()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<Office> BranchList = _unitOfWork.OfficeRepository.All().Where(x => x.IsActive == true).ToList();
            List.Add(new SelectListItem() { Text = "please select", Value = null });
            foreach (var str in BranchList)
            {
                List.Add(new SelectListItem() { Text = str.OfficeName, Value = str.OfficeId.ToString() });
            }
            return List;
        }
        public IEnumerable<SelectListItem> GetDepartmentSelectList()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<Department> DepartmentList = _unitOfWork.DepartmentRepository.All().ToList();
            List.Add(new SelectListItem() { Text = "please select", Value = null });
            foreach (var str in DepartmentList)
            {
                List.Add(new SelectListItem() { Text = str.DeptName, Value = str.DeptId.ToString() });
            }
            return List;
        }
        public IEnumerable<SelectListItem> GetGradeSelectList()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<Grade> GradeList = _unitOfWork.GradeRepository.All().ToList();
            List.Add(new SelectListItem() { Text = "please select", Value = null });
            foreach (var str in GradeList)
            {
                List.Add(new SelectListItem() { Text = str.GradeNumber.ToString(), Value = str.GradeId.ToString() });
            }
            return List;
        }
        public IEnumerable<SelectListItem> GetLevelSelectList()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<Level> LevelList = _unitOfWork.LevelRepository.All().ToList();
            List.Add(new SelectListItem() { Text = "please select", Value = null });
            foreach (var str in LevelList)
            {
                List.Add(new SelectListItem() { Text = str.LevelName, Value = str.LevelId.ToString() });
            }
            return List;
        }
        public IEnumerable<SelectListItem> GetEmployeeSelectList()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<Employee> SupervisorList = _unitOfWork.EmployeeRepository.All().Where(x => x.EmpStatus == true).OrderBy(x => x.EmpCode).ToList();
            List.Add(new SelectListItem() { Text = "please select", Value = null });
            foreach (var str in SupervisorList)
            {
                var empName = str.EmpCode.ToString() + '-' + str.EmpName;
                List.Add(new SelectListItem() { Text = empName, Value = str.EmpCode.ToString() });
            }
            return List;
        }
        public List<SelectListItem> GetEmployeeByEmpCode(int empCode)
        {


            SqlParameter[] parameter = { new SqlParameter("@EmpCode", empCode) };
            List<BranchEmployee> RecordList = _unitOfWork.BranchEmployeeRepository.ExecuteSPwithParameterForList("sp_EmployeeListByEmpCode @EmpCode", parameter);

            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            ReturnRecord.Add(new SelectListItem() { Text = "Please Select", Value = null });
            foreach (var row in RecordList)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.EmpCode + " " + row.EmpName,
                    Value = row.EmpCode.ToString()
                });
            }
            return ReturnRecord;

            /*

              List<SelectListItem> List = new List<SelectListItem>();
             List<Employee> SupervisorList = _unitOfWork.EmployeeRepository.All().Where(x => x.EmpStatus == true).OrderBy(x => x.EmpCode).ToList();
             List.Add(new SelectListItem() { Text = "please select", Value = null });
             foreach (var str in SupervisorList)
             {
                 var empName = str.EmpCode.ToString() + '-' + str.EmpName;
                 List.Add(new SelectListItem() { Text = empName, Value = str.EmpCode.ToString() });
             }
             return List;
             */

        }
        public IEnumerable<SelectListItem> LeaveTypeList()
        {
            List<SelectListItem> Result = new List<SelectListItem>();
            IEnumerable<LeaveType> data = _unitOfWork.LeaveTypeRepository.All();
            foreach (var item in data)
            {
                Result.Add(new SelectListItem() { Text = item.LeaveTypeName, Value = item.LeaveTypeId.ToString() });
            }
            return Result;
        }

        public IEnumerable<SelectListItem> LeaveYearList()
        {
            List<SelectListItem> Result = new List<SelectListItem>();
            IEnumerable<LeaveYear> data = _unitOfWork.LeaveYearRepository.All();
            Result.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            foreach (var item in data)
            {
                Result.Add(new SelectListItem() { Text = item.YearName.ToString(), Value = item.YearId.ToString() });
            }
            return Result;
        }
        public IEnumerable<SelectListItem> GetApproverSelectList(int empcode)
        {

            List<SelectListItem> List = new List<SelectListItem>();
            var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = empcode };
            List<sp_Myrecommender_Result> result = _unitOfWork.RecommenderList.ExecuteSPwithParameterForList("sp_AttendanceApprover @empCode", new[] { P_EmpCode }).ToList();
            foreach (var str in result)
            {
                List.Add(new SelectListItem() { Text = str.EmpName + "[" + str.EmpCode.ToString() + "]", Value = str.EmpCode.ToString() });
            }

            return List;
        }
        public IEnumerable<SelectListItem> GetRecommenderSelectList(int empcode)
        {

            List<SelectListItem> List = new List<SelectListItem>();
            var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = empcode };
            List<sp_Myrecommender_Result> result = _unitOfWork.RecommenderList.ExecuteSPwithParameterForList("sp_AttendanceRecommender @empCode", new[] { P_EmpCode }).ToList();
            foreach (var str in result)
            {
                List.Add(new SelectListItem() { Text = str.EmpName + "[" + str.EmpCode.ToString() + "]", Value = str.EmpCode.ToString() });
            }

            return List;
        }

        /**************for leave********************************/
        public IEnumerable<SelectListItem> GetLeaveApproverSelectList(int empcode, string approverType)
        {

            List<SelectListItem> List = new List<SelectListItem>();
            var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = empcode };
            var P_approver = new SqlParameter("approverType", SqlDbType.VarChar) { Value = approverType };
            List<sp_Myrecommender_Result> result = _unitOfWork.RecommenderList.ExecuteSPwithParameterForList("sp_LeaveApprover @empCode,@approverType", new[] { P_EmpCode, P_approver }).ToList();
            foreach (var str in result)
            {
                List.Add(new SelectListItem() { Text = str.EmpName + "[" + str.EmpCode.ToString() + "]", Value = str.EmpCode.ToString() });
            }

            return List;
        }
        public IEnumerable<SelectListItem> GetLeaveRecommenderSelectList(int empcode, string recommendType)
        {

            List<SelectListItem> List = new List<SelectListItem>();
            var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = empcode };
            var P_recommendType = new SqlParameter("recommendType", SqlDbType.VarChar) { Value = recommendType };
            List<sp_Myrecommender_Result> result = _unitOfWork.RecommenderList.ExecuteSPwithParameterForList("sp_LeaveRecommender @empCode,@recommendType", new[] { P_EmpCode, P_recommendType }).ToList();
            foreach (var str in result)
            {
                List.Add(new SelectListItem() { Text = str.EmpName + "[" + str.EmpCode.ToString() + "]", Value = str.EmpCode.ToString() });
            }

            return List;
        }
    }
}
