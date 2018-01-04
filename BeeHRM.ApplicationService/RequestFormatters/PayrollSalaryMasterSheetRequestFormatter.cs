using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class PayrollSalaryMasterSheetRequestFormatter 
    {


        public static PayrollSalaryMasterSheet ConvertRespondentInfoFromDTO(PayrollSalaryMasterSheetDTO PayrollSalaryTableDTO)
        {

            Mapper.CreateMap<PayrollSalaryMasterSheetDTO, PayrollSalaryMasterSheet>().ConvertUsing(
                m =>
                    {
                        return new PayrollSalaryMasterSheet
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
                            CitAmount = m.CitAmount,
                            TotalRankAllowances = m.TotalRankAllowances,
                            SalaryWithRankAllowance = m.SalaryWithRankAllowance,
                            RemoteAllowance = m.RemoteAllowance,
                            PfSelf = m.PfSelf,
                            EmpAttendanceIgnore =m.EmpAttendanceIgnore,
                            GradeSalary = m.GradeSalary,
                            PfCompany = m.PfCompany,
                            PfExtra = m.PfExtra,
                            RankAndGradeSalary = m.RankAndGradeSalary,
                            RemoteAllowanceId = m.RemoteAllowanceId,
                            RemoteAllowanceType = m.RemoteAllowanceType,
                            RemoteTaxExcemption = m.RemoteTaxExcemption,
                            SalaryAfterTaxDeduction = m.SalaryAfterTaxDeduction,
                            EmployeeName = m.EmployeeName,
                            TaxableSalary = m.TaxableSalary
                        };
                    });
            return Mapper.Map<PayrollSalaryMasterSheetDTO, PayrollSalaryMasterSheet>(PayrollSalaryTableDTO);
        }

        public static PayrollSalaryMasterSheetDTO ConvertRespondentInfoToDTO(PayrollSalaryMasterSheet PayrollSalaryTable)
        {
            Mapper.CreateMap<PayrollSalaryMasterSheet, PayrollSalaryMasterSheetDTO>().ConvertUsing(
                m =>
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
                        CitAmount = m.CitAmount,
                        TotalRankAllowances = m.TotalRankAllowances,
                        SalaryWithRankAllowance = m.SalaryWithRankAllowance,
                        RemoteAllowance = m.RemoteAllowance,
                        PfSelf = m.PfSelf,
                        EmpAttendanceIgnore = m.EmpAttendanceIgnore,
                        GradeSalary = m.GradeSalary,
                        PfCompany = m.PfCompany,
                        PfExtra = m.PfExtra,
                        RankAndGradeSalary = m.RankAndGradeSalary,
                        RemoteAllowanceId = m.RemoteAllowanceId,
                        RemoteAllowanceType = m.RemoteAllowanceType,
                        RemoteTaxExcemption = m.RemoteTaxExcemption,
                        SalaryAfterTaxDeduction = m.SalaryAfterTaxDeduction,
                        EmployeeName = m.EmployeeName,
                        TaxableSalary = m.TaxableSalary,
                        //TaxablePfCitAmount = m.TaxablePfCitAmount,
                        PayrollSalaryTable = new PayrollSalaryTableDTO
                        {
                            Id = m.PayrollSalaryTable.Id,
                            FyId = m.PayrollSalaryTable.FyId,
                            Details = m.PayrollSalaryTable.Details,
                            PayrollMonthDescription = new PayrollMonthDescriptionDTO
                            {
                                MonthNameNepali = m.PayrollSalaryTable.PayrollMonthDescription.MonthNameNepali,
                                MonthNameEnglish = m.PayrollSalaryTable.PayrollMonthDescription.MonthNameEnglish,
                                Id = m.PayrollSalaryTable.PayrollMonthDescription.Id
                            },
                            Fiscal = new FiscalDTO
                            {
                                FyId = m.PayrollSalaryTable.FyId,
                                FyName = m.PayrollSalaryTable.Fiscal.FyName,
                            }
                        },
                        Employee = new EmployeeDTO
                        {
                            EmpName = m.Employee.EmpName
                        },
                        Department = new DepartmentDTO
                        {
                            DeptName = m.Department.DeptName
                        },
                        Designation = new DesignationDTO
                        {
                            DsgName = m.Designation.DsgName
                        },
                        Office = new OfficeDTOs
                        {
                            OfficeName = m.Office.OfficeName,
                            OfficeCode = m.Office.OfficeCode
                        }
                    };
                });
            return Mapper.Map<PayrollSalaryMasterSheet, PayrollSalaryMasterSheetDTO>(PayrollSalaryTable);
        }


    }
}
