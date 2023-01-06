using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Iventario.Models
{
    public class ES
    {
        public int Id { get; set; }

        public int IdProductos { get; set; }

        public int Cantidad { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? Fecha { get; set; }


        public virtual Productos IdProductosNavigation { get; set; } = null!;
    }
}