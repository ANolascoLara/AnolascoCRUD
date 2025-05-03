using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {

        public int IdProducto { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Nombre Obligatorio")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Este campo solo acepta letras")]
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Descripcion Obligatoria")]
        
        public string Descripcion { get; set; }
        [DisplayName("Precio")]
        [Required(ErrorMessage = "Precio Obligatorio")]

        public decimal Precio { get; set; }
        [DisplayName("Imagen")]
        

        public byte [] Imagen { get; set; }
        
        
        public ML.SubCategoria SubCategoria { get; set; }
        public List <object> Productos { get; set; }


    }
}
