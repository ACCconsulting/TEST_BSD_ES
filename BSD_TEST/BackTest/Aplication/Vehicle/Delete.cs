using Domain.Response;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Vehicle
{
    public class Delete
    {
        public class VehicleDelete : IRequest<ResponceT<Domain.Dtos.VehicleDto>>
        {
            public int Id { get; set; }
        }

        public class VehicleDeleteHandler : IRequestHandler<VehicleDelete, ResponceT<Domain.Dtos.VehicleDto>>
        {
            private readonly AplicationContext _context;
            public VehicleDeleteHandler(AplicationContext context)
            {
                this._context = context;
            }
            public async Task<ResponceT<Domain.Dtos.VehicleDto>> Handle(VehicleDelete request, CancellationToken cancellationToken)
            {
                var Responce = new ResponceT<Domain.Dtos.VehicleDto>();
                try
                {
                     

                    var objDelete = await _context.Vehicle.FindAsync(request.Id);
                    if (objDelete == null) throw new Exception(Domain.Response.Message.messageNoFound);

                    this._context.Remove(objDelete);
                    var result = this._context.SaveChanges();
                    if (result < 0) 
                    throw new Exception(Domain.Response.Message.messageProblem);

                    Responce.ObjResult = null;
                    Responce.Result = Domain.Response.Message.Result.Ok;
                    Responce.Message = Domain.Response.Message.messageDeleteOk;
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
