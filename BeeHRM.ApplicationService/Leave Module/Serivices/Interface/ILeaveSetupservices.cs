using BeeHRM.ApplicationService.Leave_Module.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Interface
{
    public interface ILeaveSetupservices
    {
        #region LeaveType Setup
        LeaveTypesDTOs LeaveTypeCreateInfromation(int? id);
        void LeaveTypeInsert(LeaveTypesDTOs Record);
        IEnumerable<LeaveTypesDTOs> LeaveTypeList();
        void LeaveTypeUpdate(LeaveTypesDTOs Record);

        void LeaveTypeDelete(int id);
        #endregion
        #region LeaveYear Setup
        IEnumerable<LeaveYearsDTOs> LeaveYearList();
        int LeaveYearDelete(int id);
        void LeaveYearCreate(LeaveYearsDTOs Record);
        void UpdateLeaveYear(LeaveYearsDTOs Record);
        #endregion
        #region LeaveRules
        void LeaveRulesCreate(LeaveRulesDTOs Record);
        void LeaveRulesUpdate(LeaveRulesDTOs Record);        
        IEnumerable<LeaveRulesDTOs> LeaveRulesList();
        LeaveRulesDTOs LeaveRulesCreateInformation();
        #endregion
        #region LeaveRulesDetails
        IEnumerable<LeaverulesDetailsDtos> LeaveRulesDetailsList();
        #endregion
    }
}
