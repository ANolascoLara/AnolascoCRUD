using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductoSucursal
    {
        public static ML.Result GetAll(ML.ProductoSucursal productoSucursalObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = context.SucursalGetAll(Convert.ToByte(productoSucursalObj.Sucursal.IdSucursal)).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                            productoSucursal.Sucursal = new ML.Sucursal();
                            productoSucursal.Producto = new ML.Producto();

                            productoSucursal.Producto.IdProducto = obj.IdProducto;
                            productoSucursal.Producto.Imagen = obj.Imagen;
                            productoSucursal.Producto.Nombre = obj.NombreProducto;
                            productoSucursal.Sucursal.IdSucursal = obj.IdSucursal;
                            productoSucursal.Sucursal.Nombre = obj.NombreSucursal;
                            productoSucursal.Stock = Convert.ToByte(obj.Stock);
                            result.Objects.Add(productoSucursal);
                        }
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
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result GetAlllinq(int IdSucursal)
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = (from productosucursal in context.ProductoSucursals
                                 where productosucursal.IdSucursal == IdSucursal
                                 select productosucursal).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.ProductoSucursal productosucursal = new ML.ProductoSucursal();
                            productosucursal.Producto = new ML.Producto();
                            productosucursal.Sucursal = new ML.Sucursal();

                            {
                                productosucursal.Producto.IdProducto = obj.IdProducto.Value;
                                productosucursal.Producto.Imagen = obj.Producto.Imagen;
                                productosucursal.Producto.Nombre = obj.Producto.Nombre;
                                productosucursal.Sucursal.IdSucursal = obj.Sucursal.IdSucursal;
                                productosucursal.Sucursal.Nombre = obj.Sucursal.Nombre;
                                productosucursal.Stock = Convert.ToInt16(obj.Stock);
                            };
                            result.Objects.Add(productosucursal);
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
        public static ML.Result UpdateStock(int IdProducto, int IdSucursal, int Stock)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var productoSucursal = context.ProductoSucursals
                        .FirstOrDefault(ps => ps.Producto.IdProducto == IdProducto && ps.Sucursal.IdSucursal == IdSucursal);

                    if (productoSucursal != null)
                    {
                        productoSucursal.Stock = Stock;
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "ProductoSucursal no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }



        public static ML.Result Delete(int IdProducto, int IdSucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var productoSucursal = context.ProductoSucursals
                        .FirstOrDefault(ps => ps.IdProducto == IdProducto && ps.IdSucursal == IdSucursal);

                    if (productoSucursal != null)
                    {
                        context.ProductoSucursals.Remove(productoSucursal);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró la relación Producto-Sucursal a eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


    }
}
