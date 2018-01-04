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
    public class PayrollRemoteAllowanceService : IPayrollRemoteAllowanceService
    {
        private readonly IUnitOfWork _unitOfWork;

       

        public PayrollRemoteAllowanceService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public PayrollRemoteAllowancesDTO GetPayrollRemoteAllowanceByID(int rAId)
        {
            PayrollRemoteAllowancesDTO rA = PayrollRemoteAllowancesRequestFormatter.ConvertRespondentToDTO(_unitOfWork.PayrollRemoteAllowanceRepository.GetById(rAId));
            return rA;
        }

        public IEnumerable<PayrollRemoteAllowancesDTO> GetPayrollRemoteAllowanceList()
        {
            IEnumerable<PayrollRemoteAllowance> modelDatas = _unitOfWork.PayrollRemoteAllowanceRepository.All().ToList();
            return PayrollRemoteAllowancesResponseFormatter.ModelData(modelDatas);
        }

        public PayrollRemoteAllowancesDTO InsertPayrollRemoteAllowance(PayrollRemoteAllowancesDTO data)
        {
            PayrollRemoteAllowance rA = PayrollRemoteAllowancesRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return PayrollRemoteAllowancesRequestFormatter.ConvertRespondentToDTO(_unitOfWork.PayrollRemoteAllowanceRepository.Create(rA));
        }

        public int UpdatePayrollRemoteAllowance(PayrollRemoteAllowancesDTO data)
        {
            PayrollRemoteAllowance rA = PayrollRemoteAllowancesRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return (_unitOfWork.PayrollRemoteAllowanceRepository.Update(rA));
        }
        public IEnumerable<SelectListItem> GetRemoteList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<PayrollRemoteSetup> RemoteList = _unitOfWork.PayrollRemoteSetupRepository.Get().ToList();
            foreach (PayrollRemoteSetup item in RemoteList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.RemoteName;
                selectData.Value = item.RemoteId.ToString();
                Record.Add(selectData);
            }
            return Record;
        }
        public IEnumerable<SelectListItem> GetRankList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Rank> RankList = _unitOfWork.RankRepository.Get().ToList();
            foreach (Rank item in RankList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.RankName.ToString();
                selectData.Value = item.RankId.ToString();
                Record.Add(selectData);
            }
            return Record;

        }
    }
}
