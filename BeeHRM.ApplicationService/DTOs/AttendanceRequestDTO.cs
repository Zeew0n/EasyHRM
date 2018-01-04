using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class AttendanceRequestDTO
    {
        public int RequestId { get; set; }
        public int RequestEmpCode { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public string CheckInDateNP { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public string CheckOutDateNP { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        [Required(ErrorMessage = "Approver is required !!")]
        public Nullable<int> ApproverEmpCode { get; set; }
        [Required(ErrorMessage = "Recommender is required !!")]
        public Nullable<int> RecommendarEmpCode { get; set; }
       
        public byte ApproveStatus { get; set; }
        public Nullable<System.DateTime> ApproveStatusDate { get; set; }
        public byte RecommendStatus { get; set; }
        public Nullable<System.DateTime> RecommendStatusDate { get; set; }
        [Required(ErrorMessage = "Request Description is required ! ")]
        public string RequestDescription { get; set; }
        [Required(ErrorMessage = "Please Select Request Type! ")]
        public string RequestType { get; set; }

        public TimeSpan? CheckOutTime { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public string ApproverMessage { get; set; }
        public string RecommedarMessage { get; set; }

        public bool Dualday { get; set; }

        public Nullable<System.DateTime> RequestCreatedDate { get; set; }

        public string IpAddress { get; set; }
        
    }
}
