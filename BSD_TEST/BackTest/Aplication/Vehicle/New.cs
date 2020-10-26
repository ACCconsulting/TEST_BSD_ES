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
    public class New
    {
        public class VehicleNew : IRequest<ResponceT<Domain.Dtos.VehicleDto>>
        {
          
            [Required(ErrorMessage ="El Numero de pedido es requerido")]
            public string NumeroPedido { get; set; }

            [Required(ErrorMessage = "El campo Bastidor es requerido")]
            public string Bastidor { get; set; }

            [Required(ErrorMessage = "El campo Modulo es requerido")]
            public string Modelo { get; set; }

            [Required(ErrorMessage = "El campo Matricula es requerido")]
            public string Matricula { get; set; }

            [Required(ErrorMessage = "La fecha de entrega es requerida")]
           
            public DateTime FechaEntrega { get; set; }
        }

        public class VehicleNewHandler : IRequestHandler<VehicleNew, ResponceT<Domain.Dtos.VehicleDto>>
        {
            private readonly AplicationContext _context;
            public VehicleNewHandler(AplicationContext context)
            {
                this._context = context;
            }
            public async Task<ResponceT<Domain.Dtos.VehicleDto>> Handle(VehicleNew request, CancellationToken cancellationToken)
            {
                var Responce = new ResponceT<Domain.Dtos.VehicleDto>();
                try
                {
                    //Generara una segunda excepcion para las validaciones 
                    if(request.FechaEntrega.Date!=null  && request.FechaEntrega.Date<DateTime.Now) throw new Exception("La fecha de entrega no puede ser menor a la fecha actual");
                    bool exist = false;
                    exist = _context.Vehicle.Any(w => w.NumeroPedido == request.NumeroPedido);
                    if(exist) { throw new Exception($"El Numero de pedido {request.NumeroPedido} ya existe en la base de datos"); }
                    exist = _context.Vehicle.Any(w => w.Matricula == request.Matricula);
                    if (exist) { throw new Exception($"La matricula {request.Matricula} ya existe en la base de datos"); }

                    var objcreate = new Domain.Entities.Vehicle
                    {
                        NumeroPedido = request.NumeroPedido,
                        Bastidor = request.Bastidor,
                        Modelo = request.Modelo,
                        Matricula = request.Matricula,
                        FechaEntrega = request.FechaEntrega
                    };
                    _context.Vehicle.Add(objcreate);

                    var result = await _context.SaveChangesAsync();
                    if (result <= 0)
                    {
                        throw new Exception("No se pudo crear el registro");
                    }

                    Responce.ObjResult = null;
                    Responce.Result = Domain.Response.Message.Result.Ok;
                    Responce.Message = Domain.Response.Message.messageOk;
                }
                catch (Exception ex)
                {

                    Responce.ObjResult = null;
                    Responce.Result = Domain.Response.Message.Result.Error;
                    Responce.Message = ex.Message;
                }

                return Responce;


            }
        }
    }
}
