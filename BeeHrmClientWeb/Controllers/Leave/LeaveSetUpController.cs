using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Leave
{
    public class LeaveSetUpController : BaseController
    {
        private ILeaveSetupservices _LeaveSetupRepo;

        public LeaveSetUpController()
        {
            this._LeaveSetupRepo = new LeaveSetupservices();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        #region LeaveType Setup
        public ActionResult LeaveTypeList()
        {
            try
            {
                IEnumerable<LeaveTypesDTOs> Result = _LeaveSetupRepo.LeaveTypeList();
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult LeaveTypeCreate()
        {
            try
            {
                LeaveTypesDTOs Result = _LeaveSetupRepo.LeaveTypeCreateInfromation(null);
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("LeaveTypeList");
            }
        }
        [HttpPost]
        public ActionResult LeaveTypeCreate(LeaveTypesDTOs Record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LeaveTypesDTOs data = _LeaveSetupRepo.LeaveTypeCreateInfromation(null);
                    return View(data);
                }
                else
                {
                   _LeaveSetupRepo.LeaveTypeInsert(Record);
                    Session["sucess"] = "LeaveType Added Sucessfully !";
                    return RedirectToAction("LeaveTypeList");
                }

            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                LeaveTypesDTOs Result = _LeaveSetupRepo.LeaveTypeCreateInfromation(null);
                return View(Result);
            }
        }

        [HttpGet]
        public ActionResult LeaveTypeEdit(int id)
        {
            try
            {
                LeaveTypesDTOs data = _LeaveSetupRepo.LeaveTypeCreateInfromation(id);
                return View(data);

            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("LeaveTypeList");
            }
        }
        [HttpPost]
        public ActionResult LeaveTypeEdit(LeaveTypesDTOs Record,int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LeaveTypesDTOs data = _LeaveSetupRepo.LeaveTypeCreateInfromation(id);
                    return View(data);
                }
                else
                {
                    Record.LeaveTypeId = id;
                    _LeaveSetupRepo.LeaveTypeUpdate(Record);
                    Session["sucess"] = "Leave Type updatedsucessfully";
                    return RedirectToAction("LeaveTypeList");
                }
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                LeaveTypesDTOs data = _LeaveSetupRepo.LeaveTypeCreateInfromation(id);
                return View(data);
            }


        }
        public ActionResult LeaveTypedelete(int id)
        {
            try
            {
                _LeaveSetupRepo.LeaveTypeDelete(id);
                Session["sucess"] = "Leave deleted Sucessfully";
                return RedirectToAction("LeaveTypeList");
            }
            catch(Exception Ex)
            {
                Session["sucess"] = Ex.Message;
                return RedirectToAction("LeaveTypeList");
            }
        }
        #endregion
    }
}