using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class AttendanceRequestsListViewModel
    {
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string RequestType { get; set; }
        public string RecommendStatus { get; set; }
        public string RequestDate { get; set; }
        public string ApproveStatus { get; set; }
        public string Description { get; set; }
        public string Designation { get; set; }
        public string RecommendeDate { get; set; }
        public string ApproveDate { get; set; }
        public string RequestId { get; set; }
        public int? OfficeId { get; set; }
        public int? EmpCodes { get; set; }
        public DateTime startdate { get; set; }
        public string startdateNP { get; set; }
        public DateTime enddate { get; set; }
        public string enddateNP { get; set; }
        public string ApproveMessage { get; set; }
        public string RecommendMessage { get; set; }
        public string CheckIn_Date { get; set; }
        public string CheckOut_Date { get; set; }

        public string RecommenderEmpCode { get; set; }
        public string ApproverEmpCode { get; set; }
        public string EmpEmail { get; set; }
        public string JoinDate { get; set; }
        public string EmpPhoto { get; set; }
        public string Approver { get; set; }
        public string Recommender { get; set; }
        public int? RecommendSatus { get; set; }
        public int? ApproverStatus { get; set; }
        public string IpAddress { get; set; }

     
      


    }
}
