using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        //public int Id { get; set; }
        //public string Genero { get; set; }
        //public decimal Promedio { get; set; }
        //public int Grado { get; set; }
        public int IdUsuario { get; set; }
        [DisplayName("UserName")]
        [Required(ErrorMessage = "Nombre Obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Este campo solo acepta letras y numeros")]
        public string UserName { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Nombre Obligatorio")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Este campo solo acepta letras")]
        
        public string Nombre  { get; set; }
        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Este campo solo acepta letras")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Este campo solo acepta letras")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"^[^\s@@]+@[^\s@@]+\.[^\s@@]+$", ErrorMessage = "Este campo solo acepta correos correctos")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).{8}$", ErrorMessage = "Este campo solo acepta Contraseñas correctas")]
        public string Password { get; set; }
        [DisplayName("Fecha de Nacimiento")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string FechaNacimiento { get; set; }
        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Sexo { get; set; }
        [DisplayName("Telefono")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Este campo solo acepta numeros")]
        public string Telefono { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Este campo solo acepta numeros")]
        public string Celular {  get; set; }
        public bool Estatus { get; set; }
        [DisplayName("Curp")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[0-9A-Z]{2}$", ErrorMessage = "Este campo solo aceptan Curps validos")]
        public string Curp { get; set; }
        [DisplayName("Imagen")]
        
        public byte [] Imagen { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion  { get; set; }
        public List<object> Direccions { get; set; }

        public List <object> Usuarios { get; set; }

        

    }
}






