using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollMonthDescriptionService : IPayrollMonthDescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayrollMonthDescriptionService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }
        public IEnumerable<PayrollMonthDescriptionDTO> GetAllPayrollMonths(int fyId)
        {
            List<PayrollMonthDescriptionDTO> Records = new List<PayrollMonthDescriptionDTO>();
            IEnumerable<PayrollMonthDescription> modelDatas = _unitOfWork.PayrollMonthDescriptipnRepository.All().Where(x => x.FyId == Convert.ToInt32(fyId)).ToList();
            foreach (var str in modelDatas)
            {
                PayrollMonthDescriptionDTO Singles = new PayrollMonthDescriptionDTO();
                Singles = PayrollMonthDescriptionRequestFormatter.ConvertRespondentInfoToDTO(str);
                Singles.Fiscal = FiscalRequestFormatter.ConvertRespondentInfoToDTO(str.Fiscal);
                Records.Add(Singles);
            }
            return Records;
        }
        public void InsertIntoMonthDescription(PayrollMonthDescriptionDTO Record)
        {
            PayrollMonthDescription Insert = PayrollMonthDescriptionRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.PayrollMonthDescriptipnRepository.Create(Insert);
        }

        public IEnumerable<SelectListItem> GetFiscalDropDown()
        {
            List<Fiscal> Fiscals = _unitOfWork.FiscalRepository.All().ToList();
            List<SelectListItem> FiscalList = new List<SelectListItem>();
            foreach (var row in Fiscals)
            {
                FiscalList.Add(new SelectListItem
                {
                    Text = row.FyName,
                    Value = row.FyId.ToString()
                });
            }
            return FiscalList;
        }

        public PayrollMonthDescriptionDTO GetPayrollMonthById(int Id)
        {
            PayrollMonthDescription DbRecords = _unitOfWork.PayrollMonthDescriptipnRepository.GetById(Id);
            IEnumerable<SelectListItem> Record = GetFiscalDropDown();
            foreach (var str in Record)
            {
                if (Convert.ToInt32(str.Value) == DbRecords.FyId)
                {
                    str.Selected = true;
                }
            }
            PayrollMonthDescriptionDTO ViewRecord = PayrollMonthDescriptionRequestFormatter.ConvertRespondentInfoToDTO(DbRecords);
            ViewRecord.Fiscals = Record;
            return ViewRecord;
        }
        public int PayrollMonthsCheck(int monthIndex, int fyid)
        {
            int cnt = _unitOfWork.PayrollMonthDescriptipnRepository.All().Where(x => x.FyId == fyid && x.MonthIndex == monthIndex).Count();

            return cnt;
        }

        public void UpdatePayrollMonth(PayrollMonthDescriptionDTO Data)
        {
            PayrollMonthDescription UpdateData = PayrollMonthDescriptionRequestFormatter.ConvertRespondentInfoFromDTO(Data);
            _unitOfWork.PayrollMonthDescriptipnRepository.Update(UpdateData);
        }

    }
}
