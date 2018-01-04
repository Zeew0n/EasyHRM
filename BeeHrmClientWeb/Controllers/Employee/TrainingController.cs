using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class TrainingController : BaseController
    {
        private IEmployeeTrainingService _employeeTrainingService;
        private IEmployeeService _employeeService;
        private ICountryService _countryService;
        private IModulServices _moduleService;
        private IOfficeServices _officeService;
        private IDesignationService _designationService;
        private IRankService _rankService;
        List<SelectListItem> RankList = new List<SelectListItem>();
        List<SelectListItem> DesignationList = new List<SelectListItem>();
        List<SelectListItem> OfficeList = new List<SelectListItem>();

        public TrainingController()
        {
            _employeeService = new EmployeeService();
            _employeeTrainingService = new EmployeeTrainingService();
            _countryService = new CountryService();
            _moduleService = new ModuleService();
            _officeService = new OfficeServices();
            _designationService = new DesignationService();
            _rankService = new RankService();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }


        #region Employee Training

        //[Route("training/{id}")]
        //public ActionResult EmployeeTraining(int id)
        //{
        //    if (!ViewBag.AllowView)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    IEnumerable<EmployeeTrainingDTO> res = _employeeTrainingService.GetAllTrainingOfEmployee(id);
        //    ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(id);
        //    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //    return View("../Employee/Training/Index", res);
        //}

        //[Route("training/{id}/create")]
        //public ActionResult EmployeeTrainingCreate(int id)
        //{
        //    if (!ViewBag.AllowCreate)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(id);
        //    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //    IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
        //    ViewBag.countryList = CountryList;
        //    EmployeeTrainingDTO Record = new EmployeeTrainingDTO();
        //    EmployeeDTO EmployeeRecord = _employeeService.GetEmployeeDTOById(id);
        //    Record.CurrentRank = EmployeeRecord.EmpRankId;
        //    Record.CurrentDesignation = EmployeeRecord.EmpDesgId;
        //    Record.CurrentOffice = EmployeeRecord.EmpOfficeId;
        //    Record.AssignedByList = _employeeService.GetEmployeeSelectList();
        //    Record.SponsorshipList = _employeeService.GetSponsorshipList();
        //    Record.NationalInternationalList = _employeeService.GetNationInternationalList();
        //    return View("../Employee/Training/Create", Record);
        //}

        //[Route("training/{id}/create")]
        //[HttpPost]
        //public ActionResult EmployeeTrainingCreate(int id, EmployeeTrainingDTO data)
        //{
        //    data.AssignedByList = _employeeService.GetEmployeeSelectList();
        //    data.SponsorshipList = _employeeService.GetSponsorshipList();
        //    data.NationalInternationalList = _employeeService.GetNationInternationalList();
        //    if (!ViewBag.AllowCreate)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(id);
        //    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //    IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
        //    ViewBag.countryList = CountryList;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            data.EmpCode = id;
        //            _employeeTrainingService.InsertEmployeeTraining(data);
        //            TempData["Success"] = "Employee created successfully.";
        //            return RedirectToAction("EmployeeTraining", id);
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Form validation error";
        //            return View("../Employee/Training/Create", data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View("../Employee/Training/Create", data);
        //    }
        //}

        //[Route("training/{empCode}/TrainingEdit/{trainingId}")]
        //public ActionResult EmployeeTrainingEdit(int empCode, int trainingId)
        //{
        //    if (!ViewBag.AllowEdit)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
        //    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);
        //    IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
        //    ViewBag.countryList = CountryList;
        //    EmployeeTrainingDTO res = _employeeTrainingService.GetTrainingById(trainingId);
        //    res.AssignedByList = _employeeService.GetEmployeeSelectList();
        //    res.SponsorshipList = _employeeService.GetSponsorshipList();
        //    res.NationalInternationalList = _employeeService.GetNationInternationalList();
        //    return View("../Employee/Training/Edit", res);
        //}

        //[Route("training/{empCode}/TrainingEdit/{trainingId}")]
        //[HttpPost]
        //public ActionResult EmployeeTrainingEdit(int empCode, int trainingId, EmployeeTrainingDTO data)
        //{
        //    if (!ViewBag.AllowEdit)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
        //    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);

        //    IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
        //    ViewBag.countryList = CountryList;
        //    try
        //    {
        //        data.EmpCode = empCode;
        //        data.TrainingDays = (data.TrainingEndDate - data.TrainingStartDate).Value.Days;
        //        data.TrainingYear = data.TrainingStartDate.Value.Year;
        //        int res = _employeeTrainingService.UpdateTraining(data);
        //        if (res > 0)
        //            return RedirectToAction("EmployeeTraining", new { id = empCode });
        //        else
        //        {
        //            ViewBag.Error = "Couldn't update";
        //            return View("../Employee/Training/Edit");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View("../Employee/Training/Edit", data);
        //    }

        //}

        //[Route("training/{empCode}/TrainingDelete/{id}")]
        //public ActionResult EmployeeTrainingDelete(int empCode, int id)
        //{
        //    if (!ViewBag.AllowDelete)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    _employeeTrainingService.DeleteTrainingById(id);
        //    return RedirectToAction("EmployeeTraining", new { id = empCode });
        //}
        #endregion
        #region EmpTraining



        public void ListOfDatas(int empCode)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);

            foreach (var row in _officeService.GetOfficeAllData())
            {
                OfficeList.Add(new SelectListItem
                {
                    Text = row.OfficeName,
                    Value = row.OfficeId.ToString()
                });
            }



            foreach (var row in _designationService.GetDesignationList())
            {
                DesignationList.Add(new SelectListItem
                {
                    Text = row.DsgName,
                    Value = row.DsgId.ToString()
                });
            }

            foreach (var row in _rankService.GetRankList())
            {
                RankList.Add(new SelectListItem
                {
                    Text = row.RankName.ToString(),
                    Value = row.RankId.ToString()
                });
            }


            ViewBag.officeList = OfficeList;

            ViewBag.designationList = DesignationList;
            ViewBag.rankList = RankList;



        }
        public ActionResult Index(int Id)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            List<EmpTrainingDTO> Record = _employeeTrainingService.GetAllTrainingOfEmployeeWithId(Id);

            foreach (var data in Record)
            {
                string cname = _countryService.GetCountryName(data.TrainingCountry);
                data.CountryName = cname;
            }
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            return View(Record);
        }
        public ActionResult Create(int Id)
        {
            if (!ViewBag.AllowCreate)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            EmpTrainingDTO Record = new EmpTrainingDTO();
            Record.CountryList = _countryService.GetCountryList();
            EmployeeDTO EmployeeRecord = _employeeService.GetEmployeeDTOById(Id);
            Record.EmpCode = Id;
            ListOfDatas(Id);
            Record.OfficeList = OfficeList;
            Record.DesignationList = DesignationList;
            Record.RankList = RankList;
            Record.Rank = EmployeeRecord.EmpRankId;
            Record.Designation = EmployeeRecord.EmpDesgId;
            Record.Office = EmployeeRecord.EmpOfficeId;
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.SponsorshipList = _employeeService.GetSponsorshipList();
            Record.NationalInternationalList = _employeeService.GetNationInternationalList();
            return View(Record);
        }
        [HttpPost]
        public ActionResult Create(int Id, EmpTrainingDTO Record)
        {
            ListOfDatas(Record.EmpCode);
            Record.LetterDate = !string.IsNullOrEmpty(Record.LetterDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.LetterDateNP)) : Record.LetterDate;
            Record.TrainingStartDate = !string.IsNullOrEmpty(Record.TrainingStartDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingStartDateNP)) : Record.TrainingStartDate;
            Record.TrainingEndDate = !string.IsNullOrEmpty(Record.TrainingEndDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingEndDateNP)) : Record.TrainingEndDate;
            Record.TrainingVisitStartDate = !string.IsNullOrEmpty(Record.TrainingVisitStartDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingVisitStartDateNP)) : Record.TrainingVisitStartDate;
            Record.TrainingVisitEndDate = !string.IsNullOrEmpty(Record.TrainingVisitEndDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingVisitEndDateNP)) : Record.TrainingVisitEndDate;

            Record.OfficeList = OfficeList;
            Record.DesignationList = DesignationList;
            Record.RankList = RankList;
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.SponsorshipList = _employeeService.GetSponsorshipList();
            Record.CountryList = _countryService.GetCountryList();
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.NationalInternationalList = _employeeService.GetNationInternationalList();
            try
            {
                if (ModelState.IsValid)
                {
                    var TrainingId = _employeeTrainingService.CreateEmployeeTraining(Record);
                    _employeeTrainingService.UpdateAttendaceonTraining(TrainingId, "I");
                    TempData["Success"] = "Training created successfully.";
                    return RedirectToAction("Index", new { Id = Id });
                }
                else
                {
                    TempData["Danger"] = "Please fill in the required field";
                }
            }
            catch (Exception Exception)
            {
                TempData["Danger"] = Exception.Message;

            }
            return View(Record);


        }
        public ActionResult Edit(int Id, int EId)
        {
            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(EId);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(EId);
            EmpTrainingDTO Record = _employeeTrainingService.GetEmployeeTrainingById(Id);
            ListOfDatas(EId);
            Record.LetterDateNP = !String.IsNullOrEmpty(Convert.ToString(Record.LetterDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(Record.LetterDate)) : null;
            Record.TrainingStartDateNP = !String.IsNullOrEmpty(Convert.ToString(Record.TrainingStartDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(Record.TrainingStartDate)) : null;
            Record.TrainingEndDateNP = !String.IsNullOrEmpty(Convert.ToString(Record.TrainingEndDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(Record.TrainingEndDate)) : null;
            Record.TrainingVisitStartDateNP = !String.IsNullOrEmpty(Convert.ToString(Record.TrainingVisitStartDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(Record.TrainingVisitStartDate)) : null;
            Record.TrainingVisitEndDateNP = !String.IsNullOrEmpty(Convert.ToString(Record.TrainingVisitEndDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(Record.TrainingVisitEndDate)) : null;

            Record.OfficeList = OfficeList;
            Record.DesignationList = DesignationList;
            Record.RankList = RankList;
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.SponsorshipList = _employeeService.GetSponsorshipList();
            Record.CountryList = _countryService.GetCountryList();
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.NationalInternationalList = _employeeService.GetNationInternationalList();
            return View(Record);
        }
        [HttpPost]
        public ActionResult Edit(int Id, EmpTrainingDTO Record)
        {
            ListOfDatas(Record.EmpCode);
            Record.LetterDate = !string.IsNullOrEmpty(Record.LetterDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.LetterDateNP)) : Record.LetterDate;
            Record.TrainingStartDate = !string.IsNullOrEmpty(Record.TrainingStartDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingStartDateNP)) : Record.TrainingStartDate;
            Record.TrainingEndDate = !string.IsNullOrEmpty(Record.TrainingEndDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingEndDateNP)) : Record.TrainingEndDate;
            Record.TrainingVisitStartDate = !string.IsNullOrEmpty(Record.TrainingVisitStartDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingVisitStartDateNP)) : Record.TrainingVisitStartDate;
            Record.TrainingVisitEndDate = !string.IsNullOrEmpty(Record.TrainingVisitEndDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.TrainingVisitEndDateNP)) : Record.TrainingVisitEndDate;

            Record.OfficeList = OfficeList;
            Record.DesignationList = DesignationList;
            Record.RankList = RankList;
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Record.EmpCode);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Record.EmpCode);
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.SponsorshipList = _employeeService.GetSponsorshipList();
            Record.CountryList = _countryService.GetCountryList();
            Record.AssignedByList = _employeeService.GetEmployeeSelectList();
            Record.NationalInternationalList = _employeeService.GetNationInternationalList();
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeTrainingService.UpdateEmployeeTraining(Record);
                    _employeeTrainingService.UpdateAttendaceonTraining(Record.Id, "U");
                    TempData["Success"] = "Training updated successfully.";
                    return RedirectToAction("Index", new { Id = Record.EmpCode });
                }
                else
                {
                    TempData["Danger"] = "Please fill in the required field";
                }
            }
            catch (Exception Exception)
            {
                TempData["Danger"] = Exception.Message;

            }
            return View(Record);
        }
        public ActionResult Delete(int Id, int EId)
        {
            // _employeeTrainingService.DeleteTraining(Id);
            _employeeTrainingService.UpdateAttendaceonTraining(Id, "D");
            return RedirectToAction("Index", new { Id = EId });


        }
        #endregion
    }
}