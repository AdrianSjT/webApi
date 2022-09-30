namespace Web_Service_Amex.Models
{
    public class Correo
    {
        private string RespT;
        private string CorreoElectronico;

        public Correo(string respT, string correoElectronico)
        {
            RespT1 = respT;
            CorreoElectronico1 = correoElectronico;
        }

        public string RespT1 { get => RespT; set => RespT = value; }
        public string CorreoElectronico1 { get => CorreoElectronico; set => CorreoElectronico = value; }
    }
}
