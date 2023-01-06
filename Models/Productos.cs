using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Iventario.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string? codigo { get; set; }
        public string? nombre { get; set; }
        public string? marca { get; set; }

        public int stock { get; set; }
        public int precio { get; set; }

        [NotMapped]
        public virtual ICollection<Productos> Es { get; } = new List<Productos>();
    }
}