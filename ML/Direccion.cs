using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {

        public int IdDireccion {  get; set; }
        public string Calle {  get; set; }
        [DisplayName("NumeroInterior")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^([0-9]{2})$", ErrorMessage = "Este campo solo acepta numeros")]
        public string NumeroInterior { get; set; }
        [DisplayName("NumeroExterior")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("^([0-9]{2})$", ErrorMessage = "Este campo solo acepta numeros")]
        public string NumeroExterior { get; set; }
        public List<object> Direcciones{ get; set; }

        public ML.Colonia Colonia { get; set; }

    }
}
