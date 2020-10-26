using AutoMapper;
using Domain.Entities;
using Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Vehicle
{
    public class GetAllVehicle
    {
        public class Vehiclelist : IRequest<ResponceT<List<Domain.Dtos.VehicleDto>>>
        {

        }

        public class GetAllVehiclesHandler : IRequestHandler<Vehiclelist, ResponceT<List<Domain.Dtos.VehicleDto>>>
        {
            private readonly AplicationContext _context;
            private readonly IMapper _mapper;
            public GetAllVehiclesHandler(AplicationContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<ResponceT<List<Domain.Dtos.VehicleDto>>> Handle(Vehiclelist request, CancellationToken cancellationToken)
            {
                var Responce = new ResponceT<List<Domain.Dtos.VehicleDto>>();

                try
                {
                    
                    var list = await _context.Vehicle.ToListAsync();

                    var listDto = _mapper.Map<List<Domain.Entities.Vehicle>, List<Domain.Dtos.VehicleDto>>(list);
                    Responce.TotalRecords = list.Count;
                    Responce.ObjResult = listDto;
                    Responce.Result = Domain.Response.Message.Result.Ok;
                }
                catch (Exception ex)
                {
                    Responce.TotalRecords = 0;
                    Responce.ObjResult = null;
                    Responce.Result = Domain.Response.Message.Result.Error;
                    Responce.Message = "Ocurrio un error   " + ex.Message;
                    
                }
             
                return Responce;
            }
        }
    }
}
