using DataTable.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace DataTable.Controllers
{
    public class HomeController : Controller
    {
        private readonly string cadenaSQL;

        public HomeController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]

        public JsonResult ListaProductos() { 
        
            List<Empleado> lista = new List<Empleado>();

            using (var conexion = new SqlConnection(cadenaSQL)) { 
            
            conexion.Open();
                var cmd = new SqlCommand("sp_listar_Productos", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader()) {
                
                    while (dr.Read())
                    {
                        lista.Add(new Empleado
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            CodigoBarra = dr["CodigoBarra"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Precio = dr["Precio"].ToString()
                        });
                    }             
                }
            
            
            }
            return Json(new {data = lista });
        
        
        
        
        
        
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}