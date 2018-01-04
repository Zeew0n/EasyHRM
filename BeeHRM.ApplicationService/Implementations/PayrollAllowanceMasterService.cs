using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollAllowanceMasterService : IPayrollAllowanceMasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private PayrollAllowanceMaster _PayrollAllowanceMaster;
        public PayrollAllowanceMasterService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _PayrollAllowanceMaster = new PayrollAllowanceMaster();
        }
        #region NonDefaultPayrollAllowanceCRUD
        public IEnumerable<PayrollAllowanceMasterDTO> GetAllPayrollAllowanceMaster()
        {
            IEnumerable<PayrollAllowanceMaster> modelDatas = _unitOfWork.PayrollAllowanceMasterRepository.Get(x => x.AllowanceType == "O").ToList();
            return PayrollAllowanceMasterResponseFormatter.GetAllTPayrollMasterDTO(modelDatas);
        }
        public PayrollAllowanceMasterDTO GetPayrollAllowanceCreateForm()
        {
            PayrollAllowanceMasterDTO Record = new PayrollAllowanceMasterDTO();

            List<SelectListItem> AllowanceTypeSelectList = new List<SelectListItem>();
            AllowanceTypeSelectList.Add(new SelectListItem { Text = "Retirement Fund", Value = "R" });
            AllowanceTypeSelectList.Add(new SelectListItem { Text = "Other", Value = "O" });
            AllowanceTypeSelectList.Add(new SelectListItem { Text = "Default", Value = "D" });




            List<SelectListItem> EarningDeductionSelectList = new List<SelectListItem>();
            EarningDeductionSelectList.Add(new SelectListItem { Text = "Earning", Value = "E" });
            EarningDeductionSelectList.Add(new SelectListItem { Text = "Saving", Value = "S" });
            EarningDeductionSelectList.Add(new SelectListItem { Text = "Deduction", Value = "D" });


            List<SelectListItem> PercentageAmountSelectList = new List<SelectListItem>();
            PercentageAmountSelectList.Add(new SelectListItem { Text = "Amount", Value = "A" });
            PercentageAmountSelectList.Add(new SelectListItem { Text = "Percentage", Value = "P" });
            Record.EarningDeductionSelectList = EarningDeductionSelectList;
            Record.AllowanceTypeSelectList = AllowanceTypeSelectList;
            Record.PercentageAmountSelectList = PercentageAmountSelectList;
            Record.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            Record.CreatedDate = DateTime.Now;
            return Record;
        }
        public void InsertIntoPayrollAllowance(PayrollAllowanceMasterDTO Record)
        {
            PayrollAllowanceMaster Data = PayrollAllowanceMasterRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            PayrollAllowanceMaster Result = _unitOfWork.PayrollAllowanceMasterRepository.Create(Data);
        }

        #endregion
        #region DefaultPayrollAllowanceCRUD
        public PayrollAllowanceMasterDTO GetPayrollMasterByMasterId(int Id)
        {
            PayrollAllowanceMaster Record = _unitOfWork.PayrollAllowanceMasterRepository.GetById(Id);
            PayrollAllowanceMasterDTO ReturnRecord = PayrollAllowanceMasterRequestFormatter.ConvertRespondentInfoToDTO(Record);
            List<SelectListItem> EarningDeductionSelectList = new List<SelectListItem>();
            EarningDeductionSelectList.Add(new SelectListItem { Text = "Earning", Value = "E" });
            EarningDeductionSelectList.Add(new SelectListItem { Text = "Saving", Value = "N" });
            EarningDeductionSelectList.Add(new SelectListItem { Text = "Deduction", Value = "D" });

            List<SelectListItem> AllowanceTypeSelectList = new List<SelectListItem>();
            AllowanceTypeSelectList.Add(new SelectListItem { Text = "Earning", Value = "E" });
            AllowanceTypeSelectList.Add(new SelectListItem { Text = "Saving", Value = "N" });
            AllowanceTypeSelectList.Add(new SelectListItem { Text = "Deduction", Value = "D" });
            List<SelectListItem> PercentageAmountSelectList = new List<SelectListItem>();
            PercentageAmountSelectList.Add(new SelectListItem { Text = "Amount", Value = "A" });
            PercentageAmountSelectList.Add(new SelectListItem { Text = "Percentage", Value = "P" });
            ReturnRecord.EarningDeductionSelectList = EarningDeductionSelectList;
            ReturnRecord.AllowanceTypeSelectList = AllowanceTypeSelectList;
            ReturnRecord.PercentageAmountSelectList = PercentageAmountSelectList;
            return ReturnRecord;
        }
        #endregion




        public IEnumerable<EmployeeCodeName> GetAllRelatedEmployees(int officeId)
        {
            List<Employee> Record = _unitOfWork.EmployeeRepository.All().Where(a => a.EmpOfficeId == officeId).ToList();
            List<EmployeeCodeName> ReturnRecord = new List<EmployeeCodeName>();
            EmployeeCodeName EmployeeCodeName = new EmployeeCodeName();
            foreach (var str in Record)
            {
                ReturnRecord.Add(new EmployeeCodeName { Text = str.EmpName, Value = str.EmpCode.ToString() });
            }
            return ReturnRecord;
        }



        //to be removed once nondefault work is done
        public PayrollAllowanceMasterDTO GetPayrollDetailByMasterId(int Id, int officeId)
        {
            PayrollAllowanceMaster Record = _unitOfWork.PayrollAllowanceMasterRepository.GetById(Id);
            IEnumerable<PayrollAllowanceDetail> AllowanceDetail = Record.PayrollAllowanceDetails;
            IEnumerable<PayrollAllowanceDetailDTO> Detail = PayrollAllowanceDetailResponseFormatter.GetAllTPayrollDetailDTO(AllowanceDetail);
            PayrollAllowanceMasterDTO ReturnRecord = PayrollAllowanceMasterRequestFormatter.ConvertRespondentInfoToDTO(Record);
            ReturnRecord.PayrollAllowanceDetails = Detail;

            IEnumerable<EmployeeCodeName> EmployeeList = GetAllRelatedEmployees(officeId);


            List<SelectListItem> SelectedEmployeeList = new List<SelectListItem>();
            foreach (var row in Detail)
            {
                SelectedEmployeeList.Add(new SelectListItem
                {
                    Text = GetEmployeeName(row.EmployeeCode),
                    Value = row.EmployeeCode.ToString()
                });
            }

            List<SelectListItem> DropDownEmployee = new List<SelectListItem>();
            foreach (var row in EmployeeList)
            {
                bool selected = false;
                foreach (var Test in SelectedEmployeeList)
                {
                    if (row.Value == Test.Value && row.Text == Test.Text)
                    {
                        selected = true;
                        break;
                    }

                }
                DropDownEmployee.Add(new SelectListItem
                {
                    Text = row.Text,
                    Value = row.Value,
                    Selected = selected
                });
            }
            List<AssignedEmployees> AssignedEmployeesList = new List<AssignedEmployees>();
            foreach (var str in EmployeeList)
            {
                int Value = Convert.ToInt32(str.Value);
                if (Detail.Any(x => x.EmployeeCode == Value))
                {
                    PayrollAllowanceDetailDTO PayrollAllowanceDetails = Detail.Where(x => x.EmployeeCode == Value).FirstOrDefault();
                    AssignedEmployees AssignedEmployeeRow = new AssignedEmployees()
                    {

                        EmployeeCode = PayrollAllowanceDetails.EmployeeCode,
                        EmployeeName = GetEmployeeName(PayrollAllowanceDetails.EmployeeCode),
                        PercentageAmount = PayrollAllowanceDetails.PercentageAmount,
                        Selected = true,
                        Value = PayrollAllowanceDetails.Value != null ? PayrollAllowanceDetails.Value : Convert.ToDecimal("0")
                    };
                    AssignedEmployeesList.Add(AssignedEmployeeRow);
                }
                else
                {
                    AssignedEmployees AssignedEmployeeRow = new AssignedEmployees()
                    {
                        EmployeeCode = Convert.ToInt32(str.Value),
                        EmployeeName = GetEmployeeName(Convert.ToInt32(str.Value)),
                        PercentageAmount = "",
                        Selected = false,
                        Value = Convert.ToDecimal("0")
                    };
                    AssignedEmployeesList.Add(AssignedEmployeeRow);
                }

            }
            ReturnRecord.AssignedEmployees = AssignedEmployeesList;
            ReturnRecord.SelectedEmployeeList = SelectedEmployeeList;
            ReturnRecord.EmployeeList = DropDownEmployee;
            return ReturnRecord;
        }

        public void InsertIntoPayrollAllowanceDetail(PayrollAllowanceDetailDTO Record)
        {
            PayrollAllowanceDetail Data = PayrollAllowanceDetailRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.PayrollAllowanceDetailRepository.Create(Data);
        }
        public string GetEmployeeName(int Id)
        {
            Employee Record = _unitOfWork.EmployeeRepository.GetById(Id);
            return Record.EmpName;
        }

        public PayrollAllowanceMasterDTO UpdatePayrollAllowance(PayrollAllowanceMasterDTO Record)
        {
            //if (Record.EditEmployeeList)
            //{
            //    List<PayrollAllowanceDetail> RemoveData = _unitOfWork.PayrollAllowanceDetailRepository.All().Where(t => t.AllowanceMasterId == Record.AllowanceMasterId).ToList(); 
            //    foreach(var list in RemoveData){
            //        _unitOfWork.PayrollAllowanceDetailRepository.Delete(list);
            //    }
            //    if (!Record.ApplyToAllEmployee)
            //    {
            //        foreach (var str in Record.DropDownEmployees)
            //        {
            //            PayrollAllowanceDetailDTO PayrollAllowanceDetailDTO = new PayrollAllowanceDetailDTO();
            //            PayrollAllowanceDetailDTO.AllowanceMasterId = Record.AllowanceMasterId;
            //            PayrollAllowanceDetailDTO.EmployeeCode = Convert.ToInt32(str);
            //            InsertIntoPayrollAllowanceDetail(PayrollAllowanceDetailDTO);
            //        }
            //    }
            //}

            PayrollAllowanceMaster Data = PayrollAllowanceMasterRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.PayrollAllowanceMasterRepository.Update(Data);
            return Record;
        }

        public void InsertIntoPayrollAllowanceDetail(string[] SelectedEmployees, int Id, string[] Vlu, string[] PercAmount)
        {
            List<PayrollAllowanceDetail> RecordList = new List<PayrollAllowanceDetail>();
            int i = 0;
            foreach (var str in SelectedEmployees)
            {
                if (SelectedEmployees[i] != "")
                {

                    int EmpCode = Convert.ToInt32(SelectedEmployees[i].Trim());
                    int EC = Convert.ToInt32(SelectedEmployees[i]);
                    string PA = PercAmount[i];
                    decimal vlue = Convert.ToDecimal(Vlu[i]);
                    try
                    {
                        PayrollAllowanceDetail Record = new PayrollAllowanceDetail()
                        {
                            AllowanceMasterId = Id,
                            EmployeeCode = Convert.ToInt32(SelectedEmployees[i]),
                            PercentageAmount = PercAmount[i],
                            Value = Vlu[i] != "" ? Convert.ToDecimal(Vlu[i]) : Convert.ToDecimal("0")
                        };
                        RecordList.Add(Record);
                    }
                    catch (Exception exception)
                    {

                    }
                }
                i++;
            }
            List<PayrollAllowanceDetail> RemoveData = _unitOfWork.PayrollAllowanceDetailRepository.All().Where(t => t.AllowanceMasterId == Id).ToList();
            int selectedOfficeId = _unitOfWork.EmployeeRepository.GetById(RecordList.FirstOrDefault().EmployeeCode).EmpOfficeId;
            int officeId = 0;
            foreach (var list in RemoveData)
            {
                officeId= _unitOfWork.EmployeeRepository.GetById(list.EmployeeCode).EmpOfficeId;
                if (officeId==selectedOfficeId)
                {
                    _unitOfWork.PayrollAllowanceDetailRepository.Delete(list);
                }
            }

            foreach (PayrollAllowanceDetail Records in RecordList)
            {
                _unitOfWork.PayrollAllowanceDetailRepository.Create(Records);
            }
        }


        public List<SelectListItem> GetAllowanceMasterList()
        {
            IEnumerable<PayrollAllowanceMaster> DomainRecord = _unitOfWork.PayrollAllowanceMasterRepository.All();
            IEnumerable<PayrollAllowanceMasterDTO> ModelRecords = PayrollAllowanceMasterResponseFormatter.GetAllTPayrollMasterDTO(DomainRecord).Where(x => x.AllowanceType != "D");
            List<SelectListItem> returnRecord = new List<SelectListItem>();
            returnRecord.Add(new SelectListItem
            {
                Text = "Select Allowances/RetirementFund",
                Value = "",
            });
            foreach (var item in ModelRecords)
            {
                string allowanceType = "";
                if (item.AllowanceType == "R")
                {

                    allowanceType = "Retirement Fund - ";
                }
                else if (item.AllowanceType == "O")
                {
                    allowanceType = "";
                }
                else
                {
                    allowanceType = "";
                }
                SelectListItem single = new SelectListItem()
                {
                    Value = item.AllowanceMasterId.ToString(),
                    Text = allowanceType + " " + item.AllowanceName.ToString(),
                };
                returnRecord.Add(single);
            }
            return returnRecord;
        }





        #region PayrollDefaultAllowances

        public IEnumerable<PayrollAllowanceMasterDTO> GetAllRetirementFunds()
        {
            PayrollAllowanceMasterDTO Record = new PayrollAllowanceMasterDTO();
            List<PayrollAllowanceMaster> Domain = _unitOfWork.PayrollAllowanceMasterRepository.Get(x => x.AllowanceType == "R").ToList();
            IEnumerable<PayrollAllowanceMasterDTO> Model = PayrollAllowanceMasterResponseFormatter.GetAllTPayrollMasterDTO(Domain);
            return Model;
        }
        #endregion






    }
}
