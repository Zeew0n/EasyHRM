using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Implementations
{
   public class UserService: IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public UsersResponse GetUsersByRole(string roleName)
        {
            UsersResponse userResponse = new UsersResponse();
            //var roleId = _unitOfWork.RolesRepository.Get(a => a.Name.Equals(roleName)).First().Id;
            //var userIds = _unitOfWork.UserRoleRepository.Get(a => a.RoleId.Equals(roleId)).Select(a => a.UserId).ToList();
            //var users = _unitOfWork.UsersRepository.Get(a => userIds.Contains(a.Id)).ToList();

            //users.ForEach(user => {
            //    userResponse.Users.Add(new UsersDTO()
            //    {
            //        ApplicationId = user.ApplicationID,
            //        Email = user.Email,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        UserId = user.Id,
            //        UserName = user.UserName,
            //        Role = roleName
            //    });
            //});

            //return userResponse;
            return null;
        }

        public UsersResponse GetUsers()
        {
            UsersResponse userResponse = new UsersResponse();
            // var users = _unitOfWork.UsersRepository.All();

            //foreach (var user in users)
            //{
            //    userResponse.Users.Add(new UsersDTO()
            //    {
            //        ApplicationId = user.ApplicationID,
            //        Email = user.Email,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        UserId = user.Id,
            //        UserName = user.UserName

            //    });

            //}


            //return userResponse;
            return null;
        }
    }
}
