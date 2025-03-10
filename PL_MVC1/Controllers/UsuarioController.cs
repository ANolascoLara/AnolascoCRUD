using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC1.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario 
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;
            ML.Result result = BL.Usuario.GetAllEF(usuario);



            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }

            {
                return View(usuario);
            }
        }

        [HttpPost]
        
        public ActionResult GetAll (ML.Usuario usuario)
        {
            usuario.Nombre= usuario.Nombre == null ? "" :usuario.Nombre;
            usuario.ApellidoPaterno= usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno= usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;

            ML.Result result= BL.Usuario.GetAllEF(usuario);
            if (result.Correct)
            {
                usuario.Usuarios= result.Objects;
            }
            else
            {
                usuario.Usuarios = new  List<object>();
            }
                
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;



            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            
            //ML.Result resultDDL = BL.Rol.GetAll();
            

                if (IdUsuario == null)
            {
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Colonias = new List<object>();
                usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
            }

            else
            {

                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            ML.Result resultEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            //ML.Result resultEstado = BL.Estado.GetAll();
            return View(usuario);
        }

        
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["ImagenUsuario"];
            if (file != null && file.ContentLength>0) 
            {
                usuario.Imagen = ConvertirAArrayBytes(file);

            }
            


            if (usuario.IdUsuario == 0)
            {

                BL.Usuario.AddEF(usuario);
                //return View();

            }

            else
            {

                BL.Usuario.UpdateEF(usuario);
                //return View("GetAll");
            }
            return RedirectToAction("GetAll");

        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);


            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return View("GetAll");
            }

        }
        [HttpGet]

        public JsonResult CambioEstatus (int IdUsuario, bool Estatus)
        {
            ML.Result JsonResult = BL.Usuario.CambioEstatus(IdUsuario, Estatus);
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result JsonResult = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(JsonResult,JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
         
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(JsonResult,JsonRequestBehavior.AllowGet);
        }
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;

        }
    }

}


