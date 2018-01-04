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
    public class UserInfoService : IUserInfoService
    {
        private IUnitOfWork _unitOfWork;

        public static UserInfoService Instance { get { return new UserInfoService(null); } }

        public UserInfoService() : this(null)
        { }

        public UserInfoService(IUnitOfWork unitOfWork = null, IUserInfoService userInfoService = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public UserInfoDTO GetUserInfoByEmail(string email)
        {
            //User dbUser = _unitOfWork.UserInfoRepository.All().Where(x => x.UserEmailID == email).FirstOrDefault();
            //if (dbUser != null)
            //{
            //    UserInfoDTO userInfoDTO = dbUser.ConvertUserInfoToUserInfoDTO();
            //    return userInfoDTO;
            //}
            return null;
        }
    }
}
