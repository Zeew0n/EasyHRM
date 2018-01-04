using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
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
    public class PayrollArrearService : IPayrollArrearService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayrollArrearService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public IEnumerable<PayrollArrearsDTO> GetPayrollArrears()
        {
            List<PayrollArrear> modelItem = _unitOfWork.PayrollArrear.All().ToList();
            IEnumerable<PayrollArrearsDTO> items = PayrollArrearsResponseFormatter.GetPayrollArrears(modelItem);
            return items;
        }
        public PayrollArrearsDTO GetCreateForm()
        {
            PayrollArrearsDTO item = new PayrollArrearsDTO();
            IEnumerable<Employee> EmployeeList = _unitOfWork.EmployeeRepository.All().ToList();
            List<SelectListItem> EmpList = new List<SelectListItem>();
            foreach(Employee Employee in EmployeeList)
            {
                EmpList.Add(new SelectListItem
                {
                    Text = Employee.EmpName,
                    Value = Employee.EmpCode.ToString()
                });
            }
            item.EmployeeList = EmpList;
            List<SelectListItem> ArrearMonthList = new List<SelectListItem>();
            List<PayrollMonthDescription> ArrearMnthList = _unitOfWork.PayrollMonthDescriptipnRepository.All().ToList();
            foreach (PayrollMonthDescription Mnt in ArrearMnthList)
            {
                ArrearMonthList.Add(new SelectListItem
                {
                    Text = Mnt.MonthNameNepali,
                    Value = Mnt.Id.ToString()
                });
            }
            item.ArrearMonthList = ArrearMonthList;
            List<SelectListItem> AdjustMonthList = new List<SelectListItem>();
            List<PayrollMonthDescription> AdjustMnthList = _unitOfWork.PayrollMonthDescriptipnRepository.All().ToList();
            foreach (PayrollMonthDescription Mnt in AdjustMnthList)
            {
                AdjustMonthList.Add(new SelectListItem
                {
                    Text = Mnt.MonthNameNepali,
                    Value = Mnt.Id.ToString()
                });
            }
            item.AdjustMonthList = AdjustMonthList;
            List<SelectListItem> FiscalYearList = new List<SelectListItem>();
            List<Fiscal> fsclList = _unitOfWork.FiscalRepository.All().ToList();
            foreach (Fiscal yr in fsclList)
            {
                FiscalYearList.Add(new SelectListItem
                {
                    Text = yr.FyName,
                    Value = yr.FyId.ToString()
                });
            }
            item.FiscalYearList = FiscalYearList;
            List<SelectListItem> ErngsDductionList = new List<SelectListItem>();
            ErngsDductionList.Add(new SelectListItem { Text = "Earning", Value = "E" });
            ErngsDductionList.Add(new SelectListItem { Text = "Deduction", Value = "D" });
            item.EarningDeductionList = ErngsDductionList;
            return item;
        }

        public void InsertIntoArrears(PayrollArrearsDTO Record)
        {
            PayrollArrear modelItem = PayrollArrearsRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.PayrollArrear.Create(modelItem);
        }

        public PayrollArrearsDTO GetArrearById(int Id)
        {
            PayrollArrear item = _unitOfWork.PayrollArrear.GetById(Id);
            PayrollArrearsDTO viewItem = PayrollArrearsRequestFormatter.ConvertRespondentInfoToDTO(item);
            return viewItem;
        }

        public void UpdateArrears(PayrollArrearsDTO Record)
        {
            PayrollArrear modelItem = PayrollArrearsRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.PayrollArrear.Update(modelItem);
        }

    }
}
