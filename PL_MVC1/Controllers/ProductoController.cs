using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC1.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {


            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();

            ML.Result result = BL.Producto.GetAllEF(producto);
          

            if (result.Correct)
            {
                producto.Productos = result.Objects.ToList();
            }
            else
            {
                producto.Productos= new List<object>();
            }
            ML.Result resultCategoria = BL.Categoria.CategoriaGetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

            ML.Result resultSubCategoria = BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = resultSubCategoria.Objects;

            return View(producto);
        }
        [HttpGet]
        public ActionResult GetAllCards()
        {


            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();

            ML.Result result = BL.Producto.GetAllEF(producto);


            if (result.Correct)
            {
                producto.Productos = result.Objects.ToList();
            }
            else
            {
                producto.Productos = new List<object>();
            }
            ML.Result resultCategoria = BL.Categoria.CategoriaGetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

            //producto.SubCategoria.IdSubCategoria = 1;
            ML.Result resultSubCategoria = BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = resultSubCategoria.Objects;

            return View(producto);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {
            
            producto.SubCategoria.IdSubCategoria = producto.SubCategoria.IdSubCategoria == 0 ? 0 : producto.SubCategoria.IdSubCategoria; 
            ML.Result result = BL.Producto.GetAllEF(producto);
            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                producto.Productos = new List<object>();
            }
            ML.Result resultCategoria = BL.Categoria.CategoriaGetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

            ML.Result resultSubCategoria = BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = resultSubCategoria.Objects;
            return View(producto);
        }
        [HttpPost]
        public ActionResult GetAllCards(ML.Producto producto)
        {
            producto.SubCategoria.IdSubCategoria = producto.SubCategoria.IdSubCategoria == 0 ? 0 : producto.SubCategoria.IdSubCategoria;
            ML.Result result = BL.Producto.GetAllEF(producto);
            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                producto.Productos = new List<object>();
            }
            ML.Result resultCategoria = BL.Categoria.CategoriaGetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

            //producto.SubCategoria.IdSubCategoria = 1;
            ML.Result resultSubCategoria = BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = resultSubCategoria.Objects;
            return View(producto);
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {

            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.DeleteEF(IdProducto);
            if (result.Correct)
            {
                result.Correct = true;

                return RedirectToAction("GetAll");
            }
            else
            {
                result.Correct = false;


            }
            return View("GetAll");
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
           
            if (IdProducto == null)
            {
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Categoria = new ML.Categoria();

            }

            else
            {

                ML.Result result = BL.Producto.GetByIdEF(IdProducto.Value);
                producto = (ML.Producto)result.Object;
            }
            ML.Result resultCategoria = BL.Categoria.CategoriaGetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

            ML.Result resultSubCategoria = BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = resultSubCategoria.Objects;
            return View(producto);
        }


        [HttpPost]
        public ActionResult Form(ML.Producto producto)

        {
            if (ModelState.IsValid)
            {
                    HttpPostedFileBase file = Request.Files["ImagenProducto"];
                if (file != null && file.ContentLength > 0)
                {
                    producto.Imagen = ConvertirAArrayBytes(file);

                }

                if (producto.IdProducto == 0)

                {

                    ML.Result result = BL.Producto.AddEF(producto);
                    {

                        if (result.Correct)
                        {
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;

                        }

                    }
                    return RedirectToAction("GetAll");

                }
                else
                {
                    ML.Result result = BL.Producto.UpdateEF(producto);
                    {
                        if (result.Correct)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;

                        }


                        return RedirectToAction("GetAll");
                    }


                }
            }
            else
            {


                ML.Result resultCategoria = BL.Categoria.CategoriaGetAll();
                producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

                ML.Result resultSubCategoria = BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria);
                producto.SubCategoria.SubCategorias = resultSubCategoria.Objects;

                //if (producto.SubCategoria.Categoria.IdCategoria > 0)
                //{
                //    producto.SubCategoria.Categoria.Categorias= BL.SubCategoria.SubCategoriaGetById(producto.SubCategoria.Categoria.IdCategoria).Objects;
                //}

                return View(producto);
            }

        }

        [HttpGet]
        public JsonResult cargarSubCategorias(int idCategoria)
        {
            ML.Result JsonResult = BL.SubCategoria.SubCategoriaGetById(idCategoria);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;

        }
    }
}