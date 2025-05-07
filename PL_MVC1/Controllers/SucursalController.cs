using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Web.Configuration;

namespace PL_MVC1.Controllers
{
    public class SucursalController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {


            ML.ProductoSucursal productosucursal = new ML.ProductoSucursal();
            productosucursal.Sucursal = new ML.Sucursal();
            productosucursal.Producto = new ML.Producto();

            ML.Result result = BL.ProductoSucursal.GetAlllinq(productosucursal.Sucursal.IdSucursal );


            if (result.Correct)
            {
                productosucursal.ProductosSucursales = result.Objects.ToList();
            }
            else
            {
                productosucursal.ProductosSucursales = new List<object>();
            }
            ML.Result resultSucursal = BL.Sucursal.GetAll();
            productosucursal.Sucursal.Sucursals = resultSucursal.Objects;

            

            return View(productosucursal);
        }
        [HttpPost]
        public ActionResult GetAll(ML.ProductoSucursal productosucursal)
        {
            productosucursal.Sucursal.IdSucursal = Convert.ToByte(productosucursal.Sucursal.IdSucursal == 0 ? 0 : productosucursal.Sucursal.IdSucursal);

            ML.Result result = BL.ProductoSucursal.GetAlllinq(productosucursal.Sucursal.IdSucursal);

            if (result.Correct)
            {
                productosucursal.ProductosSucursales = result.Objects;
            }
            else
            {
                productosucursal.ProductosSucursales = new List<object>();
            }

            ML.Result resultDDL = BL.Sucursal.GetAll();

            if (resultDDL.Correct)
            {
                productosucursal.Sucursal.Sucursals = resultDDL.Objects;
            }
            else
            {
                productosucursal.Sucursal.Sucursals = new List<object>();
            }

            return View(productosucursal);
        }


        [HttpPost]
        public JsonResult ActualizarStock(int IdProducto, int IdSucursal, int Stock)
        {
            ML.Result result = BL.ProductoSucursal.UpdateStock(IdProducto, IdSucursal, Stock);

            if (result.Correct)
            {
                Enviar();
                return Json(new { Correct = true });
            }
            else
            {
                return Json(new { Correct= false, ErrorMessage = "No se pudo actualizar el stock" });
            }
        }

        [HttpPost]
        public JsonResult Delete(int IdProducto, int IdSucursal)
        {
            ML.Result result = BL.ProductoSucursal.Delete(IdProducto, IdSucursal);
            return Json(new
            {
                Correct = result.Correct,
                ErrorMessage = result.Correct ? "Producto eliminado correctamente de la sucursal." : "Error al eliminar el producto: " + result.ErrorMessage
            });
        }
        [NonAction]
        public ActionResult Enviar()
        {
            try

            {
                string correo = WebConfigurationManager.AppSettings["Correo"];

                string password = WebConfigurationManager.AppSettings["Password"];

                string htmlPath = Server.MapPath("~/Content/Correo/Productos.html");


                string htmlBody;

                using (StreamReader reader = new StreamReader(htmlPath))

                {

                    htmlBody = reader.ReadToEnd();
                }

                htmlBody = htmlBody.Replace("[Nombre del Usuario]", "Andrea");
                htmlBody = htmlBody.Replace("[Número de Pedido]", "1");
                htmlBody = htmlBody.Replace("[Fecha Estimada de Entrega]", "Lunes");
                htmlBody = htmlBody.Replace("[Dirección de Entrega del Usuario]", "CDMX");

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                string imagePath = Server.MapPath("~/Content/imagen/logo.jpg");

                LinkedResource logo = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);

                logo.ContentId = "IMG_LOGO";

                logo.TransferEncoding = TransferEncoding.Base64;

                avHtml.LinkedResources.Add(logo);



                MailMessage mensaje = new MailMessage

                {
                    From = new MailAddress(correo, "Jorge"),

                    Subject = "Paquete en camino",

                    IsBodyHtml = true

                };
                mensaje.To.Add("andylu.nolascol@gmail.com");

                mensaje.AlternateViews.Add(avHtml);

                
                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,

                    Credentials = new NetworkCredential(correo, password),

                    EnableSsl = true

                };

                smtp.Send(mensaje);

            }

            catch (Exception ex)

            {

                Console.WriteLine("Error al enviar correo: " + ex.Message);

            }

            return View();

        }
    }
}