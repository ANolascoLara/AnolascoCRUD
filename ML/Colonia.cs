using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        [DisplayName("Colonia")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public List<object> Colonias { get; set; }
        public ML.Municipio Municipio{ get; set; }

    }
}
