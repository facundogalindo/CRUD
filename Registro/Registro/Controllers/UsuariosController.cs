using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using Registro.Models;
using System.Reflection.Metadata.Ecma335;

namespace Registro.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IConfiguration Configuration { get; }
        public UsuariosController(IConfiguration configuration) {
            Configuration = configuration;

        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario) {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("sp_registrar", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = usuario.Nombre;
                    cmd.Parameters.Add("@Edad", System.Data.SqlDbType.Int).Value = usuario.Edad;
                    cmd.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = usuario.Correo;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Index");
    }
}
}

        
   

