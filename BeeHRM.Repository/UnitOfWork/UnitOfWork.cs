using BeeHRM.Repository.Implementations;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.StoredProcedureVariable;
using System;
using System.Threading.Tasks;

namespace BeeHRM.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        dbBeeHRMEntities _context = null;
        public UnitOfWork()
        {
            _context = new dbBeeHRMEntities();
        }
        public UnitOfWork(dbBeeHRMEntities dbcontext)
        {
            _context = dbcontext;
        }

        public dbBeeHRMEntities Context { get { return _context; } }

        private IRepository<AttendaceDevice> _attendanceDeviceRepository;
        private IRepository<AttDaily> _attDailyRepository;
        private IRepository<AspNetUser> _aspUserRepository;
        private IRepository<Module> _moduleRepository;
        private IRepository<Office> _officeRepository;
        private IRepository<Level> _levelRepository;
        private IRepository<Department> _deptRepository;
        private IRepository<Designation> _designationRepository;
        private IRepository<Rank> _rankRepository;
        private IRepository<Section> _sectionRepository;
        private IRepository<Group> _groupRepository;
        private IRepository<LeaveRule> _leaveRuleRepository;
        private IRepository<Shift> _shiftRepository;
        private IRepository<JobType> _jobTypeRepository;
        private IRepository<Employee> _employeeRepository;
        private IRepository<District> _DistrictRepository;
        private IRepository<Zone> _ZoneRepository;
        private IRepository<ServiceEventGroup> _serviceEventRepository;
        private IRepository<EmployeeJobHistory> _employeeJobHistoryRepository;
        private IRepository<AttendaceDaily> _attendanceDailyRepository;
        private IRepository<LeaveType> _leaveTypeRepository;
        private IRepository<OfficeType> _officeTypeRepository;
        private IRepository<LeaveApplication> _leaveApplicationRepository;
        private IRepository<BusinessGroup> _businessGroupRepository;
        private IRepository<LeaveRuleDetail> _leaveRuleDetailRepository;
        private IRepository<Darbandi> _darbandiRepository;
        private IRepository<Role> _roleRepository;
        private IRepository<RolesAccess> _roleAccessRepository;
        private IRepository<LeaveYear> _leaveYearRepository;
        private IRepository<AttendanceRequest> _attendanceRequestRepository;
        private IRepository<Ethnicity> _ethnicityRepository;
        private IRepository<Religion> _religionRepository;
        private IRepository<ShiftDay> _shiftDayRepository;
        private IRepository<RolesBusinessGroupAccess> _rolesBusinessGroupAccessRepository;
        private IRepository<EducationLevel> _EducationLevelRepository;
        private IRepository<Grade> _gradeRepository;
        private IRepository<LeaveAssigned> _leaveAssignedRepository;
        private IRepository<Fiscal> _fiscalRepository;
        private IRepository<EmployeeEducation> _employeeEducationRepository;
        private IRepository<EmployeeAddress> _EmployeeAddressRepository;
        private IRepository<Country> _countryRepository;
        private IRepository<EmployeeVisit> _employeeVisitRepository;
        private IRepository<EmployeePrize> _employeePrizeRepository;
        private IRepository<RemoteArea> _remoteAreaRepository;
        private IRepository<Notification> _notificationRepository;

        private IRepository<EmpTraining> _EmpTraining;
        private IRepository<ServiceEventSubGroup> _serviceEventSubGroupRepository;
        private IRepository<Holiday> _holiayRepository;
        private IRepository<Calender> _calenderRepository;
        private IRepository<CalenderMonth> _calenderMonthRepository;

        private IRepository<UserLoginLog> _userloginLog;
        private IRepository<Skill> _skillRepository;
        private IRepository<Bank> _bankRepository;
        private IRepository<EmployeeBank> _employeeBankRepository;
        private IRepository<PayrollRemoteAllowance> _payrollRemoteAllowanceRepository;
        private IRepository<PayrollRemoteSetup> _payrollRemoteSetupRepository;
        private IRepository<EmployeeFamily> _employeeFamilyRepository;
        private IRepository<LeaveYearlyReport> _LeaveYearlyReportRepository;
        private IRepository<PayrollLeaveDeduction> _PayrollLeaveDeductionRepository;
        private IRepository<PayrollLeaveBalance> _PayrollLeaveBalanceRepository;
        private IRepository<EmployeeSkill> _EmployeeSkillRepository;

        private IRepository<Approver> _ApproverList;
        private IRepository<sp_Myrecommender_Result> _RecommenderList;

        private IRepository<InsuranceCompany> _InsuranceCompanyRepository;
        private IRepository<PayrollInsurancePremium> _PayrollInsurancePremiumRepository;

        private IRepository<EmployeeDocument> _EmployeeDocumentRepository;
        private IRepository<EmployeeGradeUpdate> _EmployeeGradeUpdateRepository;
        private IRepository<PayrollIntrestGain> _InterestGainRepository;
        #region PayrollRepos
        private IRepository<PayrollTaxSetup> _TaxSetupRepository;
        private IRepository<PayrollTaxDetail> _TaxDetailRepository;
        private IRepository<PayrollAllowanceMaster> _PayrollAllowanceMasterRepository;
        private IRepository<PayrollAllowanceDetail> _PayrollAllowanceDetailRepository;
        private IRepository<PayrollMonthDescription> _PayrollMonthDescriptipnRepository;
        private IRepository<PayrollSalaryTable> _PayrollSalaryTableRepository;
        private IRepository<PayrollSalaryMasterSheet> _PayrollSalaryMasterSheetRepository;
        private IRepository<PayrollSalaryDetailSheet> _PayrollSalaryDetailSheet;
        private IRepository<PayrollEmployeeTaxDetail> _PayrollEmployeeTaxDetail;
        private IRepository<PayrollArrear> _PayrollArrear;
        private IRepository<PayrollOvertime> _payrollOvertimeRepository;
        private IRepository<payrollLeaveDay> _payrollLeaveDayRepository;
        private IRepository<LeaveMonthlyProcess> _LeaveMonthlyProcessRepository;
        private IRepository<DocumentCategory> _DocumentCategoryRepository;
        #endregion
        #region leave
        private IRepository<LeaveEarned> _leaveEarnedRepository;
        private IRepository<PayrollLeaveDeduction> _payrollLeaveDeductionRepository;
        //employee
        private IRepository<BranchEmployee> _branchRepository;
        private IRepository<sp_LeaveApplicationEmployeeBalance_Result> _leaveBalance;
        #endregion
        #region User, UserRole & UserRolePermission Section
        // private IRepository<UserRole> _userPermissionRoleRepository;
        #endregion

        public IRepository<AspNetUser> AspUserRepository
        {
            get { return _aspUserRepository ?? (_aspUserRepository = new RepositoryBase<AspNetUser>(_context)); }
        }
        public IRepository<AttendaceDevice> AttendanceDeviceRepository
        {
            get { return _attendanceDeviceRepository ?? (_attendanceDeviceRepository = new RepositoryBase<AttendaceDevice>(_context)); }
        }

        public IRepository<PayrollLeaveBalance> PayrollLeaveBalanceRepository
        {
            get { return _PayrollLeaveBalanceRepository ?? (_PayrollLeaveBalanceRepository = new RepositoryBase<PayrollLeaveBalance>(_context)); }
        }

        public IRepository<Approver> ApproverList
        {
            get { return _ApproverList ?? (_ApproverList = new RepositoryBase<Approver>(_context)); }
        }

        public IRepository<sp_Myrecommender_Result> RecommenderList
        {
            get { return _RecommenderList ?? (_RecommenderList = new RepositoryBase<sp_Myrecommender_Result>(_context)); }
        }


        public IRepository<EmployeeSkill> EmployeeSkillRepository
        {
            get { return _EmployeeSkillRepository ?? (_EmployeeSkillRepository = new RepositoryBase<EmployeeSkill>(_context)); }
        }


        public IRepository<LeaveMonthlyProcess> LeaveMonthlyProcessRepository
        {
            get { return _LeaveMonthlyProcessRepository ?? (_LeaveMonthlyProcessRepository = new RepositoryBase<LeaveMonthlyProcess>(_context)); }
        }

        public IRepository<LeaveYearlyReport> LeaveYearlyReportRepository
        {
            get { return _LeaveYearlyReportRepository ?? (_LeaveYearlyReportRepository = new RepositoryBase<LeaveYearlyReport>(_context)); }
        }
        public IRepository<UserLoginLog> UserloginLog
        {
            get { return _userloginLog ?? (_userloginLog = new RepositoryBase<UserLoginLog>(_context)); }
        }

        public IRepository<AttDaily> AttDailyRepository
        {
            get { return _attDailyRepository ?? (_attDailyRepository = new RepositoryBase<AttDaily>(_context)); }
        }
        public IRepository<Module> ModuleRepository
        {
            get { return _moduleRepository ?? (_moduleRepository = new RepositoryBase<Module>(_context)); }
        }
        public IRepository<payrollLeaveDay> payrollLeaveDayRepository
        {
            get { return _payrollLeaveDayRepository ?? (_payrollLeaveDayRepository = new RepositoryBase<payrollLeaveDay>(_context)); }
        }
        public IRepository<Office> OfficeRepository
        {
            get { return _officeRepository ?? (_officeRepository = new RepositoryBase<Office>(_context)); }
        }
        public IRepository<Level> LevelRepository
        {
            get { return _levelRepository ?? (_levelRepository = new RepositoryBase<Level>(_context)); }
        }
        public IRepository<Department> DepartmentRepository
        {
            get { return _deptRepository ?? (_deptRepository = new RepositoryBase<Department>(_context)); }
        }
        //public IRepository<ParentModule> ParentModuleRepository
        //{
        //    get { return _parentModuleRepository ?? (_parentModuleRepository = new RepositoryBase<ParentModule>(_context)); }
        //}
        public IRepository<Designation> DesignationRepository
        {
            get { return _designationRepository ?? (_designationRepository = new RepositoryBase<Designation>(_context)); }
        }
        public IRepository<Rank> RankRepository
        {
            get { return _rankRepository ?? (_rankRepository = new RepositoryBase<Rank>(_context)); }
        }
        public IRepository<Section> SectionRepository
        {
            get { return _sectionRepository ?? (_sectionRepository = new RepositoryBase<Section>(_context)); }
        }
        public IRepository<Group> GroupRepository
        {
            get { return _groupRepository ?? (_groupRepository = new RepositoryBase<Group>(_context)); }
        }

        public IRepository<District> DistrictRepository
        {
            get { return _DistrictRepository ?? (_DistrictRepository = new RepositoryBase<District>(_context)); }
        }
        public IRepository<Zone> ZoneRepository
        {
            get { return _ZoneRepository ?? (_ZoneRepository = new RepositoryBase<Zone>(_context)); }
        }
        public IRepository<LeaveRule> LeaveRuleRepository
        {
            get { return _leaveRuleRepository ?? (_leaveRuleRepository = new RepositoryBase<LeaveRule>(_context)); }
        }
        public IRepository<Shift> ShiftRepository
        {
            get { return _shiftRepository ?? (_shiftRepository = new RepositoryBase<Shift>(_context)); }
        }
        public IRepository<JobType> JobTypeRepository
        {
            get { return _jobTypeRepository ?? (_jobTypeRepository = new RepositoryBase<JobType>(_context)); }
        }
        public IRepository<Employee> EmployeeRepository
        {
            get { return _employeeRepository ?? (_employeeRepository = new RepositoryBase<Employee>(_context)); }
        }
        public IRepository<ServiceEventGroup> ServiceEventRepository
        {
            get { return _serviceEventRepository ?? (_serviceEventRepository = new RepositoryBase<ServiceEventGroup>(_context)); }
        }
        public IRepository<EmployeeJobHistory> EmployeeJobHistoryRepository
        {
            get { return _employeeJobHistoryRepository ?? (_employeeJobHistoryRepository = new RepositoryBase<EmployeeJobHistory>(_context)); }
        }
        public IRepository<AttendaceDaily> AttendanceDailyRepository
        {
            get { return _attendanceDailyRepository ?? (_attendanceDailyRepository = new RepositoryBase<AttendaceDaily>(_context)); }
        }
        public IRepository<LeaveType> LeaveTypeRepository
        {
            get { return _leaveTypeRepository ?? (_leaveTypeRepository = new RepositoryBase<LeaveType>(_context)); }
        }
        public IRepository<OfficeType> OfficeTypeRepository
        {
            get { return _officeTypeRepository ?? (_officeTypeRepository = new RepositoryBase<OfficeType>(_context)); }
        }
        public IRepository<LeaveApplication> LeaveApplicationRepository
        {
            get { return _leaveApplicationRepository ?? (_leaveApplicationRepository = new RepositoryBase<LeaveApplication>(_context)); }
        }
        public IRepository<BusinessGroup> BusinessGroupRepository
        {
            get { return _businessGroupRepository ?? (_businessGroupRepository = new RepositoryBase<BusinessGroup>(_context)); }
        }
        public IRepository<LeaveRuleDetail> LeaveRuleDetailRepository
        {
            get { return _leaveRuleDetailRepository ?? (_leaveRuleDetailRepository = new RepositoryBase<LeaveRuleDetail>(_context)); }
        }

        //public IRepository<AttEmployeeLog> AttEmployeeLogRepository
        //{
        //    get { return _attEmployeeLogRepository ?? (_attEmployeeLogRepository = new RepositoryBase<AttEmployeeLog>(_context)); }
        //}

        public IRepository<LeaveYear> LeaveYearRepository
        {
            get { return _leaveYearRepository ?? (_leaveYearRepository = new RepositoryBase<LeaveYear>(_context)); }
        }
        //public IRepository<LeaveMonthlyProcess> LeaveMonthlyProcessRepository
        //{
        //    get { return _LeaveMonthlyProcessRepository ?? (_LeaveMonthlyProcessRepository = new RepositoryBase<LeaveMonthlyProcess>(_context)); }
        //}

        public IRepository<Darbandi> DarbandiRepository
        {

            get { return _darbandiRepository ?? (_darbandiRepository = new RepositoryBase<Darbandi>(_context)); }
        }

        public IRepository<AttEmployeeLog> AttEmployeeLogRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new RepositoryBase<Role>(_context));
            }
        }
        public IRepository<RolesAccess> RoleAccessRepository
        {
            get
            {
                return _roleAccessRepository ?? (_roleAccessRepository = new RepositoryBase<RolesAccess>(_context));
            }
        }

        public IRepository<AttendanceRequest> AttendanceRequestRepository
        {
            get { return _attendanceRequestRepository ?? (_attendanceRequestRepository = new RepositoryBase<AttendanceRequest>(_context)); }
        }

        public IRepository<RolesAccess> RolesAccessRepository
        {
            get { return _roleAccessRepository ?? (_roleAccessRepository = new RepositoryBase<RolesAccess>(_context)); }
        }

        public IRepository<Ethnicity> EthnicityRepository
        {
            get { return _ethnicityRepository ?? (_ethnicityRepository = new RepositoryBase<Ethnicity>(_context)); }
        }

        public IRepository<Religion> ReligionRepository
        {
            get { return _religionRepository ?? (_religionRepository = new RepositoryBase<Religion>(_context)); }
        }

        public IRepository<ShiftDay> ShiftDayRepository
        {
            get
            {
                return _shiftDayRepository ?? (_shiftDayRepository = new RepositoryBase<ShiftDay>(_context));
            }
        }

        public IRepository<RolesBusinessGroupAccess> RolesBusinessGroupAccessRepository
        {
            get
            {
                return _rolesBusinessGroupAccessRepository ?? (_rolesBusinessGroupAccessRepository = new RepositoryBase<RolesBusinessGroupAccess>(_context));
            }
        }

        public IRepository<EducationLevel> EducationLevelRepository
        {
            get
            {
                return _EducationLevelRepository ?? (_EducationLevelRepository = new RepositoryBase<EducationLevel>(_context));
            }
        }

        public IRepository<Grade> GradeRepository
        {
            get
            {
                return _gradeRepository ?? (_gradeRepository = new RepositoryBase<Grade>(_context));
            }
        }

        public IRepository<LeaveAssigned> LeaveAssignedRepository
        {
            get { return _leaveAssignedRepository ?? (_leaveAssignedRepository = new RepositoryBase<LeaveAssigned>(_context)); }
        }

        public IRepository<Fiscal> FiscalRepository
        {
            get
            {
                return _fiscalRepository ?? (_fiscalRepository = new RepositoryBase<Fiscal>(_context));
            }
        }

        public IRepository<EmployeeEducation> EmployeeEducationRepository
        {

            get
            {
                return _employeeEducationRepository ?? (_employeeEducationRepository = new RepositoryBase<EmployeeEducation>(_context));
            }
        }
        public IRepository<EmployeeAddress> EmployeeAddressRepository
        {

            get
            {
                return _EmployeeAddressRepository ?? (_EmployeeAddressRepository = new RepositoryBase<EmployeeAddress>(_context));
            }
        }

        public IRepository<Country> CountryReposityory
        {
            get
            {
                return _countryRepository ?? (_countryRepository = new RepositoryBase<Country>(_context));
            }
        }

        public IRepository<EmployeeVisit> EmployeeVisitRepository
        {

            get
            {
                return _employeeVisitRepository ?? (_employeeVisitRepository = new RepositoryBase<EmployeeVisit>(_context));
            }
        }

        public IRepository<EmployeePrize> EmployeePrizeRepository
        {
            get
            {
                return _employeePrizeRepository ?? (_employeePrizeRepository = new RepositoryBase<EmployeePrize>(_context));
            }
        }
        public IRepository<EmployeeGradeUpdate> EmployeeGradeUpdateRepository
        {
            get
            {
                return _EmployeeGradeUpdateRepository ?? (_EmployeeGradeUpdateRepository = new RepositoryBase<EmployeeGradeUpdate>(_context));
            }
        }

        public IRepository<RemoteArea> RemoteAreaRepository
        {
            get
            {
                return _remoteAreaRepository ?? (_remoteAreaRepository = new RepositoryBase<RemoteArea>(_context));
            }
        }
        #region payrollRepos
        public IRepository<PayrollTaxSetup> TaxSetupRepository
        {
            get
            {
                return _TaxSetupRepository ?? (_TaxSetupRepository = new RepositoryBase<PayrollTaxSetup>(_context));
            }
        }
        public IRepository<PayrollTaxDetail> TaxDetailRepository
        {
            get
            {
                return _TaxDetailRepository ?? (_TaxDetailRepository = new RepositoryBase<PayrollTaxDetail>(_context));
            }
        }
        public IRepository<PayrollAllowanceMaster> PayrollAllowanceMasterRepository
        {
            get
            {
                return _PayrollAllowanceMasterRepository ?? (_PayrollAllowanceMasterRepository = new RepositoryBase<PayrollAllowanceMaster>(_context));
            }
        }
        public IRepository<PayrollAllowanceDetail> PayrollAllowanceDetailRepository
        {
            get
            {
                return _PayrollAllowanceDetailRepository ?? (_PayrollAllowanceDetailRepository = new RepositoryBase<PayrollAllowanceDetail>(_context));
            }
        }
        public IRepository<PayrollMonthDescription> PayrollMonthDescriptipnRepository
        {
            get
            {
                return _PayrollMonthDescriptipnRepository ?? (_PayrollMonthDescriptipnRepository = new RepositoryBase<PayrollMonthDescription>(_context));
            }
        }
        public IRepository<PayrollSalaryTable> PayrollSalaryTableRepository
        {
            get
            {
                return _PayrollSalaryTableRepository ?? (_PayrollSalaryTableRepository = new RepositoryBase<PayrollSalaryTable>(_context));
            }
        }
        public IRepository<PayrollSalaryMasterSheet> PayrollSalaryMasterSheetRepository
        {
            get
            {
                return _PayrollSalaryMasterSheetRepository ?? (_PayrollSalaryMasterSheetRepository = new RepositoryBase<PayrollSalaryMasterSheet>(_context));
            }
        }
        public IRepository<PayrollSalaryDetailSheet> PayrollSalaryDetailSheet
        {
            get
            {
                return _PayrollSalaryDetailSheet ?? (_PayrollSalaryDetailSheet = new RepositoryBase<PayrollSalaryDetailSheet>(_context));
            }
        }
        public IRepository<PayrollEmployeeTaxDetail> PayrollEmployeeTaxDetail
        {
            get
            {
                return _PayrollEmployeeTaxDetail ?? (_PayrollEmployeeTaxDetail = new RepositoryBase<PayrollEmployeeTaxDetail>(_context));
            }
        }
        public IRepository<PayrollArrear> PayrollArrear
        {
            get
            {
                return _PayrollArrear ?? (_PayrollArrear = new RepositoryBase<PayrollArrear>(_context));
            }
        }
        #endregion 
        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                throw ex;
            }
        }
        public Task<int> SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {

                throw ex;
            }
        }

        public IRepository<Notification> NotificationRepository
        {
            get
            {
                return _notificationRepository ?? (_notificationRepository = new RepositoryBase<Notification>(_context));
            }
        }

        //public IRepository<EmployeeTraining> EmployeeTrainingRepository
        //{
        //    get
        //    {
        //        return _employeeTrainingRepository ?? (_employeeTrainingRepository = new RepositoryBase<EmployeeTraining>(_context));
        //    }
        //}
        public IRepository<EmpTraining> EmpTrainingRepository
        {
            get
            {
                return _EmpTraining ?? (_EmpTraining = new RepositoryBase<EmpTraining>(_context));
            }
        }
        public IRepository<ServiceEventSubGroup> ServiceEventSubGroup
        {
            get
            {
                return _serviceEventSubGroupRepository ?? (_serviceEventSubGroupRepository = new RepositoryBase<ServiceEventSubGroup>(_context));
            }
        }

        public IRepository<Holiday> HolidayRepository
        {
            get
            {
                return _holiayRepository ?? (_holiayRepository = new RepositoryBase<Holiday>(_context));
            }
        }
        public IRepository<PayrollRemoteAllowance> PayrollRemoteAllowanceRepository
        {
            get
            {
                return _payrollRemoteAllowanceRepository ?? (_payrollRemoteAllowanceRepository = new RepositoryBase<PayrollRemoteAllowance>(_context));
            }
        }
        public IRepository<PayrollIntrestGain> InterestGainRepository
        {
            get
            {
                return _InterestGainRepository ?? (_InterestGainRepository = new RepositoryBase<PayrollIntrestGain>(_context));
            }
        }
        public IRepository<PayrollRemoteSetup> PayrollRemoteSetupRepository
        {
            get
            {
                return _payrollRemoteSetupRepository ?? (_payrollRemoteSetupRepository = new RepositoryBase<PayrollRemoteSetup>(_context));
            }
        }

        public IRepository<Skill> SkillRepository
        {
            get
            {

                return _skillRepository ?? (_skillRepository = new RepositoryBase<Skill>(_context));
            }
        }
        public IRepository<PayrollOvertime> PayrollOvertimeRepository
        {
            get
            {

                return _payrollOvertimeRepository ?? (_payrollOvertimeRepository = new RepositoryBase<PayrollOvertime>(_context));
            }
        }
        public IRepository<Bank> BankRepository
        {
            get
            {

                return _bankRepository ?? (_bankRepository = new RepositoryBase<Bank>(_context));
            }
        }
        public IRepository<EmployeeBank> EmployeeBankRepository
        {
            get
            {

                return _employeeBankRepository ?? (_employeeBankRepository = new RepositoryBase<EmployeeBank>(_context));
            }
        }
        public IRepository<EmployeeFamily> EmployeeFamilyRepository
        {
            get
            {

                return _employeeFamilyRepository ?? (_employeeFamilyRepository = new RepositoryBase<EmployeeFamily>(_context));
            }
        }
        public IRepository<Calender> CalenderRepository
        {
            get
            {

                {
                    return _calenderRepository ?? (_calenderRepository = new RepositoryBase<Calender>(_context));
                }
            }
        }
        public IRepository<CalenderMonth> CalenderMonthRepository
        {
            get
            {

                {
                    return _calenderMonthRepository ?? (_calenderMonthRepository = new RepositoryBase<CalenderMonth>(_context));
                }
            }
        }
        #region leave
        public IRepository<LeaveEarned> LeaveEarnedRepository
        {
            get { return _leaveEarnedRepository ?? (_leaveEarnedRepository = new RepositoryBase<LeaveEarned>(_context)); }
        }

        public IRepository<BranchEmployee> BranchEmployeeRepository
        {
            get { return _branchRepository ?? (_branchRepository = new RepositoryBase<BranchEmployee>(_context)); }
        }
        public IRepository<PayrollLeaveDeduction> PayrollLeaveDeductionRepository
        {
            get { return _payrollLeaveDeductionRepository ?? (_payrollLeaveDeductionRepository = new RepositoryBase<PayrollLeaveDeduction>(_context)); }
        }

        public IRepository<sp_LeaveApplicationEmployeeBalance_Result> LeaveBalanceRepository
        {
            get
            {
                return _leaveBalance ?? (_leaveBalance = new RepositoryBase<sp_LeaveApplicationEmployeeBalance_Result>(_context));
            }
        }


        public IRepository<EmployeeDocument> EmployeeDocumentRepository
        {
            get
            {
                return _EmployeeDocumentRepository ?? (_EmployeeDocumentRepository = new RepositoryBase<EmployeeDocument>(_context));
            }
        }


        public IRepository<DocumentCategory> DocumentCategoryRepository
        {
            get
            {
                return _DocumentCategoryRepository ?? (_DocumentCategoryRepository = new RepositoryBase<DocumentCategory>(_context));
            }
        }
        public IRepository<InsuranceCompany> InsuranceCompanyRepository
        {
            get
            {
                return _InsuranceCompanyRepository ?? (_InsuranceCompanyRepository = new RepositoryBase<InsuranceCompany>(_context));
            }
        }
        public IRepository<PayrollInsurancePremium> PayrollInsurancePremiumRepository
        {
            get
            {
                return _PayrollInsurancePremiumRepository ?? (_PayrollInsurancePremiumRepository = new RepositoryBase<PayrollInsurancePremium>(_context));
            }
        }
        #endregion
    }
}
