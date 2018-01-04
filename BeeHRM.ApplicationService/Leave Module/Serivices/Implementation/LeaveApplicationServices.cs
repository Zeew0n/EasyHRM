using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Mapper;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation
{
    public class LeaveApplicationServices : ILeaveApplicationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDynamicSelectList _DynamicSelectList;
        private ILeaveSetupservices _LeaveSetUp;
        public LeaveApplicationServices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            this._DynamicSelectList = new DynamicSelectList();
            this._LeaveSetUp = new LeaveSetupservices();
        }

        #region LeaveBalance
        public IEnumerable<LeaveBalance> LeaveBalanceList(int? YearId, int EmpCode)
        {
            List<sp_LeaveApplicationEmployeeBalance_Result> result = new List<sp_LeaveApplicationEmployeeBalance_Result>();
            int CurrentYear = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearCurrent == true).Select(x => x.YearId).FirstOrDefault();
            List<LeaveType> LeaveId = _unitOfWork.LeaveTypeRepository.All().Where(x => x.LeaveType1 == "General").ToList();
            for (int i = 0; i < LeaveId.Count; i++)
            {
                var P_YearId = new SqlParameter("leaveYearId", SqlDbType.BigInt) { Value = (YearId == null ? CurrentYear : YearId) };
                var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = EmpCode };
                var P_LeaveTypeId = new SqlParameter("leaveTypeId", SqlDbType.BigInt) { Value = LeaveId[i].LeaveTypeId };
                sp_LeaveApplicationEmployeeBalance_Result single = _unitOfWork.LeaveBalanceRepository.ExecuteSPwithParameterForList("sp_LeaveApplicationEmployeeBalance @empCode,@leaveYearId,@leaveTypeId", new[] { P_EmpCode, P_YearId, P_LeaveTypeId }).FirstOrDefault();
                single.LeaveTypeAssignment = LeaveId[i].LeaveType1;
                single.LeaveTypeName = LeaveId[i].LeaveTypeName;
                result.Add(single);
                P_YearId = P_EmpCode = P_LeaveTypeId = null;


            }
            return LeaveBalanceMapper.SpLeaveBalanceListToLeaveBalanceList(result, EmpCode, Convert.ToInt32(YearId));
        }

        public LeaveBalance LeavebalanceIndividual(int? YearId, int EmpCode, int leaveid)
        {
            List<sp_LeaveApplicationEmployeeBalance_Result> result = new List<sp_LeaveApplicationEmployeeBalance_Result>();
            int CurrentYear = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearCurrent == true).Select(x => x.YearId).FirstOrDefault();
            LeaveType LeaveId = _unitOfWork.LeaveTypeRepository.All().Where(x => x.LeaveTypeId == leaveid).FirstOrDefault();
            var P_YearId = new SqlParameter("leaveYearId", SqlDbType.BigInt) { Value = (YearId == null ? CurrentYear : YearId) };
            var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = EmpCode };
            var P_LeaveTypeId = new SqlParameter("leaveTypeId", SqlDbType.BigInt) { Value = LeaveId.LeaveTypeId };
            sp_LeaveApplicationEmployeeBalance_Result Result = _unitOfWork.LeaveBalanceRepository.ExecuteSPwithParameterForList("sp_LeaveApplicationEmployeeBalance @empCode,@leaveYearId,@leaveTypeId", new[] { P_EmpCode, P_YearId, P_LeaveTypeId }).FirstOrDefault();
            Result.LeaveTypeName = LeaveId.LeaveTypeName;
            Result.LeaveTypeAssignment = LeaveId.LeaveType1;
            return LeaveBalanceMapper.SpLeaveBalanceToLeaveBalance(Result, EmpCode, Convert.ToInt32(YearId));
        }
        public LeaveBalance LeaveBalanceSearch()
        {
            LeaveBalance Result = new LeaveBalance();
            // Result.EmpCodeList = _DynamicSelectList.GetEmployeeSelectList().ToList();
            Result.LeaveYearList = _DynamicSelectList.LeaveYearList().ToList();
            Result.LeaveYearId = _LeaveSetUp.LeaveYearList().Where(x => x.YearCurrent == true).Select(x => x.YearId).FirstOrDefault();
            return Result;

        }
        #endregion
        #region LeaveApplication
        public IEnumerable<LeaveApplicationDTOs> LeaveapplicationList()
        {
            List<LeaveApplication> Result = _unitOfWork.LeaveApplicationRepository.All().ToList();
            return LeaveApplicationMapper.ListLeaveApplicationToLeaveApplicationDTOssList(Result);
        }
        public LeaveApplicationDTOs LeaveApplicationSearch()
        {

            int EmpCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            LeaveApplicationDTOs Result = new LeaveApplicationDTOs();
            Result.EmpList = _DynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            Result.LeaveYearList = _DynamicSelectList.LeaveYearList().ToList();
            Result.LeaveStatusList = StaticSelectList.GetLeaveLeaveApprovestatusList().ToList();
            return Result;

        }
        public LeaveApplicationDTOs CreateLeaveApplicationInformation(int empcode, int leaveid, string recommendType)
        {
            LeaveApplicationDTOs Result = new LeaveApplicationDTOs();
            Result.LeaveEmpCode = empcode;
            Result.LeaveTypeId = leaveid;
            Result.LeaveTypeName = _unitOfWork.LeaveTypeRepository.All().Where(x => x.LeaveTypeId == leaveid).Select(x => x.LeaveTypeName).FirstOrDefault();

            var leaveTypeData = _unitOfWork.LeaveTypeRepository.All().Where(x => x.LeaveTypeId == leaveid).FirstOrDefault();
            // Result.Leavetypes.HalfdayAllow = leaveTypeData.HalfdayAllow;

            Result.IsHalfDayAllow = leaveTypeData.HalfdayAllow;


            LeaveBalance LeaveBalance = LeavebalanceIndividual(null, empcode, leaveid);
            if (LeaveBalance.Leave_Balance <= 0)
            {
                throw new Exception("This Leave Is not assigned for you .");
            }
            else
                if (LeaveBalance.LeaveTypeAssignment != "General")
            {
                throw new Exception("Special Leave can not be applied from  here..");
            }
            Result.ApproverList = _DynamicSelectList.GetLeaveApproverSelectList(empcode, recommendType).ToList();
            Result.RecommenderList = _DynamicSelectList.GetLeaveRecommenderSelectList(empcode, recommendType).ToList();
            return Result;
        }
        public LeaveApplicationDTOs LeaveDetails(int LeaveId, int empcode)
        {
            LeaveApplication Detail = _unitOfWork.LeaveApplicationRepository.All().Where(x => x.LeaveId == LeaveId).FirstOrDefault();
            return LeaveApplicationMapper.LeaveApplicationToLeaveApplicationDTOss(Detail);
        }
        public IEnumerable<LeaveApplicationDTOs> LeaveApplicationList()
        {
            List<LeaveApplication> Result = _unitOfWork.LeaveApplicationRepository.All().ToList();
            return LeaveApplicationMapper.ListLeaveApplicationToLeaveApplicationDTOssList(Result);
        }

        public void RejectEmployeeLeave(int id, int Empcode)
        {
            LeaveApplication Record = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveId == id && x.LeaveEmpCode == Empcode).FirstOrDefault();
            Record.LeaveStatus = 1;
            Record.RecommendStatus = 1;
            _unitOfWork.LeaveApplicationRepository.Update(Record);
        }
        public void CancelMyLeave(int id, int Empcode)
        {
            LeaveApplication Record = _unitOfWork.LeaveApplicationRepository.Get(x => x.LeaveId == id && x.LeaveEmpCode == Empcode).FirstOrDefault();
            Record.LeaveStatus = 4;
            Record.RecommendStatus = 4;
            Record.RecommenderMessage = "You canceled your leave application";
            Record.RecommendStatusDate = DateTime.Now;
            Record.ApproverMessage = "You canceled your leave application";
            Record.LeaveStatusDate = DateTime.Now;
            _unitOfWork.LeaveApplicationRepository.Update(Record);
        }
        public void LeaveApplicationCreate(LeaveApplicationDTOs Record)
        {

            LeaveApplication data = _unitOfWork.LeaveApplicationRepository.Create(LeaveApplicationMapper.LeaveApplicationDTOsToLeaveApplication(Record));
        }
        public void LeaveApplicationUpdae(LeaveApplicationDTOs Record)
        {
            _unitOfWork.LeaveApplicationRepository.Update(LeaveApplicationMapper.LeaveApplicationDTOsToLeaveApplication(Record));

            //if leave is approved
            if (Record.LeaveStatus == 2 || Record.LeaveStatus == 3)
            {
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
            else if (Record.LeaveStatus == 5)
            {
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
        }
        #endregion


        #region Leave Assigned 
        public LeaveAssignedDTOs LeaveAssignCreateInfo(int empcode)
        {
            int CurrentYear = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearCurrent == true).Select(x => x.YearId).FirstOrDefault();
            List<LeaveAssigned> data = _unitOfWork.LeaveAssignedRepository.All().Where(x => x.AssignEmpCode == empcode && x.AssignedLeaveYearId == CurrentYear && x.LeaveGainedMonth == 0).ToList();
            LeaveAssignedDTOs Result = new LeaveAssignedDTOs();
            Result.HomeBalance = Convert.ToDecimal(data.Where(x => x.AssignLeaveTypeId == 1).Select(x => x.AssignedDays).FirstOrDefault());
            Result.SickLeaveBalance = Convert.ToDecimal(data.Where(x => x.AssignLeaveTypeId == 2).Select(x => x.AssignedDays).FirstOrDefault());
            Result.NepaliMonth = StaticSelectList.GetnepaliMonth().ToList();
            decimal HomeLeaveBalance = 0;
            decimal SickLeaveBalance = 0;
            decimal CasualLeaveBalance = 0;
            List<LeaveAssignedList> AssignedList = new List<LeaveAssignedList>();
            if (data.Count != 0)
            {
                LeaveAssignedList Item = new LeaveAssignedList();
                Item.MonthName = "Previous year leave balance";
                LeaveAssigned Leave = data.Where(x => x.AssignLeaveTypeId == 1).FirstOrDefault();
                if (Leave != null)
                {
                    HomeLeaveBalance = Convert.ToDecimal(Leave.AssignedDays);
                }
                else
                {
                    HomeLeaveBalance = 0;
                }
                Leave = data.Where(x => x.AssignLeaveTypeId == 2).FirstOrDefault();
                if (Leave != null)
                {
                    SickLeaveBalance = Convert.ToDecimal(Leave.AssignedDays);
                }
                Item.HomeLeaveBalance = HomeLeaveBalance;
                Item.SickLeaveBalance = SickLeaveBalance;
                Item.CasualLeaveBalance = CasualLeaveBalance;
                Item.LeaveGainedMonth = 0;
                AssignedList.Add(Item);
            }
            else
            {
                LeaveAssignedList Item = new LeaveAssignedList();
                Item.MonthName = "Previous year leave balance";

                Item.HomeLeaveBalance = HomeLeaveBalance;
                Item.SickLeaveBalance = SickLeaveBalance;
                Item.CasualLeaveBalance = CasualLeaveBalance;
                Item.LeaveGainedMonth = 0;
                AssignedList.Add(Item);
            }
            List<CalenderMonth> Months = _unitOfWork.CalenderMonthRepository.All().OrderBy(x => x.MonthCode).ToList();
            List<LeaveAssigned> Assigns = _unitOfWork.LeaveAssignedRepository.Get(x => x.AssignEmpCode == empcode && x.AssignedLeaveYearId == CurrentYear && x.LeaveGainedMonth != 0).ToList();
            foreach (CalenderMonth Month in Months)
            {
                HomeLeaveBalance = 0;
                SickLeaveBalance = 0;
                CasualLeaveBalance = 0;
                LeaveAssignedList Item = new LeaveAssignedList();
                Item.MonthName = Month.MonthNameNp;
                LeaveAssigned Leave = Assigns.Where(x => x.AssignLeaveTypeId == 1 && x.LeaveGainedMonth == Month.MonthCode).FirstOrDefault();
                if (Leave != null)
                {
                    HomeLeaveBalance = Convert.ToDecimal(Leave.AssignedDays);
                }
                Leave = Assigns.Where(x => x.AssignLeaveTypeId == 2 && x.LeaveGainedMonth == Month.MonthCode).FirstOrDefault();
                if (Leave != null)
                {
                    SickLeaveBalance = Convert.ToDecimal(Leave.AssignedDays);
                }

                Leave = Assigns.Where(x => x.AssignLeaveTypeId == 3 && x.LeaveGainedMonth == Month.MonthCode).FirstOrDefault();
                if (Leave != null)
                {
                    CasualLeaveBalance = Convert.ToDecimal(Leave.AssignedDays);
                }
                Item.MonthId = Month.MonthId;
                Item.HomeLeaveBalance = HomeLeaveBalance;
                Item.SickLeaveBalance = SickLeaveBalance;
                Item.CasualLeaveBalance = CasualLeaveBalance;
                Item.LeaveGainedMonth = Month.MonthCode;
                AssignedList.Add(Item);
            }
            Result.LeaveAssignedList = AssignedList;
            return Result;
        }
        public void CreateLeaveAssign(LeaveAssignedDTOs data)
        {
            int CurrentYear = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearCurrent == true).Select(x => x.YearId).FirstOrDefault();
            foreach (LeaveAssignedList Item in data.LeaveAssignedList)
            {
                if (Item.LeaveGainedMonth <= 1)
                {
                    LeaveAssigned Record = _unitOfWork.LeaveAssignedRepository.Get(x => x.LeaveGainedMonth == Item.LeaveGainedMonth && x.AssignEmpCode == data.AssignEmpCode && x.AssignedLeaveYearId == CurrentYear && x.AssignLeaveTypeId == 1).FirstOrDefault();
                    if (Record != null)
                    {
                        Record.AssignedDays = Item.HomeLeaveBalance;
                        _unitOfWork.LeaveAssignedRepository.Update(Record);
                    }
                    else
                    {
                        Record = new LeaveAssigned();
                        Record.AssignedDays = Item.HomeLeaveBalance;
                        Record.AssignedLeaveYearId = CurrentYear;
                        Record.LeaveGainedMonth = Item.LeaveGainedMonth;
                        Record.AssignedRemarks = "0";
                        Record.AssignLeaveTypeId = 1;
                        Record.AssignEmpCode = data.AssignEmpCode;
                        _unitOfWork.LeaveAssignedRepository.Create(Record);
                    }


                    Record = _unitOfWork.LeaveAssignedRepository.Get(x => x.LeaveGainedMonth == Item.LeaveGainedMonth && x.AssignEmpCode == data.AssignEmpCode && x.AssignedLeaveYearId == CurrentYear && x.AssignLeaveTypeId == 2).FirstOrDefault();
                    if (Record != null)
                    {
                        Record.AssignedDays = Item.SickLeaveBalance;
                        _unitOfWork.LeaveAssignedRepository.Update(Record);
                    }
                    else
                    {
                        Record = new LeaveAssigned();
                        Record.AssignedDays = Item.SickLeaveBalance;
                        Record.AssignedLeaveYearId = CurrentYear;
                        Record.LeaveGainedMonth = Item.LeaveGainedMonth;
                        Record.AssignedRemarks = "0";
                        Record.AssignLeaveTypeId = 2;
                        Record.AssignEmpCode = data.AssignEmpCode;
                        _unitOfWork.LeaveAssignedRepository.Create(Record);
                    }

                    Record = _unitOfWork.LeaveAssignedRepository.Get(x => x.LeaveGainedMonth == Item.LeaveGainedMonth && x.AssignEmpCode == data.AssignEmpCode && x.AssignedLeaveYearId == CurrentYear && x.AssignLeaveTypeId == 3).FirstOrDefault();
                    if (Record != null)
                    {
                        Record.AssignedDays = Item.CasualLeaveBalance;
                        _unitOfWork.LeaveAssignedRepository.Update(Record);
                    }
                    else
                    {
                        Record = new LeaveAssigned();
                        Record.AssignedDays = Item.CasualLeaveBalance;
                        Record.AssignedLeaveYearId = CurrentYear;
                        Record.LeaveGainedMonth = Item.LeaveGainedMonth;
                        Record.AssignedRemarks = "0";
                        Record.AssignLeaveTypeId = 3;
                        Record.AssignEmpCode = data.AssignEmpCode;
                        _unitOfWork.LeaveAssignedRepository.Create(Record);
                    }


                }
                else
                {
                    LeaveAssigned Record = _unitOfWork.LeaveAssignedRepository.Get(x => x.LeaveGainedMonth == Item.LeaveGainedMonth && x.AssignEmpCode == data.AssignEmpCode && x.AssignedLeaveYearId == CurrentYear && x.AssignLeaveTypeId == 1).FirstOrDefault();
                    if (Record != null)
                    {
                        Record.AssignedDays = Item.HomeLeaveBalance;
                        _unitOfWork.LeaveAssignedRepository.Update(Record);
                    }
                    else
                    {
                        Record = new LeaveAssigned();
                        Record.AssignedDays = Item.HomeLeaveBalance;
                        Record.AssignedLeaveYearId = CurrentYear;
                        Record.LeaveGainedMonth = Item.LeaveGainedMonth;
                        Record.AssignedRemarks = "0";
                        Record.AssignLeaveTypeId = 1;
                        Record.AssignEmpCode = data.AssignEmpCode;
                        _unitOfWork.LeaveAssignedRepository.Create(Record);
                    }
                }

            }

        }


        #endregion



        #region leave Yearly Report

        public LeaveYearlyReportViewModel YearlyOfficewiseLeaveReport(int empCode, int LeaveTypeId, int LeaveYearId)
        {
            var P_YearId = new SqlParameter("leaveYearId", SqlDbType.BigInt) { Value = LeaveYearId };
            var P_EmpCode = new SqlParameter("empCode", SqlDbType.BigInt) { Value = empCode };
            var P_LeaveTypeId = new SqlParameter("leaveTypeId", SqlDbType.BigInt) { Value = LeaveTypeId };
            sp_LeaveApplicationEmployeeBalance_Result single = _unitOfWork.LeaveBalanceRepository.ExecuteSPwithParameterForList("sp_LeaveApplicationEmployeeBalance @empCode,@leaveYearId,@leaveTypeId", new[] { P_EmpCode, P_YearId, P_LeaveTypeId }).FirstOrDefault();

            return LeaveBalanceMapper.SpLeaveBalanceListToYearlyLeaveBalanceList(single, empCode, Convert.ToInt32(LeaveYearId));


        }

        #endregion
    }



}




