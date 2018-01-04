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


namespace BeeHRM.ApplicationService.Implementations
{

    public class UserRoleAccessService : IUserRoleAccessService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserRoleAccessService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public List<RoleAccessDTOs> GetRoleAccessData(int roleID)
        {

            List<RolesAccess> roleAccessData = _unitOfWork.RolesAccessRepository.Get(x => x.AssignRoleId == roleID).ToList();

            if (roleAccessData != null)
            {
                return RoleAccessResponseFormatter.GetAllRolesAndAccess(roleAccessData);
            }
            //throw new UnauthorizedAccessException("User Not Found");
            return null;


        }

        public RoleAccessDTOs GetRoleAccessUserAccesData(int moduleId, int roleId)
        {

            RolesAccess roleAccessUserData = _unitOfWork.RolesAccessRepository.Get(x  => x.AssignModuleId ==moduleId && x.AssignRoleId == roleId).SingleOrDefault();
            if (roleAccessUserData != null)
            {
                return RoleAccessResponseFormatter.RoleAccessModuleData(roleAccessUserData);
            }
            return null;
        }
    }
}
