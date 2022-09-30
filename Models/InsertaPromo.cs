using System;
using System.Collections.Generic;

namespace Web_Service_Amex
{
    public class InsertaPromo
    {
        string Nombre { get; set; }
        DateTime FechaAINI { get; set; }
        DateTime FechaAfin { get; set; }

        string BalanceI { get; set; }
        String BalanceF { get; set; }
        string placement { get; set; }
        string product { get; set; }
        string Herramienta { get; set; }
        string Porcentaje { get; set; }
        string plazos { get; set; }
        string corte { get; set; }
        string spin { get; set; }
        DateTime vigencia { get; set; }
        string NotaPop { get; set; }
        string NotaNego { get; set; }

        internal static dynamic Agregar(InsertaPromo insertaPromo)
        {
            List<Parametro> parametros = new List<Parametro> {

                new Parametro("@Nombre", insertaPromo.Nombre),
                new Parametro("@FechaAini", insertaPromo.FechaAINI),
                new Parametro("@FechaAfin", insertaPromo.FechaAfin),
                new Parametro("@BalanceI", insertaPromo.BalanceI),
                new Parametro("@BalanceF", insertaPromo.BalanceF),
                new Parametro("@placement", insertaPromo.placement),
                 new Parametro("@product", insertaPromo.product),
                  new Parametro("@Herramienta", insertaPromo.Herramienta),
                   new Parametro("@Porcentaje", insertaPromo.Porcentaje),
                    new Parametro("@plazos", insertaPromo.plazos),
                     new Parametro("@corte", insertaPromo.corte),
                      new Parametro("@spin", insertaPromo.spin),
                       new Parametro("@vigencia", insertaPromo.vigencia),
                        new Parametro("@NotaPop", insertaPromo.NotaPop),
                         new Parametro("@NotaNego", insertaPromo.NotaNego)
                   



            };




            return DBDatos.Ejecutar("[CJ].[InsertaPromo]", parametros);
        }

    }
}
