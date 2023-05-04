using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ezpeleta2023.Data;
using Ezpeleta2023.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ezpeleta2023.Controllers;
public class ServiciosController : Controller
{
  private readonly ILogger<ServiciosController>_logger;
  private Ezpeleta2023DbContext _context;
    public ServiciosController(ILogger<ServiciosController>logger,Ezpeleta2023DbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var SubCategoria = _context.SubCategorias.Where(p => p.SubEliminado == false).ToList();
        ViewBag.SubCategoriaID = new SelectList(SubCategoria.OrderBy(p => p.SubDescripcion), "SubCategoriaID", "SubDescripcion");
        return View();
    }
    public JsonResult BuscarServicios(int servicioID = 0)
    {
        var serviciosDB = _context.Servicios.ToList();
        var servicios = new ServiciosVista();
        foreach (var servicio in serviciosDB){
            var categoriaDescripcion = _context.SubCategorias.Where(sc => sc.SubCategoriaID == servicio.SubCategoriaID && sc.SubEliminado == false).FirstOrDefault();
            servicios.ServicioID = servicio.ServicioID;
            servicios.SubCategoriaID = servicio.SubCategoriaID;
            servicios.CategoriaDescripcion = categoriaDescripcion.CategoriaDescripcion;
            servicios.Telefono = servicio.Telefono;
            servicios.Descripcion = servicio.Descripcion;
            servicios.Desabilitado = servicio.Desabilitado;
            servicios.SubDescripcion = servicio.SubDescripcion;
        }
        if(servicioID > 0 ){
            var servicioSeleccionado = serviciosDB.Where(s => s.ServicioID == servicioID).OrderBy(s => s.Descripcion).FirstOrDefault();
            var categoriaDescripcion = _context.SubCategorias.Where(sc => sc.SubCategoriaID == servicioSeleccionado.SubCategoriaID && sc.SubEliminado == false).FirstOrDefault();
            var mostrarServicio = new ServiciosVista(){
                ServicioID = servicioSeleccionado.ServicioID,
                SubCategoriaID = servicioSeleccionado.SubCategoriaID,
                CategoriaDescripcion = categoriaDescripcion.CategoriaDescripcion,
                Telefono = servicioSeleccionado.Telefono,
                Descripcion = servicioSeleccionado.Descripcion,
                Desabilitado = servicioSeleccionado.Desabilitado,
                SubDescripcion = servicioSeleccionado.SubDescripcion
            };
            return Json(mostrarServicio);
        }
        return Json(servicios);
    }
    
}
