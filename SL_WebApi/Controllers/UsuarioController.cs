using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers


{
    [RoutePrefix("api")]
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllEF()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol= new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            { 
                
                return Content( HttpStatusCode.OK,result);
            }
            else
            {
            return Content(HttpStatusCode.BadGateway, result);

            }



        }
        [HttpDelete]
        [Route("Delete/{IdUsuario}")]
        public IHttpActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadGateway, result);

            }

        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadGateway, result);

            }
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadGateway, result);

            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadGateway, result);

            }
        }
    }


}

