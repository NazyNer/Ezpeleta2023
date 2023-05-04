using System.ComponentModel.DataAnnotations;

namespace Ezpeleta2023.Models;

public class Servicios{
  [Key]
  public int ServicioID { get; set; }
  public string? Descripcion { get; set; }
  public string? Direccion { get; set; }
  public int Telefono { get; set; }
  public int SubCategoriaID { get; set; }
  public string? SubDescripcion { get; set; }
  public bool Desabilitado { get; set; }
  public virtual SubCategorias? SubCategorias { get; set; }
}
public class ServiciosVista{
  public int ServicioID { get; set; }
  public string? Descripcion { get; set; }
  public string? Direccion { get; set; }
  public int Telefono { get; set; }
  public int SubCategoriaID { get; set; }
  public string? SubDescripcion { get; set; }
  public bool Desabilitado { get; set; }
  public string? CategoriaDescripcion { get; set; }
  public virtual SubCategorias? SubCategorias { get; set; }
}