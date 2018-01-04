using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollReportService : IPayrollReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _EmployeeService;
        public PayrollReportService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _EmployeeService = new EmployeeService();
        }
        public IndividualYearlyTaxEstimationModel GetIndividualYearlyTax(int Id)
        {
            IndividualYearlyTaxEstimationModel Record = new IndividualYearlyTaxEstimationModel();
            Employee EmployeeDetail = _unitOfWork.EmployeeRepository.GetById(Id);
            int RankId = EmployeeDetail.EmpRankId;
            decimal AnnualSalary = 0;
            decimal Gradesalary = 0;
            decimal BankAllowance = 0;
            decimal RegionalAllowance = 0;
            decimal OtherAllowances = 0;
            decimal FirstSlabTax = 0;
            decimal SecondSlabTax = 0;
            string SecondSlabName = null;
            decimal PfSelfTotal = 0;
            decimal PfCompanyTotal = 0;
            decimal CitTotal = 0;
            decimal PfExtraTotal = 0;
            decimal SpecialAllowance = 0;
            decimal InchargeshipAllowance = 0;

            Rank EmployeeRankDetail = _unitOfWork.RankRepository.GetById(RankId);
            List<PayrollSalaryMasterSheet> EmployeeSalaryDetail = _unitOfWork.PayrollSalaryMasterSheetRepository.Get(x => x.EmployeeCode == Id).ToList();
            List<decimal> SlabNames = new List<decimal>();
            foreach (PayrollSalaryMasterSheet Item in EmployeeSalaryDetail)
            {
                //####  PFSELF ####//
                decimal PfSelf = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 1).Select(x => x.CalculatedValue).Sum();
                PfSelfTotal = PfSelfTotal + PfSelf;
                //####  Company ####//
                decimal PfCompany = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 2).Select(x => x.CalculatedValue).Sum();
                PfCompanyTotal = PfCompanyTotal + PfCompany;
                //####  Extra ####//
                decimal PfExtra = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 3).Select(x => x.CalculatedValue).Sum();
                PfExtraTotal = PfExtraTotal + PfExtra;
                //####  CIT ####//
                decimal Cit = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 4).Select(x => x.CalculatedValue).Sum();
                CitTotal = CitTotal + Cit;
                //####  TAX REPORT STARTS HERE   ####//
                List<PayrollEmployeeTaxDetail> EmploteeTaxDetail = _unitOfWork.PayrollEmployeeTaxDetail.Get(x => x.EmployeeCode == Id && x.PayrollMasterSheetID == Item.Id).ToList();
                foreach (PayrollEmployeeTaxDetail TaxDetail in EmploteeTaxDetail)
                {
                    if (TaxDetail.DeductPercentage == 1)
                    {
                        FirstSlabTax = FirstSlabTax + TaxDetail.DeductedAmount;
                    }
                    else
                    {
                        if (!SlabNames.Contains(TaxDetail.DeductPercentage))
                        {
                            SlabNames.Add(TaxDetail.DeductPercentage);
                            SecondSlabName = SecondSlabName + " " + TaxDetail.DeductPercentage + " % ";
                        }
                        SecondSlabTax = SecondSlabTax + TaxDetail.DeductedAmount;
                    }
                }
                //####  END OF TAX REPORT   ####//
                AnnualSalary = AnnualSalary + Item.RankSalary;
                Gradesalary = Gradesalary + Item.GradeSalary;
                BankAllowance = BankAllowance + Item.BankAllowance;
                RegionalAllowance = RegionalAllowance + Item.RemoteAllowance;
                SpecialAllowance = SpecialAllowance + Item.RankSpecialAllowance;
                OtherAllowances = OtherAllowances + Item.RankOtherAllowances;
                InchargeshipAllowance = InchargeshipAllowance + Item.RankInchargeShipAllowance;
            }  
            Record.EmployeeId = Id;
            Record.EmployeeName = EmployeeDetail.EmpName;
            Record.DesignationName = EmployeeDetail.Designation.DsgName;
            Record.RankSalary = Convert.ToDecimal(EmployeeRankDetail.RankSalary);
            Record.AnnualSalary = AnnualSalary;
            Record.Grade = Convert.ToInt32(EmployeeDetail.CurrentGrade);
            Record.Gradesalary = Gradesalary;
            Record.BankAllowance = BankAllowance;
            Record.RegionalAllowance = RegionalAllowance;
            Record.OtherAllowances = OtherAllowances;

            Record.SpecialAllowance = SpecialAllowance;
            Record.InchargeshipAllowance = InchargeshipAllowance;


            Record.PfSelf = PfSelfTotal;
            Record.FirstSlabTax = FirstSlabTax;
            Record.SecondSlabName = SecondSlabName;
            Record.SecondSlabTax = SecondSlabTax;
            Record.PfCompany = PfCompanyTotal;
            Record.Cit = CitTotal;
            Record.PfExtra = PfExtraTotal;
            return Record;
        }

        public IndividualYearlyTaxEstimationModel GetIndividualYearlyTaxEstimation(int Id)
        {
            IndividualYearlyTaxEstimationModel Record= new IndividualYearlyTaxEstimationModel();
            Employee EmployeeDetail =  _unitOfWork.EmployeeRepository.GetById(Id);
            Office EmployeeOfficeDetail = _unitOfWork.OfficeRepository.GetById(EmployeeDetail.EmpOfficeId);
            int RankId = EmployeeDetail.EmpRankId;
            decimal AnnualSalary = 0;
            decimal Gradesalary = 0;
            decimal BankAllowance = 0;
            decimal RegionalAllowance = 0;
            decimal OtherAllowances = 0;
            decimal FirstSlabTax = 0;
            decimal SecondSlabTax = 0;
            string SecondSlabName = null;
            decimal PfSelfTotal = 0;
            decimal PfCompanyTotal = 0;
            decimal CitTotal = 0;
            decimal PfExtraTotal = 0;
            decimal SpecialAllowance = 0;
            decimal InchargeshipAllowance = 0;
            Rank EmployeeRankDetail = _unitOfWork.RankRepository.GetById(RankId);
            List<PayrollSalaryMasterSheet> EmployeeSalaryDetail = _unitOfWork.PayrollSalaryMasterSheetRepository.Get(x => x.EmployeeCode == Id).ToList();
            List<decimal> SlabNames = new List<decimal>();
            foreach (PayrollSalaryMasterSheet Item in EmployeeSalaryDetail)
            {
                //####  PFSELF ####//
                decimal PfSelf = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 1).Select(x=>x.CalculatedValue).Sum();
                PfSelfTotal = PfSelfTotal + PfSelf;
                //####  Company ####//
                decimal PfCompany = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 2).Select(x => x.CalculatedValue).Sum();
                PfCompanyTotal = PfCompanyTotal + PfCompany;
                //####  Extra ####//
                decimal PfExtra = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 3).Select(x => x.CalculatedValue).Sum();
                PfExtraTotal = PfExtraTotal + PfExtra;
                //####  CIT ####//
                decimal Cit = _unitOfWork.PayrollSalaryDetailSheet.Get(x => x.PayrollSalaryMasterId == Item.Id && x.AllowanceId == 4).Select(x => x.CalculatedValue).Sum();
                CitTotal = CitTotal + Cit;
                //####  TAX REPORT STARTS HERE   ####//
                List<PayrollEmployeeTaxDetail> EmploteeTaxDetail = _unitOfWork.PayrollEmployeeTaxDetail.Get(x => x.EmployeeCode == Id && x.PayrollMasterSheetID == Item.Id).ToList();                
                foreach (PayrollEmployeeTaxDetail TaxDetail in EmploteeTaxDetail)
                {
                    if(TaxDetail.DeductPercentage == 1)
                    {
                        FirstSlabTax = FirstSlabTax + TaxDetail.DeductedAmount;
                    }
                    else
                    {
                        if (!SlabNames.Contains(TaxDetail.DeductPercentage))
                        {
                            SlabNames.Add(TaxDetail.DeductPercentage);
                            SecondSlabName = SecondSlabName + " " + TaxDetail.DeductPercentage + " % ";
                        }
                        SecondSlabTax = SecondSlabTax + TaxDetail.DeductedAmount;
                    }
                }
                //####  END OF TAX REPORT   ####//
                AnnualSalary = AnnualSalary + Item.RankSalary;
                Gradesalary = Gradesalary + Item.GradeSalary;
                BankAllowance = BankAllowance + Item.BankAllowance;
                RegionalAllowance = RegionalAllowance + Item.RemoteAllowance;
                SpecialAllowance = SpecialAllowance + Item.RankSpecialAllowance;
                OtherAllowances = OtherAllowances + Item.RankOtherAllowances;
                InchargeshipAllowance = InchargeshipAllowance + Item.RankInchargeShipAllowance;
            
            }
            //####  ESTIMATION STARTS HERE  ####//
            int MonthIndex = _unitOfWork.PayrollSalaryMasterSheetRepository.Get(x=>x.EmployeeCode == Id).Select(x => x.PayrollSalaryTable.PayrollMonthDescription.MonthIndex).Max();
            int RemainingMonth = 12 - MonthIndex;
            decimal RankSalary = Convert.ToDecimal(EmployeeRankDetail.RankSalary);
            decimal RemainingRanksalary = RankSalary * RemainingMonth;
             //####  ESTIMATED ALLOWANNCES   ####//
            decimal EstimatedBankAllowance = Convert.ToDecimal(EmployeeRankDetail.BankAllowance) * RemainingMonth;
            decimal EstimatedInchargeship = 0;
            if (EmployeeOfficeDetail.OfficeHeadEmpCode == Id)
            {
                EstimatedInchargeship = Convert.ToDecimal(EmployeeRankDetail.RankInchargeShipAllowance) * RemainingMonth;
            }
            decimal EstimatedSpecialAllowance = Convert.ToDecimal(EmployeeRankDetail.RankSpecialAllowance) * RemainingMonth;
            decimal EstimatedRankOtherAllowance = Convert.ToDecimal(EmployeeRankDetail.RankOtherAllowances) * RemainingMonth;
            //####  ESTIMATION OF PF SELF   ####//
            decimal PfSelfEstimate = 0;
            decimal PfCompanyEstimate = 0;
            decimal PfExtraEstimate = 0;
            decimal CITEstimate = 0;
            List<PayrollAllowanceMaster> AllAllowanceMaster = _unitOfWork.PayrollAllowanceMasterRepository.Get(x=>x.AllowanceMasterId <= 4 && x.IsActive == true && x.ApplyToAllEmployee == true && x.SameForAllEmployee == true).ToList();
            foreach(PayrollAllowanceMaster Masters  in AllAllowanceMaster)
            {
                if(Masters.AllowanceMasterId == 1)
                {
                    if(Masters.PercentageAmount == "A")
                    {
                        PfSelfEstimate = PfSelfEstimate + Masters.Value;
                    }
                    else
                    {
                        PfSelfEstimate = (Convert.ToDecimal(EmployeeRankDetail.RankSalary) + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade)) * Masters.Value / 100;
                    }
                }
                if (Masters.AllowanceMasterId == 2)
                {
                    if (Masters.PercentageAmount == "A")
                    {
                        PfCompanyEstimate = PfCompanyEstimate + Masters.Value;
                    }
                    else
                    {
                        PfCompanyEstimate = (Convert.ToDecimal(EmployeeRankDetail.RankSalary) + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade)) * Masters.Value / 100;
                    }
                }
                if (Masters.AllowanceMasterId == 3)
                {
                    if (Masters.PercentageAmount == "A")
                    {
                        PfExtraEstimate = PfExtraEstimate + Masters.Value;
                    }
                    else
                    {
                        PfExtraEstimate = (Convert.ToDecimal(EmployeeRankDetail.RankSalary) + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade)) * Masters.Value / 100;
                    }
                }
                if (Masters.AllowanceMasterId == 4)
                {
                    if (Masters.PercentageAmount == "A")
                    {
                        CITEstimate = CITEstimate + Masters.Value;
                    }
                    else
                    {
                        decimal RankAllowancesAmount = Convert.ToDecimal(EmployeeRankDetail.BankAllowance) + Convert.ToDecimal(EmployeeRankDetail.RankSpecialAllowance) + Convert.ToDecimal(EmployeeRankDetail.RankOtherAllowances);
                        if (EmployeeOfficeDetail.OfficeHeadEmpCode == Id)
                        {
                            RankAllowancesAmount = RankAllowancesAmount + Convert.ToDecimal(EmployeeRankDetail.RankInchargeShipAllowance);
                        }
                        CITEstimate = (RankAllowancesAmount + Convert.ToDecimal(EmployeeRankDetail.RankSalary) + (Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade))) * Masters.Value / 100;
                    }
                }

            }
            List<PayrollAllowanceMaster> SelectiveAllowanceMaster = _unitOfWork.PayrollAllowanceMasterRepository.Get(x => x.AllowanceMasterId <= 4 && x.IsActive == true && x.ApplyToAllEmployee == true && x.SameForAllEmployee == false).ToList();
            foreach(PayrollAllowanceMaster Selective in SelectiveAllowanceMaster)
            {
                PayrollAllowanceDetail Details = _unitOfWork.PayrollAllowanceDetailRepository.Get(x => x.AllowanceMasterId == Selective.AllowanceMasterId && x.EmployeeCode == Id).FirstOrDefault();
                if(Details != null)
                {
                    if (Details.AllowanceMasterId == 1)
                    {
                        PfSelfEstimate = 0;
                        if (Details.PercentageAmount == "A")
                        {
                            PfSelfEstimate = PfSelfEstimate + Convert.ToDecimal(Details.Value);
                        }
                        else
                        {
                            PfSelfEstimate = (Convert.ToDecimal(EmployeeRankDetail.RankSalary) + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade)) * Convert.ToDecimal(Details.Value) / 100;
                        }
                    }
                    if (Details.AllowanceMasterId == 2)
                    {
                        PfCompanyEstimate = 0;
                        if (Details.PercentageAmount == "A")
                        {
                            PfCompanyEstimate = PfCompanyEstimate + Convert.ToDecimal(Details.Value);
                        }
                        else
                        {
                            PfCompanyEstimate = (Convert.ToDecimal(EmployeeRankDetail.RankSalary) + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade)) * Convert.ToDecimal(Details.Value) / 100;
                        }
                    }
                    if (Details.AllowanceMasterId == 3)
                    {
                        PfExtraEstimate = 0;
                        if (Details.PercentageAmount == "A")
                        {
                            PfExtraEstimate = PfExtraEstimate + Convert.ToDecimal(Details.Value);
                        }
                        else
                        {
                            PfExtraEstimate = (Convert.ToDecimal(EmployeeRankDetail.RankSalary) + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade)) * Convert.ToDecimal(Details.Value) / 100;
                        }
                    }
                    if (Details.AllowanceMasterId == 4)
                    {
                        CITEstimate = 0;
                        if (Details.PercentageAmount == "A")
                        {
                            CITEstimate = CITEstimate + Convert.ToDecimal(Details.Value);
                        }
                        else
                        {

                            decimal RankAllowancesAmount = Convert.ToDecimal(EmployeeRankDetail.BankAllowance) + Convert.ToDecimal(EmployeeRankDetail.RankSpecialAllowance) + Convert.ToDecimal(EmployeeRankDetail.RankOtherAllowances);
                            if (EmployeeOfficeDetail.OfficeHeadEmpCode == Id)
                            {
                                RankAllowancesAmount = RankAllowancesAmount + Convert.ToDecimal(EmployeeRankDetail.RankInchargeShipAllowance);
                            }
                            CITEstimate = (RankAllowancesAmount + Convert.ToDecimal(EmployeeRankDetail.RankSalary) + (Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount) * Convert.ToDecimal(EmployeeDetail.CurrentGrade))) * Convert.ToDecimal(Details.Value) / 100;
                        }
                    }
                }
            }
            //####  YEARLY SOCIAL SECURITY TAX  ####//
            decimal TaxableYearlyIncome = (AnnualSalary + Convert.ToDecimal(EmployeeRankDetail.RankSalary) * RemainingMonth) + ((BankAllowance + RegionalAllowance + OtherAllowances) * RemainingMonth)+(Gradesalary + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount*EmployeeDetail.CurrentGrade) * RemainingMonth);
            int TaxId = Convert.ToInt32(EmployeeDetail.EmpTaxSetupId);
            List<PayrollTaxDetail> TaxDetails = _unitOfWork.TaxDetailRepository.Get(x => x.MasterId == TaxId).OrderBy(x => x.OrderNumber).ToList();
            decimal YearlySocialSecurityTax = 0;
            decimal TaxAccumulator = 0;
            decimal AccumulatedTaxAmount = 0;
            foreach(PayrollTaxDetail Detail in TaxDetails)
            {
                if(Detail.OrderNumber == 1)
                {
                    if(TaxableYearlyIncome<=Detail.MaxAmount)
                    {
                        YearlySocialSecurityTax = TaxableYearlyIncome * Detail.Percentage/100;
                    }
                    else
                    {
                        YearlySocialSecurityTax = Detail.MaxAmount * Detail.Percentage / 100;
                        TaxAccumulator = TaxableYearlyIncome - Detail.MaxAmount;
                    }
                }
                else
                {
                    if(TaxAccumulator>0)
                    {
                       if(TaxAccumulator > Detail.MaxAmount)
                       {
                           AccumulatedTaxAmount = (AccumulatedTaxAmount + (Detail.MaxAmount * Detail.Percentage)) / 100;
                           TaxAccumulator = TaxableYearlyIncome - Detail.MaxAmount;
                       }
                       else
                       {
                           AccumulatedTaxAmount = (AccumulatedTaxAmount + (TaxAccumulator * Detail.Percentage)) / 100;
                           TaxAccumulator = TaxableYearlyIncome - Detail.MaxAmount;
                       }
                    }
                }
            }
            decimal TotalGotRankAllowances = BankAllowance + SpecialAllowance + OtherAllowances + InchargeshipAllowance + RegionalAllowance;
            decimal EstimatedRankAllowances = EstimatedBankAllowance + EstimatedInchargeship + EstimatedSpecialAllowance + EstimatedRankOtherAllowance;
            //#################     END OF ESTIMATION    ################//
            Record.EmployeeId = Id;
            Record.EmployeeName = EmployeeDetail.EmpName;
            Record.DesignationName = EmployeeDetail.Designation.DsgName;
            Record.RankSalary = AnnualSalary + Convert.ToDecimal(EmployeeRankDetail.RankSalary) * RemainingMonth;
            Record.AnnualSalary = AnnualSalary + RemainingRanksalary;
            Record.Grade = Convert.ToInt32(EmployeeDetail.CurrentGrade);
            Record.Gradesalary = Gradesalary + Convert.ToDecimal(EmployeeRankDetail.RankPerGradeAmount*EmployeeDetail.CurrentGrade) * RemainingMonth;
            Record.BankAllowance = BankAllowance;
            Record.RegionalAllowance = RegionalAllowance;
            Record.OtherAllowances = OtherAllowances;
            Record.RankAllowancesTotal = TotalGotRankAllowances + EstimatedRankAllowances;
            Record.PfSelf = PfSelfTotal + PfSelfEstimate*RemainingMonth;
            Record.FirstSlabTax = FirstSlabTax;
            Record.SecondSlabName = SecondSlabName;
            Record.SecondSlabTax = SecondSlabTax;
            Record.PfCompany = PfCompanyTotal + PfCompanyEstimate * RemainingMonth;
            Record.Cit = CitTotal + CITEstimate * RemainingMonth;
            Record.PfExtra = PfExtraTotal;
            Record.TaxableYearlyIncome = Record.RankSalary + Record.RankAllowancesTotal + Record.Gradesalary;
            Record.YearlySocialSecurityTax = YearlySocialSecurityTax;
            Record.AccumulatedTaxAmount = AccumulatedTaxAmount;
            //Record.YearlySocialSecuritTax 
            return Record;
        }

        public List<SelectListItem> GetOfficeSelectList()
        {
            List<Office> OfficeList = _unitOfWork.OfficeRepository.Get(x=>x.OfficeStatus == true).ToList();
            List<SelectListItem> Record = new List<SelectListItem>();
            foreach(Office Offices in OfficeList)
            {
                Record.Add(new SelectListItem() { Text = Offices.OfficeName, Value = Offices.OfficeId.ToString() });
            }
            return Record;
        }
    }
}
