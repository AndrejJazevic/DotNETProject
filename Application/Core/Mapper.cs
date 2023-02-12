using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Item, Item>().ForAllMembers(o => o.Condition((source, destination, member) => member != null)); ;
        }
    }
}
