using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Implementations
{
    public class RolesAccessService : IRolesAccessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesAccessService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteRoleAccess(short v)
        {
           _unitOfWork.RolesAccessRepository.Delete(v);
            _unitOfWork.Save();
        }

        public IEnumerable<RolesAccessDTO> GetAllRolesAccess()
        {
            IEnumerable<RolesAccess> modelDatas = _unitOfWork.RolesAccessRepository.All().ToList();
            return RoleAccessResponseFormatter.ModelData(modelDatas);
        }


        //public dynamic GetNotInModules(int roleId)
        //{
        //    var ModulesList = _unitOfWork.ModuleRepository.All();
        //   var parentModulesList = _unitOfWork.ParentModuleRepository.All();
        //    List<Module> data = new List<Module>();
        //    var parentModuleId = (from ml in parentModulesList select ml.ParentModulesId);
        //    var query = (from ml in ModulesList where parentModuleId.Contains(ml.ModuleParentId) select ml);
        //    return query;
        //}

        //public dynamic GetParentModules(int parentModuleId, int roleId)
        //{
        //    var ModulesList = _unitOfWork.ModuleRepository.All();
        //    var parentModulesList = _unitOfWork.ParentModuleRepository.All();
        //    List<Module> data = new List<Module>();
        //    var roleAccessData = _unitOfWork.RolesAccessRepository.Get(x => x.AssignRoleId == roleId);
        //    var roleDataAssignModel = (from ml in roleAccessData select ml.AssignModuleId);
        //    var query = (from ml in ModulesList where (ml.ModuleParentId == parentModuleId) && !roleDataAssignModel.Contains(ml.ModuleId) select ml);
        //    return query;
        //}

        //public dynamic GetParentModulesList()
        //{
        //    var parentModulesList = _unitOfWork.ParentModuleRepository.All();
        //    List<ParentModule> data = new List<ParentModule>();
        //    foreach (var item in parentModulesList)
        //    {
        //        data.Add(item);
        //    }
        //    return data;
        //} 
        public IEnumerable<RolesAccessDTO> GetRoleByRoleID(int roleId)
        {
            IEnumerable<RolesAccess> roleAccessData   = _unitOfWork.RolesAccessRepository.All().Where(x => x.AssignRoleId == roleId).ToList();

            return RoleAccessResponseFormatter.ModelData(roleAccessData);
        }


        public RolesAccessDTO GetRoleByRoleIdModuleId(string roleid, string moduleid,string assignid)
        {
            int roleId = Convert.ToInt16(roleid);
            int moduleId = Convert.ToInt16(moduleid);
            int assignId = Convert.ToInt16(assignid);
            RolesAccess values = _unitOfWork.RolesAccessRepository.All().Where(x => x.AssignRoleId == roleId && x.AssignModuleId==moduleId && x.AssignId==assignId).First();
            return RolesAccessRequestFormatter.ConvertRespondentInfoToDTO(values);
        }

        public RolesAccessDTO InserRoleAccess(RolesAccessDTO data)
        {
            RolesAccess rolesToInsert = new RolesAccess();
            rolesToInsert = RolesAccessRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var responseData = _unitOfWork.RolesAccessRepository.Create(rolesToInsert);
            return RolesAccessRequestFormatter.ConvertRespondent(responseData);

        }

        public int UpdateRolesAccess(RolesAccessDTO data)
        {
            RolesAccess rolesToUpdate = new RolesAccess();
            rolesToUpdate = RolesAccessRequestFormatter.ConvertRespondentInfoFromDTO(data);
            int res= _unitOfWork.RolesAccessRepository.Update(rolesToUpdate);
            _unitOfWork.Save();
            return res;
        }
        public List<TreeModules> GetMenuTree(int Id)
        {

            IEnumerable<RolesAccess> PreviouslySelectedList = _unitOfWork.RolesAccessRepository.All().Where(x => x.AssignRoleId == Id).ToList();
            List<TreeModules> Record = new List<TreeModules>();
            IEnumerable<Module> ParentModule = _unitOfWork.ModuleRepository.All().Where(x => x.ModuleParentId == 0 && x.IsDefault==false).OrderBy(x => x.Order).ToList();
            int Level = 1;
            foreach (Module Module in ParentModule)
            {
                IEnumerable<RolesAccess> SetModels = PreviouslySelectedList.Where(x => x.AssignModuleId == Module.ModuleId);
                TreeModules Childs = new TreeModules();
                if (SetModels != null)
                {
                   foreach(RolesAccess RolesAccess in SetModels)
                   {
                       Childs.Add = RolesAccess.AllowAdd;
                       Childs.Edit = RolesAccess.AllowEdit;
                       Childs.Delete = RolesAccess.AllowDelete;
                       Childs.Views = RolesAccess.AllowView;
                   }
                }
                Childs.Id = Module.ModuleId;
                Childs.MOduleName = Module.MOduleName;
                Childs.ModuleParentId = Module.ModuleParentId;
                Childs.Level = Level;
                Childs.ModuleCssClass = Module.ModuleCssClass;
                Childs.RoleId = Id;
                Record.Add(Childs);
                List<TreeModules> ChildMenu = GetAllChildMenus(Module.ModuleId, Level,Id);
                foreach (TreeModules ModuleDTOs in ChildMenu)
                {
                    Record.Add(ModuleDTOs);
                }
            }
            return Record;
        }
        public List<TreeModules> GetAllChildMenus(int ParentId, int Level,int Id)
        {
            IEnumerable<RolesAccess> PreviouslySelectedList = _unitOfWork.RolesAccessRepository.All().Where(x => x.AssignRoleId == Id).ToList();
            Level = Level + 1;
            List<TreeModules> Record = new List<TreeModules>();
            IEnumerable<Module> ParentModule = _unitOfWork.ModuleRepository.All().Where(x => x.ModuleParentId == ParentId).OrderBy(x => x.Order).ToList();
            foreach (Module Module in ParentModule)
            {
                IEnumerable<RolesAccess> SetModels = PreviouslySelectedList.Where(x => x.AssignModuleId == Module.ModuleId);
                TreeModules Childs = new TreeModules();
                if (SetModels != null)
                {
                    foreach (RolesAccess RolesAccess in SetModels)
                    {
                        Childs.Add = RolesAccess.AllowAdd;
                        Childs.Edit = RolesAccess.AllowEdit;
                        Childs.Delete = RolesAccess.AllowDelete;
                        Childs.Views = RolesAccess.AllowView;
                    }
                }
                Childs.Id = Module.ModuleId;
                Childs.MOduleName = Module.MOduleName;
                Childs.ModuleParentId = Module.ModuleParentId;
                Childs.Level = Level;
                Childs.ModuleCssClass = Module.ModuleCssClass;
                Childs.RoleId = Id;
                Record.Add(Childs);
                List<TreeModules> ChildMenu = GetAllChildMenus(Module.ModuleId, Level,Id);
                foreach (TreeModules ModuleDTOs in ChildMenu)
                {
                    Record.Add(ModuleDTOs);
                }
            }
            return Record;
        }
        public void InsertIntoRolesAccess(List<TreeModules> Insert, int RoleId){

            _unitOfWork.RolesAccessRepository.Delete(x => x.AssignRoleId == RoleId);
            _unitOfWork.Save();
            foreach (TreeModules Data in Insert)
            {
                if (Data.Views == true || Data.Edit == true || Data.Add == true || Data.Delete == true)
                {
                    RolesAccess Record = new RolesAccess();
                    Record.AssignModuleId = Data.Id;
                    Record.AllowView = Data.Views;
                    Record.AllowEdit = Data.Edit;
                    Record.AllowAdd = Data.Add;
                    Record.AllowDelete = Data.Delete;
                    Record.AssignRoleId = Data.RoleId;
                    _unitOfWork.RolesAccessRepository.Create(Record);
                }
            }    
        }

    }
}
