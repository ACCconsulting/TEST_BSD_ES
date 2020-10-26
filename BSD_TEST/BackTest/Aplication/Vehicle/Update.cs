using Domain.Response;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Vehicle
{
 public   class Update
    {
        public class VehicleUpdate : IRequest<ResponceT<Domain.Dtos.VehicleDto>>
        {
            [Required(ErrorMessage = "El Id es requerido")]
            public int VehicleId { get; set; }

            [Required(ErrorMessage = "El Numero de pedido es requerido")]
            public string NumeroPedido { get; set; }

            [Required(ErrorMessage = "El campo Bastidor es requerido")]
            public string Bastidor { get; set; }

            [Required(ErrorMessage = "El campo Modulo es requerido")]
            public string Modelo { get; set; }

            [Required(ErrorMessage = "El campo Matricula es requerido")]
            public string Matricula { get; set; }

            [Required(ErrorMessage = "El campo Fecha de entrega es requerido")]
            public DateTime FechaEntrega { get; set; }
        }


        public class VehicleUpdateHandler : IRequestHandler<VehicleUpdate, ResponceT<Domain.Dtos.VehicleDto>>
        {
            private readonly AplicationContext _context;
            public VehicleUpdateHandler(AplicationContext context)
            {
                this._context = context;
            }
            public async Task<ResponceT<Domain.Dtos.VehicleDto>> Handle(VehicleUpdate request, CancellationToken cancellationToken)
            {
                var Responce = new ResponceT<Domain.Dtos.VehicleDto>();

                try
                {
                    if(request.FechaEntrega.Date!=null  && request.FechaEntrega.Date<DateTime.Now) throw new Exception("La fecha de entrega no puede ser menor o la la fecha actual");

                    var objUpdate = await _context.Vehicle.FindAsync(request.VehicleId);
                    if (objUpdate == null) throw new Exception($"No se encontro el id {request.VehicleId} en la base de datos");

                    var exist = _context.Vehicle.Where((w => (w.NumeroPedido == request.NumeroPedido || w.Matricula == request.Matricula) && w.VehicleId != request.VehicleId)).FirstOrDefault();
                    if (exist != null)
                    {
                        if (exist.NumeroPedido == request.NumeroPedido) throw new Exception($"El numero de pedido {request.NumeroPedido} ya existe en la base de datos");
                        else if (exist.Matricula == request.Matricula) throw new Exception($"La matrícula {request.Matricula} ya existe en la base de datos");

                    }


                    objUpdate.NumeroPedido = request.NumeroPedido;
                    objUpdate.Bastidor = request.Bastidor;
                    objUpdate.Modelo = request.Modelo;
                    objUpdate.Matricula = request.Matricula;
                    objUpdate.FechaEntrega = request.FechaEntrega;


                    var result = await this._context.SaveChangesAsync();

                    if (result <= 0) throw new Exception("No se guardaron los cambios ");                       

                    Responce.ObjResult = null;
                    Responce.Result = Domain.Response.Message.Result.Ok;
                    Responce.Message = Domain.Response.Message.messageUpdateOK;

                }
                catch (Exception ex)
                {

                    Responce.ObjResult = null;
                    Responce.Result = Domain.Response.Message.Result.Error;
                    Responce.Message = "Error:  " + ex.Message;
                }
                return Responce;
            }
        }
    
  
    }
}
