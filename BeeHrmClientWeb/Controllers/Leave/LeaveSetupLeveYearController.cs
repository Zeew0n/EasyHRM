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
    public class LeaveSetupLeveYearController:BaseController
    {
        private ILeaveSetupservices _LeaveSetupRepo;

        public LeaveSetupLeveYearController()
        {
            this._LeaveSetupRepo = new LeaveSetupservices();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        // GET: LeaveSetupLeveYear
        public ActionResult List()
        {
            try
            {
                IEnumerable<LeaveYearsDTOs> Result = _LeaveSetupRepo.LeaveYearList();
                return View(Result);
            }
            catch(Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            try
            {               
                return View();
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(LeaveYearsDTOs Data)
        {
            try
            {
               if(ModelState.IsValid)
                {
                    _LeaveSetupRepo.LeaveYearCreate(Data);
                    Session["sucess"] = "Data Inserted Sucessfully";
                    return RedirectToAction("List");
                }
               else
                {
                    return View();
                }
                
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            try
            {
                LeaveYearsDTOs Result = _LeaveSetupRepo.LeaveYearList().Where(x => x.YearId == id).FirstOrDefault();
                Result.Isactive = (bool)Result.YearCurrent;
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("List");
            }
        }
        public ActionResult Edit(LeaveYearsDTOs Data,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _LeaveSetupRepo.UpdateLeaveYear(Data);
                    Session["sucess"]= "Data Updated Sucessfully";
                    return RedirectToAction("List");
                }
                else
                {
                    LeaveYearsDTOs Result = _LeaveSetupRepo.LeaveYearList().Where(x => x.YearId == id).FirstOrDefault();
                    Result.Isactive = (bool)Result.YearCurrent;
                    return View(Result);
                }

            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                LeaveYearsDTOs Result = _LeaveSetupRepo.LeaveYearList().Where(x => x.YearId == id).FirstOrDefault();
                Result.Isactive = (bool)Result.YearCurrent;
                return View(Result);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                 _LeaveSetupRepo.LeaveYearDelete(id);
                    Session["sucess"] = "Data Deleted Sucessfully";
                    return RedirectToAction("List");
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("List");
            }
        }
    }
}