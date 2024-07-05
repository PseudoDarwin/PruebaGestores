using pruebaGestores.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pruebaGestores.Controllers
{
    public class HomeController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        public ActionResult Index()
        {
            List<ModelAsignacion> lstasig = new List<ModelAsignacion>();
            lstasig = MostrarAsignacion();
            return View(lstasig);
        }

        public ActionResult Gestor()
        {
            List<ModelGestor> lstgestor = new List<ModelGestor>();
            lstgestor = MostrarGestor();
            return View(lstgestor);
        }

        public ActionResult Saldo()
        {
            List<ModelSaldo> lstsaldo = new List<ModelSaldo>();
            lstsaldo = MostrarSaldos();
            return View(lstsaldo);
        }


        public List<ModelAsignacion> MostrarAsignacion()
        {
            List<ModelAsignacion> lstasig = new List<ModelAsignacion>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(@"select * from ConsultarMontos order by Nombre", oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModelAsignacion ModelAsignacion = new ModelAsignacion();
                            ModelAsignacion.Nombre = Convert.ToString(dr["Nombre"]);
                            ModelAsignacion.Monto = Convert.ToString(dr["Montos"]);


                            lstasig.Add(ModelAsignacion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstasig = null;
                Console.WriteLine(ex);
            }
            return lstasig;
        }


        public List<ModelSaldo> MostrarSaldos()
        {
            List<ModelSaldo> lstsaldo = new List<ModelSaldo>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(@"select * from Saldo ", oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModelSaldo ModelSaldo = new ModelSaldo();
                            ModelSaldo.IdSaldo = Convert.ToInt32(dr["Id"]);
                            ModelSaldo.Saldo = Convert.ToDouble(dr["Monto"]);


                            lstsaldo.Add(ModelSaldo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstsaldo = null;
                Console.WriteLine(ex);
            }
            return lstsaldo;
        }


        public List<ModelGestor> MostrarGestor()
        {
            List<ModelGestor> lstgestor = new List<ModelGestor>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(@"select * from Gestor ", oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModelGestor ModelGestor = new ModelGestor();
                            ModelGestor.IdGestor = Convert.ToInt32(dr["Id"]);
                            ModelGestor.Gestor = Convert.ToString(dr["Nombre"]);


                            lstgestor.Add(ModelGestor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstgestor = null;
                Console.WriteLine(ex);
            }
            return lstgestor;
        }
    }
}