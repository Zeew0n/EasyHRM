using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
   public  class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public LoginDTOs GetLoginData(LoginDTOs loginModel)
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO GetLoginData(string EmpUserName, string EmpPassword)
        {

           Employee loginData = _unitOfWork.EmployeeRepository.Get(x => x.EmpUserName == EmpUserName && x.EmpPassword == EmpPassword).FirstOrDefault();
          
            if (loginData!= null)
            {
                return EmployeeRequestFormatter.ConvertRespondentInfoToDTO(loginData); ;
            }
            throw new UnauthorizedAccessException("User Not Found");
            return null;


        }
    }
}
