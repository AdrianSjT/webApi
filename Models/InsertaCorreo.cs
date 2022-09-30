using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Web_Service_Amex
{
    public class InsertaCorreo
    {
        public string Expediente { get; set; }
        public string Correo { get; set; }


        internal static dynamic Agregar(InsertaCorreo insertaCorreo)
        {
            List<Parametro> parametros = new List<Parametro> {

                new Parametro("@Expediente", insertaCorreo.Expediente),
                new Parametro("@Correo", insertaCorreo.Correo)
            };




            return DBDatos.Ejecutar("[CJ].[Sp_InsertaCorreo]", parametros);
        }


    }


   
}
