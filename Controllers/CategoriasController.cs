using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ezpeleta2023.Data;
using Ezpeleta2023.Models;

namespace Ezpeleta2023.Controllers;

public class CategoriasController : Controller
{
    private readonly ILogger<CategoriasController>_logger;
    private Ezpeleta2023DbContext _context;
    public CategoriasController(ILogger<CategoriasController>logger,Ezpeleta2023DbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
    public JsonResult BuscarCategorias(int categoriaId = 0)
    {
        var categorias = _context.Categorias.ToList();
        if(categoriaId > 0 ){
            categorias = categorias.Where(c => c.CategoriaID == categoriaId).OrderBy(c => c.Descripcion).ToList();
        }
        return Json(categorias);
    }
    public JsonResult GuardarCategoria(int categoriaID, string descripcion)
    {
        bool resultado = false;
        if(!string.IsNullOrEmpty(descripcion)){

            //SI ES 0 QUIERE DECIR QUE ESTA CREANDO LA CATEGORIA
            if(categoriaID == 0)
            {
                //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMA DESCRIPCION
                var categoriaOriginal = _context.Categorias.Where(c => c.Descripcion == descripcion).FirstOrDefault();
                if(categoriaOriginal == null){
                    //DECLARAMOS EL OBJETO DADO EL VALOR
                    var categoriaGuardar = new Categoria{
                        Descripcion = descripcion
                    };
                    _context.Add(categoriaGuardar);
                    _context.SaveChanges();
                    resultado = true;
                }
            }else{
                //BUSCAMOS EN LA TABLA SI EXISTE CON LA MISMA DESCRIPCION Y DISTINTO ID DE REGISTRO AL QUE ESTAMOS EDITANDO
                var categoriaOriginal = _context.Categorias.Where(c => c.Descripcion == descripcion && c.CategoriaID != categoriaID).FirstOrDefault();
                if(categoriaOriginal == null){
                    //CREAR VARIABLE QUE GUARDE EL OBJETO SEGUN EL ID DESEADO
                    var categoriaEditar = _context.Categorias.Find(categoriaID);
                    if(categoriaEditar != null){
                        categoriaEditar.Descripcion = descripcion;
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
            }
        }
        return Json(resultado);
    }
    public JsonResult eliminarCategoria(int categoriaID){
        bool resultado = false;
            //SI ES DISTINTO A 0 QUIERE DECIR QUE ESTA ELIMINANDO LA CATEGORIA
            if(categoriaID != 0)
            {
                //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMO ID
                var categoriaOriginal = _context.Categorias.Find(categoriaID);
                //SI LA CATEGORIA NO ESTE ELIMINADA PROCEDEMOS A HACERLO
                if(categoriaOriginal?.Eliminado == false)
                {
                    categoriaOriginal.Eliminado = true;
                    _context.SaveChanges();
                    resultado = true;
                }else{
                    categoriaOriginal.Eliminado = false;
                    _context.SaveChanges();
                    resultado = true;
                }
            }
            return Json(resultado);
            }
}