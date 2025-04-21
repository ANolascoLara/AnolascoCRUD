using BL;
using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Net.WebRequestMethods;

namespace PL_MVC1.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario 
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result= GetAllRest();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result resultRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultRol.Objects.ToList();

            if(result.Objects.Count > 0)
            {
                usuario.Usuarios = result.Objects.ToList();
            }
            //ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(IdMunicipio);
            //UsuarioReference.UsuarioClient objeto = new UsuarioReference.UsuarioClient();

            //var resultDDL = objeto.GetAll(usuario);
            //string xml = GetAllXML();
            //if (!string.IsNullOrEmpty(xml))
            //{
            //    var usuarioxml = GetAllSOAPXML( xml);
            //    usuario.Usuarios=usuarioxml.Usuarios;
            //}
            
            //ML.Result result = BL.Usuario.GetAllEF(usuario);

            return View(usuario);


            //if (resultDDL.Correct)
            //{
            //    usuario.Usuarios = resultDDL.Objects.ToList();
            //}
            //else
            //{
            //    usuario.Usuarios = new List<object>();
            //}

            //{
            //}
        }

        [HttpPost]

        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;



            return View(usuario);
        }

        [NonAction]
        private string GetAllXML()
        {
            string action = "http://tempuri.org/IUsuario/GetAll";
            string url = "http://localhost:9012/CRUDWebService/Usuario.svc";


            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
           <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:GetAll>
        
         <tem:usuario>
            
            <ml:ApellidoMaterno></ml:ApellidoMaterno>
           
            <ml:ApellidoPaterno></ml:ApellidoPaterno>
           
            <ml:Nombre></ml:Nombre>
            
            <ml:Rol>
              
               <ml:IdRol>0</ml:IdRol>
             
            </ml:Rol>
           
         </tem:usuario>
      </tem:GetAll>
   </soapenv:Body>
</soapenv:Envelope>";
            // Enviar la solicitud
            // Obtener la respuesta
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        return result; // Devuelve el XML como string
                    }
                }

            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
                return null; // Devuelve nada
            }

        }
        [NonAction]
        private ML.Usuario GetAllSOAPXML(string xml)
        {
            var usuario1 = new ML.Usuario();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            var xdoc = XDocument.Parse(xml);
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");


            foreach (var elem in objects)
            {
                var usuario = new ML.Usuario();

                // Extraer ApellidoMaterno
                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);

                // Extraer ApellidoPaterno
                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);

                // Extraer Celular
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);

                // Extraer Curp
                usuario.Curp = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Curp")?.Value ?? string.Empty);

                //Extraer Direccion
                usuario.Direccion = new ML.Direccion();
                var direccionElement = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                usuario.Direccion.Calle = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInterior = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);


                usuario.Direccion.Colonia = new ML.Colonia();
                var coloniaElement = direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                usuario.Direccion.Colonia.CodigoPostal = (string)(coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}CodigoPostal")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Nombre = (string)(coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                var municipioElement = coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                usuario.Direccion.Colonia.Municipio.Nombre = (string)(municipioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                var estadoElement = municipioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");
                usuario.Direccion.Colonia.Municipio.Estado.Nombre = (string)(estadoElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);


                // Extraer Email
                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);

                // Extraer Estatus
                bool Estatus;
                bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out Estatus);
                usuario.Estatus = Estatus;


                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                // Extraer IdUsuario (Ahora después de FechaNacimiento)
                int IdUsuario;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out IdUsuario);
                usuario.IdUsuario = IdUsuario;

                // Extraer Nombre
                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                // Extraer Password
                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);

                //Extraer Rol(IdRol, Nombre)
                usuario.Rol = new ML.Rol();
                var rolElement = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                usuario.Rol.Nombre = (string)(rolElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);



                // Extraer Sexo
                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);

                // Extraer Telefono
                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);

                // Extraer UserName
                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);

                // Agregar el objeto usuario a la lista
                result.Objects.Add(usuario);

            }
            usuario1.Usuarios = result.Objects;
            return usuario1;
        }


        [NonAction]
        public string EscapeXml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        [HttpPost]
        public ML.Result FormXML(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string url = "http://localhost:9012/CRUDWebService/Usuario.svc";// Cambia a la URL del servicio
            string action;
            string soapEnvelope;

            // Verificar si IdUsuario es null o 0 (o algún valor que determines como "nuevo")
            if (usuario.IdUsuario == 0)
            {
                // Crear el sobre SOAP para agregar un nuevo usuario
                action = "http://tempuri.org/IUsuario/Add";

                soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Add>
         <!--Optional:-->
         <tem:usuario>
            <!--Optional:-->
            <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
            <!--Optional:-->
            <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
            <!--Optional:-->
            <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
            <!--Optional:-->
            <ml:Curp>{EscapeXml(usuario.Curp)}</ml:Curp>
            <!--Optional:-->
            <ml:Direccion>
               <!--Optional:-->
               <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
               <!--Optional:-->
               <ml:Colonia>
                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
               </ml:Colonia>
               <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
               <!--Optional:-->
               <ml:NumeroInterior>{EscapeXml(usuario.Direccion.NumeroInterior)}</ml:NumeroInterior>
            </ml:Direccion>
            <!--Optional:-->
            <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
            <!--Optional:-->
         
            <!--Optional:-->
            <ml:FechaNacimiento>{EscapeXml(usuario.FechaNacimiento)}</ml:FechaNacimiento>
            <ml:Nombre>Andy</ml:Nombre>
            <!--Optional:-->
            <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
            <!--Optional:-->
            <ml:Rol>
               <!--Optional:-->
               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
            </ml:Rol>
            <!--Optional:-->
            <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>
            <!--Optional:-->
            <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
            <!--Optional:-->
            <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
            <!--Optional:-->
         </tem:usuario>
      </tem:Add>
   </soapenv:Body>
</soapenv:Envelope>";
            }
            else
            {
                // Crear el sobre SOAP para actualizar un usuario existente
                action = "http://tempuri.org/IUsuario/Update";
                soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
       <soapenv:Header/>
       <soapenv:Body>
           <tem:Update>
             <tem:usuario>
               <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
                <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
                <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
                <ml:Curp>{EscapeXml(usuario.Curp)}</ml:Curp>
                <ml:Direccion>
                   <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
                   <ml:Colonia>
                      <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                   </ml:Colonia>
                   <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
                   <ml:NumeroInterior>{EscapeXml(usuario.Direccion.NumeroInterior)}</ml:NumeroInterior>
                   <ml:Usuario/>
                </ml:Direccion>
                <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
                <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                <ml:Nombre>{EscapeXml(usuario.Nombre)}</ml:Nombre>
                <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
                <ml:Rol>
                   <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                </ml:Rol>
                <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>

                <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
                <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
                <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
             </tem:usuario>
           </tem:Update>
       </soapenv:Body>
    </soapenv:Envelope>";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action); // Aquí ya existe la variable action
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }


            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string xml = reader.ReadToEnd();// se convierte a string
                        var xdoc = XDocument.Parse(xml);
                        // Acceder a GetUsuarioByIdResult usando el namespace correcto
                        var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                            e.Name.LocalName == "Correct" &&
                            e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
                        result.Correct = bool.Parse(usuarioElement.Value);
                        if (result.Correct)
                        {
                            result.Correct = true;
                        }
                        // Aquí puedes manejar la respuesta según sea necesario
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return result; // Redirigir a la lista de usuarios después de agregar o actualizar
        }

        public ML.Usuario GetByIdXML(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            //Acceder a GetUsuarioByIdResult usando el namespace correcto 
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "Object" && e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
            if (usuarioElement != null)
            {
                var usuario = new ML.Usuario()

                {
                    IdUsuario = int.TryParse(usuarioElement.Element(XName.Get("IdUsuario", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idUsuario) ? idUsuario : 0,
                    Nombre = usuarioElement.Element(XName.Get("Nombre", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    ApellidoMaterno = usuarioElement.Element(XName.Get("ApellidoMaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    ApellidoPaterno = usuarioElement.Element(XName.Get("ApellidoPaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Celular = usuarioElement.Element(XName.Get("Celular", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Telefono = usuarioElement.Element(XName.Get("Telefono", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Curp = usuarioElement.Element(XName.Get("Curp", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Email = usuarioElement.Element(XName.Get("Email", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    FechaNacimiento = usuarioElement.Element(XName.Get("FechaNacimiento", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Password = usuarioElement.Element(XName.Get("Password", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Estatus = bool.TryParse(usuarioElement.Element(XName.Get("Estatus", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out bool estatus) && estatus,
                    Sexo = usuarioElement.Element(XName.Get("Sexo", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    UserName = usuarioElement.Element(XName.Get("UserName", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Rol = new ML.Rol
                    {
                        IdRol = int.TryParse(usuarioElement.Element(XName.Get("Rol", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdRol", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idRol) ? idRol : 0
                    },
                    Direccion = new ML.Direccion
                    {
                        IdDireccion = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdDireccíon", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idDireccion) ? idDireccion : 0,
                        Calle = usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Calle", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        NumeroExterior = usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("NumeroExterior", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        NumeroInterior = usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("NumeroInteriror", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        Colonia = new ML.Colonia
                        {
                            IdColonia = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdColonia", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idColonia) ? idColonia : 0,

                            Municipio = new ML.Municipio
                            {
                                IdMunicipio = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Municipio", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdMunicipio", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idMunicipio) ? idMunicipio : 0,

                                Estado = new ML.Estado
                                {
                                    IdEstado = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Municipio", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Estado", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdEstado", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idEstado) ? idEstado : 0
                                }
                            }

                        }
                    }

                };
                // Obtener lista de roles desde la capa de negocio
                ML.Result resultDDLL = BL.Rol.GetAll();
                usuario.Rol.Roles = resultDDLL.Objects;

                ML.Result resultEstados = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

                ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                return usuario;
            }
            return null;

        }
        //  Aquie
        [HttpGet]
        public ActionResult Prueba(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects; //le pasa todos los valores a roles, para que pueda pintar el ddl

            ML.Result resultEstados = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

            ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            if (IdUsuario.HasValue)
            {
                //OBTENER EL USUARIO ID
                string action = "http://tempuri.org/IUsuario/GetById";
                string url = "http://localhost:9012/CRUDWebService/Usuario.svc";

                //CREAR EL SOBRE SOAP PARA OBTENER UN USUARIO POR SU ID
                string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
               <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:GetById>
                         <!--Optional:-->
                         <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                      </tem:GetById>
                   </soapenv:Body>
                </soapenv:Envelope>";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml; charset= \"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                //Enviarla solicitud
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }
                //OBTENER LA RESPUESTA
                try
                {
                    using (WebResponse response = (WebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {

                            string xml = reader.ReadToEnd();// se convierte a string
                            var xdoc = XDocument.Parse(xml);
                            // Acceder a GetUsuarioByIdResult usando el namespace correcto
                            var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                                e.Name.LocalName == "Correct" &&
                                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
                            ML.Result result = new ML.Result();
                            result.Correct = bool.Parse(usuarioElement.Value);
                            if (result.Correct)
                            {
                                result.Correct = true;
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return View(usuario);
            //DEVUELVE LA VISTA CON EL USUARIO SI EXISTE
        }
        [HttpGet]
        public ActionResult Form1(int? IdUsuario)
        {
            string action = "http://tempuri.org/IUsuario/GetById";
            string url = "http://localhost:9012/CRUDWebService/Usuario.svc";
            //Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

            // Crear el sobre SOAP
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
               <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:GetById>
                         <!--Optional:-->
                         <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                      </tem:GetById>
                   </soapenv:Body>
                </soapenv:Envelope>";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())//cacha el soap
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);//almacena el xml
                stream.Write(content, 0, content.Length);//se envía 
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse()) //obtiene la respuesta
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))//lee la respuesta
                    {
                        string result = reader.ReadToEnd();// se convierte a string
                                                           // Asegúrate de que tu vista esté lista para recibir este objeto
                        ML.Result resultGBI = DeserealizacionGetById(result);
                        ML.Usuario usuario = new ML.Usuario();
                        usuario = (ML.Usuario)resultGBI.Object;


                        ML.Result resultDDLEstados = BL.Estado.GetAll();//ejecuta el getall para todos los estados
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultDDLEstados.Objects;//el resultado se lo pasa a la lista de estados

                        ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);//espera el id del estado, si es que se seleccionó, para que se ejecute la parte de getbyid, por el estado y esta se pueda guardar el resultado y se lo pasa a la lista de municipios
                        usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;

                        ML.Result resultDDLColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Direccion.Colonia.Colonias = resultDDLColonia.Objects;

                        ML.Result resultDDL = BL.Rol.GetAll(); //->mandamos a llamar al mètodo y lo guardamos en le resul
                        usuario.Rol.Roles = resultDDL.Objects; //->lo que tenga result, lo va a guardar en la lista de roles

                        return View(usuario);


                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return View(); // Devuelve la vista
        }
        private ML.Result DeserealizacionGetById(string xml)
        {
            //string action = "http:";
            //string url = "http://localhost:51160/Usuario.svc"; // Cambia a la URL del servicio

            var usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            //result.Object = new List<object>();
            var xdoc = XDocument.Parse(xml);

            // Acceder a GetUsuarioByIdResult usando el namespace correcto
            var elem = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "Object" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            if (elem != null)
            {

                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                // Manejo de IdUsuario null  kjhfdkj
                //byte[]
                int IdUsuario;
                //if (elem.Element("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}IdUsuario")?.Value != null)
                //{
                //    usuario.IdUsuario = int.Parse(elem.Element("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}IdUsuario")?.Value);
                //}
                //else
                //{
                //    usuario.IdUsuario = 0;
                //}

                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out IdUsuario); //0
                usuario.IdUsuario = IdUsuario;

                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);

                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);

                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);

                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);

                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);

                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);

                var Rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");

                int IdRol;
                int.TryParse(Rol.Element("{http://schemas.datacontract.org/2004/07/ML}IdRol")?.Value, out IdRol); //0
                usuario.Rol.IdRol = IdRol;

                bool Estatus;
                bool.TryParse(elem.Element("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}Estatus")?.Value, out Estatus); //0
                usuario.Estatus = Estatus;

                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);

                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);

                usuario.Curp = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);

                var Direccion = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                usuario.Direccion.Calle = (string)(Direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInterior = (string)(Direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = (string)(Direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);

                var Colonia = Direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                //int IdColonia;
                //int.TryParse(Colonia.Element("{http://schemas.datacontract.org/2004/07/ML}IdColonia")?.Value, out IdColonia) ? IdColonia : 0;//0
                //usuario.Direccion.Colonia.IdColonia = IdColonia;
                usuario.Direccion.Colonia.IdColonia = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}IdColonia")?.Value, out int IdColoinia) ? IdColoinia : 0;
                usuario.Direccion.Colonia.CodigoPostal = (string)(Colonia.Element("{http://schemas.datacontract.org/2004/07/ML}CodigoPostal")?.Value ?? string.Empty);

                var Municipio = Colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                //int IdMunicipio;
                //int.TryParse(Rol.Element("{http://schemas.datacontract.org/2004/07/ML}IdRol")?.Value, out IdMunicipio); //0
                //usuario.Direccion.Colonia.Municipio.IdMunicipio = IdMunicipio;
                usuario.Direccion.Colonia.Municipio.IdMunicipio = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Municipio").Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio")?.Value, out int IdMunicipio) ? IdMunicipio : 0;
                var Estado = Municipio.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");

                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Municipio").Element("{http://schemas.datacontract.org/2004/07/ML}Estado").Element("{http://schemas.datacontract.org/2004/07/ML}IdEstado")?.Value, out int IdEstado) ? IdEstado : 0;
                //int IdEstado;
                //int.TryParse(Estado.Element("{http://schemas.datacontract.org/2004/07/ML}IdRol")?.Value, out IdEstado); //0
                //usuario.Direccion.Colonia.Municipio.Estado.IdEstado = IdEstado;
                //var imagen = "";
                //var Imagen = Convert.FromBase64String(usuario.Imagen.Element(XName.Get("Imagen","http://schemas.datacontract.org/2004/07/ML"))?.Value);

                result.Object = usuario;
            }
            return result;
        }
        [NonAction]
        public ML.Result GetAllRest()
        {
            ML.Result result = new ML.Result();

            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<Object>();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndpoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var respuesta = cliente.GetAsync("GetAll");

                    respuesta.Wait();

                    var resultcliente = respuesta.Result;

                    if(resultcliente.IsSuccessStatusCode)
                    {
                        var readTask = resultcliente.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects= new List<Object>();
                        foreach (var item in readTask.Result.Objects)
                        {

                            ML.Usuario itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                            result.Objects.Add(itemlist);


                          
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        [NonAction]
        public ML.Result AddRest(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndpoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var postTask = cliente.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                    postTask.Wait();

                    var resultServicio = postTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }catch (Exception ex) {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        [NonAction]
        public ML.Result UpdateRest(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndpoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var putTask = cliente.PutAsJsonAsync<ML.Usuario>("Update", usuario);
                    putTask.Wait();

                    var resultServicio =putTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        [NonAction]
        public ML.Result GetByIdRest(int IdUsuario)
        {
            ML.Result result = new Result();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndpoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var getByIdTask = cliente.GetAsync($"GetById/{IdUsuario}");
                    getByIdTask.Wait();

                    var resultServicio = getByIdTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [NonAction]
        public ML.Result DeleteRest(int IdUsuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);

                    var deleteTask = cliente.DeleteAsync($"Delete/{IdUsuario}");

                    deleteTask.Wait();

                    var resultServicio = deleteTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                    
                        result.Correct = true;
                        
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [NonAction]
        private ML.Usuario FormSOAP(string xmlResponse)
        {
            // Inicializamos el objeto Usuario
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                // Cargar el XML en un objeto XDocument para navegarlo fácilmente
                XDocument doc = XDocument.Parse(xmlResponse);

                // Buscar el nodo <AddResult> o <UpdateResult> dependiendo de la respuesta
                var resultNode = doc.Descendants("AddResult").FirstOrDefault() ?? doc.Descendants("UpdateResult").FirstOrDefault();

                if (resultNode != null)
                {
                    // Mapear el contenido de XML a las propiedades del objeto Usuario
                    usuario.IdUsuario = int.Parse(resultNode.Element("IdUsuario")?.Value ?? "0");
                    usuario.Nombre = resultNode.Element("Nombre")?.Value;
                    usuario.ApellidoPaterno = resultNode.Element("ApellidoPaterno")?.Value;
                    usuario.ApellidoMaterno = resultNode.Element("ApellidoMaterno")?.Value;
                    usuario.Celular = resultNode.Element("Celular")?.Value;
                    usuario.Curp = resultNode.Element("Curp")?.Value;
                    usuario.Email = resultNode.Element("Email")?.Value;
                    string estatusStr = resultNode.Element("Estatus")?.Value;

                    // Convertir el valor a booleano de forma segura
                    usuario.Estatus = !string.IsNullOrEmpty(estatusStr) && estatusStr.ToLower() == "true";


                    usuario.FechaNacimiento = resultNode.Element("FechaNacimiento")?.Value;
                    usuario.Password = resultNode.Element("Password")?.Value;
                    usuario.Sexo = resultNode.Element("Sexo")?.Value;
                    usuario.Telefono = resultNode.Element("Telefono")?.Value;
                    usuario.UserName = resultNode.Element("UserName")?.Value;
                    // Agregar más campos si es necesario según el XML de respuesta
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al deserializar la respuesta SOAP: {ex.Message}");
            }

            return usuario;
        }

        [HttpGet]
        public ML.Result DeleteXML(int IdUsuario)
        {
            string action = "http://tempuri.org/IUsuario/Delete";
            string url = "http://localhost:9012/CRUDWebService/Usuario.svc"; // Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            string soapEnvelope =
                $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
             <soapenv:Header/>
                 <soapenv:Body>
                 <tem:Delete>
                 <!--Optional:-->
                 <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
             </tem:Delete>
          </soapenv:Body>
         </soapenv:Envelope>";
            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())//cacha el soap
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);//almacena el xml
                stream.Write(content, 0, content.Length);//se envía 
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse()) //obtiene la respuesta
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))//lee la respuesta
                    {
                        string xml = reader.ReadToEnd();// se convierte a string
                        var xdoc = XDocument.Parse(xml);
                        // Acceder a GetUsuarioByIdResult usando el namespace correcto
                        var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                            e.Name.LocalName == "Correct" &&
                            e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

                        result.Correct = bool.Parse(usuarioElement.Value);

                        if (result.Correct)
                        {
                            ViewBag.succes = "El registro se eliminó correctamente";
                            //return PartialView("_Parcial");
                        }
                        else
                        {
                            ViewBag.error = "Hubo un error al eliminar el registro";
                            //return View("_Parcial");
                        }

                        // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return result; // Devuelve la vista
        }

       
        [HttpPost]
        public ActionResult CargaMasiva()
        {
            if (Session["RutaExcel"] == null)
            {

                HttpPostedFileBase excelUsuario = Request.Files["Excel"];

                string extensionPermitida = ".xlsx";

                if (excelUsuario.ContentLength > 0)
                {
                    //t
                    string extensionObtenida = Path.GetExtension(excelUsuario.FileName);

                    if (extensionObtenida == extensionPermitida)
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHmmssff") + ".xlsx";

                        if (!System.IO.File.Exists(ruta))
                        {
                            excelUsuario.SaveAs(ruta);

                            string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta;

                            ML.Result resultExcel = BL.Usuario.LeerExcel(cadenaConexion);

                            //BL.Usuario.LeerExcel(cadenaConexion);

                            if (resultExcel.Objects.Count > 0)
                            {
                                ML.ResultExcel resultValidacion = BL.Usuario.ValidarExcel(resultExcel.Objects);

                                if (resultValidacion.Errores.Count > 0)
                                {
                                    ViewBag.ErroresExcel = resultValidacion.Errores;
                                    ViewBag.ErroresExcel = "Se encontraron errores en el archivo.";
                                    //ViewBag.MensajeExito = null;
                                    //ViewBag.EsExito = false;
                                    return PartialView("_Model");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                    ViewBag.ErroresExcel = "La validacion del Excel es correcta";
                                    //ViewBag.MensajeError = null;
                                    //ViewBag.EsExito = true;
                                    //return RedirectToAction("GetAll");
                                    return PartialView("_Model");
                                }

                            }

                        }
                        else
                        {
                            ViewBag.ErroresExcel = "Vuelve a cargar el archivo, porque ya existe";
                            return PartialView("_Model");

                        }

                    }
                    else
                    {
                        ViewBag.ErroresExcel = "El archivo no es un Excel";
                        return PartialView("_Model");

                    }

                }
                else
                {
                    ViewBag.ErroresExcel = "No me diste ningun archivo";
                    return PartialView("_Model");

                }

            }
            else
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();

                ML.Result resultLeer = BL.Usuario.LeerExcel(cadenaConexion);

                if (resultLeer.Objects.Count > 0)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    int registrosCorrectos = 0;
                    int registrosIncorrectos = 0;

                    List<ML.Usuario> usuariosCorrectos = new List<ML.Usuario>();

                    List<ML.Usuario> usuariosIncorrectos = new List<ML.Usuario>();


                    foreach (ML.Usuario usuario in resultLeer.Objects)
                    {
                        ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                        if (!resultInsertar.Correct)
                        {

                            registrosIncorrectos++;
                            usuariosIncorrectos.Add(usuario);
                        }
                        else
                        {
                            registrosCorrectos++;
                            usuariosCorrectos.Add(usuario);
                        }

                    }

                    //if (resultErrores.Objects.Count > 0)
                    //{
                    //    ViewBag.ErroresExcel = resultErrores.Objects;
                    //}

                    // Mostrar los conteos de inserciones correctas e incorrectas
                    ViewBag.UsuariosCorrectos = usuariosCorrectos;
                    ViewBag.UsuariosIncorrectos = usuariosIncorrectos;
                    ViewBag.RegistrosCorrectos = registrosCorrectos;
                    ViewBag.RegistrosIncorrectos = registrosIncorrectos;

                    if (registrosIncorrectos > 0)
                    {
                        // Si hay errores, mostrar el modal con los errores
                        ViewBag.ErroresExcel = "Se hizo el proceso de registro" + resultLeer.Objects.Count + ", usuarios de los cuales fallaron" + registrosIncorrectos + ", y se insertaron" + registrosCorrectos;
                        Session["RutaExcel"] = null;
                        return PartialView("_Model");

                    }
                    else
                    {
                        // Si no hay errores, mostrar un mensaje de éxito
                        ViewBag.ErroresExcel = "La carga masiva fue realizada con éxito.";
                        Session["RutaExcel"] = null;

                        return PartialView("_Model");
                    }

                }
                else
                {
                    ViewBag.ErroresExcel = "No se puede insertar";
                    Session["RutaExcel"] = null;
                    return PartialView("_Model");

                }
                //archivo

            }
            Session["RutaExcel"] = null;

            return RedirectToAction("GetAll");

        }


        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

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
                //ML.Result result = GetByIdSOAP(IdUsuario);
                ML.Result result = GetByIdRest(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;
                //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                //UsuarioReference.UsuarioClient objeto = new UsuarioReference.UsuarioClient();

                //var resultGetById = objeto.GetById(IdUsuario.Value);
                //string xml = GetByIdXML(IdUsuario.Value);
                //if (!string.IsNullOrEmpty(xml))
                //{
                //    var usuarioxml = GetByIdSOAPXML(xml);
                //    usuario.Usuarios = usuarioxml.Usuarios;
                //}
                //usuario = (ML.Usuario)resultGetById.Object;
                

            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            ML.Result resultEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            //ML.Result resultEstado = BL.Estado.GetAll();
            return View(usuario);
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)

        {

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImagenUsuario"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);

                }

                if (usuario.IdUsuario == 0)

                {
                    ML.Result result = AddRest(usuario);

                    
                    //ML.Result result = FormXML(usuario);


                    //UsuarioReference.UsuarioClient objeto = new UsuarioReference.UsuarioClient();

                    //var resultAgregar = objeto.Add(usuario);
                    //ML.Result result = BL.Usuario.AddEF(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Add = true;
                        ViewBag.Message = "El usuario se inserto correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = "ERROR" + result.ErrorMessage;
                    }

                    return PartialView("_Mensajes");

                }

                else
                {
                    //    UsuarioReference.UsuarioClient objeto = new UsuarioReference.UsuarioClient();

                    //    var resultActualizar = objeto.Update(usuario);
                    //ML.Result result = BL.Usuario.UpdateEF(usuario);
                    //ML.Result result = FormXML(usuario);

                    ML.Result result = UpdateRest(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Add = true;
                        ViewBag.Message = "El usuario se actualizo correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = "ERROR" + result.ErrorMessage;
                    }


                    //return View("GetAll");
                }

                //return RedirectToAction("GetAll");
                return PartialView("_Mensajes");


            }
            else
            {
                ML.Result resultDDL = BL.Rol.GetAll();
                usuario.Rol.Roles = resultDDL.Objects;

                ML.Result resultEstado = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;


                //usuario.Direccion.Colonia.Municipio.Municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;

                //usuario.Direccion.Colonia.Colonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;

                if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                {
                    usuario.Direccion.Colonia.Municipio.Municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;
                }

                // Si el usuario ya seleccionó un municipio, cargamos las colonias de ese municipio
                if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                {
                    usuario.Direccion.Colonia.Colonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;
                }

                return View(usuario);

            }



        }


        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            //ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            //UsuarioReference.UsuarioClient objeto = new UsuarioReference.UsuarioClient();
            //var resultDelete = objeto.Delete(IdUsuario);
            //ML.Result result = DeleteXML(IdUsuario);
            ML.Result result = DeleteRest(IdUsuario);

            if (result.Correct)
            {
                ViewBag.Add = true;
                ViewBag.Message = "El usuario se elimino correctamente";
                //return RedirectToAction("GetAll");
            }
            else
            {
                ViewBag.Add = false;
                ViewBag.Message = "ERROR" + result.ErrorMessage;
                //return View("GetAll");
            }
            return PartialView("_Mensajes");
        }
        [HttpGet]

        public JsonResult CambioEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsonResult = BL.Usuario.CambioEstatus(IdUsuario, Estatus);
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result JsonResult = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(IdMunicipio);
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









