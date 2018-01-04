using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class HolidayController : BaseController
    {


        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private IEthnicityService _ethnicityServices;
        private IReligionService _religionServices;
        private IHolidayServices _holidayServices;


        public HolidayController()
        {


            _officeServices = new OfficeServices();
            _moduleService = new ModuleService();
            _ethnicityServices = new EthnicityService();
            _religionServices = new ReligionService();
            _holidayServices = new HolidayServices();

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


            try
            {
                HolidayDTOs htd = new HolidayDTOs();
                IEnumerable<HolidayDTOs> holidayList = _holidayServices.HolidayList();
                return View(holidayList);
            }

            catch (Exception ex)
            {

                return null;
            }

        }

        [Route("holiday/create")]
        [HttpGet]
        public ActionResult CreateHoliday()
        {
            HolidayDTOs offieList = new HolidayDTOs();
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            IEnumerable<EthnicityDTO> ethnicity = _ethnicityServices.GetEthnicityList();
            IEnumerable<ReligionDTO> religion = _religionServices.GetReligionList();
            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> eth = new List<SelectListItem>();
            List<SelectListItem> rel = new List<SelectListItem>();
            List<SelectListItem> gender = new List<SelectListItem>();

            gender.Add(new SelectListItem
            {
                Text = "All",
                Value = "All"
            });

            gender.Add(new SelectListItem
            {
                Text = "Male",
                Value = "Male"
            });
            gender.Add(new SelectListItem
            {
                Text = "Female",
                Value = "Female"
            });


            rel.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            eth.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            sl.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }



            foreach (EthnicityDTO row in ethnicity)
            {
                eth.Add(new SelectListItem
                {
                    Text = row.EthnicityName,
                    Value = row.EthnicityId.ToString()
                });

            }

            foreach (ReligionDTO row in religion)
            {
                rel.Add(new SelectListItem
                {
                    Text = row.ReligionName,
                    Value = row.ReligionId.ToString()
                });

            }


            offieList.BranchSelectList = sl;
            offieList.EthnicitySelectList = eth;
            offieList.ReligionSelectList = rel;
            offieList.GenderList = gender;

            return View(offieList);

        }

        [HttpPost]
        [Route("holiday/create")]
        public ActionResult CreateHoliday(HolidayDTOs atd, int[] offices)
        {
            atd.HolidayDate = !string.IsNullOrEmpty(atd.HolidayDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(atd.HolidayDateNP)) : atd.HolidayDate;

            HolidayDTOs offieList = new HolidayDTOs();
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            IEnumerable<EthnicityDTO> ethnicity = _ethnicityServices.GetEthnicityList();
            IEnumerable<ReligionDTO> religion = _religionServices.GetReligionList();
            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> eth = new List<SelectListItem>();
            List<SelectListItem> rel = new List<SelectListItem>();
            List<SelectListItem> gender = new List<SelectListItem>();


            rel.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            eth.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            sl.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            gender.Add(new SelectListItem
            {
                Text = "All",
                Value = "All"
            });

            gender.Add(new SelectListItem
            {
                Text = "Male",
                Value = "Male"
            });
            gender.Add(new SelectListItem
            {
                Text = "Female",
                Value = "Female"
            });

            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }

            foreach (EthnicityDTO row in ethnicity)
            {
                eth.Add(new SelectListItem
                {
                    Text = row.EthnicityName,
                    Value = row.EthnicityId.ToString()
                });

            }

            foreach (ReligionDTO row in religion)
            {
                rel.Add(new SelectListItem
                {
                    Text = row.ReligionName,
                    Value = row.ReligionId.ToString()
                });

            }


            offieList.BranchSelectList = sl;
            offieList.EthnicitySelectList = eth;
            offieList.ReligionSelectList = rel;
            offieList.GenderList = gender;


            if (ModelState.IsValid)
            {
                try
                {
                    atd.HolidayStatus = true;

                    if (atd.HolidayOfficeId == 1)
                    {

                        if (offices == null)
                        {
                            ModelState.AddModelError("SelectList", "Please select some offices form the list !!");
                            return View(offieList);
                        }
                        else
                        {
                            int id = _holidayServices.CreateHolidays(atd);
                            for (int i = 0; i < offices.Length; i++)
                            {
                                int officeId = offices[i];
                                _holidayServices.InsertholidayOffices(id, officeId);

                            }

                        }



                    }
                    else
                    {
                        int id = _holidayServices.CreateHolidays(atd);

                    }
                    Session["UpdateHoliday"] = "Holiday Created  Sucessfully !!";
                    return RedirectToAction("Index", "Holiday");

                }
                catch (Exception Ex)
                {
                    return View(Ex.Message);
                }
            }
            else
            {
                return View(offieList);
            }




        }

        [Route("holiday/officeholidaylist/{id}")]
        public JsonResult HolidayOfficeList(int id)
        {
            IEnumerable<HolidayOfficesViewModel> lst = _holidayServices.HolidayOfficeById(id);
            return Json(lst, JsonRequestBehavior.AllowGet);


        }


        [Route("holiday/delete/{id}")]
        public JsonResult DeleteHoliday(int id)
        {

            try
            {
                _holidayServices.DeleteHoliday(id);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
        }

        [Route("holiday/edit/{id}")]
        public ActionResult EditHolidy(int id)
        {


            try
            {
                HolidayDTOs holidaydata = _holidayServices.HolidayListbyId(id);
                IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
                IEnumerable<EthnicityDTO> ethnicity = _ethnicityServices.GetEthnicityList();
                IEnumerable<ReligionDTO> religion = _religionServices.GetReligionList();
                IEnumerable<HolidayOfficesViewModel> selectedoffice = _holidayServices.HolidayOfficeById(id);
                List<SelectListItem> office = new List<SelectListItem>();
                List<SelectListItem> eth = new List<SelectListItem>();
                List<SelectListItem> rel = new List<SelectListItem>();
                List<SelectListItem> gender = new List<SelectListItem>();
                List<SelectListItem> seldoffice = new List<SelectListItem>();

                gender.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "All"
                });

                gender.Add(new SelectListItem
                {
                    Text = "Male",
                    Value = "Male"
                });
                gender.Add(new SelectListItem
                {
                    Text = "Female",
                    Value = "Female"
                });


                rel.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

                eth.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });



                foreach (EthnicityDTO row in ethnicity)
                {
                    eth.Add(new SelectListItem
                    {
                        Text = row.EthnicityName,
                        Value = row.EthnicityId.ToString()
                    });

                }
                foreach (HolidayOfficesViewModel row in selectedoffice)
                {
                    seldoffice.Add(new SelectListItem
                    {
                        Text = row.OfficeName.ToString(),
                        Value = row.OfficeId.ToString()
                    });
                }

                foreach (OfficeDTOs row in officelist)
                {
                    office.Add(new SelectListItem
                    {
                        Text = row.OfficeName.ToString(),
                        Value = row.OfficeId.ToString()
                    });

                }

                foreach (ReligionDTO row in religion)
                {
                    rel.Add(new SelectListItem
                    {
                        Text = row.ReligionName,
                        Value = row.ReligionId.ToString(),

                    });

                }

                holidaydata.BranchSelectList = office;
                holidaydata.EthnicitySelectList = eth;
                holidaydata.GenderList = gender;
                holidaydata.ReligionSelectList = rel;
                holidaydata.SelectedOffices = seldoffice;

                return View(holidaydata);
            }
            catch (Exception Ex)
            {
                Session["updateerroe"] = "Error While Updating " + Ex.Message;
                return RedirectToAction("Index", "Holiday");
            }

        }


        [Route("holiday/edit/{id}")]
        [HttpPost]
        public ActionResult EditHolidy(HolidayDTOs htd, int[] offices)
        {
            htd.HolidayDate = !string.IsNullOrEmpty(htd.HolidayDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(htd.HolidayDateNP)) : htd.HolidayDate;

            try
            {
                HolidayDTOs holidaydata = _holidayServices.HolidayListbyId(htd.HolidayId);
                IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
                IEnumerable<EthnicityDTO> ethnicity = _ethnicityServices.GetEthnicityList();
                IEnumerable<ReligionDTO> religion = _religionServices.GetReligionList();
                IEnumerable<HolidayOfficesViewModel> selectedoffice = _holidayServices.HolidayOfficeById(htd.HolidayId);
                List<SelectListItem> office = new List<SelectListItem>();
                List<SelectListItem> eth = new List<SelectListItem>();
                List<SelectListItem> rel = new List<SelectListItem>();
                List<SelectListItem> gender = new List<SelectListItem>();
                List<SelectListItem> seldoffice = new List<SelectListItem>();

                gender.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "All"
                });

                gender.Add(new SelectListItem
                {
                    Text = "Male",
                    Value = "Male"
                });
                gender.Add(new SelectListItem
                {
                    Text = "Female",
                    Value = "Female"
                });

                rel.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

                eth.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });



                foreach (EthnicityDTO row in ethnicity)
                {
                    eth.Add(new SelectListItem
                    {
                        Text = row.EthnicityName,
                        Value = row.EthnicityId.ToString()
                    });

                }
                foreach (HolidayOfficesViewModel row in selectedoffice)
                {
                    seldoffice.Add(new SelectListItem
                    {
                        Text = row.OfficeName.ToString(),
                        Value = row.OfficeId.ToString()
                    });
                }

                foreach (OfficeDTOs row in officelist)
                {
                    office.Add(new SelectListItem
                    {
                        Text = row.OfficeName.ToString(),
                        Value = row.OfficeId.ToString()
                    });

                }

                foreach (ReligionDTO row in religion)
                {
                    rel.Add(new SelectListItem
                    {
                        Text = row.ReligionName,
                        Value = row.ReligionId.ToString(),

                    });

                }

                holidaydata.BranchSelectList = office;
                holidaydata.EthnicitySelectList = eth;
                holidaydata.GenderList = gender;
                holidaydata.ReligionSelectList = rel;
                holidaydata.SelectedOffices = seldoffice;


                if (ModelState.IsValid)
                {
                    if (htd.HolidayOfficeId == 0)
                    {
                        _holidayServices.DeleteHolidayOffice(htd.HolidayId);
                        _holidayServices.UpdateHoliday(htd);

                    }
                    else
                    if (htd.HolidayOfficeId == 1)
                    {
                        if (offices == null)
                        {
                            ModelState.AddModelError("SelectList", "Please select some offices form the list !!");
                            return View(holidaydata);
                        }
                        else
                        {
                            _holidayServices.DeleteHolidayOffice(htd.HolidayId);
                            for (int i = 0; i < offices.Length; i++)
                            {
                                int officeId = offices[i];
                                _holidayServices.InsertholidayOffices(htd.HolidayId, officeId);

                            }

                            _holidayServices.UpdateHoliday(htd);
                        }


                    }
                    Session["UpdateHoliday"] = "Holiday Updated Sucessfully !!";
                    return RedirectToAction("Index", "Holiday");
                }
                else
                {
                    return View(holidaydata);
                }


            }
            catch (Exception Ex)
            {
                Session["updateerroe"] = "Error While Updating " + Ex.Message;
                return RedirectToAction("Index", "Holiday");
            }


        }

        [Route("holiday/details/{id}")]
        public ActionResult HolidayDetails(int id)
        {

            HolidayDetailsViewModel htd = _holidayServices.HolidayDetails(id);
            return View(htd);
        }


        [Route("admin/holiday/update/{date}")]
        public ActionResult UpdateHoliday(string date)
        {

            try
            {
                _holidayServices.UpdateHoliday(date);
                Session["UpdateHoliday"] = "Holiday Updated  Sucessfully !!";
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {

                Session["UpdateHoliday"] = "Error While Updating Holiday !!" + Ex.Message;
                return RedirectToAction("Index");
            }

        }

    }
}