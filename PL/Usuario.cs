using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        //public static void Add() //logica para pedir la informacion
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("Ingresa el nombre del usuario:");
        //    usuario.Nombre = Console.ReadLine();
        //    Console.WriteLine("Ingresa el genero:");
        //    usuario.Genero = Console.ReadLine();
        //    Console.WriteLine("Ingresa el promedio:");
        //    usuario.Promedio = Convert.ToDecimal(Console.ReadLine());
        //    Console.WriteLine("Ingresa el grado:");
        //    usuario.Grado = Convert.ToInt32(Console.ReadLine());

        //    BL.Usuario.Add(usuario);
        //}
        //public static void Delete() //logica para pedir la informacion
        //{
        //    Console.WriteLine("Ingresa el Id del usuario:");

        //    int Id = Convert.ToInt32(Console.ReadLine());


        //    BL.Usuario.Delete(Id);
        //}

        //public static void Update() //logica para pedir la informacion
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("Ingresa el id:");
        //    usuario.Id = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("Ingresa el nombre del usuario:");
        //    usuario.Nombre = Console.ReadLine();
        //    Console.WriteLine("Ingresa el genero:");
        //    usuario.Genero = Console.ReadLine();
        //    Console.WriteLine("Ingresa el promedio:");
        //    usuario.Promedio = Convert.ToDecimal(Console.ReadLine());
        //    Console.WriteLine("Ingresa el grado:");
        //    usuario.Grado = Convert.ToInt32(Console.ReadLine());

        //    BL.Usuario.Update(usuario);

        //}
        //public static void GetAll() //logica para pedir la informacion
        //{

        //    ML.Result result = BL.Usuario.GetAll();

        //    if (result.Correct)
        //    {
        //        //mostrar los registros
        //        foreach (ML.Usuario usuario in result.Objects)
        //        {
        //            Console.WriteLine(usuario.Id);
        //            Console.WriteLine(usuario.Nombre);
        //            Console.WriteLine(usuario.Genero);
        //            Console.WriteLine(usuario.Promedio);
        //            Console.WriteLine(usuario.Grado);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Hubo un error " + result.ErrorMessage);
        //    }
        //public static void GetById() //logica para pedir la informacion
        //{
        //    Console.WriteLine("Dame el id que quieres buscar:");
        //    int IdUsuario = Convert.ToInt32(Console.ReadLine());

        //    ML.Result result = BL.Usuario.GetById(IdUsuario);

        //    if (result.Correct)
        //    {
        //        //mostrar los registros
        //        ML.Usuario usuario = (ML.Usuario)result.Object;

        //        Console.WriteLine(usuario.Nombre);
        //        Console.WriteLine(usuario.Genero);
        //        Console.WriteLine(usuario.Promedio);
        //        Console.WriteLine(usuario.Grado);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Hubo un error " + result.ErrorMessage);
        //    }

        //public static void Add() //logica para pedir la informacion
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("ingresa el username:");
        //    usuario.UserName = Console.ReadLine();
        //    Console.WriteLine("ingresa el nombre:");
        //    usuario.Nombre = Console.ReadLine();
        //    Console.WriteLine("ingresa el apeliidopaterno:");
        //    usuario.ApellidoPaterno = Console.ReadLine();
        //    Console.WriteLine("ingresa el apellidomaterno:");
        //    usuario.ApellidoMaterno = Console.ReadLine();
        //    Console.WriteLine("ingresa el email:");
        //    usuario.Email = Console.ReadLine();
        //    Console.WriteLine("ingresa el password:");
        //    usuario.Password = Console.ReadLine();
        //    Console.WriteLine("ingresa el fechanacimiento:");
        //    usuario.FechaNacimiento = Console.ReadLine();
        //    Console.WriteLine("ingresa el sexo:");
        //    usuario.Sexo = Console.ReadLine();
        //    Console.WriteLine("ingresa el celular:");
        //    usuario.Celular = Console.ReadLine();
        //    Console.WriteLine("ingresa el telefono:");
        //    usuario.Telefono = Console.ReadLine();
        //    Console.WriteLine("ingresa el estatus:");
        //    usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
        //    Console.WriteLine("ingresa el curp:");
        //    usuario.Curp = Console.ReadLine();
        //    //console.writeline("ingresa el imagen:");
        //    //usuario.imagen = console.readline();
        //    Console.WriteLine("ingresa el idrol:");
        //    usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());



        //    BL.Usuario.AddEF(usuario);
        //}

        //public static void Delete() //logica para pedir la informacion
        //{
        //    Console.WriteLine("Ingresa el Id del usuario:");

        //    int IdUsuario = Convert.ToInt32(Console.ReadLine());


        //    BL.Usuario.DeleteEF(IdUsuario);
        //}

    
        //public static void Update() //logica para pedir la informacion
        //{
        //    ML.Usuario usuario = new ML.Usuario();

        //    Console.WriteLine("Ingresa el IdUsuario:");
        //    usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("Ingresa el username:");
        //    usuario.UserName = Console.ReadLine();
        //    Console.WriteLine("Ingresa el nombre:");
        //    usuario.Nombre = Console.ReadLine();
        //    Console.WriteLine("Ingresa el apeliidopaterno:");
        //    usuario.ApellidoPaterno = Console.ReadLine();
        //    Console.WriteLine("Ingresa el apellidomaterno:");
        //    usuario.ApellidoMaterno = Console.ReadLine();
        //    Console.WriteLine("Ingresa el email:");
        //    usuario.Email = Console.ReadLine();
        //    Console.WriteLine("Ingresa el password:");
        //    usuario.Password = Console.ReadLine();
        //    Console.WriteLine("Ingresa el fechanacimiento:");
        //    usuario.FechaNacimiento = Console.ReadLine();
        //    Console.WriteLine("Ingresa el sexo:");
        //    usuario.Sexo = Console.ReadLine();
        //    Console.WriteLine("Ingresa el celular:");
        //    usuario.Celular = Console.ReadLine();
        //    Console.WriteLine("Ingresa el telefono:");
        //    usuario.Telefono = Console.ReadLine();
        //    Console.WriteLine("Ingresa el estatus:");
        //    usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
        //    Console.WriteLine("Ingresa el curp:");
        //    usuario.Curp = Console.ReadLine();
        //    //Console.WriteLine("Ingresa el imagen:");
        //    //usuario.Imagen = Console.ReadLine();
        //    Console.WriteLine("Ingresa el idrol:");
        //    usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

        //    BL.Usuario.UpdateLINQ(usuario);

        //}

        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            Console.WriteLine("Entrando a carga masiva");
            string ruta = @"C:\Users\digis\OneDrive\Documents\Andrea Guadlupe Nolasco Lara\bd.txt";
            try
            {
                StreamReader streamReader = new StreamReader(ruta);
                string fila = "";

                streamReader.ReadLine();

                while ((fila = streamReader.ReadLine()) != null)
                {
                    string[] valores = fila.Split('|');
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.UserName = valores[0];
                    usuario.Nombre = valores[1];
                    usuario.ApellidoPaterno = valores[2];
                    usuario.ApellidoMaterno = valores[3];
                    usuario.Email = valores[4];
                    usuario.Password = valores[5];
                    usuario.FechaNacimiento = valores[6];
                    usuario.Sexo = valores[7];
                    usuario.Telefono = valores[8];
                    usuario.Celular = valores[9];
                    usuario.Estatus =Convert.ToBoolean(valores[10].ToString());
                    usuario.Curp = valores[11];
                    usuario.Rol.IdRol = Convert.ToInt32(valores[12]);
                    //usuario.Rol.Nombre = valores[3];
                    usuario.Direccion.Calle = valores[13];
                    usuario.Direccion.NumeroInterior = valores[14];
                    usuario.Direccion.NumeroExterior = valores[15];
                    //usuario.Direccion.Colonia.Nombre = valores[16];
                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(valores[16]);
                    //usuario.Direccion.Colonia.Municipio.Nombre = valores[3];
                    //usuario.Direccion.Colonia.Municipio.Estado.Nombre = valores[3];

                    BL.Usuario.AddEF(usuario);

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




    

//        public static void GetAll() //logica para pedir la informacion
//        {

//            ML.Result result = BL.Usuario.GetAllEF();

//            if (result.Correct)
//            {
//                //mostrar los registros
//                foreach (ML.Usuario usuario in result.Objects)
//                {
//                    Console.WriteLine(usuario.IdUsuario);
//                    Console.WriteLine(usuario.UserName);
//                    Console.WriteLine(usuario.Nombre);
//                    Console.WriteLine(usuario.ApellidoPaterno);
//                    Console.WriteLine(usuario.ApellidoPaterno);
//                    Console.WriteLine(usuario.Email);
//                    Console.WriteLine(usuario.Password);
//                    Console.WriteLine(usuario.FechaNacimiento);
//                    Console.WriteLine(usuario.Sexo);
//                    Console.WriteLine(usuario.Telefono);
//                    Console.WriteLine(usuario.Celular);
//                    Console.WriteLine(usuario.Estatus);
//                    Console.WriteLine(usuario.Curp);
//                    Console.WriteLine(usuario.Rol.IdRol);


//                }
//            }
//            else
//            {
//                Console.WriteLine("Hubo un error " + result.ErrorMessage);
//            }
//        }
//        public static void GetById() //logica para pedir la informacion
//        {
//            Console.WriteLine("Dame el id que quieres buscar:");
//            int IdUsuario = Convert.ToInt32(Console.ReadLine());

//            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

//            if (result.Correct)
//            {
//                //mostrar los registros
//                ML.Usuario usuario = (ML.Usuario)result.Object;

//                Console.WriteLine(usuario.IdUsuario);
//                Console.WriteLine(usuario.UserName);
//                Console.WriteLine(usuario.ApellidoPaterno);
//                Console.WriteLine(usuario.ApellidoMaterno);
//                Console.WriteLine(usuario.Email);
//                Console.WriteLine(usuario.Password);
//                Console.WriteLine(usuario.FechaNacimiento);
//                Console.WriteLine(usuario.Sexo);
//                Console.WriteLine(usuario.Celular);
//                Console.WriteLine(usuario.Telefono);
//                Console.WriteLine(usuario.Estatus);
//                Console.WriteLine(usuario.Curp);
//                Console.WriteLine(usuario.Rol.IdRol);
//            }
//            else
//            {
//                Console.WriteLine("Hubo un error " + result.ErrorMessage);
//            }
//        }

//    }

//}




