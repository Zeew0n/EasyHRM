using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation
{
   public class LeaveAddAdminServices: ILeaveAddAdminServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDynamicSelectList _DynamicSelectList;

        public LeaveAddAdminServices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            this._DynamicSelectList = new DynamicSelectList();
        }
        public LeaveBalance LeaveBalanceList(int YearId, int Empcode)
        {
            var data1 = _unitOfWork.LeaveAssignedRepository.All().Where(x => x.AssignEmpCode == Empcode && x.AssignedLeaveYearId == YearId).GroupBy(x => x.AssignLeaveTypeId).Select(x=>x);
            return null;
        }
    }
}
