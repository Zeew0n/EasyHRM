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
    public class LevelService : ILevelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Level _level;

        public LevelService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _level = new Level();
        }

        public IEnumerable<LevelDTO> GetLevellist()
        {

            IEnumerable<Level> modelDatas = _unitOfWork.LevelRepository.All().ToList();
            return LevelResponseFormatter.ModelData(modelDatas);
        }

        public LevelDTO InsertLevel(LevelDTO data)
        {
            Level level = LevelRequestFormatter.ConvertRespondentInfoFromDTO(data);
            _level.LevelId = level.LevelId;
            _level.LevelName = level.LevelName;
            _level.LevelOrder = level.LevelOrder;
            return LevelRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.LevelRepository.Create(level));
        }

        public LevelDTO GetLevelById(int levelId)
        {
            var levellist = _unitOfWork.LevelRepository.All();
            LevelDTO levelDetail = (from lvl in levellist
                                                 select new LevelDTO
                                                 {
                                                     LevelId = lvl.LevelId,
                                                     LevelName = lvl.LevelName,
                                                     LevelOrder = lvl.LevelOrder
                                                 }).Where(x => x.LevelId == levelId).FirstOrDefault();
            return levelDetail;
        }

        public int UpdateLevelbyId(LevelDTO data)
        {
            Level lvl = LevelRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.LevelRepository.Update(lvl);
            _unitOfWork.Save();
            return response;
        }

      
    }
}
