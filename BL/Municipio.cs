using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = context.MunicipioGetByIdEstado(IdEstado).ToList();

                    if (query.Count>0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            ML.Estado estado = new ML.Estado();
                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre;
                       
                            result.Objects.Add(municipio);
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
