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
    public class HomeController : Controller
    {
        public ActionResult Productos()
        {
            return View();
        }

        public ActionResult Ventas()
        {

            return View();
        }

        public ActionResult Devoluciones()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [WebMethod]
        public ActionResult ObtenerProductos(string path)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            List<Producto> items = new List<Producto>();


            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT p.idProducto, p.nombreProducto,  FORMAT(p.fechaDeIngreso, 
                     'yyyy-MM-dd') fechaDeIngreso, p.estadoProducto, p.valorCalculado, tp.idTipoProducto, tp.tipoProducto,
					 v.idVenta
                     FROM producto p
					 LEFT JOIN Venta v ON(p.idProducto=v.idProducto)
                     INNER JOIN TipoProducto tp ON (p.idTipoProducto=tp.idTipoProducto)
                     ORDER BY p.idProducto ";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Producto item = new Producto();
                        TipoProducto tipoProducto = new TipoProducto();
                        Venta venta = new Venta();
                        item.IdProducto = int.Parse(ds.Tables[0].Rows[i]["idProducto"].ToString()); 
                        item.NombreProducto = (ds.Tables[0].Rows[i]["nombreProducto"].ToString());
                        item.FechaDeIngreso = (ds.Tables[0].Rows[i]["fechaDeIngreso"].ToString());
                        item.EstadoProducto = (ds.Tables[0].Rows[i]["estadoProducto"].ToString());
                        item.ValorCalculado = float.Parse(ds.Tables[0].Rows[i]["valorCalculado"].ToString());
                        tipoProducto.IdTipoProducto = int.Parse(ds.Tables[0].Rows[i]["idTipoProducto"].ToString());
                        tipoProducto.TipoProductoNombre = (ds.Tables[0].Rows[i]["tipoProducto"].ToString());

                        bool tieneVenta = false;
                        if(ds.Tables[0].Rows[i]["idVenta"] != DBNull.Value)
                        {
                            venta.IdVenta = int.Parse(ds.Tables[0].Rows[i]["idVenta"].ToString());
                            tieneVenta = true;
                        }
                        
                        item.tipoProducto = tipoProducto;


                        string botones;

                        if(tieneVenta)
                        {
                            botones = "<button  onclick=''  class='btn btn-edit btn-sm'> " +
                                "<span class='fa fa-edit mr-1'></span>Vendido o en venta</button>";
                        }
                        else
                        {
                            botones = "<button  onclick='product.ponerProductoEnVenta(" + item.IdProducto + ")'  class='btn btn-success btn-sm'>" +
                                " <span class='fa fa-edit mr-1'></span>Poner en venta</button>";

                        }


                        item.Accion = botones;

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
        public ActionResult ObtenerProductosParaDevolucion(string path)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            List<Producto> items = new List<Producto>();


            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT d.idDevolucion, p.idProducto, p.nombreProducto,  FORMAT(p.fechaDeIngreso, 
                     'yyyy-MM-dd') fechaDeIngreso, p.estadoProducto, p.valorCalculado, tp.idTipoProducto, tp.tipoProducto,
					 v.idVenta
                     FROM producto p
					 LEFT JOIN Venta v ON(p.idProducto=v.idProducto)
                     INNER JOIN TipoProducto tp ON (p.idTipoProducto=tp.idTipoProducto)
                     LEFT JOIN Devolucion d ON (p.idProducto=d.idProducto)
                     ORDER BY p.idProducto ";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Producto item = new Producto();
                        TipoProducto tipoProducto = new TipoProducto();
                        Venta venta = new Venta();
                        Devolucion devolucion = new Devolucion();
                        item.IdProducto = int.Parse(ds.Tables[0].Rows[i]["idProducto"].ToString());
                        item.NombreProducto = (ds.Tables[0].Rows[i]["nombreProducto"].ToString());
                        item.FechaDeIngreso = (ds.Tables[0].Rows[i]["fechaDeIngreso"].ToString());
                        item.EstadoProducto = (ds.Tables[0].Rows[i]["estadoProducto"].ToString());
                        item.ValorCalculado = float.Parse(ds.Tables[0].Rows[i]["valorCalculado"].ToString());
                        tipoProducto.IdTipoProducto = int.Parse(ds.Tables[0].Rows[i]["idTipoProducto"].ToString());
                        tipoProducto.TipoProductoNombre = (ds.Tables[0].Rows[i]["tipoProducto"].ToString());
                        

                        bool tieneVenta = false;
                        if (ds.Tables[0].Rows[i]["idVenta"] != DBNull.Value)
                        {
                            venta.IdVenta = int.Parse(ds.Tables[0].Rows[i]["idVenta"].ToString());
                            tieneVenta = true;
                        }

                        bool tieneDevolucion = false;
                        if (ds.Tables[0].Rows[i]["idDevolucion"] != DBNull.Value)
                        {
                            devolucion.IdDevolucion = int.Parse(ds.Tables[0].Rows[i]["idDevolucion"].ToString());
                            tieneDevolucion = true;
                        }

                        item.tipoProducto = tipoProducto;


                        string botones;

                        if (tieneVenta || tieneDevolucion)
                        {
                            botones = "<button  onclick=''  class='btn btn-edit btn-sm'> " +
                                "<span class='fa fa-edit mr-1'></span>Vendido, en Venta o Devuelto</button>";
                        }
                        else
                        {
                            botones = "<button  onclick='returns.devolverProducto(" + item.IdProducto + ")'  class='btn btn-success btn-sm'>" +
                                " <span class='fa fa-edit mr-1'></span>Devolver</button>";

                        }


                        item.Accion = botones;

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
        public ActionResult ObtenerTipoProductos(string path)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            List<TipoProducto> items = new List<TipoProducto>();


            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT tp.idTipoProducto, tp.tipoProducto FROM TipoProducto tp";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TipoProducto item = new TipoProducto();
                        item.IdTipoProducto = int.Parse(ds.Tables[0].Rows[i]["idTipoProducto"].ToString());
                        item.TipoProductoNombre = (ds.Tables[0].Rows[i]["tipoProducto"].ToString());

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
        public object RegistrarProducto(string path, Producto producto)
        {

            Utils.Log("\n==>INICIANDO Método-> " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n");
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            SqlConnection conn = new SqlConnection(strConexion);


            try
            {

               


                conn.Open();
                string sql = "";

                sql =
                    sql = @" INSERT INTO Producto(nombreProducto, fechaDeIngreso, estadoProducto, valorCalculado, idTipoProducto) 
                    VALUES (@nombreProducto, @fechaDeIngreso, @estadoProducto, @valorCalculado, @idTipoProducto) ";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@nombreProducto", SqlDbType.VarChar).Value = producto.NombreProducto;
                cmd.Parameters.Add("@fechaDeIngreso", SqlDbType.VarChar).Value = DateTime.Now;
                cmd.Parameters.Add("@estadoProducto", SqlDbType.VarChar).Value = producto.EstadoProducto;
                cmd.Parameters.Add("@valorCalculado", SqlDbType.VarChar).Value = producto.ValorCalculado;
                cmd.Parameters.Add("@idTipoProducto", SqlDbType.VarChar).Value = producto.IdTipoProducto;




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
        public ActionResult GetProducto(string path, string idProducto)
        {

            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            Producto item = new Producto();
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = " SELECT p.idProducto , p.nombreProducto, p.estadoProducto, p.valorCalculado" +
                    "  FROM Producto p WHERE p.idProducto =  @idProducto ";

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
                        item.NombreProducto = (ds.Tables[0].Rows[i]["nombreProducto"].ToString());
                        item.EstadoProducto = (ds.Tables[0].Rows[i]["estadoProducto"].ToString());
                        item.ValorCalculado = float.Parse(ds.Tables[0].Rows[i]["valorCalculado"].ToString());


                    }
                }

                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Utils.Log("Error ... " + ex.Message);
                Utils.Log(ex.StackTrace);
                item.IdProducto = 0;
                return Json(item, JsonRequestBehavior.AllowGet);
            }

            finally
            {
                conn.Close();
            }

        }

    }
}