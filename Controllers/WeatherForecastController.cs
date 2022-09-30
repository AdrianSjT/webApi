using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Service_Amex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<WeatherForecast> Get(String Accion,string a)
        {
            if (Accion != null) {

                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            return  null ;
        }

      /*  [HttpGet]
        public string Verificacion(string Expediente)
        {
            string resT = "";

       
                Conexiones conex = new Conexiones("192.168.7.21", "DB_WEB_AMEX", "Hermes062014/&+-", "sa");
              
                DataTable dtb = new DataTable();
                conex.abrirConexion();
                dtb = conex.ejecutaSQLR("SELECT * FROM dbo.CuentasBloqueadas WHERE Expediente = '" + Expediente.ToString().Trim() + "'");
                conex.cierreconexion();
                if (dtb.Rows.Count > 0)
                {
                    resT = "true";
                }

            
            
            return resT;
        }
      */
    }
}
