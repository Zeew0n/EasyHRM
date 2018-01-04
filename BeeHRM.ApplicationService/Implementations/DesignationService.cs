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

namespace BeeHRM.ApplicationService.Implementations
{
    public class DesignationService : IDesignationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Designation _desg;

        public DesignationService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _desg = new Designation();
        }
        public IEnumerable<DesignationDTO> GetDesignationList()
        {
            IEnumerable<Designation> modelDatas = _unitOfWork.DesignationRepository.All().ToList();
            return DesignationResponseFormatter.ModelData(modelDatas);
        }
        public IEnumerable<DesignationDTO> GetDesignationListWithLevelName()
        {
            var designationList = _unitOfWork.DesignationRepository.All();
            var levellist = _unitOfWork.LevelRepository.All();
            IEnumerable<DesignationDTO> list = (from desg in designationList
                                                join lvl in levellist on desg.DesgLevelId equals lvl.LevelId
                                                select new DesignationDTO
                                                {
                                                    LeaveApprove = desg.LeaveApprove,
                                                    LeaveRecommendation=desg.LeaveRecommendation,
                                                    AttendanceApprove=desg.AttendanceApprove,
                                                    AttendanceRecommendation=desg.AttendanceRecommendation,
                                                    DesgCode = desg.DesgCode,
                                                    DesgLevelId = desg.DesgLevelId,
                                                    DesgMaxPeriodDays = desg.DesgMaxPeriodDays,
                                                    DesgOrder = desg.DesgOrder,
                                                    
                                                    DesgShortCode = desg.DesgShortCode,
                                                    DsgId = desg.DsgId,
                                                    DsgName = desg.DsgName,
                                                    DsgParentId = desg.DsgParentId,
                                                    LevelName =  lvl.LevelName 
                                                }).ToList();
            return list;
        }
        public DesignationDTO InsertDesignation(DesignationDTO data)
        {
            Designation desg = DesignationRequestFormatter.ConvertRespondentInfoFromDTO(data);
            _desg.LeaveApprove = desg.LeaveApprove;
            _desg.DesgCode = desg.DesgCode;
            _desg.DesgLevelId = desg.DesgLevelId;
            _desg.DesgMaxPeriodDays = desg.DesgMaxPeriodDays;
            _desg.DesgOrder = desg.DesgOrder;
            _desg.LeaveRecommendation = desg.LeaveRecommendation;
            _desg.AttendanceApprove = desg.AttendanceApprove;
            _desg.AttendanceRecommendation = desg.AttendanceRecommendation;
            _desg.DesgShortCode = desg.DesgShortCode;
            _desg.DsgId = desg.DsgId;
            _desg.DsgName = desg.DsgName;
            _desg.DsgParentId = desg.DsgParentId;
            return DesignationRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.DesignationRepository.Create(desg));
        }

        public DesignationDTO GetDesignationById(int desgId)
        {
            var designationList = _unitOfWork.DesignationRepository.All();
            DesignationDTO list = (from desg in designationList
                                   select new DesignationDTO {
                                      LeaveApprove = desg.LeaveApprove,
                                        DesgCode = desg.DesgCode,
                                        DesgLevelId = desg.DesgLevelId,
                                        DesgMaxPeriodDays = desg.DesgMaxPeriodDays,
                                        DesgOrder = desg.DesgOrder,
                                        LeaveRecommendation = desg.LeaveRecommendation,
                                        AttendanceApprove=desg.AttendanceApprove,
                                        AttendanceRecommendation=desg.AttendanceRecommendation,
                                        DesgShortCode = desg.DesgShortCode,
                                        DsgId = desg.DsgId,
                                        DsgName = desg.DsgName,
                                        DsgParentId = desg.DsgParentId
        }).Where(x => x.DsgId == desgId).FirstOrDefault();
          
            return list;
        }

        public int UpdateDesignation(DesignationDTO data)
        {
           
            Designation desg = DesignationRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.DesignationRepository.Update(desg);
            _unitOfWork.Save();
            return response;
        }
    }
}
