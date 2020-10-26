using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
   public class VehicleDto
    {
        public int key { get { return this.VehicleId; } }
        public int VehicleId { get; set; }

        public string NumeroPedido { get; set; }
        public string Bastidor { get; set; }
        public string Modelo { get; set; }
        public string Matricula { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string FechaFormat { get { return this.FechaEntrega.Date.ToString("dddd, dd MMMM yyyy"); }}
    }
}
