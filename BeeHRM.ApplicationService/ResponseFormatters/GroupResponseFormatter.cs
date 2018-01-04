using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class GroupResponseFormatter
    {
        public static IEnumerable<GroupDTO> ModelData(IEnumerable<Group> modelData)
        {

            Mapper.CreateMap<Group, GroupDTO>().ConvertUsing(

                m =>
                {
                    return new GroupDTO
                    {
                        GroupId = m.GroupId,
                        GroupName = m.GroupName
                    };

                }
                );
            return Mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(modelData);
        }
    }
}
