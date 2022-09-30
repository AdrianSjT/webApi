using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web_Service_Amex
{
    public class ConsultaCorreoP
    {
        public string Expediente { get; set; }
       



        internal static string Agregar(ConsultaCorreoP consultaCorreo)
        {
            List<Parametro> parametros = new List<Parametro> {

                new Parametro("@Expediente", consultaCorreo.Expediente),
               
            };




            return DBDatos.Ejecutar("[CJ].[Sp_ConsultaCorreos]", parametros).ToString();
        }


    }



}