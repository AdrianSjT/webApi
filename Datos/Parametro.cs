namespace Web_Service_Amex
{
    public class Parametro
    {
        public string Nombre { get; set; }
        public object Valor { get; set; }
        public bool Salida { get; set; }

        public Parametro(string nombre, object valor)
        {
            Nombre = nombre;
            Valor = valor;
            Salida = false;
        }

        public Parametro(string nombre)
        {
            Nombre = nombre;
            Salida = true;
        }
    }
}