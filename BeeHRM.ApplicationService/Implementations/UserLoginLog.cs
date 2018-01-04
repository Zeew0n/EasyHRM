using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Implementations
{
    public class UserLoginLog : IUserLoginLog
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLoginLog(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void SaveUserLoginInfo(UserLoginLogDTO loginInfo)
        {

        }

        public UserLoginLogDTO GetUserLoginInfo(string guId, int loginUser)
        {
            return null;
        }
    }
}
