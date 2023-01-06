using CRUD_Iventario.Data;
using CRUD_Iventario.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_Iventario.Controllers
{
    public class ProductosController : Controller
    {
        public readonly Contexto _contexto;

        public ProductosController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            using (SqlConnection con = new(_contexto.Conexion))
            {
                List<Productos> lista_product = new();
                using (SqlCommand cmd = new("istar_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        lista_product.Add(new Productos
                        {
                            Id = (int)rd["id"],
                            codigo = rd["codigo"].ToString(),
                            nombre = rd["nombre"].ToString(),
                            marca = rd["marca"].ToString(),
                            stock = (int)rd["stock"],
                            precio = (int)rd["precio"],

                        });
                    }
                }
                ViewBag.listado = lista_product;
                return View();
            }
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Productos pro)
        {
            try
            {



                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("insertar_productos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = pro.codigo;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = pro.nombre;
                        cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = pro.marca;
                        cmd.Parameters.Add("@stock", SqlDbType.Int).Value = pro.stock;
                        cmd.Parameters.Add("@precio", SqlDbType.Int).Value = pro.precio;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }



            }
            catch (System.Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            using (SqlConnection con = new(_contexto.Conexion))
            {
                Productos registro = new();
                using (SqlCommand cmd = new("buscar_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();

                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id = (int)dt.Rows[0][0];
                    registro.codigo = dt.Rows[0][1].ToString();
                    registro.nombre = dt.Rows[0][2].ToString();
                    registro.marca = dt.Rows[0][3].ToString();
                    registro.stock = (int)dt.Rows[0][4];
                    registro.precio = (int)dt.Rows[0][5];
                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult Editar(Productos pro)
        {
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {

                    using (SqlCommand cmd = new("actualizar_productos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = pro.Id;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = pro.codigo;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = pro.nombre;
                        cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = pro.marca;
                        cmd.Parameters.Add("@stock", SqlDbType.Int).Value = pro.stock;
                        cmd.Parameters.Add("@precio", SqlDbType.Int).Value = pro.precio;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            using (SqlConnection con = new(_contexto.Conexion))
            {
                Productos registro = new();
                using (SqlCommand cmd = new("buscar_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();

                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.codigo = dt.Rows[0][1].ToString();
                    registro.nombre = dt.Rows[0][2].ToString();
                    registro.marca = dt.Rows[0][3].ToString();
                    registro.stock = (int)dt.Rows[0][4];
                    registro.precio = (int)dt.Rows[0][5];
                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult Eliminar(Productos pro)
        {
            using (SqlConnection con = new(_contexto.Conexion))
            {
                using (SqlCommand cmd = new("liminar_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = pro.Id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
        }
    }
}