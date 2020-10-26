using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Response
{
   public class ResponceT<T>
    {
        public T ObjResult { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public double TotalRecords{ get; set; }
    }
}
