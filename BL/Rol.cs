using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())


                try
                {
                    var query = (from rolBD in context.Rols
                                 select new
                                 {
                                     IdRol = rolBD.IdRol,

                                     Nombre = rolBD.Nombre,

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();


                        foreach (var item in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = item.IdRol;
                            rol.Nombre = item.Nombre;
                            result.Objects.Add(rol);
                            
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }

            return result;











        }
    }

}





















