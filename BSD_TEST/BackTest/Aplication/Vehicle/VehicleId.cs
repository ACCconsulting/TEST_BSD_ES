using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Vehicle
{
    public class VehicleId
    {
        public class VehicleById : IRequest<Domain.Entities.Vehicle>
        {
            public int Id { get; set; }
        }

        public class VehiclebyIdHaandler : IRequestHandler<VehicleById, Domain.Entities.Vehicle>
        {
            private readonly AplicationContext _context;
            public VehiclebyIdHaandler(AplicationContext context)
            {
                this._context = context;
            }
            public async Task<Domain.Entities.Vehicle> Handle(VehicleById request, CancellationToken cancellationToken)
            {
                var result =await _context.Vehicle.FindAsync(request.Id);

                return result;
            }
        }
    }
}
