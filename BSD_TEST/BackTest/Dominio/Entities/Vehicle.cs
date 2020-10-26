using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Vehicle: EntityAudit
    {
        public int VehicleId { get; set; }
        public string NumeroPedido { get; set; }
        public string Bastidor { get; set; }
        public string Modelo { get; set; }
        public string Matricula { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
