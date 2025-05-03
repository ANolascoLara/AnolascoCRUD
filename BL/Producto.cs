using DL_EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result AddEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    int rowsAffect = context.ProductoAdd(producto.Nombre, producto.Descripcion, Convert.ToDecimal(producto.Precio),
                        producto.Imagen, producto.SubCategoria.IdSubCategoria);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                        Console.WriteLine("La insercion se realizo correctamente");
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("La insercion fallo");
                    }
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

        public static ML.Result DeleteEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities contex = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    int rowsAffect = contex.ProductoDelete(IdProducto);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

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

        public static ML.Result UpdateEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {

                    int rowsAffect = context.ProductoUpdate(Convert.ToInt32(producto.IdProducto), producto.Nombre, producto.Descripcion, Convert.ToDecimal(producto.Precio),
                        producto.Imagen, producto.SubCategoria.IdSubCategoria);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;


                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                //CERRAR

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAllEF(ML.Producto productoObj)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = context.ProductoGetAll(productoObj.SubCategoria.IdSubCategoria).ToList();

                    if (query.Count > 0)
                    {
                        //si trae registros
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {

                            ML.Producto producto = new ML.Producto();
                            producto.SubCategoria = new ML.SubCategoria();
                            producto.SubCategoria.Categoria = new ML.Categoria();

                            producto.IdProducto = objBD.IdProducto;
                            producto.Nombre = objBD.NombreProducto;
                            producto.Descripcion = objBD.Descripcion;
                            producto.Precio = Convert.ToDecimal(objBD.Precio);
                            producto.Imagen = objBD.Imagen;
                            producto.SubCategoria.IdSubCategoria = Convert.ToInt16(objBD.IdSubCategoria);
                            producto.SubCategoria.Nombre = objBD.NombreSubCategoria;
                            producto.SubCategoria.Categoria.IdCategoria = Convert.ToInt16(objBD.IdCategoria);
                            producto.SubCategoria.Categoria.Nombre = objBD.NombreCategoria;
                       

                            
                            result.Objects.Add(producto);
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

        public static ML.Result GetByIdEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = context.ProductoGetById(IdProducto).FirstOrDefault();

                    if (query != null)
                    {


                        ML.Producto producto = new ML.Producto();
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.Categoria = new ML.Categoria();

                        producto.IdProducto= query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.Descripcion = query.Descripcion;
                        producto.Precio = Convert.ToDecimal(query.Precio);
                        producto.Imagen = query.Imagen;
                        producto.SubCategoria.IdSubCategoria = Convert.ToInt16(query.IdSubCategoria);
                        producto.SubCategoria.Nombre = query.NombreSubCategoria;
                        producto.SubCategoria.Categoria.IdCategoria = Convert.ToInt16(query.IdCategoria);
                        producto.SubCategoria.Categoria.Nombre = query.NombreCategoria;

                        result.Object = producto;

                        result.Correct = true;
                        //commi
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
