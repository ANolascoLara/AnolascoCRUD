using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class CalculadoraController : ApiController
    {

        [HttpPost]

        public IHttpActionResult Suma (int numero1,  int numero2)
        {
            int suma= numero1 + numero2;
            return Content(HttpStatusCode.OK, suma);
        }

        [HttpPost]
        public IHttpActionResult Resta (int numero1, int numero2)
        {
            int resta= numero1 - numero2;
            return Content(HttpStatusCode.OK, resta);
        }

        [HttpPost]
        public IHttpActionResult Multiplicacion (int numero1, int numero2)
        {
            int multiplicacion = numero1 * numero2;
            return Content (HttpStatusCode.OK, multiplicacion);
        }
        [HttpPost]
        public IHttpActionResult Division (int numero1, int numero2)
        {
            int division = numero1 / numero2;
            return Content(HttpStatusCode.OK, division);
        }
    }
}
