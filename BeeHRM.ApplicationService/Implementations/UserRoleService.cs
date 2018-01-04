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
    public class UserRoleService : IUserRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUnitOfWork unitOfWork = null)
        {
            this._unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public UserRoleService(UnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public UserRoleDTO GetAllUserRoleByRoleID(int roleID)
        {
            //UserRole userRoledb = new UserRole();
            //userRoledb = _unitOfWork.UserPermissionRoleRespository.GetById(roleID);
            //if (userRoledb != null)
            //{
            //    UserRoleDTO userRoleDTO = userRoledb.GetUserRoleResponse();
            //    return userRoleDTO;
            //}
            //else
            //    return null;
            return null;
        }


    }
}
