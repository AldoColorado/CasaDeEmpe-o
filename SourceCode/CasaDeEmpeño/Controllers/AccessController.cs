using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using CasaDeEmpeño.Models;

namespace CasaDeEmpeño.Controllers
{
    public class AccessController : Controller
    {


        // GET: Access
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [WebMethod] 
        public ActionResult Loggearse(string path, Usuario usuario)
        {

            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            Console.WriteLine(strConexion);

            Usuario item = new Usuario();
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();


                string query = @"SELECT u.idUsuario, u.correo, u.tipoUsuario, u.password FROM Usuario u WHERE u.correo = @correo";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);
                adp.SelectCommand.Parameters.AddWithValue("@correo", usuario.Correo);


                adp.Fill(ds);

                if(ds.Tables[0].Rows.Count > 0)
                {

                   

                    Session["usuario"] = item;

                    string passwordEncriptadaAlamacenada = (ds.Tables[0].Rows[0]["password"].ToString());

                    if(passwordEncriptadaAlamacenada.Equals(CreateMD5Hash(usuario.Password)))
                    {
                        Utils.Log("Inicio de sesión exitoso para el usuario: " + usuario.Correo);

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            item = new Usuario();

                            item.IdUsuario = int.Parse(ds.Tables[0].Rows[i]["idUsuario"].ToString());
                            item.Correo = (ds.Tables[0].Rows[i]["correo"].ToString());
                            item.TipoUsuario = ds.Tables[0].Rows[i]["tipoUsuario"].ToString();


                        }

                        return Json(item, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Utils.Log("Correo o contrasenia incorrectos: " + usuario.Correo);
                        item.IdUsuario = 0;
                        return Json(item, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    item.IdUsuario = 0;
                    return Json(item, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                Utils.Log("Error mensaje ... " + ex.Message);
                Utils.Log(ex.StackTrace);
                item.IdUsuario = 0;
                return Json(item, JsonRequestBehavior.AllowGet);
            }

            finally
            {
               
                conn.Close();
            }

           
        }

        [WebMethod]
        public object Registrarse(string path, Usuario usuario)
        {

            Utils.Log("\n==>INICIANDO Método-> " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n");
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {

                string passwordEncriptada = CreateMD5Hash(usuario.Password);


                conn.Open();
                string sql = "";

                sql =
                    sql = @" INSERT INTO Usuario(correo, password, tipoUsuario) 
                    VALUES (@correo, @password, 'Administrador') ";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = passwordEncriptada;
                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = usuario.Correo;



                int r = cmd.ExecuteNonQuery();
                Utils.Log("Guardado -> OK ");



                return r;
            }
            catch (Exception ex)
            {
                Utils.Log("Error ... " + ex.Message);
                Utils.Log(ex.StackTrace);
                return -1; //Retornamos menos uno cuando se dió por alguna razón un error
            }

            finally
            {
                conn.Close();
            }


        }

        public string CreateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }


    }
}