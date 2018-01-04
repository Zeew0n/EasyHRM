using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollAllowanceController : BaseController
    {
        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IUnitOfWork _unitOfWork;
        private IPayrollArrearService _PayrollArrearService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        private IOfficeServices _officeTypeServices;
        public PayrollAllowanceController()
        {
            _officeTypeServices = new OfficeServices();
            _unitOfWork = new UnitOfWork();
            _moduleService = new ModuleService();
            _PayrollAllowanceMasterService = new PayrollAllowanceMasterService();
            _departmentServices = new DepartmentService();
            _PayrollArrearService = new PayrollArrearService();
            _EmployeeService = new EmployeeService();
            _PayrollReportService = new PayrollReportService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        // GET: PayrollAllowance
        #region PayrollNotDefaultAllowances
        public ActionResult Index()
        {

            IEnumerable<PayrollAllowanceMasterDTO> PayrollAllowanceMasterSetup = _PayrollAllowanceMasterService.GetAllPayrollAllowanceMaster();
            return View(PayrollAllowanceMasterSetup);
        }
        private DropDownListViewmodelcs DropDownlist()
        {

            IEnumerable<OfficeDTOs> office = _officeTypeServices.GetOfficeData();
            List<SelectListItem> officeList = new List<SelectListItem>();


            officeList.Add(new SelectListItem
            {
                Text = "Select Office ",
                Value = ""
            });
            foreach (OfficeDTOs str in office)
            {
                officeList.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }


            DropDownListViewmodelcs ddlvm = new DropDownListViewmodelcs();
            ddlvm.OfficeList = officeList;
            return ddlvm;

        }
        public ActionResult PayrollAllowancesCreate()
        {
            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollAllowanceCreateForm();
            return View(Record);
        }
        [HttpPost]
        public ActionResult PayrollAllowancesCreate(PayrollAllowanceMasterDTO Record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    _PayrollAllowanceMasterService.InsertIntoPayrollAllowance(Record);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }
        public ActionResult PayrollAllowancesEdit(int Id)
        {
            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollMasterByMasterId(Id);
            return View(Record);
        }
        [HttpPost]
        public ActionResult PayrollAllowancesEdit(PayrollAllowanceMasterDTO Record)
        {
            PayrollAllowanceMasterDTO data = _PayrollAllowanceMasterService.GetPayrollMasterByMasterId(Record.AllowanceMasterId);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                else
                {
                    PayrollAllowanceMasterDTO ReturnRecord = _PayrollAllowanceMasterService.UpdatePayrollAllowance(Record);
                    ViewBag.Success = "Data Updated Successfully";
                    return View(data);
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View(data);
            }

        }
        #endregion

        [HttpGet]
        public ActionResult AllowanceImport()
        {
            PayrollAllowanceDetailDTO Record = new PayrollAllowanceDetailDTO();
            Record.AllowanceMasterList = _PayrollAllowanceMasterService.GetAllowanceMasterList();
            DropDownListViewmodelcs ddlvm = DropDownlist();
            Record.OfficeList = ddlvm.OfficeList;
            return View(Record);
        }
        [HttpPost]
        public ActionResult AllowanceImport(PayrollAllowanceDetailDTO Record, HttpPostedFileBase file)
        {
            Record.AllowanceMasterList = _PayrollAllowanceMasterService.GetAllowanceMasterList();
            DropDownListViewmodelcs ddlvm = DropDownlist();
            Record.OfficeList = ddlvm.OfficeList;
            try
            {
                string result;
                result = file.FileName;
                DataSet ds = new DataSet();
                DataTable dts = new DataTable();

                string fileExtension =
                    System.IO.Path.GetExtension(result);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {

                    string fileLocation = Server.MapPath("~/Content/AllowanceImport/") + result;
                    if (!Directory.Exists(Server.MapPath("~/Content/AllowanceImport/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/AllowanceImport/"));
                    }
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    file.SaveAs(Path.Combine(Server.MapPath("~/Content/AllowanceImport/"), result));
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation +
                                            ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation +
                                                ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation +
                                                ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        TempData["Danger"] = "Corrupt file. Please check the file and try again";
                        return RedirectToAction("Index");
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {

                        dataAdapter.Fill(ds);
                        dataAdapter.Fill(dts);

                    }
                    excelConnection.Close();

                }
                List<PayrollAllowanceDetailDTO> modelrecord = new List<PayrollAllowanceDetailDTO>();
                foreach (DataRow d in dts.Rows)
                {
                    PayrollAllowanceDetailDTO single = new PayrollAllowanceDetailDTO()
                    {
                        AllowanceMasterId = Record.AllowanceMasterId,
                        EmployeeCode = Convert.ToInt32(d["EmployeeCode"].ToString()),
                        PercentageAmount = d["PercentageAmount"].ToString(),
                        Value = Convert.ToDecimal(d["Value"].ToString()),

                    };

                    single.EmployeeName = _EmployeeService.GetEmployeeByID(single.EmployeeCode).Name;
                    modelrecord.Add(single);
                    PayrollAllowanceDetail domainrecord = PayrollAllowanceDetailRequestFormatter.ConvertRespondentInfoFromDTO(single);
                    PayrollAllowanceDetail pastrecord = _unitOfWork.PayrollAllowanceDetailRepository.Get(x => x.AllowanceMasterId == single.AllowanceMasterId && x.EmployeeCode == single.EmployeeCode).FirstOrDefault();
                    if (pastrecord != null)
                    {
                        _unitOfWork.PayrollAllowanceDetailRepository.Delete(pastrecord);
                    }
                    _unitOfWork.PayrollAllowanceDetailRepository.Create(domainrecord);


                }

                ViewBag.AllowanceImportedList = modelrecord;
                TempData["Success"] = "Allowance Data  imported successfully";



            }
            catch (Exception ex)
            {
                TempData["Danger"] = ex.Message;
            }
            return View(Record);

        }


        public ActionResult ExportToExcel(int Id)
        {
            int i = 1;
            var gv = new GridView();
            var employeeData = _EmployeeService.GetEmployeeByOfficeid(Id);
            List<ExportExcelModel> Record = new List<ExportExcelModel>();

            foreach (var item in employeeData)
            {
               
                ExportExcelModel single = new ExportExcelModel()
                {
                    SNo = i,
                    EmployeeCode = item.EmpCode,
                    EmployeeName = item.EmpName,
                    PercentageAmount = "",
                    Value = Convert.ToDecimal("00.00"),
                };
                i++;
                Record.Add(single);


            }

            gv.DataSource = Record;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("AllowanceImport");
        }
    }
}