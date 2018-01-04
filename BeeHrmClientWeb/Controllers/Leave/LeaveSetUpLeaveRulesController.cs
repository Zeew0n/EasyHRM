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
    public class LeaveSetUpLeaveRulesController : BaseController
    {
        private ILeaveSetupservices _LeaveSetupRepo;

        public LeaveSetUpLeaveRulesController()
        {
            this._LeaveSetupRepo = new LeaveSetupservices();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        // GET: LraveSetUpLeaveRules
        public ActionResult List()
        {
            try
            {
                IEnumerable<LeaveRulesDTOs> Result = _LeaveSetupRepo.LeaveRulesList();
                return View(Result);
            }
            catch(Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();

            }
           
        }

        public ActionResult Create()
        {
            try
            {
                LeaveRulesDTOs Result = _LeaveSetupRepo.LeaveRulesCreateInformation();
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();

            }
        }
        [HttpPost]
        public ActionResult Create(LeaveRulesDTOs Record)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _LeaveSetupRepo.LeaveRulesCreate(Record);
                    Session["sucess"] = "Data Inserted Sucessfully .";
                    return RedirectToAction("List");
                }
                else
                {                    
                    return View(Record);
                }
                
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View(Record);
               

            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                LeaveRulesDTOs Result = _LeaveSetupRepo.LeaveRulesList().Where(x=>x.LeaveRuleId==id).FirstOrDefault();
                Result.LeaveTypeList = _LeaveSetupRepo.LeaveRulesCreateInformation().LeaveTypeList;
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("List");

            }
        }
        [HttpPost]
        public ActionResult Edit(LeaveRulesDTOs Record ,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _LeaveSetupRepo.LeaveRulesUpdate(Record);
                    Session["sucess"] = "Data Updated sucessfully ";
                    return RedirectToAction("List");
                }
                else
                {
                    return View(Record);
                }
            }
            catch (Exception Ex)
            {
                
                Session["error"] = Ex.Message;
                return View(Record);

            }

        }


    }
}