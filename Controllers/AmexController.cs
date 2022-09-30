using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using Web_Service_Amex.Models;

namespace Web_Service_Amex.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class AmexController : ControllerBase
    {
        
       // [HttpGet]
       // [Route("api/Login/{Expediente}")]
        public string Login(string ExpedienteT ,string fecha2)
        {

          //  string fecha = "01-01-1996";
            string result = "null";
            string dd = fecha2.Substring(1, 2);
            string mm = fecha2.Substring(4, 5);
            string anio = fecha2.Substring(4, 5);
            string fechafinal = anio + "-" + mm + "-" + dd;
           
            //dtb.Columns.Add("FechaNacimiento", typeof(string));

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            DataTable dtb = new DataTable();
            DataTable dtb2 = new DataTable();
            DataRow workRow;

            

            // dtb.Columns.Add("Expediente", typeof(string));
            dtb = conex.ejecutaSQLR("exec [CJ].[Sp_AccesoFolio] '" + ExpedienteT.Trim() + "','" + fecha2.Trim().Replace("-", "").Substring(6, 2) + fecha2.Trim().Replace("-", "").Substring(4, 2) + fecha2.Trim().Replace("-", "").Substring(0, 4) + "',null");
            // dtb2.Rows.Add(0, dtb.Rows[0]["Expediente"]);
            // dtb2.Rows.Add(2, "Login");
            //dtb2.Rows.Add(3, dtb.Rows[0]["FechaNacimiento"]);


            DataTable workTable = new DataTable("Rest");
            DataColumn Expediente2 = new DataColumn("Expediente");
            DataColumn Respuesta = new DataColumn("Respuesta");
            workTable.Columns.Add(Expediente2);
            workTable.Columns.Add(Respuesta);
            DataRow row1 = workTable.NewRow();
            conex.abrirConexion();
            ///conex.ejecutaSQLR("Select * from [CJ].[tbl_Cuentas] WHERE Expediente ='" + Expediente.ToString().Trim() + "' and FechaNacimiento ='" + fechafinal + "'");
            if (dtb.Rows.Count ==1)
            {


                conex.ejecutaSQL("Exec cj.insertaLog '" + ExpedienteT.ToString() + "','Acceso a sistema'");
                row1["Expediente"] = dtb.Rows[0]["Expediente"];
                row1["Respuesta"] = "true";

                workTable.Rows.Add(row1);

              

                return JsonConvert.SerializeObject(workTable);
            }
            //  DataRow row1 = workTable.NewRow();
            row1["Expediente"] = ExpedienteT;
            row1["Respuesta"] = "false";
            workTable.Rows.Add(row1);
            conex.cierreconexion();
            return  JsonConvert.SerializeObject(workTable);





        }



        [Route("Amex2")]
        [HttpPost]
        public dynamic Listar2()
        {

            return "hola desde post2";
        }

        [Route("AgregarTEL")]
        [HttpPost]
        public dynamic Crear(InsertaTelefonos insertaTelefonos)
        {



            return InsertaTelefonos.Agregar(insertaTelefonos);

        }

        [Route("AgregarCorreo")]
        [HttpPost]
        public dynamic Crear(InsertaCorreo insertaCorreo)
        {



            return InsertaCorreo.Agregar(insertaCorreo);

        }

        [Route("InsertaPromo")]
        [HttpPost]
        public dynamic Crear(InsertaPromo insertaPromo)
        {



            return InsertaPromo.Agregar(insertaPromo);

        }


        [Route("ConsultaCorreo")]
        [HttpGet]
        public dynamic ConsultaCorreoP(String Expediente)
        {
            // return JsonConvert.SerializeObject(ConsultaCorreoP.Agregar(consultaCorreo));


            string resT = "false";

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            conex.abrirConexion();
            DataTable dtb = new DataTable();
            DataTable dtb2 = new DataTable();
            //dtb = conex.ejecutaSQLR("SELECT Expediente FROM dbo.CuentasBloqueadas WHERE Expediente = '" + Expediente + "'");
            dtb = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaCorreos '" + Expediente.ToString() + "'");
          //  int aux = 0;
            // dtb2 = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaCuenta @Expediente = '" + Expediente + "'");


          //  DataTable workTable = new DataTable("Rest");
          //  DataColumn CorreoElectronico = new DataColumn("CorreoElectronico");
           // DataColumn Respuesta = new DataColumn("Respuesta");
          //  workTable.Columns.Add(CorreoElectronico);
           // workTable.Columns.Add(Respuesta);

          //  DataRow row = workTable.NewRow();


           // if (dtb.Rows.Count > 0)
           // {
               



              /*  while (aux <= dtb.Rows.Count)
                {
                  /*  DataRow row = dt2.NewRow();
                    row["No. Pago"] = aux;
                    row["Monto"] = "$" + string.Format("{0:0,0.00}", float.Parse(dt.Rows[pago]["Monto"].ToString()));
                    row["Fecha"] = dt.Rows[pago]["FechaPago"].ToString().Substring(0, 10);
                    row["CantidadNego"] = dt3.Rows.Count;

                    aux = aux + 1;*/

                  //  pago++;

                  //  dt2.Rows.Add(row);
                 //   dt2.AcceptChanges();
                //  resT = "true";

               // row["CorreoElectronico"] = dtb.Rows[0]["CorreoElectronico"];
               // row["Respuesta"] = "true";

              //  workTable.Rows.Add(row);

              
               // }*/

            return JsonConvert.SerializeObject(dtb).Trim();

            //  }
            // DataRow row1 = workTable.NewRow();



            //  row["CorreoElectronico"] = CorreoElectronico;
            //  row["Respuesta"] = "False";
            //  row1["Respuesta"] = dtb.Rows[0][resT];

            //workTable.Rows.Add(row);
            //  conex.cierreconexion();
            //  return JsonConvert.SerializeObject(workTable);
        }

        [Route("AddTel")]
        [HttpPost]
        public string Add(string Expediente, string Telefono) {

           
            string result = "null";
            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            DataTable dtb = new DataTable();
            DataTable dtb2 = new DataTable();
            DataRow workRow;

            try
            {
                dtb = conex.ejecutaSQLR("exec CJ.Sp_InsertaTelefono '" + Expediente.ToString().Trim() + "', '" + Telefono.ToString().Trim() + "'");


                DataTable workTable = new DataTable("Rest");
                DataColumn Expediente2 = new DataColumn("Expediente");
                DataColumn Respuesta = new DataColumn("Respuesta");
                workTable.Columns.Add(Expediente2);
                workTable.Columns.Add(Respuesta);
                DataRow row1 = workTable.NewRow();
                conex.abrirConexion();
                ///conex.ejecutaSQLR("Select * from [CJ].[tbl_Cuentas] WHERE Expediente ='" + Expediente.ToString().Trim() + "' and FechaNacimiento ='" + fechafinal + "'");


                //  DataRow row1 = workTable.NewRow();
                row1["Expediente"] = Expediente;
                row1["Respuesta"] = "true";
                workTable.Rows.Add(row1);
                conex.cierreconexion();
                return JsonConvert.SerializeObject(workTable);
            }


            catch (Exception ex) {
                DataTable workTable = new DataTable("Rest");
                DataColumn Expediente2 = new DataColumn("Expediente");
                DataColumn Respuesta = new DataColumn("Respuesta");
                workTable.Columns.Add(Expediente2);
                workTable.Columns.Add(Respuesta);
                DataRow row1 = workTable.NewRow();
            
                ///conex.ejecutaSQLR("Select * from [CJ].[tbl_Cuentas] WHERE Expediente ='" + Expediente.ToString().Trim() + "' and FechaNacimiento ='" + fechafinal + "'");


                //  DataRow row1 = workTable.NewRow();
                row1["Expediente"] = Expediente;
                row1["Respuesta"] = "false";
                workTable.Rows.Add(row1);
             
                return JsonConvert.SerializeObject(workTable);
            }


            // dtb.Columns.Add("Expediente", typeof(string));
          //  dtb = conex.ejecutaSQLR("exec CJ.Sp_InsertaTelefono '" + Expediente.ToString().Trim() + "', '" + Telefono.ToString().Trim() + "'");
            

           


        }



        [HttpGet]

        public string Get(string Expediente,string accion,string fecha,String Estado) {
            string resT= "";
            /*
            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");

            DataTable dtb = new DataTable();
            dtb = conex.ejecutaSQLR("SELECT * FROM dbo.CuentasBloqueadas WHERE Expediente = '" + Expediente.ToString().Trim() + "'");

            if (dtb.Rows.Count > 0) { 
            resT = "true";
            }          

            
            return resT;
            */


            
            switch (accion)
            {
                case "Login":
                    resT = Login(Expediente,fecha).ToString();
                    break;

                case "Verificacion":
                    resT= Verificacion(Expediente);
                    break;

                case "ConsultaCuenta":

                    resT = ConsultaCuenta(Expediente);
                    break;

                case "validapaso":
                    resT = Nopasa(Expediente).ToString();
                    break;

                case "CuentaNeg_AMEX":

                    resT = CuentaNeg_AMEX(Expediente);
                    break;
                case "ConsultainforTH":

                    resT = ConsultainforTH(Expediente);
                    break;

                case "ConsultaTelefonos":

                    resT = ConsultaTelefonos(Expediente);
                    break;

                case "Negociaciones":
                    resT = Negociaciones(Expediente);
                    break;

                case "CantidadPagos":
                    resT = CantidadPagos(Expediente);
                    break;

                case "ConsultaDomicilio":
                    resT = ConsultaDomicilio(Expediente);
                    break;

                case "ConsultaMunicipios":
                    resT = ConsultaMunicipios(Estado);
                    break;
                case "ConsultaCorreo":
                    resT = ConsultaCorreo(Expediente);
                    break;








            }
            return resT;

            

           


            



           
        }







        private string ConsultaDomicilio(string expediente)
        {
            string resT = "false";

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            conex.abrirConexion();
            DataTable dtb = new DataTable();
            DataTable dtb2 = new DataTable();
            dtb = conex.ejecutaSQLR("SELECT Expediente FROM dbo.CuentasBloqueadas WHERE Expediente = '" + expediente + "'");


            dtb2 = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaDomicilio @Expediente = '" + expediente + "'");

/*
            DataTable workTable = new DataTable("Rest");
            DataColumn ExpedienteE = new DataColumn("Expediente");
            DataColumn Respuesta = new DataColumn("Respuesta");
           workTable.Columns.Add(ExpedienteE);
           workTable.Columns.Add(Respuesta);

            DataRow row = workTable.NewRow();
*/

            //http://192.168.4.43:85/Amex?Expediente=AMX1049579&accion=ConsultaDomicilio
            if (dtb2.Rows.Count > 0)
            {
                resT = "true";

              // row["Expediente"] = dtb.Rows[0]["Expediente"];
               //row["Respuesta"] = "true";

              // workTable.Rows.Add(row);

                return JsonConvert.SerializeObject(dtb2);


            }
            // DataRow row1 = workTable.NewRow();


            /*
            row["Expediente"] = expediente;
           row["Respuesta"] = "False";
           //  row1["Respuesta"] = dtb.Rows[0][resT];

            workTable.Rows.Add(row);
            */
            conex.cierreconexion();
            return JsonConvert.SerializeObject(dtb2);

        }

        private string Verificacion(string Expediente) {

            string resT = "false";

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            conex.abrirConexion();
            DataTable dtb = new DataTable();
            DataTable dtb2 = new DataTable();
            dtb = conex.ejecutaSQLR("SELECT Expediente FROM dbo.CuentasBloqueadas WHERE Expediente = '" + Expediente+"'");


            dtb2 = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaCuenta @Expediente = '" + Expediente + "'");


            DataTable workTable = new DataTable("Rest");
            DataColumn ExpedienteE = new DataColumn("Expediente");
            DataColumn Respuesta = new DataColumn("Respuesta");
          
            workTable.Columns.Add(ExpedienteE);
            workTable.Columns.Add(Respuesta);
           

            DataRow row = workTable.NewRow();


    if (dtb.Rows.Count > 0)
            {
                resT = "true";
              
                row["Expediente"] = dtb.Rows[0]["Expediente"];
                row["Respuesta"] = "true";

                workTable.Rows.Add(row);

                return JsonConvert.SerializeObject(workTable);


            }
           // DataRow row1 = workTable.NewRow();

          

            row["Expediente"] = Expediente;
            row["Respuesta"] = "False";
            //  row1["Respuesta"] = dtb.Rows[0][resT];

            workTable.Rows.Add(row);
            conex.cierreconexion();
            return JsonConvert.SerializeObject(workTable);

        }
        private string Negociaciones(string Expediente)
        {
            Conexiones con = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            con.abrirConexion();
            int CanTNegociaciones;
            
            DataTable dt = new DataTable();
            DataTable dt3 = new DataTable();




            dt = con.ejecutaSQLR("SELECT * FROM CJ.tbl_NumPagos WHERE Expediente = '" + Expediente.ToString() + "' AND BandNego = '0' ORDER BY FechaPago ASC");
            dt3 = con.ejecutaSQLR("SELECT * FROM CJ.tbl_Negociaciones WHERE Expediente = '" + Expediente.ToString() + "'");
            // NumNegociacion = dt3.Rows.Count;

            /* if (dt3.Rows.Count == 1)
             {
                 CanTNegociaciones = dt3.Rows.Count;
              //   Label1.Text = "Usted tiene actualmente una negociación vigente, ¿desea cancelarla y hacer una nueva? </br></br> Esta opción solo estará disponible una ocasión.</br></br>Negociación actual:";
              //  Button1.Visible = true;
             }

             else if (dt3.Rows.Count > 1)
             {
                 CanTNegociaciones = dt3.Rows.Count;
                 // Label1.Text = "Usted ya ha negociado dos veces por este canal. Para negociar nuevamente comuniquese al: (55) 47 74 04 10</br>";
                 //  Button1.Visible = false;
             }
            */
            CanTNegociaciones = dt3.Rows.Count;

            DataTable dt2 = new DataTable();
            int aux = 1;
            int pago = 0;
            DataColumn num = dt2.Columns.Add("No. Pago", typeof(Int32));
            DataColumn monto = dt2.Columns.Add("Monto", typeof(string));
            DataColumn fec = dt2.Columns.Add("Fecha", typeof(string));
            
            // DataColumn CantidadNego = new DataColumn("CantidadNego");


            while (aux <= dt.Rows.Count)
            {
                DataRow row = dt2.NewRow();
                row["No. Pago"] = aux;
                row["Monto"] = "$" + string.Format("{0:0,0.00}", float.Parse(dt.Rows[pago]["Monto"].ToString()));
                row["Fecha"] = dt.Rows[pago]["FechaPago"].ToString().Substring(0, 10);
                row["CantidadNego"] = dt3.Rows.Count;

                aux = aux + 1;
                pago++;

                dt2.Rows.Add(row);
                dt2.AcceptChanges();
            }

         
            return JsonConvert.SerializeObject(dt3);
        }

        private string CantidadPagos(string Expediente)
        {
            Conexiones con = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            con.abrirConexion();
            DataTable dt = new DataTable();
            DataTable dt3 = new DataTable();

            dt = con.ejecutaSQLR("SELECT * FROM CJ.tbl_NumPagos WHERE Expediente = '" + Expediente.ToString() + "' AND BandNego = '0' ORDER BY FechaPago ASC");
            //dt3 = con.ejecutaSQLR("SELECT * FROM CJ.tbl_Negociaciones WHERE Expediente = '" + Expediente.ToString() + "'");
            // NumNegociacion = dt3.Rows.Count;

           /// if (dt3.Rows.Count == 1)
           // {
                //   Label1.Text = "Usted tiene actualmente una negociación vigente, ¿desea cancelarla y hacer una nueva? </br></br> Esta opción solo estará disponible una ocasión.</br></br>Negociación actual:";
                //  Button1.Visible = true;
          //  }

          //  else if (dt3.Rows.Count == 2)
          //  {
                // Label1.Text = "Usted ya ha negociado dos veces por este canal. Para negociar nuevamente comuniquese al: (55) 47 74 04 10</br>";
                //  Button1.Visible = false;
          //  }


            DataTable dt2 = new DataTable();
            int aux = 1;
            int pago = 0;
            DataColumn num = dt2.Columns.Add("No. Pago", typeof(Int32));
            DataColumn monto = dt2.Columns.Add("Monto", typeof(string));
            DataColumn fec = dt2.Columns.Add("Fecha", typeof(string));
            while (aux <= dt.Rows.Count)
            {
                DataRow row = dt2.NewRow();
                row["No. Pago"] = aux;
                row["Monto"] = "$" + string.Format("{0:0,0.00}", float.Parse(dt.Rows[pago]["Monto"].ToString()));
                row["Fecha"] = dt.Rows[pago]["FechaPago"].ToString().Substring(0, 10);


                aux = aux + 1;
                pago++;

                dt2.Rows.Add(row);
                dt2.AcceptChanges();
            }


            return JsonConvert.SerializeObject(dt2);
        }
        private string ConsultaCuenta(string Expediente) {

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");

            DataTable dtc = new DataTable();
            conex.abrirConexion();
            dtc = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaCuenta @Expediente = '" + Expediente.ToString() + "'");

           



            conex.cierreconexion();
            return JsonConvert.SerializeObject(dtc);


        }


        public void GuardaCambios()
        {
            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");

            conex.abrirConexion();

           // conex.ejecutaSQLR("INSERT INTO dbo.Domicilios_Actualizados (Expediente,Calle, Exterior, Interior, CP, Col_Loc, Del_Mun, Estado, FechaInsert) VALUES('" + Session["expediente"].ToString() + "','" + txtCalle2.Text + "','" + txtExterior2.Text + "','" + txtInterior2.Text + "','" + txtCP2.Text + "','" + txtCol2.Text + "','" + txtDel2.Text + "','" + txtEstado2.Text + "', GETDATE())");

            conex.cierreconexion();


        }

        private String Nopasa(string Expediente) {
            bool respT= true;
          

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            DataTable dt2 = new DataTable();
            conex.abrirConexion();
            dt2 = conex.ejecutaSQLR("SELECT * FROM [dbo].[CuentasNoPasan] WHERE Cuenta = '" + Expediente.Trim() + "'");
            /*if (dt2.Rows.Count <= 0)
            {
             respT = false;
            }
            */
            DataTable workTable = new DataTable("Rest");
            DataColumn Cuenta2 = new DataColumn("Cuenta");
            DataColumn Recovery2 = new DataColumn("Recovery");
            workTable.Columns.Add(Cuenta2);
            workTable.Columns.Add(Recovery2);

            DataRow row = workTable.NewRow();


            if (dt2.Rows.Count > 0)
            {
                //  resT = "true";
                /*
                  row["Cuenta"] = dt2.Rows[0]["Cuenta"];
                  row["Recovery"] = dt2.Rows[0]["Recovery"];
                */
                row["Cuenta"] = Expediente;
                row["Recovery"] = false;
                workTable.Rows.Add(row);

                return JsonConvert.SerializeObject(workTable);

            }
            else {
                row["Cuenta"] = Expediente;
                row["Recovery"] = true;

                //  row1["Respuesta"] = dtb.Rows[0][resT];

                workTable.Rows.Add(row);
                conex.cierreconexion();
            }
            // DataRow row1 = workTable.NewRow();
            /*
            row["Cuenta"] = "NoPAsa";
            row["Recovery"] = "NoPAsa";
            */
            
              
            return JsonConvert.SerializeObject(workTable);



          

        }

        /*
        private bool Nopasa(string Expediente)
        {
            bool respT = false;


            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            DataTable dt2 = new DataTable();
            conex.abrirConexion();
            dt2 = conex.ejecutaSQLR("SELECT * FROM [dbo].[CuentasNoPasan] WHERE Cuenta = '" + Expediente.Trim() + "'");
            if (dt2.Rows.Count == 0)
            {
                respT = true;
            }
            return respT;

        }

        */

        private string CuentaNeg_AMEX(string Expediente) {


            Conexiones conex = new Conexiones("192.168.16.11", "dbcollection", "Hermes062014/&+-*", "sa");

            DataTable dt3 = new DataTable();
            conex.abrirConexion();
            dt3 = conex.ejecutaSQLR("exec dbcollection.[dbo].[CuentaNeg_AMEX] ''" + Expediente.ToString().Trim() + "''')");


            conex.cierreconexion();

            return JsonConvert.SerializeObject(dt3);

        }

        private string ConsultainforTH(string Expediente)
        {

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            DataTable dt3 = new DataTable();
            conex.abrirConexion();
                       
           
            dt3 = conex.ejecutaSQLR("exec [CJ].[Sp_ConsultaInfoTH] '" + Expediente.ToString().Trim() + "'");

            conex.ejecutaSQL("Exec cj.insertaLog '" + Expediente.ToString() + "','Acceso a sistema'");

            conex.cierreconexion();
            return JsonConvert.SerializeObject(dt3);

        }

        private string ConsultaTelefonos(string Expediente)
        {

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            DataTable dt3 = new DataTable();
            conex.abrirConexion();


            dt3 = conex.ejecutaSQLR("exec CJ.Sp_ConsultaTelefonos '" + Expediente.Trim().ToString() + "'");

          //  conex.ejecutaSQL("Exec cj.insertaLog '" + Expediente.ToString() + "','Acceso a sistema'");

            conex.cierreconexion();
            return JsonConvert.SerializeObject(dt3);

        }


        private string ConsultaMunicipios(string Estado)
        {
            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_NISSAN", "Hermes062014/&+-", "sa");
            DataTable dt3 = new DataTable();
            conex.abrirConexion();
            try
            {
                string mun = "";
                mun = Estado.ToString();
                dt3 = conex.ejecutaSQLR("select Municipio from Municip where Estado= '" + Estado + "' order by Municipio asc");
                return JsonConvert.SerializeObject(dt3);

            }
            catch (Exception ex)
            {
                string error = "";
                error = ex.ToString();

            }
            conex.cierreconexion();

            return JsonConvert.SerializeObject(dt3);
        }

        private string ConsultaCorreo(string Expediente)
        {

            string resT = "false";

            Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
            conex.abrirConexion();
            DataTable dtb = new DataTable();
            DataTable dtb2 = new DataTable();
            //dtb = conex.ejecutaSQLR("SELECT Expediente FROM dbo.CuentasBloqueadas WHERE Expediente = '" + Expediente + "'");
            dtb = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaCorreos '" + Expediente.ToString() + "'");

            // dtb2 = conex.ejecutaSQLR("EXEC CJ.Sp_ConsultaCuenta @Expediente = '" + Expediente + "'");


            DataTable workTable = new DataTable("Rest");
            DataColumn CorreoElectronico = new DataColumn("CorreoElectronico");
           DataColumn Respuesta = new DataColumn("Respuesta");
            workTable.Columns.Add(CorreoElectronico);
           workTable.Columns.Add(Respuesta);

            DataRow row = workTable.NewRow();


            if (dtb.Rows.Count > 0)
            {
                resT = "true";

                row["CorreoElectronico"] = dtb.Rows[0]["CorreoElectronico"];
                row["Respuesta"] = "true";

                workTable.Rows.Add(row);

                return JsonConvert.SerializeObject(workTable);


            }
            // DataRow row1 = workTable.NewRow();



            row["CorreoElectronico"] = CorreoElectronico;
            row["Respuesta"] = "False";
            //  row1["Respuesta"] = dtb.Rows[0][resT];

            workTable.Rows.Add(row);
            conex.cierreconexion();
            return JsonConvert.SerializeObject(workTable);

        }





    }



}
