using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollOvertimeService : IPayrollOvertimeSerivce
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayrollOvertimeService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeletePayrollOvertime(int id)
        {
            _unitOfWork.SkillRepository.Delete(id);
            _unitOfWork.Save();
        }

        public List<PayrollOvertimeDTO> GetAllPayrollOvertime()
        {
            return PayrollOvertimeResponseFormatter.ModelData(_unitOfWork.PayrollOvertimeRepository.All().ToList());
        }

        public PayrollOvertimeDTO GetPayrollOvertimeById(int id)
        {
            return PayrollOvertimeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.PayrollOvertimeRepository.GetById(id));
        }

        public PayrollOvertimeDTO InsertPayrollOvertime(PayrollOvertimeDTO data)
        {
            PayrollOvertime dataToInsert = new PayrollOvertime();
            dataToInsert = PayrollOvertimeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return PayrollOvertimeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.PayrollOvertimeRepository.Create(dataToInsert));
        }

        public int UpdatePayrollOvertime(PayrollOvertimeDTO data)
        {
            PayrollOvertime dataToUpdate = PayrollOvertimeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.PayrollOvertimeRepository.Update(dataToUpdate);
            return res;
        }
        public IEnumerable<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Employee> EmployeeList = _unitOfWork.EmployeeRepository.Get().ToList();
            foreach (Employee item in EmployeeList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.EmpName.ToString();
                selectData.Value = item.EmpCode.ToString();
                Record.Add(selectData);
            }
            return Record;

        }

        public IEnumerable<SelectListItem> GetApproverList(int empCode)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_MyApprover_Result]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            List<SelectListItem> Records = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem single = new SelectListItem()
                {
                    Value = dr["EmpCode"].ToString(),
                    Text = dr["EmpName"].ToString()
                };
                Records.Add(single);
            }
            return Records;
        }
    }
}
