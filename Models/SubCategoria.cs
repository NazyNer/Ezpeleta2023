using System.ComponentModel.DataAnnotations;

namespace Ezpeleta2023.Models;

public class SubCategorias{

  [Key]
    public int SubCategoriaID { get; set; }
    public string? SubDescripcion { get; set; }
    public bool SubEliminado { get; set; }
    public int CategoriaID { get; set; }
    public string? CategoriaDescripcion { get; set; }
    public virtual Categoria? Categorias { get; set; }
    public virtual ICollection<Servicios>? Servicios { get; set; }
}