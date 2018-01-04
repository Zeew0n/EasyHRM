using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ConnectDb;
using System.Data.SqlClient;
using System.Data;

namespace BeeHRM.ApplicationService.Implementations
{
    public class BusinessGroupService : IBusinessGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private BusinessGroup _businessGroup;

        public BusinessGroupService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _businessGroup = new BusinessGroup();
        }
        public IEnumerable<BusinessGroupDTO> GetBusinessGroupList()
        {
            IEnumerable<BusinessGroup> modelDatas = _unitOfWork.BusinessGroupRepository.All().ToList();
            return BusinessGroupResponseFormatter.ModelData(modelDatas);
        }

        public BusinessGroupDTO InsertDepartment(BusinessGroupDTO data)
        {
            BusinessGroup businessGroup = BusinessGroupRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return BusinessGroupRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.BusinessGroupRepository.Create(businessGroup));
        }

        public BusinessGroupDTO GetBusinessGroupById(int bgID)
        {
            //BusinessGroupDTO modelDatas = BusinessGroupRequestFormatter.ConvertRespondentInfoToDTO( _unitOfWork.BusinessGroupRepository.All().Where(x => x.BgId == bgID).FirstOrDefault());
            BusinessGroupDTO modelDatas = BusinessGroupRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.BusinessGroupRepository.GetById(bgID));
            return modelDatas;
        }

        public int UpdateBusinessGroup(BusinessGroupDTO data)
        {
            BusinessGroup BusinessGroupEditData = BusinessGroupRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.BusinessGroupRepository.Update(BusinessGroupEditData);
            _unitOfWork.Save();
            return response;
        }

        public IEnumerable<BusinessGroupDTO> GetBusinessGroupByEmpRoleId(int roleId)
        {
            BusinessGroupDTO bgGroup = new BusinessGroupDTO();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MyRoleBusinessGroups", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleId", roleId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new BusinessGroupDTO
                {
                    BgId = Convert.ToInt32(dr["BgId"].ToString()),
                    BgName = dr["BgName"].ToString()
                };
            }
           
        }
    }
}
