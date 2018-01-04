using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class AttendanceDeviceController : BaseController
    {
        private IAttendanceDeviceService _attendanceDeviceService;
        public AttendanceDeviceController()
        {
            _attendanceDeviceService = new AttendanceDeviceService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        public ActionResult Index()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            IEnumerable<AttendanceDeviceDTO> AttendanceDeviceList = _attendanceDeviceService.GetAttendanceDeviceList();
            return View(AttendanceDeviceList);

        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(AttendanceDeviceDTO data)
        {
            try
            {
                _attendanceDeviceService.InsertAttendanceDevice(data);
                ViewBag.success = String.Format("New AttendanceDevice Added");
                ModelState.Clear();
                return View();
            }
            catch (Exception Ex)
            {

                ViewBag.error = Ex.Message;
                TempData["Danger"] = Ex.Message;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            AttendanceDeviceDTO attendanceDevicebyId = _attendanceDeviceService.GetAttendanceDeviceByID(id);
            return View(attendanceDevicebyId);
        }
        [HttpPost]
        public RedirectResult Edit(AttendanceDeviceDTO data)
        {
            int attendanceDevice = _attendanceDeviceService.UpdateAttendanceDevice(data);
            return Redirect("/AttendanceDevice/edit/" + data.DeviceId);
        }
    }
}