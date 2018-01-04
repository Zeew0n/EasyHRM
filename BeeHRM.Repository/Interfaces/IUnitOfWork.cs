using BeeHRM.Repository.StoredProcedureVariable;
using System.Threading.Tasks;

namespace BeeHRM.Repository.Interfaces
{
    public interface IUnitOfWork
    {



        IRepository<AttDaily> AttDailyRepository
        {
            get;
        }
        IRepository<AspNetUser> AspUserRepository
        {
            get;
        }
        //object UsersRepository { get; set; }
        IRepository<Approver> ApproverList { get; }

        IRepository<AttendaceDevice> AttendanceDeviceRepository { get; }
        IRepository<Module> ModuleRepository { get; }
        IRepository<UserLoginLog> UserloginLog { get; }
        //IRepository<ParentModule> ParentModuleRepository { get; }
        IRepository<LeaveMonthlyProcess> LeaveMonthlyProcessRepository { get; }
        IRepository<Office> OfficeRepository { get; }
        IRepository<Level> LevelRepository { get; }
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Designation> DesignationRepository { get; }
        IRepository<Rank> RankRepository { get; }
        IRepository<Section> SectionRepository { get; }
        IRepository<payrollLeaveDay> payrollLeaveDayRepository { get; }
        IRepository<Group> GroupRepository { get; }
        IRepository<LeaveRule> LeaveRuleRepository { get; }
        IRepository<Shift> ShiftRepository { get; }
        IRepository<JobType> JobTypeRepository { get; }
        IRepository<District> DistrictRepository { get; }
        IRepository<Zone> ZoneRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<LeaveYearlyReport> LeaveYearlyReportRepository { get; }
        IRepository<ServiceEventGroup> ServiceEventRepository { get; }
        IRepository<EmployeeJobHistory> EmployeeJobHistoryRepository { get; }
        IRepository<AttendaceDaily> AttendanceDailyRepository { get; }
        IRepository<LeaveType> LeaveTypeRepository { get; }
        IRepository<OfficeType> OfficeTypeRepository { get; }
        IRepository<LeaveApplication> LeaveApplicationRepository { get; }
        IRepository<BusinessGroup> BusinessGroupRepository { get; }
        IRepository<LeaveRuleDetail> LeaveRuleDetailRepository { get; }
        IRepository<RolesAccess> RolesAccessRepository { get; }
        IRepository<Darbandi> DarbandiRepository { get; }
        IRepository<LeaveYear> LeaveYearRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<AttendanceRequest> AttendanceRequestRepository { get; }
        IRepository<Ethnicity> EthnicityRepository { get; }
        IRepository<Religion> ReligionRepository { get; }
        IRepository<LeaveAssigned> LeaveAssignedRepository { get; }
        IRepository<Skill> SkillRepository { get; }
        IRepository<EmployeeAddress> EmployeeAddressRepository { get; }
        IRepository<AttEmployeeLog> AttEmployeeLogRepository { get; }
        //Search Employee Repository
        IRepository<ShiftDay> ShiftDayRepository { get; }

        IRepository<RolesBusinessGroupAccess> RolesBusinessGroupAccessRepository { get; }

        IRepository<EducationLevel> EducationLevelRepository { get; }
        IRepository<Grade> GradeRepository { get; }
        IRepository<Fiscal> FiscalRepository { get; }
        IRepository<EmployeeEducation> EmployeeEducationRepository { get; }
        IRepository<Country> CountryReposityory { get; }
        IRepository<EmployeeVisit> EmployeeVisitRepository { get; }
        IRepository<Notification> NotificationRepository { get; }
        IRepository<EmployeePrize> EmployeePrizeRepository { get; }
        IRepository<RemoteArea> RemoteAreaRepository { get; }
        //IRepository<EmployeeTraining> EmployeeTrainingRepository { get; }
        IRepository<EmpTraining> EmpTrainingRepository { get; }
        IRepository<ServiceEventSubGroup> ServiceEventSubGroup { get; }
        IRepository<Holiday> HolidayRepository { get; }
        IRepository<Calender> CalenderRepository { get; }
        IRepository<CalenderMonth> CalenderMonthRepository { get; }
        IRepository<PayrollRemoteAllowance> PayrollRemoteAllowanceRepository { get; }
        IRepository<PayrollRemoteSetup> PayrollRemoteSetupRepository { get; }
        IRepository<PayrollLeaveBalance> PayrollLeaveBalanceRepository { get; }
        IRepository<InsuranceCompany> InsuranceCompanyRepository { get; }
        IRepository<PayrollInsurancePremium> PayrollInsurancePremiumRepository { get; }

        IRepository<DocumentCategory> DocumentCategoryRepository { get; }
        IRepository<EmployeeDocument> EmployeeDocumentRepository { get; }
        IRepository<sp_Myrecommender_Result> RecommenderList { get; }
        #region PayrollRepos
        IRepository<PayrollTaxSetup> TaxSetupRepository { get; }
        IRepository<PayrollTaxDetail> TaxDetailRepository { get; }
        IRepository<PayrollAllowanceMaster> PayrollAllowanceMasterRepository { get; }
        IRepository<PayrollAllowanceDetail> PayrollAllowanceDetailRepository { get; }
        IRepository<PayrollMonthDescription> PayrollMonthDescriptipnRepository { get; }
        IRepository<PayrollSalaryTable> PayrollSalaryTableRepository { get; }
        IRepository<PayrollSalaryMasterSheet> PayrollSalaryMasterSheetRepository { get; }
        IRepository<PayrollSalaryDetailSheet> PayrollSalaryDetailSheet { get; }
        IRepository<PayrollEmployeeTaxDetail> PayrollEmployeeTaxDetail { get; }
        IRepository<PayrollArrear> PayrollArrear { get; }
        IRepository<PayrollOvertime> PayrollOvertimeRepository { get; }
        IRepository<Bank> BankRepository { get; }
        IRepository<EmployeeBank> EmployeeBankRepository { get; }
        IRepository<EmployeeFamily> EmployeeFamilyRepository { get; }
        IRepository<EmployeeSkill> EmployeeSkillRepository { get; }
        IRepository<EmployeeGradeUpdate> EmployeeGradeUpdateRepository { get; }
        IRepository<PayrollIntrestGain> InterestGainRepository { get; }

        #endregion

        #region leave
        IRepository<LeaveEarned> LeaveEarnedRepository { get; }
        IRepository<PayrollLeaveDeduction> PayrollLeaveDeductionRepository { get; }
        //employee
        IRepository<BranchEmployee> BranchEmployeeRepository { get; }
        IRepository<sp_LeaveApplicationEmployeeBalance_Result> LeaveBalanceRepository { get; }

        #endregion
        int Save();
        Task<int> SaveAsync();
    }
}
