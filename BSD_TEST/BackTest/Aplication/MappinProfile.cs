using AutoMapper;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication
{
   public class MappinProfile:Profile
    {
        public MappinProfile()
        {
            CreateMap<Domain.Entities.Vehicle, VehicleDto>();

        }
    }
}
