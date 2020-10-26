using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Response
{
    public class Message
    {
        public class Result
        {
            public static string Ok => "Ok";
            public static string NoData => "NoData";
            public static string Error => "Error";
            public static string Validation => "Validation";
      
        }

        public static string messageOk => "Registro Creado Correctamente";
        public static string messageDeleteOk => "Registro Eliminado Correctamente";
        public static string messageUpdateOK => "Registro Actualizado Correctamente";
        public static string messageProblem => "Ha ocurrido un problema, favor de contactar a su adminsitrador de sistemas";
        public static string messageNoFound => "Registro no encotrado en la base de datos";




    }
}
