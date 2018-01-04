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
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private EmployeeJobHistory _ejh;


        public EmployeeService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _ejh = new EmployeeJobHistory();
        }

        public EmployeeDTO GetLoginData(EmployeeDTO loginModel)
        {

            Employee loginData = _unitOfWork.EmployeeRepository.Get(x => x.EmpUserName == loginModel.EmpUserName && x.EmpPassword == loginModel.EmpPassword).FirstOrDefault();

            if (loginData != null && loginData.EmpStatus == true)
            {
                return EmployeeRequestFormatter.ConvertRespondentInfoToDTO(loginData); ;
            }
            throw new UnauthorizedAccessException("Username or Password is invalid!!!");
            return null;


        }

        public EmployeeDTO InsertEmployee(EmployeeDTO newEmployee)
        {
            Employee emp = EmployeeRequestFormatter.ConvertRespondentInfoFromDTO(newEmployee);
            _ejh.EmpCode = emp.EmpCode;
            _ejh.DeptId = emp.EmpDeptId;
            _ejh.OfficeId = emp.EmpOfficeId;


            _ejh.SectionId = emp.EmpSectionId;
            _ejh.DesgId = emp.EmpDesgId;
            _ejh.RankId = emp.EmpRankId;
            _ejh.LevelId = emp.EmpLevelId;
            _ejh.BusinessGroupId = emp.EmpBgId;
            _ejh.ShiftId = emp.EmpShiftId;
            _ejh.JobTypeId = emp.EmpJobTypeId;
            _ejh.ServiceEventGroupId = newEmployee.EmpServiceEventGroupId;
            _ejh.ServiceEventSubGroupId = newEmployee.EmpServiceSubEventGroupId;
            _ejh.KaajType = "V";
            EmployeeDTO employee = new EmployeeDTO();

            employee = EmployeeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeRepository.Create(emp));
            _unitOfWork.EmployeeJobHistoryRepository.Create(_ejh);
            return employee;
        }

        public IEnumerable<EmployeeDTO> GetEmployeeList(int EmpCode)
        {
            List<EmployeeDTO> phoneModelList = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeLists", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return phoneModelList = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }

        public bool EmployeeExists(EmployeeDTO newEmp)
        {
            Employee emp = EmployeeRequestFormatter.ConvertRespondentInfoFromDTO(newEmp);
            return _unitOfWork.EmployeeRepository.Exists(emp);
        }

        public EmployeeEditViewModel GetEmployeeByID(int id)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeDetailsById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            int cnt = dt.Rows.Count;
            if (cnt <= 0)
            {
                return new EmployeeEditViewModel();
            }
            return new EmployeeEditViewModel
            {
                Code = dt.Rows[0]["EmpCode"].ToString(),
                CustomerId = dt.Rows[0]["CustomerId"].ToString(),
                Name = dt.Rows[0]["EmpName"].ToString(),
                Username = dt.Rows[0]["EmpUserName"].ToString(),
                AppointedDate = dt.Rows[0]["EmpAppointmentDate"].ToString(),
                PhotoName = dt.Rows[0]["EmpPhoto"].ToString(),
                Gender = dt.Rows[0]["EmpGender"].ToString(),
                Status = dt.Rows[0]["EmpStatus"].ToString(),
                MaritalStatus = dt.Rows[0]["EmpMaritalStatus"].ToString(),
                IsIncapacitated = dt.Rows[0]["EmpIncapacitated"].ToString(),
                IsDepartmentHead = dt.Rows[0]["EmpDeptHead"].ToString(),
                IsOfficeHead = dt.Rows[0]["EmpOfficeHead"].ToString(),
                AttenfanceIgnore = dt.Rows[0]["EmpAttendanceIgnore"].ToString(),
                Payroll = dt.Rows[0]["EmpPayroll"].ToString(),
                Email = dt.Rows[0]["EmpEmail"].ToString(),
                Password = dt.Rows[0]["EmpPassword"].ToString(),
                ////Employee Details
                AdvNumber = dt.Rows[0]["EmpAppointmentAdvNumber"].ToString(),
                PermanentAddress = dt.Rows[0]["EmpPAddress"].ToString(),
                TempAddress = dt.Rows[0]["EmpTAddress"].ToString(),
                HomePhone = dt.Rows[0]["EmpHomePhone"].ToString(),
                MobileNumber = dt.Rows[0]["EmpMobileNumber"].ToString(),
                OfficeEmail = dt.Rows[0]["EmpOfficeEmail"].ToString(),
                OfficePhone = dt.Rows[0]["EmpOfficePhone"].ToString(),
                CitizenNumber = dt.Rows[0]["EmpCitizenshipNumber"].ToString(),
                CitIssDate = dt.Rows[0]["EmpCitizenshipIssuDate"].ToString(),
                CitIssDistrictId = dt.Rows[0]["EmpCitizenshipDistrictId"].ToString(),
                Huliya = dt.Rows[0]["EmpHuliya"].ToString(),
                DateOfBirth = dt.Rows[0]["EmpDob"].ToString(),
                MarriageAnniversary = dt.Rows[0]["EmpMarriageAnniversary"].ToString(),
                AgeRetireDate = dt.Rows[0]["EmpAgeRetireDate"].ToString(),
                EthnicityId = dt.Rows[0]["EmpEthnicityDid"].ToString(),
                ReligionInd = dt.Rows[0]["EmpReligionId"].ToString(),
                BloodGroup = dt.Rows[0]["EmpBloodGroup"].ToString(),
                PANNumber = dt.Rows[0]["EmpPANNumber"].ToString(),
                CitNumber = dt.Rows[0]["EmpCITNumber"].ToString(),
                PFNumber = dt.Rows[0]["EmpPFNumber"].ToString(),
                DrivingLicenseNumber = dt.Rows[0]["EmpDrivingLicenseNumber"].ToString(),
                PassportNumber = dt.Rows[0]["EmpPassportNumber"].ToString(),
                TwitterLink = dt.Rows[0]["EmpTwitter"].ToString(),
                FBLink = dt.Rows[0]["EmpFacebook"].ToString(),
                LinkedInLink = dt.Rows[0]["EmpLinkedin"].ToString(),
                EmerContactName = dt.Rows[0]["EmergencyContactName"].ToString(),
                EmerContactRelation = dt.Rows[0]["EmergencyContactRelation"].ToString(),
                EmerContact = dt.Rows[0]["EmergencyContact"].ToString(),
                EmerAddress = dt.Rows[0]["EmergencyAddress"].ToString(),
                NomineeName = dt.Rows[0]["NomineeName"].ToString(),
                NomineeDob = dt.Rows[0]["NomineeDob"].ToString(),
                NomineeAddress = dt.Rows[0]["NomineeAddress"].ToString(),
                NomineeRelation = dt.Rows[0]["NomineeRelation"].ToString(),
                NomCitNo = dt.Rows[0]["NomineeCitizenshipNumber"].ToString(),
                NomCitIssueDate = dt.Rows[0]["NomineeCitizenshipIssuDate"].ToString(),
                NomCitIssDistrictId = dt.Rows[0]["NomineeCitizenshipIssueDistrictId"].ToString(),
                NomineePhoto = dt.Rows[0]["NomineePhoto"].ToString(),
                Nationality = dt.Rows[0]["Nationality"].ToString(),
                SpouseName = dt.Rows[0]["SpouseName"].ToString(),
                EmployeeHeight = dt.Rows[0]["EmployeeHeight"].ToString(),
                PfEffectiveDate = dt.Rows[0]["PfEffectiveDate"].ToString(),
                GratuityEffectiveDate = dt.Rows[0]["GratuityEffectiveDate"].ToString(),
                EmployeeBankAccountNumber = dt.Rows[0]["EmployeeBankAccountNumber"].ToString(),
                EmpBgId = Convert.ToInt32(dt.Rows[0]["EmpBgId"]),
                EmpShiftId = Convert.ToInt32(dt.Rows[0]["EmpShiftId"]),
                EmpTypeId = Convert.ToInt32(dt.Rows[0]["EmpTypeId"]),
                CurrentGrade = (dt.Rows[0]["CurrentGrade"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["CurrentGrade"]) : 0,
                EmpTaxSetupId = (dt.Rows[0]["EmpTaxSetupId"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["EmpTaxSetupId"]) : 0



            };
        }

        public int UpdateEmployee(EmployeeEditViewModel empToEdit)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeEdit", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", empToEdit.Code);
            cmd.Parameters.AddWithValue("@CustomerId", empToEdit.CustomerId);
            cmd.Parameters.AddWithValue("@EmpName", empToEdit.Name);
            cmd.Parameters.AddWithValue("@EmpJoinDate", empToEdit.AppointedDate);
            cmd.Parameters.AddWithValue("@EmpPhoto", empToEdit.PhotoName);
            cmd.Parameters.AddWithValue("@EmpGender", empToEdit.Gender);
            cmd.Parameters.AddWithValue("@EmpStatus", empToEdit.Status);
            cmd.Parameters.AddWithValue("@EmpMaritalStatus", empToEdit.MaritalStatus);
            cmd.Parameters.AddWithValue("@EmpIncapacitated", empToEdit.IsIncapacitated);
            cmd.Parameters.AddWithValue("@EmpDeptHead", empToEdit.IsDepartmentHead);
            cmd.Parameters.AddWithValue("@EmpOfficeHead", empToEdit.IsOfficeHead);
            cmd.Parameters.AddWithValue("@EmpAttendanceIgnore", empToEdit.AttenfanceIgnore);
            cmd.Parameters.AddWithValue("@EmpPayroll", empToEdit.Payroll);
            cmd.Parameters.AddWithValue("@EmpEmail", empToEdit.Email);
            cmd.Parameters.AddWithValue("@EmpPassword", empToEdit.Password);
            cmd.Parameters.AddWithValue("@EmpBgId", empToEdit.EmpBgId);
            cmd.Parameters.AddWithValue("@EmpTypeId", empToEdit.EmpTypeId);
            cmd.Parameters.AddWithValue("@EmpShiftId", empToEdit.EmpShiftId);
            cmd.Parameters.AddWithValue("@EmpCurrentGradeNumber", empToEdit.CurrentGrade);
            cmd.Parameters.AddWithValue("@EmpTaxRuleid", empToEdit.EmpTaxSetupId);
            //Employee Details
            cmd.Parameters.AddWithValue("@EmpAppointmentAdvNumber", empToEdit.AdvNumber);
            cmd.Parameters.AddWithValue("@EmpDOB", empToEdit.DateOfBirth);
            cmd.Parameters.AddWithValue("@EmpMarriageAnniversary", empToEdit.MarriageAnniversary);
            cmd.Parameters.AddWithValue("@EmpEthnicity", empToEdit.EthnicityId);
            cmd.Parameters.AddWithValue("@EmpReligionId", empToEdit.ReligionInd);
            cmd.Parameters.AddWithValue("@EmpBloodGroup", empToEdit.BloodGroup);
            cmd.Parameters.AddWithValue("@EmpPanNumber", empToEdit.PANNumber);
            cmd.Parameters.AddWithValue("@EmpCitNumber", empToEdit.CitNumber);
            cmd.Parameters.AddWithValue("@EmpPFNumber", empToEdit.PFNumber);
            cmd.Parameters.AddWithValue("@EmpDrivingLicenseNumber", empToEdit.DrivingLicenseNumber);
            cmd.Parameters.AddWithValue("@EmpPassportNumber", empToEdit.PassportNumber);
            cmd.Parameters.AddWithValue("@EmpAgeRetireDate", empToEdit.AgeRetireDate);
            cmd.Parameters.AddWithValue("@EmpHuliya", empToEdit.Huliya);
            cmd.Parameters.AddWithValue("@EmpOfficeEmail", empToEdit.OfficeEmail);
            cmd.Parameters.AddWithValue("@EmpOfficePhone", empToEdit.OfficePhone);
            cmd.Parameters.AddWithValue("@EmpHomePhone", empToEdit.HomePhone);
            cmd.Parameters.AddWithValue("@EmpPAddress", empToEdit.PermanentAddress);
            cmd.Parameters.AddWithValue("@EmpTAddress", empToEdit.TempAddress);
            cmd.Parameters.AddWithValue("@EmpMobileNumber", empToEdit.MobileNumber);
            cmd.Parameters.AddWithValue("@EmpEmergencyContactName", empToEdit.EmerContactName);
            cmd.Parameters.AddWithValue("@EmpEmergencyContactRelation", empToEdit.EmerContactRelation);
            cmd.Parameters.AddWithValue("@EmpEmergencyContact", empToEdit.EmerContact);
            cmd.Parameters.AddWithValue("@EmpEmergencyAddress", empToEdit.EmerAddress);
            cmd.Parameters.AddWithValue("@EmpFacebook", empToEdit.FBLink);
            cmd.Parameters.AddWithValue("@EmpTwitter", empToEdit.TwitterLink);
            cmd.Parameters.AddWithValue("@EmpLinkedIn", empToEdit.LinkedInLink);
            cmd.Parameters.AddWithValue("@EmpHiringMethodId", empToEdit.HiringMethodId);
            cmd.Parameters.AddWithValue("@EmpCitizenshipNumber", empToEdit.CitizenNumber);
            cmd.Parameters.AddWithValue("@EmpCitizenshipIssueDate", empToEdit.CitIssDate);
            cmd.Parameters.AddWithValue("@EmpCitizenshipDistrictId", empToEdit.CitIssDistrictId);
            cmd.Parameters.AddWithValue("@EmpNomineeName", empToEdit.NomineeName);
            cmd.Parameters.AddWithValue("@EmpNomineeAddress", empToEdit.NomineeAddress);
            cmd.Parameters.AddWithValue("@EmpNomineePhoto", empToEdit.NomineePhoto);
            cmd.Parameters.AddWithValue("@EmpNomineeDOB", empToEdit.NomineeDob);
            cmd.Parameters.AddWithValue("@EmpNomineeCitizenshipNumber", empToEdit.NomCitNo);
            cmd.Parameters.AddWithValue("@EmpNomineeCitizenshipIssueDate", empToEdit.NomCitIssueDate);
            cmd.Parameters.AddWithValue("@EmpNomineeCitizenshipIssueDistrictId", empToEdit.NomCitIssDistrictId);
            cmd.Parameters.AddWithValue("@EmpNomineeRelation", empToEdit.NomineeRelation);
            cmd.Parameters.AddWithValue("@Nationality", empToEdit.Nationality);
            cmd.Parameters.AddWithValue("@SpouseName", empToEdit.SpouseName);
            cmd.Parameters.AddWithValue("@EmployeeHeight", empToEdit.EmployeeHeight);
            cmd.Parameters.AddWithValue("@PfEffectiveDate", empToEdit.PfEffectiveDate);
            cmd.Parameters.AddWithValue("@GratuityEffectiveDate", empToEdit.GratuityEffectiveDate);
            cmd.Parameters.AddWithValue("@EmployeeBankAccountNumber", empToEdit.EmployeeBankAccountNumber);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return 0;
        }

        public EmployeeDetailsViewModel GetEmployeeDetails(int id)
        {
            EmployeeDetailsViewModel empl = new EmployeeDetailsViewModel();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return new EmployeeDetailsViewModel
            {
                Username = dt.Rows[0]["EmpUserName"].ToString(),
                ContractExpiryDate = dt.Rows[0]["EmpContractExpiryDate"].ToString(),
                MaritalStatus = dt.Rows[0]["MaritalStatus"].ToString(),
                Incapacitated = dt.Rows[0]["EmpIncapacitated"].ToString(),
                AttendanceIgnore = dt.Rows[0]["EmpAttendanceIgnore"].ToString(),
                Contact = dt.Rows[0]["Contact"].ToString(),
                OfficeName = dt.Rows[0]["OfficeName"].ToString(),
                Department = dt.Rows[0]["DeptName"].ToString(),
                Group = dt.Rows[0]["GroupName"].ToString(),
                Rank = dt.Rows[0]["RankName"].ToString(),
                Shift = dt.Rows[0]["ShiftName"].ToString(),
                Designation = dt.Rows[0]["DesignationName"].ToString(),
                ServiceStatus = dt.Rows[0]["ServiceStatus"].ToString(),
                DateOfBirth = dt.Rows[0]["EmpDob"].ToString(),
                PAN = dt.Rows[0]["PAN"].ToString(),
                PFNUmber = dt.Rows[0]["PFNumber"].ToString(),
                CITNumber = dt.Rows[0]["CITNumber"].ToString(),
                RemoteArea = dt.Rows[0]["RemoteArea"].ToString(),
                CreatedDate = Convert.ToDateTime(dt.Rows[0]["EmpCreatedDate"].ToString()),
                Email = dt.Rows[0]["EmpEmail"].ToString(),
                MarriageAnniversary = dt.Rows[0]["EmpMarriageAnniversary"].ToString(),
                BloodGroup = dt.Rows[0]["BloodGroup"].ToString(),
                Level = dt.Rows[0]["LevelName"].ToString(),
                Name = dt.Rows[0]["EmpName"].ToString(),
                OfficeId = Convert.ToInt32(dt.Rows[0]["EmpOfficeId"].ToString()),
                Code = Convert.ToInt32(dt.Rows[0]["EmpCode"].ToString()),
                PhotoName = dt.Rows[0]["EmpPhoto"].ToString(),
                JoinDate = dt.Rows[0]["EmpAppointmentDate"].ToString(),
                JobType = dt.Rows[0]["JobType"].ToString(),
                BusinessGroup = dt.Rows[0]["BusinessGroup"].ToString(),
                LeaveRuleId = Convert.ToInt32(dt.Rows[0]["EmpLeaveRuleId"].ToString()),
                RemoteCode = dt.Rows[0]["EmpRemoteType"].ToString(),
                //AppointBranch = dt.Rows[0]["AppointOfficeName"].ToString(),
                //AppointDate = dt.Rows[0]["AppointDate"].ToString(),
                //AppointDepartment = dt.Rows[0]["AppointDeptName"].ToString(),
                //AppointDesignation = dt.Rows[0]["AppointDesignationName"].ToString(),
                //AppointGroup = dt.Rows[0]["AppointGroupName"].ToString(),
                //AppointLevel = dt.Rows[0]["AppointLevel"].ToString(),
                //AppointRank = dt.Rows[0]["AppointRankName"].ToString(),
                //AppointService = dt.Rows[0]["AppointServiceStatus"].ToString(),
                //AppointShift = dt.Rows[0]["AppointShiftName"].ToString()
                //ExpiryDate = dt.Rows[0]["ExpiryDate"].ToString()

            };
        }
        public IEnumerable<EmployeeAppointmentDetailsViewModel> GetEmployeeAppointmentDetails(int id)
        {
            //IEnumerable<EmployeeAppointmentDetailsViewModel> empl = new IEnumerable<EmployeeAppointmentDetailsViewModel>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeDetailsWithJobHistories", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeAppointmentDetailsViewModel
                {
                    AppointBranch = dr["AppointOfficeName"].ToString(),
                    AppointDate = dr["AppointDate"].ToString(),
                    AppointDepartment = dr["AppointDeptName"].ToString(),
                    AppointDesignation = dr["AppointDesignationName"].ToString(),
                    //AppointGroup = dr["AppointGroupName"].ToString(),
                    AppointLevel = dr["AppointLevel"].ToString(),
                    AppointRank = dr["AppointRankName"].ToString(),
                    AppointService = dr["AppointServiceStatus"].ToString(),
                    AppointShift = dr["AppointShiftName"].ToString(),
                    ExpiryDate = dr["ExpiryDate"].ToString()
                };
            }
        }

        public IEnumerable<EmployeeDTO> SearchEmployeesByRoleId(int? officId, int? roleId)
        {
            List<EmployeeDTO> phoneModelList = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchEmployeeByRoleId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@officeId", officId);
            cmd.Parameters.AddWithValue("@RoleID", roleId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return phoneModelList = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();

        }


        public IEnumerable<EmployeeDTO> GetApproverByOffice(int officeId)
        {
            List<EmployeeDTO> EmpApprover = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_ApproverByOffice", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@officeId", officeId);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();


            return EmpApprover = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }


        public IEnumerable<EmployeeDTO> GetLeaveApprover(int EmpCode, string approverType)
        {
            List<EmployeeDTO> EmpMyrecommender = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApprover", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            cmd.Parameters.AddWithValue("@approverType", approverType);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return EmpMyrecommender = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }
        public IEnumerable<EmployeeDTO> GetLeaveRecommander(int EmpCode, string recommendType)
        {
            List<EmployeeDTO> EmpMyrecommender = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveRecommender", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            cmd.Parameters.AddWithValue("@recommendType", recommendType);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return EmpMyrecommender = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }

        public IEnumerable<EmployeeDTO> GetAttendanceApprover(int EmpCode)
        {
            List<EmployeeDTO> EmpApprover = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceApprover", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();


            return EmpApprover = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }
        public IEnumerable<EmployeeDTO> GetAttendanceRecommander(int EmpCode)
        {
            List<EmployeeDTO> EmpMyrecommender = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceRecommender", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return EmpMyrecommender = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }

        public IEnumerable<DistrictViewModel> GetDistrictList()
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetDistrictsList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new DistrictViewModel
                {
                    Id = Convert.ToInt32(dr["DistrictId"].ToString()),
                    Name = dr["DistrictName"].ToString()
                };
            }
        }

        public IEnumerable<EmployeeDTO> GetEmployeeByOfficeid(int id)
        {
            List<EmployeeDTO> EmpApprover = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select EmpCode,CustomerId,EmpName from Employees where EmpOfficeId="+id, conn);
            cmd.CommandType = CommandType.Text;

            //cmd.Parameters.AddWithValue("@officeid", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();


            return EmpApprover = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();

        }


        public IEnumerable<EmployeeDTO> GetEmployeeDetailsByOfficeid(int id)
        {
            List<EmployeeDTO> EmpApprover = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            string qry = "Select EmpCode,EmpName,CurrentGrade,EmpCurrentRankAppDate from Employees where EmpOfficeId ="+id+"and EmpStatus=1";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeDTO
                {
                    EmpCode = Convert.ToInt32(dr["EmpCode"]),
                    EmpName = dr["EmpName"].ToString(),
                    CurrentGrade = DBNull.Value.Equals(dr["CurrentGrade"])? 0: Convert.ToInt32(dr["CurrentGrade"]),  
                    EmpCurrentRankUpgradeDate = DBNull.Value.Equals(dr["CurrentGrade"]) ? null : (DateTime?)(dr["EmpCurrentRankAppDate"])
                };
            }

            //return EmpApprover = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();

        }

        public IEnumerable<EmployeeDTO> GetEmployeeByDesgination(int id)
        {
            var employeeList = _unitOfWork.EmployeeRepository.All();
            IEnumerable<EmployeeDTO> list = (from emp in employeeList
                                             select new EmployeeDTO
                                             {
                                                 EmpCode = emp.EmpCode,
                                                 EmpName = emp.EmpName
                                             }).Where(x => x.EmpDesgId == id);
            return list;

        }


        public int UpdatePassword(int Empcode, string Password)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_ChangeUserpassword", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empCode", Empcode);
            cmd.Parameters.AddWithValue("@Password", Password);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return 0;
        }

        public int GetIsProfileViewable(int id, int adminEmpcode)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_IsProfileViewable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeProfileEmpCode", id);
            cmd.Parameters.AddWithValue("@AdminEmpCode", adminEmpcode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return Convert.ToInt16(dt.Rows[0]["IsAccible"]);
        }


        public int UpdateEmployeeRoles(int Empcode, int RoleId)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateEmpRole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empcode", Empcode);
            cmd.Parameters.AddWithValue("@roleId", RoleId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return 0;
        }
        public IEnumerable<EmployeeFamilyViewModel> GetEmployeeFamilyByID(int id)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from EmployeeFamilies where empcode = '" + id + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeFamilyViewModel
                {
                    FamilyId = Convert.ToInt32(dr["FamilyId"].ToString()),
                    EmpCode = Convert.ToInt32(dr["EmpCode"].ToString()),
                    Fname = dr["Fname"].ToString(),
                    FDob = dr["FDob"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(dr["FDob"]),
                    FContactAddress = dr["FContactAddress"].ToString(),
                    FContactNumber = dr["FContactNumber"].ToString(),
                    FGender = dr["FGender"].ToString(),
                    FRelation = dr["FRelation"].ToString()
                };
            }
        }
        public IEnumerable<EmployeeDocumentViewModel> GetEmployeeDocumentsByID(int id)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("select * from EmployeeDocuments where documentempcode = '" + id + "'", conn);
            SqlCommand cmd = new SqlCommand("select ek.DocumentRemarks,ek.DocumentCreatedAt,ek.DocumentTitle, s.CatTitle from EmployeeDocuments ek join DocumentCategory s on ek.DocumentCatId = s.CatId where ek.documentempcode = '" + id + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeDocumentViewModel
                {
                    //DocumentId = Convert.ToInt32(dr["DocumentId"].ToString()),
                    //DocumentCatId = Convert.ToInt32(dr["DocumentCatId"].ToString()),
                    CatTitle = dr["CatTitle"].ToString(),
                    DocumentCreatedAt = Convert.ToDateTime(dr["DocumentCreatedAt"].ToString()),
                    //DocumentEmpCode = Convert.ToInt32(dr["DocumentEmpCode"].ToString()),
                    //DocumentOnlyAdmin = Convert.ToBoolean(dr["DocumentOnlyAdmin"].ToString()),
                    DocumentRemarks = dr["DocumentRemarks"].ToString(),
                    DocumentTitle = dr["DocumentTitle"].ToString(),
                    //DocumentVerified = Convert.ToBoolean(dr["DocumentVerified"].ToString()),
                };
            }


        }
        public IEnumerable<EmployeeSkillViewModel> GetEmployeeSkillsByID(int id)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ek.EmpCode,ek.EmpSkillPoint,ek.EmpSkillStatus,s.SkillName from EmployeeSkills ek join Skills s on ek.EmpSkillId = s.SkillId where EmpCode = '" + id + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeSkillViewModel
                {
                    SkillName = dr["SkillName"].ToString(),
                    EmpSkillPoint = Convert.ToInt32(dr["EmpSkillPoint"].ToString()),
                    EmpSkillStatus = Convert.ToBoolean(dr["EmpSkillStatus"].ToString())
                };
            }
        }
        public IEnumerable<EmployeeExperinceViewModel> GetEmployeeExperiencesByID(int id)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from employeeExperience where empCode= '" + id + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeExperinceViewModel
                {
                    CompanyName = dr["CompanyName"].ToString(),
                    EmpCode = Convert.ToInt32(dr["EmpCode"].ToString()),
                    ExpId = Convert.ToInt32(dr["ExpId"].ToString()),
                    CompanyContact = dr["CompanyContact"].ToString(),
                    Designation = dr["Designation"].ToString(),
                    EnDate = Convert.ToDateTime(dr["EnDate"].ToString()),
                    JobDescription = dr["JobDescription"].ToString(),
                    RefDesignation = dr["RefDesignation"].ToString(),
                    RefEmail = dr["RefEmail"].ToString(),
                    RefName = dr["RefName"].ToString(),
                    RefPhone = dr["RefPhone"].ToString(),
                    StartDate = Convert.ToDateTime(dr["StartDate"])
                };
            }
        }
        public IEnumerable<EmployeeExperinceViewModel> GetEmployeeExperienceDetailsByID(int id)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from employeEexperience where expId= '" + id + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeExperinceViewModel
                {
                    CompanyName = dr["CompanyName"].ToString(),
                    EmpCode = Convert.ToInt32(dr["EmpCode"].ToString()),
                    ExpId = Convert.ToInt32(dr["ExpId"].ToString()),
                    CompanyContact = dr["CompanyContact"].ToString(),
                    Designation = dr["Designation"].ToString(),
                    EnDate = Convert.ToDateTime(dr["EnDate"].ToString()),
                    JobDescription = dr["JobDescription"].ToString(),
                    RefDesignation = dr["RefDesignation"].ToString(),
                    RefEmail = dr["RefEmail"].ToString(),
                    RefName = dr["RefName"].ToString(),
                    RefPhone = dr["RefPhone"].ToString(),
                    StartDate = Convert.ToDateTime(dr["StartDate"])
                };
            }
        }
        public int UpdateEmployeeLeaveRuleId(int Empcode, int LeaveId)
        {
            try
            {
                var conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Employees SET EmpLeaveRuleId=" + LeaveId + "WHERE EmpCode=" + Empcode, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmpRoleId", LeaveId);
                cmd.Parameters.AddWithValue("@EmpCode", Empcode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ&#@$%";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public IEnumerable<EmployeeDTO> EmployeeListByRoleId(int RoleId)
        {

            List<EmployeeDTO> EmpApprover = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeListByRoleId", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleId", RoleId); 

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();


            return EmpApprover = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }

        public IEnumerable<EmployeeDTO> GetEmployeeLists(int roleId)
        {
            List<EmployeeDTO> phoneModelList = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeLists", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", roleId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            phoneModelList =
                  (from DataRow dr in dt.Rows
                   select new EmployeeDTO()
                   {
                       Address = dr["Address"].ToString(),
                       JobTypeName = dr["JobTypeName"].ToString(),
                       EmpCurrentRankAppDate = dr["EmpCurrentRankAppDate"].ToString(),
                       OfficeParentId = dr["OfficeParentId"].ToString(),
                       EmpGender = dr["EmpGender"].ToString(),
                       DegreeName = dr["DegreeName"].ToString(),
                       DeptName = dr["DeptName"].ToString(),
                       DistrictName = dr["DistrictName"].ToString(),
                       Division = dr["Division"].ToString(),
                       DsgName = dr["DsgName"].ToString(),
                       //EmpAppointmentDate =Convert.ToDateTime(dr["EmpAppointmentDate"].ToString()),
                       EmpName = dr["EmpName"].ToString(),
                       EthnicityName = dr["EthnicityName"].ToString(),
                       FacultyName = dr["FacultyName"].ToString(),
                       OfficeName = dr["OfficeName"].ToString(),
                       //JoinDate =Convert.ToDateTime(dr["JoinDate"].ToString()),
                       SectionName = dr["SectionName"].ToString(),
                       RankName = dr["RankName"].ToString(),
                       EmpDob = dr["EmpDob"].ToString(),
                       EmpCode = Convert.ToInt32(dr["EmpCode"].ToString()),


                   }).ToList();
            return phoneModelList;
            //= new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();
        }
        public string GetUpperOfficeName(int id)
        {
            if (id == 0)
            {
                return "Head Office";
            }
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select OfficeName from Offices where OfficeId = '" + id + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmployeeProfileEmpCode", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return dt.Rows[0]["OfficeName"].ToString(); ;
        }

        public EmployeeAppointmentDetailsViewModel GetEmployeeAppointmentDetail(int id)
        {
            //IEnumerable<EmployeeAppointmentDetailsViewModel> empl = new IEnumerable<EmployeeAppointmentDetailsViewModel>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmployeeDetailsWithJobHistories", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            if (dt.Rows.Count < 1)
            {
                return new EmployeeAppointmentDetailsViewModel
                {
                    AppointBranch = "",
                    AppointDate = "",
                    AppointDepartment = "",
                    AppointDesignation = "",
                    //AppointGroup = dr["AppointGroupName"].ToString(),
                    AppointLevel = "",
                    AppointRank = "",
                    AppointService = "",
                    AppointShift = "",
                    ExpiryDate = ""
                };
            }
            else
            {
                return new EmployeeAppointmentDetailsViewModel
                {
                    AppointBranch = dt.Rows[0]["AppointOfficeName"].ToString(),
                    AppointDate = dt.Rows[0]["AppointDate"].ToString(),
                    AppointDepartment = dt.Rows[0]["AppointDeptName"].ToString(),
                    AppointDesignation = dt.Rows[0]["AppointDesignationName"].ToString(),
                    //AppointGroup = dr["AppointGroupName"].ToString(),
                    AppointLevel = dt.Rows[0]["AppointLevel"].ToString(),
                    AppointRank = dt.Rows[0]["AppointRankName"].ToString(),
                    AppointService = dt.Rows[0]["AppointServiceStatus"].ToString(),
                    AppointShift = dt.Rows[0]["AppointShiftName"].ToString(),
                    ExpiryDate = dt.Rows[0]["ExpiryDate"].ToString()
                };
            }

        }

        public int InsertApprover(int OfficeId, int Empcode)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_InsertIntoApprover", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empcode", Empcode);
            cmd.Parameters.AddWithValue("@OfficeId", OfficeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return 0;
        }

        public void UpdateEncryptPassword()
        {
            List<Employee> Record = _unitOfWork.EmployeeRepository.All().Where(x => x.EmpStatus == true).ToList();
            foreach (Employee Emp in Record)
            {
                MD5 md5Hash = MD5.Create();
                string hash = GetMd5Hash(md5Hash, Emp.EmpPassword);
                UpdatePassword(Emp.EmpCode, hash);
            }

        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {


            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString());
            }
            return sBuilder.ToString();
        }
        public IEnumerable<EmployeeDTO> GetEmployeeByOfficeidPayroll(int OfficeId)
        {
            IEnumerable<Employee> Domain = _unitOfWork.EmployeeRepository.Get(x => x.EmpOfficeId == OfficeId && x.EmpStatus == true).ToList();
            List<EmployeeDTO> Model = new List<EmployeeDTO>();
            foreach (Employee Employee in Domain)
            {
                EmployeeDTO Record = EmployeeRequestFormatter.ConvertRespondentInfoToDTO(Employee);
                Model.Add(Record);
            }
            return Model;
        }

        public EmpAllCodesVIewmodels EmployeesIds(int empcode)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EmpAllCodes", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", empcode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return new EmpAllCodesVIewmodels
            {
                EmpBgId = Convert.ToInt32(dt.Rows[0]["EmpBgId"].ToString()),
                Empcode = Convert.ToInt32(dt.Rows[0]["Empcode"].ToString()),
                EmpDeptId = Convert.ToInt32(dt.Rows[0]["EmpDeptId"].ToString()),
                EmpDesgId = Convert.ToInt32(dt.Rows[0]["EmpDesgId"].ToString()),
                EmpJobTypeId = Convert.ToInt32(dt.Rows[0]["EmpJobTypeId"].ToString()),
                EmpLeaveRuleId = Convert.ToInt32(dt.Rows[0]["EmpLeaveRuleId"].ToString()),
                EmpLevelId = Convert.ToInt32(dt.Rows[0]["EmpLevelId"].ToString()),
                EmpOfficeId = Convert.ToInt32(dt.Rows[0]["EmpOfficeId"].ToString()),
                EmpRankId = Convert.ToInt32(dt.Rows[0]["EmpRankId"].ToString()),
                EmpRoleId = Convert.ToInt32(dt.Rows[0]["EmpRoleId"].ToString()),
                EmpSectionId = Convert.ToInt32(dt.Rows[0]["EmpSectionId"].ToString()),
                EmpShiftId = Convert.ToInt32(dt.Rows[0]["EmpBgId"].ToString()),
                EmpTaxSetupId = Convert.ToInt32(dt.Rows[0]["EmpTaxSetupId"].ToString()),
                EmpTypeId = Convert.ToInt32(dt.Rows[0]["EmpTypeId"].ToString()),

            };
        }
        public List<SelectListItem> GetEmployeeSelectList()
        {
            List<Employee> Record = _unitOfWork.EmployeeRepository.Get(x => x.EmpStatus == true).OrderBy(x => x.EmpCode).ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (Employee Item in Record)
            {
                SelectListItem emptyItem = new SelectListItem()
                {
                    Value = Item.EmpCode.ToString(),
                    Text = Item.EmpCode.ToString() + " - " + Item.EmpName
                };
                ReturnRecord.Add(emptyItem);
            }
            return ReturnRecord;
        }
        public List<SelectListItem> GetSponsorshipList()
        {
            List<SelectListItem> Record = new List<SelectListItem>
            {
                new SelectListItem{ Text="ADBL", Value = "A" },
                new SelectListItem{ Text="Self", Value = "S" },
                new SelectListItem{ Text="Other Organization", Value = "O"},
            };
            return Record;
        }
        public List<SelectListItem> GetNationInternationalList()
        {
            List<SelectListItem> Record = new List<SelectListItem>
            {
                new SelectListItem{ Text="National", Value = "N" },
                new SelectListItem{ Text="InterNational", Value = "I" },
            };
            return Record;
        }
        public EmployeeDTO GetEmployeeDTOById(int Id)
        {
            Employee Record = _unitOfWork.EmployeeRepository.GetById(Id);
            EmployeeDTO ReturnRecord = new EmployeeDTO()
            {
                EmpCode = Record.EmpCode,
                EmpName = Record.EmpName,
                EmpRankId = Record.EmpRankId,
                EmpDesgId = Record.EmpDesgId,
                EmpOfficeId = Record.EmpOfficeId
            };
            return ReturnRecord;
        }
        public List<EmployeeAddressDTO> GetAddressesofEmployeeById(int id)
        {
            List<EmployeeAddress> DomainRecord = _unitOfWork.EmployeeAddressRepository.Get(x => x.EmployeeCode == id).ToList();
            IEnumerable<EmployeeAddressDTO> ReturnRecord = EmployeeAddressResponseFormatter.ModelData(DomainRecord);
            return ReturnRecord.ToList();
        }

        public List<SelectListItem> GetDistrictSelectList()
        {
            List<District> Record = _unitOfWork.DistrictRepository.All().ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (District Item in Record)
            {
                SelectListItem emptyItem = new SelectListItem()
                {
                    Value = Item.Id.ToString(),
                    Text = Item.DistrictName
                };
                ReturnRecord.Add(emptyItem);
            }
            return ReturnRecord;
        }
        public List<SelectListItem> GetZoneSelectList()
        {
            List<Zone> Record = _unitOfWork.ZoneRepository.All().ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (Zone Item in Record)
            {
                SelectListItem emptyItem = new SelectListItem()
                {
                    Value = Item.Id.ToString(),
                    Text = Item.ZoneName
                };
                ReturnRecord.Add(emptyItem);
            }
            return ReturnRecord;
        }
        public List<SelectListItem> GetAddressTypeSelectList()
        {
            List<SelectListItem> Record = new List<SelectListItem>
            {
                new SelectListItem{ Text="Premanent", Value = "P" },
                new SelectListItem{ Text="Temporary", Value = "T" }
            };
            return Record;
        }
        public void CreateEmployeeAddress(EmployeeAddressDTO record)
        {
            EmployeeAddress Domain = EmployeeAddressRequestFormatter.ConvertEmployeeAddressFromDTO(record);
            _unitOfWork.EmployeeAddressRepository.Create(Domain);
        }
        public EmployeeAddressDTO GetAddressById(int Id)
        {
            EmployeeAddress Record = _unitOfWork.EmployeeAddressRepository.GetById(Id);
            EmployeeAddressDTO ReturnRecord = EmployeeAddressRequestFormatter.ConvertEmployeeAddressToDTO(Record);
            return ReturnRecord;
        }
        public void UpdateEmployeeAddress(EmployeeAddressDTO record)
        {
            EmployeeAddress Domain = EmployeeAddressRequestFormatter.ConvertEmployeeAddressFromDTO(record);
            _unitOfWork.EmployeeAddressRepository.Update(Domain);
        }


        public IEnumerable<EmployeeDTO> EmployeeSearch(int officeId, int? desgnId = 0)
        {
            List<Employee> employeeList = _unitOfWork.EmployeeRepository.Get(x => x.EmpOfficeId == officeId).ToList();
            if (desgnId > 0)
            {
                employeeList = employeeList.Where(a => a.EmpDesgId == desgnId).ToList();
            }
            List<EmployeeDTO> ReturnRecord = EmployeeRequestFormatter.CovertRespontFormatterListToDTO(employeeList);
            return ReturnRecord;

        }



        public IEnumerable<EmployeeDTO> SearchEmployees(int? code, string name, int? officId, int? deptId, int? desgId, int? groupId, int? bgId, int roleId)
        {
            List<EmployeeDTO> phoneModelList = new List<EmployeeDTO>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_SearchEmployees", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            int AdminEmpCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            cmd.Parameters.AddWithValue("@AdminEmpCode", AdminEmpCode);
            cmd.Parameters.AddWithValue("@EmpCode", code);
            cmd.Parameters.AddWithValue("@EmpNamey", name);
            cmd.Parameters.AddWithValue("@officeId", officId);
            cmd.Parameters.AddWithValue("@deptId", deptId);
            cmd.Parameters.AddWithValue("@desgId", desgId);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@bgId", bgId);
            cmd.Parameters.AddWithValue("@RoleID", roleId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return phoneModelList = new DataTableToEntityMapper().Map<EmployeeDTO>(dt).ToList();

        }
        public void DeleleEmployeeApprover(int id)
        {
            _unitOfWork.ApproverList.Delete(id);
            _unitOfWork.Save();
        }


    }

}
