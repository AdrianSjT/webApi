using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Web_Service_Amex
{
    public class InsertaTelefonos
    {
        public string Expediente { get; set; }
        public string Telefono { get; set; }


        internal static dynamic Agregar(InsertaTelefonos insertaTelefonos)
        {
            List<Parametro> parametros = new List<Parametro> {

                new Parametro("@Expediente", insertaTelefonos.Expediente),
                new Parametro("@Telefono", insertaTelefonos.Telefono)
            };




            return DBDatos.Ejecutar("[CJ].[Sp_InsertaTelefono]", parametros);
        }


    }


   
}
