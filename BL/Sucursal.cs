using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = (from sucursal in context.Sucursals
                                 select new
                                 {
                                     IdSucursal = sucursal.IdSucursal,
                                     Nombre = sucursal.Nombre,
                                     Latitud = sucursal.Latitud,
                                     Longitud = sucursal.Longitud

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal()
                            {
                                IdSucursal = obj.IdSucursal,
                                Nombre = obj.Nombre,
                                Latitud = obj.Latitud,
                                Longitud = obj.Longitud
                            };
                            result.Objects.Add(sucursal);
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
