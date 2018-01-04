using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BeeHRM.ApplicationService.Implementations
{
    public class OfficeServices : IOfficeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private Office _office;

        public OfficeServices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _office = new Office();
        }

        public IEnumerable<OfficeDTOs> GetOfficeData()
        {
            int EmpCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);

            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MyRoleOfficesListIds", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            List<Office> OfficeList = new List<Office>();
            foreach (DataRow dr in dt.Rows)
            {
                int OfficeId = Convert.ToInt32(dr["ChildOfficeId"]);
                Office Office = _unitOfWork.OfficeRepository.GetById(OfficeId);

                OfficeList.Add(Office);
            }

            return OfficeResponseFormatter.ModelData(OfficeList);

        }
        public IEnumerable<OfficeDTOs> getBranchOfficeData(string id)
        {
            var officeId = Convert.ToInt32(id);
            var modelDatas = _unitOfWork.OfficeRepository.All().ToList();
            modelDatas = modelDatas.Where(m => m.OfficeParentId == officeId).ToList();

            return OfficeResponseFormatter.ModelData(modelDatas);
        }
        public IEnumerable<OfficeDTOs> GetOfficeAllData()
        {
            var officeList = _unitOfWork.OfficeRepository.All();
            var officeTypeList = _unitOfWork.OfficeTypeRepository.All();

            IEnumerable<OfficeDTOs> list = (from office in officeList
                                            select new OfficeDTOs
                                            {
                                                OfficeAddress = office.OfficeAddress,
                                                OfficeCode = office.OfficeCode,
                                                OfficeGeoLocation = office.OfficeGeoLocation,
                                                OfficeTypeId = office.OfficeTypeId,
                                                OfficeId = office.OfficeId,
                                                OfficeName = office.OfficeName,
                                                OfficeParentId = office.OfficeParentId,
                                                OfficePhone = office.OfficePhone,
                                                OfficeStatus = office.OfficeStatus,
                                                //OfficeTypeName = officeType.OfficeTypeName
                                            }).ToList();
            return list;
        }

        public OfficeDTOs InsertOffice(OfficeDTOs data)
        {
            Office off = OfficeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            Office Offices = _unitOfWork.OfficeRepository.GetById(data.OfficeId);
            return OfficeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.OfficeRepository.Create(off));
        }

        public OfficeDTOs GetOfficeById(int officeId)
        {
            return OfficeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.OfficeRepository.GetById(officeId));

        }
        public string GetOfficeName(int officeId)
        {
            if (officeId == 0)
            {
                return "Head Office";
            }
            OfficeDTOs office = new OfficeDTOs();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECt OfficeName FROM Offices WHERE OfficeId = '" + officeId + "'", conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@officeId", officeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dt.Rows[0]["officeName"].ToString();
        }
        public int UpdateDarbandi(OfficeDTOs data)
        {
            Office off = OfficeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.OfficeRepository.Update(off);
            _unitOfWork.Save();
            return response;
        }
        public List<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Employee> EmployeeList = _unitOfWork.EmployeeRepository.Get(x => x.EmpStatus == true).ToList();
            foreach (Employee item in EmployeeList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.EmpName;
                selectData.Value = item.EmpCode.ToString();
                Record.Add(selectData);
            }
            return Record;

        }
        public List<SelectListItem> GetRemoteList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<PayrollRemoteSetup> RemoteList = _unitOfWork.PayrollRemoteSetupRepository.Get().ToList();
            foreach (PayrollRemoteSetup item in RemoteList)
            {
                SelectListItem selectRemote = new SelectListItem();
                selectRemote.Text = item.RemoteName;
                selectRemote.Value = item.RemoteId.ToString();
                Record.Add(selectRemote);
            }
            return Record;
        }
        public IEnumerable<OfficeDTOs> GetOfficeListByEmpRole(int roleId)
        {
            int EmpCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            OfficeDTOs office = new OfficeDTOs();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MyRoleOfficesList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new OfficeDTOs
                {
                    OfficeId = Convert.ToInt32(dr["ChildOfficeId"].ToString()),
                    OfficeName = dr["ChildOfficeName"].ToString()
                };
            }
        }

        public IEnumerable<OfficeDTOs> GetClildOfficeListByEmpCode(int Empcode)
        {
            OfficeDTOs office = new OfficeDTOs();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MyRoleOfficesList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", Empcode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new OfficeDTOs
                {
                    OfficeId = Convert.ToInt32(dr["ChildOfficeId"].ToString()),
                    OfficeName = dr["ChildOfficeName"].ToString()
                };
            }
        }
        public IEnumerable<OfficeDTOs> GetActiveOfficeList()
        {
            IEnumerable<Office> Record = _unitOfWork.OfficeRepository.Get(x => x.OfficeStatus == true).ToList();
            IEnumerable<OfficeDTOs> ReturnRecord = OfficeResponseFormatter.ModelData(Record);
            return ReturnRecord;
        }
        public List<int> MyAccessOfficeList()
        {

            int EmpCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MyRoleOfficesListIds", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            List<int> OfficeList = new List<int>();
            foreach (DataRow dr in dt.Rows)
            {
                int Officeid = Convert.ToInt32(dr["ChildOfficeId"]);
                OfficeList.Add(Officeid);
            }
            return OfficeList;
        }
    }

}
