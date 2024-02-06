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
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Ventas()
        {
            return View();
        }


        [WebMethod]
        public ActionResult ObtenerVentas(string path)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            List<Venta> items = new List<Venta>();


            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT v.idVenta, v.precioVenta, v.vendido, p.idProducto, 
                    p.nombreProducto, p.estadoProducto, tp.idTipoProducto, tp.tipoProducto
                    FROM Venta v 
                    INNER JOIN Producto p ON v.idProducto = p.idProducto
                    INNER JOIN TipoProducto tp ON p.idTipoProducto = tp.idTipoProducto
                    ORDER BY v.idVenta";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Venta item = new Venta();
                        Producto producto = new Producto();
                        TipoProducto tipoProducto = new TipoProducto();
                        item.IdVenta = int.Parse(ds.Tables[0].Rows[i]["idVenta"].ToString());
                        item.Vendido = int.Parse(ds.Tables[0].Rows[i]["vendido"].ToString());
                        item.PrecioVenta = float.Parse(ds.Tables[0].Rows[i]["precioVenta"].ToString());
                        producto.IdProducto = int.Parse(ds.Tables[0].Rows[i]["idProducto"].ToString());
                        producto.NombreProducto = (ds.Tables[0].Rows[i]["nombreProducto"].ToString());
                        producto.EstadoProducto = (ds.Tables[0].Rows[i]["estadoProducto"].ToString());
                        tipoProducto.IdTipoProducto = int.Parse(ds.Tables[0].Rows[i]["idTipoProducto"].ToString());
                        tipoProducto.TipoProductoNombre = (ds.Tables[0].Rows[i]["tipoProducto"].ToString());

                        producto.tipoProducto = tipoProducto;
                        item.producto = producto;

                        string botones = "";

                        if (item.Vendido == 1)
                        {
                            botones = "<button  onclick=''  class='btn btn-edit btn-sm'> " +
                                "<span class='fa fa-edit mr-1'></span>Producto vendido</button>";
                        }else
                        {

                            botones = "<button  onclick='sales.hacerOferta(" + item.IdVenta + ")'  class='btn btn-primary btn-sm'>" +
                                    " <span class='fa fa-edit mr-1'></span>Hacer oferta</button>";
                            botones += "&nbsp; <button  onclick='sales.verOfertas(" + item.IdVenta + ")'  " +
                                    "class='btn btn-success btn-sm'> <span class='fa fa-remove mr-1'></span>Ver ofertas</button>";
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
        public ActionResult ObtenerOfertas(string path, string idVenta)
        {
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;

            SqlConnection conn = new SqlConnection(strConexion);
            List<Oferta> items = new List<Oferta>();


            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT o.idOferta, o.nombrePersonaOferta, o.numeroCelular, o.montoOferta FROM Oferta o WHERE o.idVenta=@idVenta";

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);
                adp.SelectCommand.Parameters.AddWithValue("@idVenta", idVenta);

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Oferta item = new Oferta();
                        
                        item.IdOferta = int.Parse(ds.Tables[0].Rows[i]["idOferta"].ToString());
                        item.NombrePersonaOferta = (ds.Tables[0].Rows[i]["nombrePersonaOferta"].ToString());
                        item.NumeroCelular = (ds.Tables[0].Rows[i]["numeroCelular"].ToString());
                        item.MontoOferta = float.Parse(ds.Tables[0].Rows[i]["numeroCelular"].ToString());


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
        public object PonerProductoEnVenta(string path, Venta venta)
        {

            Utils.Log("\n==>INICIANDO Método-> " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n");
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {

                


                conn.Open();
                string sql = "";

                sql =
                    sql = @" INSERT INTO Venta(precioVenta, vendido, idProducto) 
                    VALUES (@precioVenta, 0, @idProducto) ";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@precioVenta", SqlDbType.VarChar).Value = venta.PrecioVenta;
                cmd.Parameters.Add("@idProducto", SqlDbType.VarChar).Value = venta.IdProducto;



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
        public object GuardarOferta(string path, Oferta oferta)
        {

            Utils.Log("\n==>INICIANDO Método-> " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n");
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {




                conn.Open();
                string sql = "";

                sql =
                    sql = @" INSERT INTO Oferta(nombrePersonaOferta, numeroCelular, montoOferta, idVenta) 
                    VALUES (@nombrePersonaOferta, @numeroCelular,  @montoOferta, @idVenta) ";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@nombrePersonaOferta", SqlDbType.VarChar).Value = oferta.NombrePersonaOferta;
                cmd.Parameters.Add("@numeroCelular", SqlDbType.VarChar).Value = oferta.NumeroCelular;
                cmd.Parameters.Add("@montoOferta", SqlDbType.VarChar).Value = oferta.MontoOferta;
                cmd.Parameters.Add("@idVenta", SqlDbType.VarChar).Value = oferta.IdVenta;



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
        public object VenderAOfertaMasAlta(string path, string idVenta)
        {

            Utils.Log("\n==>INICIANDO Método-> " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n");
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {

                conn.Open();
                string sql = "";

                    sql = @" UPDATE Venta
                             SET precioEnQueSeVendio = o.montoOferta,
                                 vendido = 1
                             FROM Venta
                             JOIN (
                             SELECT idVenta, MAX(montoOferta) AS montoOferta
                             FROM Oferta
                             WHERE idVenta = @idVenta
                             GROUP BY idVenta
                                   ) o ON Venta.idVenta = o.idVenta
                             WHERE Venta.idVenta = @idVenta;";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@idVenta", SqlDbType.VarChar).Value = idVenta;
         



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
        public object ComprobarNumeroOfertas(string path, string idVenta)
        {
            int r = 0;
            
            string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[path].ConnectionString;
            Producto item = new Producto();
            SqlConnection conn = new SqlConnection(strConexion);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                string query = "SELECT o.idOferta, o.nombrePersonaOferta FROM Oferta o Where idVenta=@idVenta";

                Utils.Log("\nMétodo-> " +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "\n" + query + "\n");
                Utils.Log("idVenta =  " + idVenta);

                SqlDataAdapter adp = new SqlDataAdapter(query, conn);
                adp.SelectCommand.Parameters.AddWithValue("@idVenta", idVenta);

                adp.Fill(ds);


                if (ds.Tables[0].Rows.Count >= 3)
                {
                    r = 1;
                }
                else
                {
                    r = 0;
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