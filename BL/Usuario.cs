using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace BL
{
    public class Usuario
    {
        //public static void Add(ML.Usuario usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "INSERT INTO Usuario VALUES (@nombre, @genero, @promedio, @grado)";

        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query; //query que va a ejecutar
        //            cmd.Connection = context; //BD

        //            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
        //            cmd.Parameters.AddWithValue("@genero", usuario.Genero);
        //            cmd.Parameters.AddWithValue("@promedio", usuario.Promedio);
        //            cmd.Parameters.AddWithValue("@grado", usuario.Grado);

        //            context.Open(); //abrir la conexion con la BD
        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                Console.WriteLine("EL registro se realizo correctamente");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error al insertar");
        //            }
        //        }
        //        //CERRAR

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error de conexion " + ex.Message);
        //    }

        //}

        //public static void Delete(int Id)
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "DELETE FROM Usuario";

        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query; //query que va a ejecutar
        //            cmd.Connection = context; //BD

        //            cmd.Parameters.AddWithValue("@Id", Id);


        //            context.Open(); //abrir la conexion con la BD
        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                Console.WriteLine("La eliminacio se realizo correctamente");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error al eliminar");
        //            }
        //        }
        //        //CERRAR

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error de conexion" + ex.Message);
        //    }

        //}
        //public static void Update(ML.Usuario usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "UPDATE Usuario SET Nombre= @nombre, Genero=@genero, Promedio=@promedio, Grado=@grado WHERE Id=@Id";

        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query; //query que va a ejecutar
        //            cmd.Connection = context; //BD

        //            cmd.Parameters.AddWithValue("@Id", usuario.Id);
        //            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
        //            cmd.Parameters.AddWithValue("@genero", usuario.Genero);
        //            cmd.Parameters.AddWithValue("@promedio", usuario.Promedio);
        //            cmd.Parameters.AddWithValue("@grado", usuario.Grado);

        //            context.Open(); //abrir la conexion con la BD
        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                Console.WriteLine("La modificacion se realizo correctamente");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error al modificar");
        //            }
        //        }
        //        //CERRAR

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error de conexion" + ex.Message);
        //    }
        // }
        //public static ML.Result GetAll()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "SELECT * FROM Usuario";
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query;
        //            cmd.Connection = context;

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dataTable = new DataTable();
        //            adapter.Fill(dataTable);

        //            if (dataTable.Rows.Count > 0)
        //            {
        //                //si trae registros
        //                result.Objects = new List<object>();

        //                foreach (DataRow row in dataTable.Rows)
        //                {
        //                    ML.Usuario usuario = new ML.Usuario();
        //                    usuario.Id = Convert.ToInt32(row[0].ToString());
        //                    usuario.Nombre = row[1].ToString();
        //                    usuario.Genero = row[2].ToString();
        //                    usuario.Promedio = Convert.ToDecimal(row[3].ToString());

        //                    result.Objects.Add(usuario);
        //                }
        //                result.Correct = true;

        //            }
        //            else
        //            {
        //                //no hay registros
        //                result.Correct = false;
        //                result.ErrorMessage = "No hay datos/registros";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //HUBO UN ERROR
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;




        //}

        //public static ML.Result GetById(int Id)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "SELECT * FROM Usuario WHERE Id = @Id";
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query;
        //            cmd.Connection = context;

        //            cmd.Parameters.AddWithValue("@Id", Id);


        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dataTable = new DataTable();
        //            adapter.Fill(dataTable);

        //            if (dataTable.Rows.Count > 0)
        //            {
        //                //si trae registros
        //                DataRow row = dataTable.Rows[0];
        //                ML.Usuario usuario = new ML.Usuario();
        //                usuario.Id = Convert.ToInt32(row[0].ToString());
        //                usuario.Nombre = row[1].ToString();
        //                usuario.Genero = row[2].ToString();
        //                usuario.Promedio = Convert.ToInt32(row[3].ToString());
        //                usuario.Grado = Convert.ToInt32(row[4].ToString());

        //                result.Object = usuario;

        //                result.Correct = true;

        //            }
        //            else
        //            {
        //                //no hay registros
        //                result.Correct = false;
        //                result.ErrorMessage = "No hay datos/registros";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //HUBO UN ERROR
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;

        //}


        //public static ML.Result Add(ML.Usuario usuario)

        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "UsuarioAdd";
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query;
        //            cmd.Connection = context;
        //            cmd.CommandType=CommandType.StoredProcedure;


        //            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
        //            cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
        //            cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
        //            cmd.Parameters.AddWithValue("@Email", usuario.Email);
        //            cmd.Parameters.AddWithValue("@Password", usuario.Password);
        //            //cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
        //            cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
        //            cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
        //            cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
        //            cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
        //            cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
        //            //cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
        //            cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);



        //            context.Open(); //abrir la conexion con la BD
        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                //si trae registros

        //                result.Correct = true;

        //            }
        //            else
        //            {
        //                //no hay registros
        //                result.Correct = false;
        //                result.ErrorMessage = "No se pudo insertar";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //HUBO UN ERROR
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;

        //}


        //public static ML.Result Delete(int IdUsuario)
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = " Usuario.Delete";

        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query; //query que va a ejecutar
        //            cmd.Connection = context; //BD
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);


        //            context.Open(); //abrir la conexion con la BD
        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                Console.WriteLine("La eliminacio se realizo correctamente");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error al eliminar");
        //            }
        //        }
        //        //CERRAR

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error de conexion" + ex.Message);
        //    }


        //}

        //public static ML.Result Update(ML.Usuario usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "UsuarioUpdate";

        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query; //query que va a ejecutar
        //            cmd.Connection = context; //BD
        //            cmd.CommandType = CommandType.StoredProcedure;


        //            cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
        //            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
        //            cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
        //            cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
        //            cmd.Parameters.AddWithValue("@Email", usuario.Email);
        //            cmd.Parameters.AddWithValue("@Password", usuario.Password);
        //            //cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
        //            cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
        //            cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
        //            cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
        //            cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
        //            cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
        //            //cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
        //            cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);
        //            context.Open(); //abrir la conexion con la BD
        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                Console.WriteLine("La modificacion se realizo correctamente");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error al modificar");
        //            }
        //        }
        //        //CERRAR

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error de conexion" + ex.Message);
        //    }
        //}

        //public static ML.Result GetAll()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            string query = "UsuarioGetAll";
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query;
        //            cmd.Connection = context;
        //            cmd.CommandType = CommandType.StoredProcedure;


        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dataTable = new DataTable();
        //            adapter.Fill(dataTable);

        //            if (dataTable.Rows.Count > 0)
        //            {
        //                //si trae registros
        //                result.Objects = new List<object>();

        //                foreach (DataRow row in dataTable.Rows)
        //                {
        //                    ML.Usuario usuario = new ML.Usuario();
        //                    usuario.Id = Convert.ToInt32(row[0].ToString());
        //                    usuario.Nombre = row[1].ToString();
        //                    usuario.Genero = row[2].ToString();
        //                    usuario.Promedio = Convert.ToDecimal(row[3].ToString());

        //                    usuario.UserName = Console.ReadLine();

        //                    usuario.Nombre = Console.ReadLine();Console.WriteLine("Ingresa el apeliidopaterno:");
        //                    usuario.ApellidoPaterno = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el apellidomaterno:");
        //                    usuario.ApellidoMaterno = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el email:");
        //                    usuario.Email = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el password:");
        //                    usuario.Password = Console.ReadLine();
        //                    // Console.WriteLine("Ingresa el fechanacimiento:");
        //                    //usuario.FechaNacimiento = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el sexo:");
        //                    usuario.Sexo = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el celular:");
        //                    usuario.Celular = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el telefono:");
        //                    usuario.Telefono = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el estatus:");
        //                    usuario.Estatus = Convert.ToByte(Console.ReadLine());
        //                    Console.WriteLine("Ingresa el curp:");
        //                    usuario.Curp = Console.ReadLine();
        //                    //Console.WriteLine("Ingresa el imagen:");
        //                    //usuario.Imagen = Console.ReadLine();
        //                    Console.WriteLine("Ingresa el idrol:");
        //                    usuario.IdRol = Convert.ToInt32(Console.ReadLine());


        //                    result.Objects.Add(usuario);
        //                }
        //                result.Correct = true;

        //            }
        //            else
        //            {
        //                //no hay registros
        //                result.Correct = false;
        //                result.ErrorMessage = "No hay datos/registros";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //HUBO UN ERROR
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;




        //}

        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    int rowsAffect = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno, usuario.Email, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono,
                        usuario.Celular, usuario.Estatus, usuario.Curp, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
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

        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities contex = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    int rowsAffect = contex.UsuarioDelete(IdUsuario);
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


        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {

                    int rowsAffect = context.UsuarioUpdate(Convert.ToInt32(usuario.IdUsuario), usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno, usuario.Email, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono,
                        usuario.Celular, usuario.Estatus, usuario.Curp, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

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

        public static ML.Result GetAllEF(ML.Usuario usuarioObj)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    //var query = context.UsuarioGetAll(usuarioObj.Nombre, usuarioObj.ApellidoPaterno, usuarioObj.ApellidoPaterno, usuarioObj.Rol.IdRol).ToList();
                    var query = context.UsuarioGetAllViewSQL(usuarioObj.Nombre ?? "", usuarioObj.ApellidoPaterno ?? "", usuarioObj.ApellidoPaterno ?? "", usuarioObj.Rol.IdRol = 0).ToList();

                    if (query.Count > 0)
                    {
                        //si trae registros
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                            usuario.IdUsuario = objBD.IdUsuario;
                            usuario.UserName = objBD.UserName;
                            usuario.Nombre = objBD.NombreUsuario;
                            usuario.ApellidoPaterno = objBD.ApellidoPaterno;
                            usuario.ApellidoMaterno = objBD.ApellidoMaterno;
                            usuario.Email = objBD.Email;
                            usuario.Password = objBD.Password;
                            usuario.FechaNacimiento = objBD.FechaNacimiento;
                            usuario.Sexo = objBD.Sexo;
                            usuario.Telefono = objBD.Telefono;
                            usuario.Celular = objBD.Celular;
                            if(objBD.Imagen != null)
                            {
                                usuario.ImagenBase64 = Convert.ToBase64String(objBD.Imagen);
                            }
                            else
                            {
                                usuario.ImagenBase64 = null;
                            }
                             
                            usuario.Rol.Nombre = objBD.NombreRol;
                            usuario.Estatus = Convert.ToBoolean(objBD.Estatus);
                            usuario.Direccion.Calle = objBD.Calle;
                            usuario.Direccion.NumeroInterior = objBD.NumeroInterior;
                            usuario.Direccion.NumeroExterior = objBD.NumeroExterior;
                            usuario.Direccion.Colonia.Nombre = objBD.NumeroExterior;
                            usuario.Direccion.Colonia.CodigoPostal = objBD.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio.Nombre = objBD.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = objBD.NombreEstado;



                            usuario.Curp = objBD.Curp;

                            if (objBD.IdRol != null)
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            else
                            {
                                usuario.Rol.IdRol = objBD.IdRol.Value;
                            }


                            result.Objects.Add(usuario);
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
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Imagen = query.Imagen;
                        usuario.Direccion.Calle = query.calle;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia != null ? query.IdColonia.Value : 0;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio != null ? query.IdMunicipio.Value : 0;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado != null ? query.IdEstado.Value : 0;
                        if (query.Estatus != true)
                        {
                            usuario.Estatus = true;
                        }
                        else
                        {
                            usuario.Estatus = false;
                        }
                        usuario.Curp = query.CURP;


                        if (query.IdRol != null)
                        {
                            usuario.Rol.IdRol = query.IdRol.Value;
                        }
                        else
                        {
                            usuario.Rol.IdRol = 0;
                        }

                        result.Object = usuario;

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

        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {

                    DL_EF.Usuario usuarioBD = new DL_EF.Usuario();
                    usuarioBD.UserName = usuario.UserName;
                    usuarioBD.Nombre = usuario.Nombre;
                    usuarioBD.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioBD.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioBD.Email = usuario.Email;
                    usuarioBD.Password = usuario.Password;
                    usuarioBD.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                    usuarioBD.Sexo = usuario.Sexo;
                    usuarioBD.Telefono = usuario.Telefono;
                    usuarioBD.Celular = usuario.Celular;
                    usuarioBD.Estatus = usuario.Estatus;
                    usuarioBD.Curp = usuario.Curp;
                    usuarioBD.IdRol = usuario.Rol.IdRol;

                    context.Usuarios.Add(usuarioBD);


                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "ERROR AL INSERTAR";
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

        public static ML.Result DeleteLINQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {

                    var soloDelete = (from usuariolinq in context.Usuarios
                                      where usuariolinq.IdUsuario == IdUsuario
                                      select usuariolinq).SingleOrDefault();

                    var buscar = (from usuario in context.Usuarios
                                  where usuario.IdUsuario == IdUsuario
                                  select new
                                  {
                                      IdUsuario = usuario.IdUsuario,
                                      UserName = usuario.UserName,
                                      Nombre = usuario.Nombre,
                                      ApellidoPaterno = usuario.ApellidoPaterno,
                                      ApellidoMaterno = usuario.ApellidoMaterno,
                                      Email = usuario.Email,
                                      Password = usuario.Password,
                                      FechaNacimiento = usuario.FechaNacimiento,
                                      Sexo = usuario.Sexo,
                                      Telefono = usuario.Telefono,
                                      Celular = usuario.Celular,
                                      Estatus = usuario.Estatus,
                                      Curp = usuario.Curp,
                                      IdRol = usuario.IdRol
                                  }).SingleOrDefault();
                    if (soloDelete != null)
                    {
                        context.Usuarios.Remove(soloDelete);

                        int filasafectadas = context.SaveChanges();
                        if (filasafectadas > 0)
                        {
                            result.Correct = true;


                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar";
                        }
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

        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    var busqueda = (from usuarioBD in context.Usuarios
                                    where usuarioBD.IdUsuario == usuario.IdUsuario
                                    select usuarioBD).SingleOrDefault();

                    if (busqueda != null)
                    {
                        busqueda.UserName = usuario.UserName;
                        busqueda.Nombre = usuario.Nombre;
                        busqueda.ApellidoPaterno = usuario.ApellidoPaterno;
                        busqueda.ApellidoMaterno = usuario.ApellidoMaterno;
                        busqueda.Email = usuario.Email;
                        busqueda.Password = usuario.Password;
                        busqueda.FechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
                        busqueda.Sexo = usuario.Sexo;
                        busqueda.Telefono = usuario.Telefono;
                        busqueda.Celular = usuario.Celular;
                        busqueda.Estatus = usuario.Estatus;
                        busqueda.Curp = usuario.Curp;
                        busqueda.IdRol = usuario.Rol.IdRol;
                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo ";

                        }



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


        public static ML.Result CambioEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
                {
                    int rowsAffect = context.CambioEstatus(IdUsuario, Estatus);
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


        public static ML.Result LeerExcel(string cadenaConexion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConexion))
                {

                    string query = "Select * from [Hoja1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        OleDbDataAdapter adapter = new OleDbDataAdapter(); 
                        adapter.SelectCommand = cmd;
                        DataTable tablaUsuario = new DataTable();
                        adapter.Fill(tablaUsuario);
                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>(); 
                            foreach (DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();


                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password= row[5].ToString();
                                usuario.FechaNacimiento= row[6].ToString();
                                usuario.Sexo= row[7].ToString();
                                usuario.Telefono= row[8].ToString();
                                usuario.Celular= row[9].ToString();
                                usuario.Estatus= Convert.ToBoolean(row[10]);
                                usuario.Curp = row[11].ToString();
                                usuario.Imagen = null;
                                
                                usuario.Rol.IdRol= Convert.ToUInt16(row[12]);
                                usuario.Direccion.Calle= row[13].ToString();
                                usuario.Direccion.NumeroInterior= row[14].ToString();
                                usuario.Direccion.NumeroExterior= row[15].ToString();
                                usuario.Direccion.Colonia.IdColonia = Convert.ToUInt16(row[16]);
                                

                                result.Objects.Add(usuario);

                            }

                            result.Correct = true;
                        }
                
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

        public static ML.ResultExcel ValidarExcel(List<object> registros)
        {
            ML.ResultExcel result = new ML.ResultExcel();
            result.Errores = new List<object>();

            int contador = 1;

            foreach (ML.Usuario usuario in registros) {
                ML.ResultExcel errorRegistro = new ML.ResultExcel();

                errorRegistro.NumeroRegistro = contador;

                    if (usuario.UserName.Length > 50 || usuario.UserName == "" || usuario.UserName ==null) {


                        errorRegistro.ErrorMessage += "La columna UserName contiene un valor muy extenso o se encuentra vacia";


                    }
                    if (usuario.Nombre.Length > 50 || usuario.Nombre == "" || usuario.Nombre == null)
                    {


                            errorRegistro.ErrorMessage += "La columna Nombre contiene un valor muy extenso o se encuentra vacia";


                    }
                    if (usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null)
                    {


                        errorRegistro.ErrorMessage += "La columna  Apellido Paterno contiene un valor muy extenso o se encuentra vacia";


                    }
                    if (usuario.ApellidoMaterno.Length > 50 || usuario.ApellidoMaterno == "" || usuario.ApellidoMaterno == null)
                    {

                        
                        errorRegistro.ErrorMessage += "La columna Apellido Materno contiene un valor muy extenso o se encuentra vacia";


                    }
                    if (usuario.Email.Length > 50 || usuario.Email == "" || usuario.Email == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Email contiene un valor muy extenso o se encuentra vacia";


                    }
                    if (usuario.Password.Length != 8 || usuario.Password == "" || usuario.Password == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Password contiene un valor muy extenso o se encuentra vacia";


                    }
                    if ( usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Fecha de Nacimiento se encuentra vacia ";


                    }
                    if (usuario.Sexo == "" || usuario.Sexo== null)
                    {


                        errorRegistro.ErrorMessage += "La columna Sexo se encuentra vacia";


                    }
                    if (usuario.Telefono.Length != 10 || usuario.Telefono == "" || usuario.Telefono == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Telefono contiene un valor muy extenso o se encuentra vacia";


                    }
                    if (usuario.Celular.Length != 10 || usuario.Celular == "" || usuario.Celular == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Celular contiene un valor muy extenso o se encuentra vacia";


                    }
                    //if ( usuario.Estatus != true || usuario.Estatus != false)
                    //{


                    //    result.ErrorMessage += "El Estatus esta vacio";


                    //}
                
                    if (usuario.Curp.Length != 18 || usuario.Curp == "" || usuario.Curp == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Curp contiene un valor muy extenso o se encuentra vacia";


                    }
                    if ( usuario.Rol.IdRol == 0)
                    {


                        errorRegistro.ErrorMessage += "La columna Rol se encuentra vacia";


                    }
                    if (usuario.Direccion.Calle.Length == 50 || usuario.Direccion.Calle == "" || usuario.Direccion.Calle == null)
                    {


                        errorRegistro.ErrorMessage += "La columna Calle contiene un valor muy extenso o se encuentra vacia";


                    }
                    if ( usuario.Direccion.NumeroInterior == "" || usuario.Direccion.NumeroInterior == null)
                    {

                        
                        errorRegistro.ErrorMessage += "La columna NumeroInterior se encuentra vacia";


                    }
                    if (usuario.Direccion.NumeroExterior == "" || usuario.Direccion.NumeroExterior == null)
                    {


                        errorRegistro.ErrorMessage += "La columna NumeroExterior se encuentra vacia";


                    }
                    if (usuario.Direccion.Colonia.IdColonia == 0)
                    {


                        errorRegistro.ErrorMessage += "La columna Colonia se encuentra vacia";


                    }
                    if( errorRegistro.ErrorMessage != null)
                    {
                        result.Errores.Add(errorRegistro);
                    }

                    contador++;
            }

            return result;
        }
    }
}

    

//    public static ML.Result GetAllLINQ(ML.Usuario usuario)
//        {
//            ML.Result result = new ML.Result();

//            try
//            {
//                using (DL_EF.ANolascoProgramacionNCapasEntities context = new DL_EF.ANolascoProgramacionNCapasEntities())
//                {
//                    var query = (from usuarioDB in context.Usuarios
//                                 select new
//                                 {
//                                     IdUsuario = usuarioDB.IdUsuario,
//                                     UserName = usuarioDB.UserName,
//                                     Nombre = usuarioDB.Nombre,
//                                     ApellidoPaterno = usuarioDB.ApellidoPaterno,
//                                     ApellidoMaterno = usuarioDB.ApellidoMaterno,
//                                     Email = usuarioDB.Email,
//                                     Password = usuarioDB.Password,
//                                     FechaNacimiento = usuarioDB.FechaNacimiento,
//                                     Sexo = usuarioDB.Sexo,
//                                     Telefono = usuarioDB.Telefono,
//                                     Celular = usuarioDB.Celular,
//                                     Estatus = usuarioDB.Estatus,
//                                     Curp = usuarioDB.Curp,
//                                     IdRol = usuarioDB.IdRol,



//                                 }).ToList();
//                    if (query.Count > 0)
//                    {
//                        result.Objects = new List<object>();
//                        foreach (var objBD in query)
//                        {

//                            //   ML.Usuario usuario = new ML.Usuario();
//                            usuario.IdUsuario = objBD.IdUsuario;
//                            usuario.UserName = objBD.UserName;
//                            usuario.Nombre = objBD.Nombre;
//                            usuario.ApellidoPaterno = objBD.ApellidoPaterno;
//                            usuario.ApellidoMaterno = objBD.ApellidoMaterno;
//                            usuario.Email = objBD.Email;
//                            usuario.Password = objBD.Password;
//                            usuario.FechaNacimiento = objBD.FechaNacimiento.ToString();
//                            usuario.Sexo = objBD.Sexo;
//                            usuario.Telefono = objBD.Telefono;
//                            usuario.Celular = objBD.Celular;
//                            usuario.Estatus = Convert.ToBoolean(objBD.Estatus);
//                            usuario.Curp = objBD.Curp;
//                            usuario.IdRol = Convert.ToInt32( objBD.IdRol);



//                            result.Objects.Add(Usuario);

//                        }
//                        result.Correct

//                    }
//            catch (Exception ex)
//            {
//                result.Correct = false;
//                result.ErrorMessage = ex.Message;
//                result.Ex = ex;
//            }
//            return result;
//        }


//    }

//}











//}















