using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}