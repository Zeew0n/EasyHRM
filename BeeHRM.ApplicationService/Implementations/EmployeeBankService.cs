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
     public class EmployeeBankService : IEmployeeBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeBankService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public void DeleteEmployeeBankId(int id)
        {
            _unitOfWork.EmployeeBankRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<EmployeeBankDTO> GetEmployeeBankList()
        {
            return EmployeeBankResponseFormatter.ModelData(_unitOfWork.EmployeeBankRepository.All());
        }

        public EmployeeBankDTO GetEmployeeBankId(int id)
        {
            return EmployeeBankRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeBankRepository.GetById(id));
        }

        public EmployeeBankDTO InsertEmployeeBank(EmployeeBankDTO data)
        {
            EmployeeBank dataToInsert = new EmployeeBank();
            dataToInsert = EmployeeBankRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EmployeeBankRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeBankRepository.Create(dataToInsert));
        }

        public int UpdateEmployeeBank(EmployeeBankDTO data)
        {
            EmployeeBank dataToUpdate = EmployeeBankRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.EmployeeBankRepository.Update(dataToUpdate);
            return res;
        }
        public IEnumerable<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Employee> EmployeeList = _unitOfWork.EmployeeRepository.Get().ToList();
            foreach (Employee item in EmployeeList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.EmpName.ToString();
                selectData.Value = item.EmpCode.ToString();
                Record.Add(selectData);
            }
            return Record;
        }
        public IEnumerable<SelectListItem> GetBankList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Bank> bankList = _unitOfWork.BankRepository.Get().ToList();
            foreach (Bank item in bankList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.BankName.ToString();
                selectData.Value = item.BankId.ToString();
                Record.Add(selectData);
            }
            return Record;

        }
    }
}
