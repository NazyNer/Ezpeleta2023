using System.ComponentModel.DataAnnotations;

namespace Ezpeleta2023.Models;

public class Categoria
{
    [Key]
    public int CategoriaID { get; set; }
    public string? Descripcion { get; set; }
    public bool Eliminado { get; set; }
    public virtual ICollection<SubCategorias>? subCategorias { get; set; }
}