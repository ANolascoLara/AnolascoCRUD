using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();

                    if (query.Count>0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            ML.Municipio municipio = new ML.Municipio();

                           colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;
                            result.Objects.Add(colonia);
                        }



                        result.Correct = true;

                    }
                    else
                    {
                        //no hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos/registros";
                    }

                }

            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;




        }
    }
}
