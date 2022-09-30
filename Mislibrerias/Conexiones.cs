using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Data.Common;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace Web_Service_Amex
{
    public class Conexiones
    {

        public SqlConnection conexion;
        private SqlDataAdapter adaptador;
        private SqlCommandBuilder b;
        private SqlCommand c;
        /// <summary>
        /// Constructor que define la conexion a al Servidor y DB
        /// </summary>
        /// <param name="ipServidor">Ip de Servidor al que se va a conectar (podra ir vacio y conectara a la 192.168.7.13 por default)</param>
        /// <param name="DB">Define el nombre de la DB a conctar si se envia vacio se conectara a DB RH por default</param>
        public Conexiones(string ipServidor, string DB, string contraseña, string usuario)
        {
            string cadenadeconexion = "Data Source=" + (ipServidor == "" ? "192.168.7.21" : ipServidor) + "; database=" + (DB == "" ? "DB_WEB_AMEX" : DB) + "; uid=" + (usuario == "" ? "sa" : usuario) + "; pwd=" + (contraseña == "" ? "Hermes062014/&+-" : contraseña) + ";encrypt=true;trustservercertificate=true; connection timeout = 2000000000";
            //string cadenadeconexion = "Data Source=192.168.7.13; database=cj; uid=sa; pwd=segCONSadminDB1423/; connection timeout = 0";
            conexion = new SqlConnection(cadenadeconexion);
        }


        public bool abrirConexion()
        {
            bool ret;
            try
            {
                conexion.Open();
                ret = true;
            }
            catch (Exception e)
            {
                ret = false;
            }
            return ret;
        }

        public DataTable Consulta(string campos, string nombretabla, string Expresion, int acceso)
        {
            DataTable res = new DataTable();
            SqlCommand comando = new SqlCommand("SET DATEFORMAT DMY select " + campos + " from " + nombretabla + " where " + Expresion, conexion);
            adaptador = new SqlDataAdapter(comando);
            b = new SqlCommandBuilder(adaptador);
            if (acceso == 1)
            {
                adaptador.UpdateCommand = b.GetUpdateCommand();
                adaptador.InsertCommand = b.GetInsertCommand();
            }
            comando.CommandTimeout = 0;
            adaptador.Fill(res);
            return res;
        }

        public DataTable Consulta(string campos, string nombretabla, int acceso)
        {
            DataTable res = new DataTable();
            SqlCommand comando = new SqlCommand("SET DATEFORMAT DMY  select " + campos + " from " + nombretabla, conexion);
            adaptador = new SqlDataAdapter(comando);
            b = new SqlCommandBuilder(adaptador);
            if (acceso == 1)
            {
                adaptador.UpdateCommand = b.GetUpdateCommand();
                adaptador.InsertCommand = b.GetInsertCommand();
            }
            comando.CommandTimeout = 0;
            adaptador.Fill(res);
            return res;
        }


        public void actualizasql(string nombretabla, string campos, string Expresion)
        {
            c = new SqlCommand("SET DATEFORMAT DMY  update " + nombretabla + " set " + campos + " where " + Expresion, conexion);
            c.ExecuteNonQuery();
        }

        public void actualizasql(string nombretabla, string campos)
        {
            c = new SqlCommand("SET DATEFORMAT DMY  update " + nombretabla + " set " + campos, conexion);
            c.ExecuteNonQuery();
        }


        public void actualizasql(string nombretabla, string campos, byte[] Parametro, string Expresion)
        {
            c = new SqlCommand("SET DATEFORMAT DMY  update " + nombretabla + " set " + campos + " where " + Expresion, conexion);
            c.Parameters.AddWithValue("@Archivo", Parametro);
            c.ExecuteNonQuery();
        }

        public void eliminasql(string nombretabla, string Expresion)
        {
            c = new SqlCommand("SET DATEFORMAT DMY  delete from " + nombretabla + " where " + Expresion, conexion);
            c.ExecuteNonQuery();
        }

        public void insertasql(string nombretabla, string campos, string Expresion)
        {
            c = new SqlCommand("SET DATEFORMAT DMY  insert into " + nombretabla + "(" + campos + ")" + "values(" + Expresion + ")", conexion);
            c.ExecuteNonQuery();
        }

        public void insertasql(string nombretabla, string campos, string Expresion, byte[] Parametro)
        {
            c = new SqlCommand("SET DATEFORMAT DMY  insert into " + nombretabla + "(" + campos + ")" + "values(" + Expresion + ")", conexion);
            c.Parameters.AddWithValue("@Archivo",Parametro);
            c.ExecuteNonQuery();
        }

        public DataTable execproc(string nombre, string parametro)
        {
            DataTable res = new DataTable();
            c = new SqlCommand(nombre, conexion);
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.AddWithValue("@Expediente", parametro);
            adaptador = new SqlDataAdapter(c);
            adaptador.Fill(res);
            return res;
        }

        public DataTable execproc(string nombre)
        {
            DataTable res = new DataTable();
            c = new SqlCommand(nombre, conexion);
            c.CommandType = CommandType.StoredProcedure;
            //c.Parameters.AddWithValue("");
            adaptador = new SqlDataAdapter(c);
            adaptador.Fill(res);
            return res;
        }

        public bool comparacion(string Cadena1, string Cadena2, float exactitud)
        {
            String[] partes = descomposicion(Cadena2, " ");
            float porcentaje = exactitud / 100;

            //string cadena_compara = "";
            int ele = 0, igual = 0;

            while (ele < partes.Length - 1)
            {
                igual += (Cadena1.Contains(partes[ele].Substring(0, int.Parse((Math.Ceiling((float)(partes[ele].Trim().Length - 1) * porcentaje)).ToString()))) ? 1 : 0);
                //cadena_compara += "%" + partes[ele].Substring(0, int.Parse(Math.Round(porcentaje).ToString()));
                ele++;
            }

            return (igual >= int.Parse((Math.Ceiling((float)(partes.Length - 1) * porcentaje)).ToString()) ? true : false);

            //cadena_compara += "%";
            //return SqlMethods.Like(Cadena2,cadena_compara,' ');
        }

        public void ejecutaSQL(string Sentencia)
        {
            c = new SqlCommand(Sentencia, conexion);
            c.CommandTimeout = 0;
            c.ExecuteNonQuery();
        }

        public DataTable ejecutaSQLR(string Sentencia)
        {
            DataTable res = new DataTable();
            SqlCommand comando = new SqlCommand(Sentencia, conexion);
            adaptador = new SqlDataAdapter(comando);
            b = new SqlCommandBuilder(adaptador);
            comando.CommandTimeout = 0;
             adaptador.Fill(res);
            return res;
        }

        public String[] descomposicion(String cadena, String delimitador)
        {
            int ele;
            int cont_ini, cont_fin, largo;
            String[] elementos;
            elementos = new String[conteo_c(cadena, delimitador) + 1];
            cadena += delimitador;
            cont_ini = 0;
            cont_fin = 0;
            ele = 0;
            largo = 0;
            while (largo < cadena.Trim().Length)
            {
                if (cadena.Substring(largo, 1) == delimitador)
                {
                    elementos[ele] = cadena.Substring(cont_ini, cont_fin);
                    cont_ini = largo + 1;
                    cont_fin = -1;
                    ele = ele + 1;
                }
                largo++;
                cont_fin++;
            }
            ele--;
            //elementos[ele] = "ultimo";
            return elementos;
        }

        public int conteo_c(String CadenaB, String CaracterB)
        {
            int cont = 1, conta = 0;
            CadenaB += " ";
            while (cont < CadenaB.Length)
            {
                if (CadenaB.Substring(cont, 1) == CaracterB.Substring(0, 1))
                    conta++;

                cont++;
            }
            return conta;
        }


        public void cierreconexion()
        {
            conexion.Close();
            conexion.Dispose();
            conexion = null;
        }

        public static string rigth(string cadena_origen, int cantidad)
        {
            return cadena_origen.Trim().Substring((cadena_origen.Trim().Length) - cantidad, cantidad);
        }
        public static string left(string cadena_origen, int cantidad)
        {
            return cadena_origen.Trim().Substring(0, cantidad);
        }

        public static string EnvioMail(int tipo,string mailDestino,string Exp="", string Mensaje = "",string tel="",string DatosPex="")
        {
            string res = "";
            MailMessage email = new MailMessage();
            //email.To.Add(new MailAddress(mailDestino));
            email.To.Add(mailDestino);
            email.From = new MailAddress("registro.self@conjur.mx");
            if (tipo==1)
            { 
                email.Subject = "Confirmacion de registro";
                email.Body = "Estimado :" + Exp + "</br> " +
                              "El presente correo electrónico es para confirmarle que su registro a nuestro portal de servicio personalizado ha sido exitoso.</br>" +
                              "¡Le damos la más cordial bienvenida!.</ br></ br>" +
                              "Atentamente,</Br></ br>" + 
                              "Consorcio Juridico de Cobranza Especializada";
            }
            else if (tipo==2)
            {
                email.Subject = "Solicitud de Llamada";
                email.Body =  Exp + "</br> </br>" +
                              "Deudor socilita comunicación al numero." + tel+ "</br></br></br><img src='http://200.78.224.74:96/Imagenes/firmaConjurNet.png' width=220px height=120px>";

            }
            else if(tipo==3)
            {
                /*email.Subject = "Solicitud de Pex";
                email.Body = "No. de Expediente " + Exp + "</br> " +
                              "Datos de cobro Pex.</br>" +
                              DatosPex;*/
                email.Subject = "Confirmacion de pago";
                email.Body = "No. de Expediente " + Exp + "</br>" +
                             "Se confirmar la calendarización de pago, en breve uno de nuestros asesores lo contactara para la confirmación del mismo. </br>" +
                             "</br> Gracias por su preferencia";



            }
            else if (tipo == 4)
            {
                email.Subject = "Notificación Negociación";
                email.Body = "Se ha registrado una negociación con el No. de Expediente " + Exp;

            }
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.exchangeadministrado.com";
            smtp.Port = 587;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("registro.self@conjur.mx", "Soportecj09+");
            //smtp.Credentials = new NetworkCredential("javier.lopezf@conjur.mx", "Soportecj09+");

            //  string output = null;


            try
            {
                smtp.Send(email);
                email.Dispose();
                //    output = "Corre electrónico fue enviado satisfactoriamente.";

                res = "Correo electrónico fue enviado satisfactoriamente.";

            }
            catch (Exception ex)
            {
                //  output = "Error enviando correo electrónico: " + ex.Message;

                res = "Error enviando correo electrónico: " + ex.Message;
            }
            return res;
        }

    }
}