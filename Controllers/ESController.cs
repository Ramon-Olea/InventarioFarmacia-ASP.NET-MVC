using CRUD_Iventario.Data;
using CRUD_Iventario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_Iventario.Controllers
{
    public class ESController : Controller
    {
        public readonly Contexto _contexto;

        public ESController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            using (SqlConnection con = new(_contexto.Conexion))
            {
                List<ES> lista_es = new();
                using (SqlCommand cmd = new("listar_es", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        lista_es.Add(new ES
                        {
                            Id = (int)rd["id"],
                            IdProductos = (int)rd["id_productos"],
                            Cantidad = (int)rd["cantidad"],
                            Descripcion = rd["descripcion"].ToString(),
                            Fecha = (DateTime)rd["fecha"],

                        });
                    }
                }
                ViewBag.listado = lista_es;
                return View();
            }
        }

        public IActionResult Crear()
        {
            //ViewData["IdProductos"] = new SelectList(_contexto.Conexion, "Id", "Id");
            return View();
        }

        [HttpPost]
        public IActionResult Crear(ES es)
        {
            try
            {



                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("ProcedureEntradas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_productos", SqlDbType.Int).Value = es.IdProductos;
                        cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = es.Cantidad;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "Entrada";
                        cmd.Parameters.Add("@ModeProcedure", SqlDbType.Int).Value = 1;
                        cmd.Parameters.Add("@MessageCode", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@MessageValue", SqlDbType.VarChar).Value = "";



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


        public IActionResult Salida()
        {
            //ViewData["IdProductos"] = new SelectList(_contexto.Conexion, "Id", "Id");
            return View();
        }

        [HttpPost]
        public IActionResult Salida(ES es)
        {
            try
            {



                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("ProcedureSalidas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_productos", SqlDbType.Int).Value = es.IdProductos;
                        cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = es.Cantidad;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "Salida";
                        cmd.Parameters.Add("@ModeProcedure", SqlDbType.Int).Value = 1;
                        cmd.Parameters.Add("@MessageCode", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@MessageValue", SqlDbType.VarChar).Value = "";



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
        public IActionResult Editar(Productos ess)
        {
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {

                    using (SqlCommand cmd = new("actualizar_productos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ess.Id;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = ess.codigo;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = ess.nombre;
                        cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = ess.marca;
                        cmd.Parameters.Add("@stock", SqlDbType.Int).Value = ess.stock;
                        cmd.Parameters.Add("@precio", SqlDbType.Int).Value = ess.precio;
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
        public IActionResult Eliminar(Productos rmn)
        {
            using (SqlConnection con = new(_contexto.Conexion))
            {
                using (SqlCommand cmd = new("liminar_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = rmn.Id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
        }
    }
}