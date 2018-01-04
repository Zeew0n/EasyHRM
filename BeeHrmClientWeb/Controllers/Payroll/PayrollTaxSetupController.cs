using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollTaxSetupController : BaseController
    {
        private IModulServices _moduleService;
        private IPayrollGenerationService _PayrollGenerationService;
        private ITaxSetupService _TaxSetupService;
        private ITaxDetailService _TaxDetailService;
        public PayrollTaxSetupController()
        {
            _moduleService = new ModuleService();
            _PayrollGenerationService = new PayrollGenerationService();
            _TaxSetupService = new TaxSetupService();
            _TaxDetailService = new TaxDetailService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        // GET: TaxSetup

        #region TaxSetup
        public ActionResult Index()
        {
            IEnumerable<TaxSetupDTO> TaxSetupList = _TaxSetupService.GetAllTaxSetup();
            return View(TaxSetupList);
        }

        public ActionResult TaxSetupCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaxSetupCreate(TaxSetupDTO Record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Form validation errors.";
                    return View();
                }
                else
                {
                    ViewBag.Success = "Tax setup created successfully.";
                    _TaxSetupService.InsertIntoTaxSetup(Record);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Exception)
            {
                ViewBag.Error = Exception.Message;
                return View();
            }
        }
        public ActionResult TaxSetupEdit(int Id)
        {
            TaxSetupDTO TaxSetupDTO = _TaxSetupService.GetTaxSetupById(Id);
            return View(TaxSetupDTO);
        }
        [HttpPost]
        public ActionResult TaxSetupEdit(TaxSetupDTO Record)
        {
            int Id = _TaxSetupService.UpdateTaxSetup(Record);
            IEnumerable<TaxSetupDTO> TaxSetupList = _TaxSetupService.GetAllTaxSetup();
            return View("Index", TaxSetupList);
        }

        public ActionResult TaxDetails()
        {
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetails();
            return View(TaxDetailList);
        }
        public ActionResult TaxSetupDetails(int MasterId)
        {
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(MasterId);
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetailsByMasterId(MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            return View(TaxDetailList);
        }


        public ActionResult TaxDetailCreate(int MasterId)
        {
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            return View();
        }

        [HttpPost]

        public ActionResult TaxDetailCreate(TaxDetailDTO Record)
        {
            _TaxDetailService.InsertIntoTaxDetail(Record);
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(Record.MasterId);
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetailsByMasterId(Record.MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            return View("TaxSetupDetails", TaxDetailList);
        }
        [Route("Payroll/TaxDetailEdit/{Id}")]
        public ActionResult TaxDetailEdit(int Id)
        {
            TaxDetailDTO TaxDetailList = _TaxDetailService.GetTaxDetailById(Id);
            TaxSetupDTO Setup = _TaxSetupService.GetTaxSetupById(TaxDetailList.MasterId);
            ViewBag.MasterName = Setup.TaxName;
            ViewBag.MasterId = Setup.MasterId;
            return View(TaxDetailList);
        }
        [HttpPost]
        [Route("Payroll/TaxDetailEdit/{Id}")]
        public ActionResult TaxDetailEdit(TaxDetailDTO Record)
        {
            _TaxDetailService.UpdateTaxDetail(Record);
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(Record.MasterId);
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetailsByMasterId(Record.MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            ViewBag.MasterId = TaxSetup.MasterId;
            return View("TaxSetupDetails", TaxDetailList);
        }
        #endregion
    }
}