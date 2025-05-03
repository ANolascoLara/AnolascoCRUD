using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        public static ML.Result SubCategoriaGetById(int IdCategoria)
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = (from subCategoria in context.SubCategorias
                                 where subCategoria.IdCategoria == IdCategoria
                                 select subCategoria).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.SubCategoria subCategoria = new ML.SubCategoria()
                            {
                                IdSubCategoria = obj.IdSubCategoria,
                                Nombre = obj.Nombre
                            };
                            result.Objects.Add(subCategoria);
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
