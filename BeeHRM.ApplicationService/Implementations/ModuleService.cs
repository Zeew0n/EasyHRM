using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Implementations;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace BeeHRM.ApplicationService.Implementations
{
    public class ModuleService : IModulServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<ModuleDTOs> GetAllModules()
        {
            IEnumerable<Module> modelDatas = _unitOfWork.ModuleRepository.All().ToList();
            return ModuleResponseFormatter.ModelData(modelDatas);
        }


        public IEnumerable<ModuleDTOs> GetModuleParents(int? id)
        {
            IEnumerable<Module> modelDatas = _unitOfWork.ModuleRepository.Get(x => x.ModuleId == id).OrderBy(x => x.Order).ToList();
            return ModuleResponseFormatter.ModelData(modelDatas);
            //select * from Modules mo,RolesAccess ro where mo.ModuleId = ro.AssignModuleId AND mo.ModuleParentId =0 and ro.AssignRoleId = id
        }
        public IEnumerable<ModuleDTOs> GetDefaultMenu(int id)
        {
            IEnumerable<Module> modelDatas = _unitOfWork.ModuleRepository.Get(x => x.ModuleParentId == id).OrderBy(x => x.Order).ToList();
            return ModuleResponseFormatter.ModelData(modelDatas);
            //select * from Modules mo,RolesAccess ro where mo.ModuleId = ro.AssignModuleId AND mo.ModuleParentId =0 and ro.AssignRoleId = id
        }

        public List<ModuleDTOs> GetTopLevelModules(int roleId)
        {
            List<ModuleDTOs> ModuleDtos = new List<ModuleDTOs>();

            List<RolesAccess> test = _unitOfWork.RolesAccessRepository.All().Where(x => x.AssignRoleId == roleId && x.Module.ModuleParentId == 0).OrderBy(x => x.Module.Order).ToList();
            foreach (RolesAccess RolesAccess in test)
            {
                List<Module> modelDatas = _unitOfWork.ModuleRepository.All().Where(x => x.ModuleId == RolesAccess.AssignModuleId).OrderBy(x => x.Order).ToList();
                foreach (Module Module in modelDatas)
                {
                    ModuleDTOs AddToModule = new ModuleDTOs();
                    AddToModule.ModuleId = Module.ModuleId;
                    AddToModule.MOduleName = Module.MOduleName;
                    AddToModule.ModuleParentId = Module.ModuleParentId;
                    AddToModule.Order = Module.Order;
                    AddToModule.MduleLink = Module.MduleLink;
                    AddToModule.ModuleCssClass = Module.ModuleCssClass;
                    AddToModule.Controller = Module.Controller;
                    ModuleDtos.Add(AddToModule);
                }
            }
            List<Module> tr = _unitOfWork.ModuleRepository.All().Where(x => x.IsDefault == true && x.ModuleParentId == 0).OrderBy(x => x.Order).ToList();
            foreach (Module Second in tr)
            {
                ModuleDTOs AddToModule = new ModuleDTOs();
                AddToModule.ModuleId = Second.ModuleId;
                AddToModule.MOduleName = Second.MOduleName;
                AddToModule.ModuleParentId = Second.ModuleParentId;
                AddToModule.Order = Second.Order;
                AddToModule.MduleLink = Second.MduleLink;
                AddToModule.ModuleCssClass = Second.ModuleCssClass;
                AddToModule.Controller = Second.Controller;
                ModuleDtos.Add(AddToModule);
            }
            return ModuleDtos;
        }

        public IEnumerable<ModuleDTOs> GetModuleIdByModuleName(string ControllerName)
        {
            IEnumerable<Module> ModuleList = _unitOfWork.ModuleRepository.All();
            List<Module> modelDatas = new List<Module>();
            foreach (Module Module in ModuleList)
            {

                if (Module.Controller != null)
                {
                    if (Module.Controller.Replace("\r\n", string.Empty).ToLower() == ControllerName.ToLower())
                    {
                        Module.Controller = Module.Controller.Replace("\r\n", string.Empty);
                        modelDatas.Add(Module);
                    }
                }
            }

            // IEnumerable<Module> modelDatas = _unitOfWork.ModuleRepository.Get(x=>x.Controller== ControllerName).ToList();
            return ModuleResponseFormatter.ModelData(modelDatas);

        }


        //public List<ModuleDTOsForparent>  GetDefaultParentMenu()
        //{
        //    List<ParentModule> modelDatas = new List<ParentModule>();
        //    ModuleDTOsForparent modelDtos = new ModuleDTOsForparent();
        //    List<ModuleDTOsForparent> modelDtosList = new List<ModuleDTOsForparent>();
        //    modelDatas = _unitOfWork.ParentModuleRepository.Get(x => x.IsDefault == true).ToList();
        //    foreach (var item2 in modelDatas)
        //    {
        //        modelDtos.MduleLink = item2.ParentModuleLink;
        //        modelDtos.MOduleName = item2.parentModuleName;
        //        modelDtos.Order = item2.ParentModuleOrder;
        //        modelDtos.ModuleCssClass = item2.ParentModulesCssClass;
        //        modelDtos.ModuleParentId = item2.ParentModulesId;
        //    };
        //    var da = ModuleResponseFormatter.ConvertModuleDatatoParentModel(modelDtos);
        //    modelDtosList.Add(da);
        //    modelDtosList = modelDtosList.GroupBy(x => x.ModuleParentId).Select(x => x.First()).ToList();
        //    return modelDtosList;
        //}


        public int GetParentId(string ControllerName)
        {
            ControllerName = ControllerName.ToLower();
            int ReturnValue = 0;
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetMainParentId", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@controllerName", ControllerName);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                ReturnValue = Convert.ToInt32(row["ModuleId"].ToString());

            }
            return ReturnValue;
        }


        public IEnumerable<ModuleDTOs> GetDefaultMenu()
        {
            Module Module = _unitOfWork.ModuleRepository.All().Where(x => x.IsDefault == true).OrderBy(x => x.Order).ToList().FirstOrDefault();
            List<Module> ReturnModule = new List<Module>();
            if (Module != null)
            {
                ReturnModule = _unitOfWork.ModuleRepository.All().Where(x => x.ModuleParentId == Module.ModuleId).OrderBy(x => x.Order).ToList();
            }
            return ModuleResponseFormatter.ModelData(ReturnModule);
        }

        public Module GetModuleByController(int parentid)
        {

            Module ModuleList = _unitOfWork.ModuleRepository.Get().Where(x => x.ModuleId == parentid).FirstOrDefault();


            return ModuleList;
        }

        public IEnumerable<ModuleDTOs> GetChildMenuList(int parentid, int roleid)
        {
            // throw new NotImplementedException();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetChildMenuList", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ParentId", parentid);
            cmd.Parameters.AddWithValue("@RoleId", roleid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                yield return new ModuleDTOs
                {
                    MOduleName = row["MOduleName"].ToString(),
                    ModuleId = Convert.ToInt32(row["ModuleId"].ToString()),
                    ModuleCssClass = row["ModuleCssClass"].ToString(),
                    MduleLink = row["MduleLink"].ToString(),
                    Controller = row["Controller"].ToString(),
                };

            }
        }

        public IEnumerable<EmployeeDetailsMenuViewModel> AdminEmployeeDetailsMenu(int EmpCode)
        {
            List<EmployeeDetailsMenuViewModel> Menu = new List<EmployeeDetailsMenuViewModel>();
            Employee Record = _unitOfWork.EmployeeRepository.Get(x => x.EmpCode == EmpCode).FirstOrDefault();

            string emp_image_link;

            if (Record.EmpPhoto == null || Record.EmpPhoto == "")
            {
                emp_image_link = "/Uploads/profile.jpg";
            }
            else
            {
                emp_image_link = "/Uploads/" + EmpCode.ToString() + "/" + Record.EmpPhoto;
            }
            var emp_image = "<p align='center'><img src='" + emp_image_link + "' class='emp_image' />" + "<br />[" + EmpCode + "] " + " " + Record.EmpName + "</p>";

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "#",
                MOduleName = emp_image
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "admin/userDetail/" + EmpCode.ToString()
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/admin/userDetail/" + EmpCode,
                ModuleCssClass = "fa fa-user",
                MOduleName = "EmpDetails"
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/AttendanceDetails/Index/" + EmpCode,
                ModuleCssClass = "fa fa-calendar-o",
                MOduleName = "Attendance"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/UpdateAttendance/index/" + EmpCode,
                ModuleCssClass = "fa fa-book",
                MOduleName = "Attendance Update"
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/transfer/" + EmpCode,
                ModuleCssClass = "fa fa-space-shuttle",
                MOduleName = " सरुवा "
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/kajmakhataune/" + EmpCode,
                ModuleCssClass = "fa fa-bicycle",
                MOduleName = " काज "
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/badhuwa/" + EmpCode,
                ModuleCssClass = "fa fa-arrow-circle-up",
                MOduleName = " बढुवा "
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/punishment/" + EmpCode,
                ModuleCssClass = "fa fa-asterisk",
                MOduleName = "कारबाही र सजाय"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/abakas/" + EmpCode,
                ModuleCssClass = "fa fa-road",
                MOduleName = "अवकास"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/history/" + EmpCode,
                ModuleCssClass = "fa fa-backward",
                MOduleName = "History"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/LeaveAdmin/LeaveApplistList?EmpCode=" + EmpCode,
                ModuleCssClass = "fa fa-plane",
                MOduleName = "Apply Leave"
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/education/" + EmpCode,
                ModuleCssClass = "fa fa-book",
                MOduleName = "Education"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/admin/assignroles/" + EmpCode,
                ModuleCssClass = "fa fa-hand-rock-o",
                MOduleName = "Employee Roles"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/LeaveAdmin/LeaveBalance/" + EmpCode,
                ModuleCssClass = "fa fa-university",
                MOduleName = "Leave Balance"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/LeaveAdmin/LeaveAssign/" + EmpCode,
                ModuleCssClass = "fa fa-car",
                MOduleName = "Leave Assign"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/prize/" + EmpCode,
                ModuleCssClass = "fa fa-gift",
                MOduleName = "Prize"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/training/index/" + EmpCode,
                ModuleCssClass = "fa fa-leanpub",
                MOduleName = "Training"
            });
            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/admin/resetpassword/" + EmpCode,
                ModuleCssClass = "fa fa-key",
                MOduleName = " Reset Password"
            });


            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/address/index/" + EmpCode,
                ModuleCssClass = "fa fa-user",
                MOduleName = "Address"
            });


            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/Family/Index/" + EmpCode,
                ModuleCssClass = "fa fa-user",
                MOduleName = "Families"
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/Skill/Index/" + EmpCode,
                ModuleCssClass = "fa fa-user",
                MOduleName = "Skills"
            });

            Menu.Add(new EmployeeDetailsMenuViewModel
            {
                MduleLink = "/Document/Index/" + EmpCode,
                ModuleCssClass = "fa fa-file-text",
                MOduleName = "Documents"
            });



            return Menu;
        }
    }
}