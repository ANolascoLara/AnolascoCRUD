using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static ML.Result CategoriaGetAll()
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = (
                            from categoria in context.Categorias
                            select new
                            {
                                Id = categoria.IdCategoria,
                                Nombre = categoria.Nombre,
                                
                            }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            ML.Categoria categoria = new ML.Categoria();

                            categoria.IdCategoria = obj.Id;
                            categoria.Nombre = obj.Nombre;
                            
                            result.Objects.Add(categoria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }


                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }


            return result;
        }


    }
}
