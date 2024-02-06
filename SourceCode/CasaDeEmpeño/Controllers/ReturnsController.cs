using CasaDeEmpeño.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace CasaDeEmpeño.Controllers
{
    public class ReturnsController : Controller
    {

        private ConfiguracionVencimiento configuracionVencimiento;
        // GET: Returns
        public ActionResult Devoluciones()
        {
            return View();
        }

        public string ObtenerConfiguracionVencimiento(string path)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            string tiempoVencimiento = "";
            
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT c.idConfiguracionVencimiento, c.tiempoVencimiento FROM ConfiguracionVencimiento c
                                  WHERE ISNull(c.esActual, 0) = 1";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tiempoVencimiento = (ds.Tables[0].Rows[i]["tiempoVencimiento"].ToString()); ;

                    }
                }

                return tiempoVencimiento;

                
            }
            catch (Exception ex)
            {
                Utils.Log("Error ... " + ex.Message);
                Utils.Log(ex.StackTrace);
                return tiempoVencimiento;
            }

            finally
            {
                conn.Close();
            }
        }

        [WebMethod]
        public ActionResult ObtenerDevoluciones(string path)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            List<Devolucion> items = new List<Devolucion>();


            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT d.idDevolucion, d.comentarioDevolucion, d.fechaDevolucion, p.idProducto, p.nombreProducto FROM Devolucion d
                                  INNER JOIN Producto p ON (d.idProducto=p.idProducto)
                                  ORDER BY d.idDevolucion";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Devolucion item = new Devolucion();
                        Producto producto = new Producto();
                        item.IdDevolucion = int.Parse(ds.Tables[0].Rows[i]["idDevolucion"].ToString());
                        item.ComentarioDevolucion = (ds.Tables[0].Rows[i]["comentarioDevolucion"].ToString());
                        item.FechaDevolucion = (ds.Tables[0].Rows[i]["fechaDevolucion"].ToString());
                        producto.IdProducto = int.Parse(ds.Tables[0].Rows[i]["idProducto"].ToString());
                        producto.NombreProducto = (ds.Tables[0].Rows[i]["nombreProducto"].ToString());
                        

                      
                        item.producto = producto;

                        items.Add(item);


                    }
                }

                return Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Utils.Log("Error ... " + ex.Message);
                Utils.Log(ex.StackTrace);
                return Json(items, JsonRequestBehavior.AllowGet);
            }

            finally
            {
                conn.Close();
            }
        }

        [WebMethod]
        public object GuardarDevolucion(string path, Devolucion devolucion)
        {

            Utils.Log("\n==>INICIANDO Método-> " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n");
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {




                conn.Open();
                string sql = "";

                sql =
                    sql = @" INSERT INTO Devolucion(comentarioDevolucion, fechaDevolucion, idProducto) 
                    VALUES (@comentarioDevolucion, @fechaDevolucion, @idProducto) ";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@comentarioDevolucion", SqlDbType.VarChar).Value = devolucion.ComentarioDevolucion;
                cmd.Parameters.Add("@fechaDevolucion", SqlDbType.VarChar).Value = DateTime.Now;
                cmd.Parameters.Add("@idProducto", SqlDbType.VarChar).Value = devolucion.IdProducto;



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


        [WebMethod]
        public object ComprobarVigenciaDevolucion(string path, string idProducto)
        {
            int r = 0;
            string configuracionVencimiento = ObtenerConfiguracionVencimiento(path);

            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            Producto item = new Producto();
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = "SELECT p.idProducto, p.fechaDeIngreso FROM Producto p Where idProducto=@idProducto";

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");
                Utils.Log("idProducto =  " + idProducto);

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);
                adp.SelectCommand.Parameters.AddWithValue("@idProducto", idProducto);

                adp.Fill(ds);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        item = new Producto();

                        item.IdProducto = int.Parse(ds.Tables[0].Rows[i]["idProducto"].ToString());
                        item.FechaDeIngreso = (ds.Tables[0].Rows[i]["fechaDeIngreso"].ToString());


                    }
                }

                DateTime dateIngresoProducto = DateTime.Parse(item.FechaDeIngreso);

                TimeSpan tiempoVencimiento = TimeSpan.Parse(configuracionVencimiento);

                DateTime fechaActual = DateTime.Now;

                TimeSpan diferencia = fechaActual - dateIngresoProducto;

                if(diferencia > tiempoVencimiento)
                {
                    r= 1;
                }
                else
                {
                    r= 0;
                }

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
    }
}