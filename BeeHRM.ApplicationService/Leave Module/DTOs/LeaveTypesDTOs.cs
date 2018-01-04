using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{

   
    public class LeaveTypesDTOs
    {
        public int LeaveTypeId { get; set; }
        [Required]
        [DisplayName("Leave Name")]
        public string LeaveTypeName { get; set; }
        [Required]
        [DisplayName("Leave Description")]
        public string LeaveTypeDescription { get; set; }
        [Required]
        public Nullable<byte> NumberOfTime { get; set; }
        [Required]
        public Nullable<bool> IsPayable { get; set; }
        [Required]
        public Nullable<bool> IsCashable { get; set; }
        public Nullable<int> MaxCashable { get; set; }
        [Required]
        public Nullable<bool> IsTransferable { get; set; }
        public Nullable<int> MaxTransferable { get; set; }
        public Nullable<int> LeaveApplyBefore { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string Gender { get; set; }
        public Nullable<decimal> ProRataLeaveRatio { get; set; }
        public string LeaveType { get; set; }
        public string LeaveTypeAssignment { get; set; }
        public Nullable<int> LeaveDeductionPriority { get; set; }
        public Nullable<byte> LeaveCalculation { get; set; }
        public Nullable<bool> HalfdayAllow { get; set; }

        public List<SelectListItem> LeaveTransferableList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> LeaveFrequencyList { get; set; }
        public List<SelectListItem> LeaveCashableList { get; set; }
        public List<SelectListItem> MaritialStatusList { get; set; }
        public List<SelectListItem> LeaveTypeAssignmentList { get; set; }
        public List<SelectListItem> LeavePayableList { get; set; }
       

    }
}
