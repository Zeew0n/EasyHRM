using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Mapper;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmInterface.GlobalSelectLists;
using English2NepaliDateConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation
{
    public class LeaveSetupservices : ILeaveSetupservices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDynamicSelectList _DynamicSelectList;

        public LeaveSetupservices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            this._DynamicSelectList = new DynamicSelectList();
        }
        #region LeaveType Setup
        public LeaveTypesDTOs LeaveTypeCreateInfromation(int? id)
        {
            LeaveTypesDTOs result = new LeaveTypesDTOs();
            if (id != null)
            {
                LeaveType data = _unitOfWork.LeaveTypeRepository.All().Where(x => x.LeaveTypeId == id).FirstOrDefault();
                result = LeaveTypesMapper.LeaveTypeToLeaveTypesDTO(data);
            }

            result.GenderList = StaticSelectList.GetGenderList().ToList();
            result.LeaveCashableList = StaticSelectList.GetLeaveCashSelectList().ToList();
            result.LeaveFrequencyList = StaticSelectList.GetLeaveAllowFrequencySelectList().ToList();
            result.MaritialStatusList = StaticSelectList.GetMaritialStatusList().ToList();
            result.LeaveTypeAssignmentList = StaticSelectList.GetLeaveTypeAssignmentSelectList().ToList();
            result.LeavePayableList = StaticSelectList.GetLeavePayableSelectList().ToList();
            result.LeaveTransferableList = StaticSelectList.GetLeaveTransferSelectList().ToList();
            return result;
        }

        public IEnumerable<LeaveTypesDTOs> LeaveTypeList()
        {
            List<LeaveType> Data = _unitOfWork.LeaveTypeRepository.All().ToList();
            IEnumerable<LeaveTypesDTOs> Result = LeaveTypesMapper.LeaveTypeListToLeaveTypesDTOList(Data);
            return Result;
        }
        public void LeaveTypeInsert(LeaveTypesDTOs Record)
        {
            LeaveType Data = LeaveTypesMapper.LeaveTypeDTOToLeaveTypes(Record);
            _unitOfWork.LeaveTypeRepository.Create(Data);
        }
        public void LeaveTypeUpdate(LeaveTypesDTOs Record)
        {
            LeaveType Data = LeaveTypesMapper.LeaveTypeDTOToLeaveTypes(Record);
            _unitOfWork.LeaveTypeRepository.Update(Data);
        }
        public void LeaveTypeDelete(int id)
        {

            _unitOfWork.LeaveTypeRepository.Delete(id);
        }
        #endregion

        #region LeaveYearSetUp
        public IEnumerable<LeaveYearsDTOs> LeaveYearList()
        {
            List<LeaveYear> Data = _unitOfWork.LeaveYearRepository.All().ToList();
            return LeaveYearsMapper.LeaveYearsListToLeaveYearsDtosList(Data);
        }
        public void LeaveYearCreate(LeaveYearsDTOs Record)
        {
            LeaveYear Data = LeaveYearsMapper.LeaveYearsDtoToLeaveYears(Record);
            Data.YearCurrent = Record.Isactive;
            NepaliDate StartNepali = GeneralServices.GetNepaliDate(Convert.ToDateTime(Data.YearStartDate));
            NepaliDate EndNepali = GeneralServices.GetNepaliDate(Convert.ToDateTime(Data.YearEndDate));
            Data.YearStartDateNp = StartNepali.npDate;
            Data.YearEndDateNp = EndNepali.npDate;
            _unitOfWork.LeaveYearRepository.Create(Data);
        }
        public void UpdateLeaveYear(LeaveYearsDTOs Record)
        {
            LeaveYear Data = LeaveYearsMapper.LeaveYearsDtoToLeaveYears(Record);
            Data.YearCurrent = Record.Isactive;
            NepaliDate StartNepali = GeneralServices.GetNepaliDate(Convert.ToDateTime(Data.YearStartDate));
            NepaliDate EndNepali = GeneralServices.GetNepaliDate(Convert.ToDateTime(Data.YearEndDate));
            Data.YearStartDateNp = StartNepali.npDate;
            Data.YearEndDateNp = EndNepali.npDate;
            
            _unitOfWork.LeaveYearRepository.Update(Data);
        }
        public int LeaveYearDelete(int id)
        {
            LeaveYear data = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearId == id).FirstOrDefault();
            _unitOfWork.LeaveYearRepository.Delete(data);
            return 0;
        }
        #endregion
        #region LeaveRules
        public IEnumerable<LeaveRulesDTOs> LeaveRulesList()
        {
            IEnumerable<LeaveRule> Data = _unitOfWork.LeaveRuleRepository.All();
            return LeaveRulesMapper.LeaveRuleListToLeaveRulesDTOList(Data.ToList());
        }
        public LeaveRulesDTOs LeaveRulesCreateInformation()
        {
            LeaveRulesDTOs Result = new LeaveRulesDTOs();
            Result.LeaveTypeList = _DynamicSelectList.LeaveTypeList().ToList();
            return Result;
        }
        public void LeaveRulesCreate(LeaveRulesDTOs Record)
        {

            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {

                    LeaveRule test = _unitOfWork.LeaveRuleRepository.Create(LeaveRulesMapper.LeaveRuleDtoToLeaveRules(Record));
                    int count = Record.LeaveRuleDetailsColection.Count();
                    for (int i = 0; i < count; i++)
                    {
                        LeaverulesDetailsDtos data = new LeaverulesDetailsDtos();
                        data.LeaveRuleId = test.LeaveRuleId;
                        data.LeaveTypeId = Record.LeaveRuleDetailsColection[i].LeaveTypeId;
                        if (Record.LeaveRuleDetailsColection[i].LeaveDays == null)
                        {
                            data.LeaveDays = 0;
                        }
                        data.LeaveDays = Record.LeaveRuleDetailsColection[i].LeaveDays;
                        LeaveRuleDetail insertdata = LeaveRuleDetailsMapper.LeaveRulesdetailDtoToLeaveRuleDetails(data);
                        _unitOfWork.LeaveRuleDetailRepository.Create(insertdata);
                     }
                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void LeaveRulesUpdate(LeaveRulesDTOs Record)
        {
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {

                    _unitOfWork.LeaveRuleRepository.Update(LeaveRulesMapper.LeaveRuleDtoToLeaveRules(Record));
                    int count = Record.LeaveRuleDetailsColection.Count();
                    for (int i = 0; i < count; i++)
                    {
                        LeaverulesDetailsDtos data = new LeaverulesDetailsDtos();
                        data.LeaveRuleId = Record.LeaveRuleId;
                        data.LeaveTypeId = Record.LeaveRuleDetailsColection[i].LeaveTypeId;
                        data.DetailId = Record.LeaveRuleDetailsColection[i].DetailId;
                        if (Record.LeaveRuleDetailsColection[i].LeaveDays == null)
                        {
                            data.LeaveDays = 0;
                        }
                        else
                        {
                            data.LeaveDays = Record.LeaveRuleDetailsColection[i].LeaveDays;
                        }
                        
                        LeaveRuleDetail insertdata = LeaveRuleDetailsMapper.LeaveRulesdetailDtoToLeaveRuleDetails(data);
                       
                            _unitOfWork.LeaveRuleDetailRepository.Update(insertdata);                        
                        
                    }
                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region LeaveRulesDetails
        public IEnumerable<LeaverulesDetailsDtos> LeaveRulesDetailsList()
        {
            List<LeaveRuleDetail> Data = _unitOfWork.LeaveRuleDetailRepository.All().ToList();
            return LeaveRuleDetailsMapper.LeaveRuleDetailListToLeaveRuleDetailsDto(Data);

        }


        #endregion
    }
}
