using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class PayrollSalaryMasterSheetResponseFormatter
    {
        public static IEnumerable<PayrollSalaryMasterSheetDTO> GetAllTPayrollMasterSheet(IEnumerable<PayrollSalaryMasterSheet> Record)
        {
            Mapper.CreateMap<PayrollSalaryMasterSheet, PayrollSalaryMasterSheetDTO>().ConvertUsing(m =>
            {
                return new PayrollSalaryMasterSheetDTO
                {
                    Id = m.Id,
                    EmployeeCode = m.EmployeeCode,
                    CurrentGrade = m.CurrentGrade,
                    DesignationCode = m.DesignationCode,
                    DepartmentCode = m.DepartmentCode,
                    BranchCode = m.BranchCode,
                    MaritialStatus = m.MaritialStatus,
                    RankSalary = m.RankSalary,
                    RankMaxGrade = m.RankMaxGrade,
                    RankPerGradeAmount = m.RankPerGradeAmount,
                    BankAllowance = m.BankAllowance,
                    RankSpecialAllowance = m.RankSpecialAllowance,
                    RankInchargeShipAllowance = m.RankInchargeShipAllowance,
                    RankOtherAllowances = m.RankOtherAllowances,
                    TaxableAllowanceAmount = m.TaxableAllowanceAmount,
                    NonTaxableAllowanceAmount = m.NonTaxableAllowanceAmount,
                    TaxAmount = m.TaxAmount,
                    GrossSalary = m.GrossSalary,
                    WorkingDays = m.WorkingDays,
                    WorkedDays = m.WorkedDays,
                    PerDaysalary = m.PerDaysalary,
                    DeductableDays = m.DeductableDays,
                    ActualSalary = m.ActualSalary,
                    PayrollSalaryTableId = m.PayrollSalaryTableId,
                    GrossIncome = m.GrossIncome,
                    TaxablePfCitAmount = m.TaxablePfCitAmount,
                    TaxableSalary = m.TaxableSalary,
                    EmployeeName = m.EmployeeName,
                    SalaryAfterTaxDeduction = m.SalaryAfterTaxDeduction,
                    RemoteTaxExcemption = m.RemoteTaxExcemption,
                    RemoteAllowanceType = m.RemoteAllowanceType,
                    RemoteAllowanceId = m.RemoteAllowanceId,
                    RankAndGradeSalary = m.RankAndGradeSalary,
                    PfExtra = m.PfExtra,
                    CitAmount = m.CitAmount,
                    EmpAttendanceIgnore = m.EmpAttendanceIgnore,
                    GradeSalary = m.GradeSalary,
                    PfCompany = m.PfCompany,
                    PfSelf = m.PfSelf,
                    RemoteAllowance = m.RemoteAllowance,
                    SalaryWithRankAllowance = m.SalaryWithRankAllowance,
                    TotalRankAllowances = m.TotalRankAllowances,
                    PayrollSalaryTable = new PayrollSalaryTableDTO
                    {
                        Id = m.PayrollSalaryTable.Id,
                        Details = m.PayrollSalaryTable.Details,
                        PayrollMonthDescription = new PayrollMonthDescriptionDTO
                        {
                            MonthNameNepali = m.PayrollSalaryTable.PayrollMonthDescription.MonthNameNepali,
                            MonthNameEnglish = m.PayrollSalaryTable.PayrollMonthDescription.MonthNameEnglish,
                            Id = m.PayrollSalaryTable.PayrollMonthDescription.Id
                        }
                    },
                    Employee = new EmployeeDTO
                    {
                        EmpCode = m.Employee.EmpCode,
                        EmpName = m.Employee.EmpName
                    },
                    Designation = new DesignationDTO
                    {
                        DsgId = m.Designation.DsgId,
                        DsgName = m.Designation.DsgName,
                    },
                    Department = new DepartmentDTO
                    {
                        DeptId = m.Department.DeptId,
                        DeptName = m.Department.DeptName,
                        DeptCode = m.Department.DeptCode
                    }
                };
            });
            return Mapper.Map<IEnumerable<PayrollSalaryMasterSheet>, IEnumerable<PayrollSalaryMasterSheetDTO>>(Record);
        }
    }
}
