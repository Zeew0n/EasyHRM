using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Leave
{
    public class LeaveMonthlyProcessController : BaseController
    {
        private ILeaveApplicationService _leaveServices;
        public LeaveMonthlyProcessController()
        {
            _leaveServices = new LeaveApplicationService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        public ActionResult Index()
        {
            List<LeaveMonthlyProcessDTO> Record = _leaveServices.GetLeaveYearList();
            return View("../Leave/LeaveMonthlyProcess/LeaveMonthlyProcess", Record);
        }
        public ActionResult MonthlyProcess(int Id)
        {
            try
            {
                _leaveServices.ProcessMonthlyByProcessId(Id);
                TempData["Success"] = "Leave processed successfully";
            }
            catch (Exception Exception)
            {
                TempData["Danger"] = Exception.Message;
            }
            return RedirectToAction("Index");
        }


    }
}
