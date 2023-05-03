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
}