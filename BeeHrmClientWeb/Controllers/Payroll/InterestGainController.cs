using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmClientWeb.CodeBase;
using BeeHrmInterface.GlobalSelectLists;
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
    public class InterestGainController : BaseController
    {
        private IOfficeServices _officeService;
        private IUnitOfWork _unitOfWork;
        private IEmployeeService _empService;
        private IInterestGainService _IntGainService;
        private IDynamicSelectList _DynamicSelectList;
        public InterestGainController()
        {
            _officeService = new OfficeServices();
            _unitOfWork = new UnitOfWork();
            _empService = new EmployeeService();
            _IntGainService = new InterestGainService();
            _DynamicSelectList = new DynamicSelectList();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }

        private DropDownListViewmodelcs DropDownlist()
        {

            IEnumerable<OfficeDTOs> office = _officeService.GetOfficeData();
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

        // GET: InterestGain
        public ActionResult Index(int? EmpId, int? OfcId)
        {
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            if (OfcId == null && EmpId == null)
            {
                OfcId = Convert.ToInt32(Session["OfficeId"]);
            }
            IEnumerable<PayrollIntrestGainDTO> Record = _IntGainService.GetInterestGainList(EmpId, OfcId);
            ViewBag.OfficeList = _DynamicSelectList.GetBranchSelectList();
            ViewBag.EmpList = _DynamicSelectList.GetEmployeeSelectList().ToList();
            ViewBag.EmpCode = EmpId;
            ViewBag.OfficeId = OfcId;
            return View(Record);
        }

        public ActionResult Import()
        {
            PayrollIntrestGainDTO Record = new PayrollIntrestGainDTO();
            DropDownListViewmodelcs ddlvm = DropDownlist();
            Record.OfficeList = ddlvm.OfficeList;
            return View(Record);
        }


        [HttpPost]
        public ActionResult Import(PayrollIntrestGainDTO Record, HttpPostedFileBase file)
        {
            //Record.AllowanceMasterList = _PayrollAllowanceMasterService.GetAllowanceMasterList();
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

                    string fileLocation = Server.MapPath("~/Content/InterestGainImport/") + result;
                    if (!Directory.Exists(Server.MapPath("~/Content/InterestGainImport/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/InterestGainImport/"));
                    }
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    file.SaveAs(Path.Combine(Server.MapPath("~/Content/InterestGainImport/"), result));
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
                List<PayrollIntrestGainDTO> modelrecord = new List<PayrollIntrestGainDTO>();
                foreach (DataRow d in dts.Rows)
                {
                    PayrollIntrestGainDTO single = new PayrollIntrestGainDTO()
                    {
                        CustomerId = d["CustomerId"].ToString(),
                        //AllowanceMasterId = Record.AllowanceMasterId,
                        //EmpCode = Convert.ToInt32(d["EmployeeCode"].ToString()),
                        InterestGain = Convert.ToDecimal(d["Value"].ToString()),
                        //PercentageAmount = d["PercentageAmount"].ToString(),
                        //Value = Convert.ToDecimal(d["Value"].ToString()),

                    };
                    single.EmpCode = _unitOfWork.EmployeeRepository.Get(x => x.CustomerId == single.CustomerId).FirstOrDefault().EmpCode;
                    single.EmployeeName = _empService.GetEmployeeByID((int)single.EmpCode).Name;
                    modelrecord.Add(single);
                    PayrollIntrestGain domainrecord = PayrollInterestGainResponseFormatter.ConvertRespondentInfoFromDTO(single);
                    PayrollIntrestGain pastrecord = _unitOfWork.InterestGainRepository.Get(x => x.EmpCode == single.EmpCode).FirstOrDefault();
                    if (pastrecord != null)
                    {
                        _unitOfWork.InterestGainRepository.Delete(pastrecord);
                    }
                    _unitOfWork.InterestGainRepository.Create(domainrecord);


                }

                ViewBag.AllowanceImportedList = modelrecord;
                TempData["Success"] = "Allowance Data  imported successfully";



            }
            catch (IOException ex)
            {
                TempData["Danger"] = ex.Message;
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
            var employeeData = _empService.GetEmployeeByOfficeid(Id);
            List<InterestExportExcelModel> Record = new List<InterestExportExcelModel>();

            foreach (var item in employeeData)
            {

                InterestExportExcelModel single = new InterestExportExcelModel()
                {
                    SNo = i,
                    CustomerId = item.CustomerId,
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
            return RedirectToAction("Import");
        }
    }
}