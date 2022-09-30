namespace Web_Service_Amex
{
    public class Respuesta
    {
        public string status { get; set; }
        public bool exito { get; set; }
        public string message { get; set; }
        public dynamic result { get; set; }

        public Respuesta()
        {
            status = "success";
        }
    }
}
