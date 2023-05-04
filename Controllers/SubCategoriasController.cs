using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ezpeleta2023.Data;
using Ezpeleta2023.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ezpeleta2023.Controllers;

public class SubCategoriasController : Controller
{
    private readonly ILogger<SubCategoriasController>_logger;
    private Ezpeleta2023DbContext _context;
    public SubCategoriasController(ILogger<SubCategoriasController>logger,Ezpeleta2023DbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        var categoria = _context.Categorias?.Where(p => p.Eliminado == false).ToList();
        ViewBag.CategoriaID = new SelectList(categoria?.OrderBy(p => p.Descripcion), "CategoriaID", "Descripcion");
        return View();
    }

    public JsonResult BuscarSubCategorias(int subCategoriaId = 0)
    {
        var subCategorias = _context.SubCategorias?.ToList();
        if(subCategoriaId > 0 ){
            subCategorias = subCategorias?.Where(c => c.SubCategoriaID == subCategoriaId).OrderBy(c => c.SubDescripcion).ToList();
        }
        return Json(subCategorias);
    }
    public JsonResult GuardarSubCategoria(int subCategoriaId, string descripcion, int categoriaID){
        bool resultado = false;
        if(!string.IsNullOrEmpty(descripcion)){
            var Categoria = _context.Categorias?.Where(c => c.CategoriaID == categoriaID).FirstOrDefault();
            if (Categoria != null)
            {
                //VERIFICAMOS QUE LA CATEGORIA NO ESTE ELIMINADA
                if (Categoria.Eliminado == false)
                {
                    //SI ES 0 QUIERE DECIR QUE ESTA CREANDO LA SUBCATEGORIA
                    if(subCategoriaId == 0)
                    {
                        //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMA DESCRIPCION
                        var subCategoriaOriginal = _context.SubCategorias?.Where(c => c.SubDescripcion == descripcion).FirstOrDefault();
                        if(subCategoriaOriginal == null){
                            //DECLARAMOS EL OBJETO DADO EL VALOR
                            var subCategoriaGuardar = new SubCategorias{
                                SubDescripcion = descripcion,
                                CategoriaID = categoriaID,
                                CategoriaDescripcion = Categoria.Descripcion
                            };
                            _context.Add(subCategoriaGuardar);
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }else{
                        //BUSCAMOS EN LA TABLA SI EXISTE CON LA MISMA DESCRIPCION Y DISTINTO ID DE REGISTRO AL QUE ESTAMOS EDITANDO
                        var subCategoriaOriginal = _context.SubCategorias?.Where(c => c.SubDescripcion == descripcion && c.SubCategoriaID != subCategoriaId && c.CategoriaID == categoriaID).FirstOrDefault();
                        if(subCategoriaOriginal == null){
                            //CREAR VARIABLE QUE GUARDE EL OBJETO SEGUN EL ID DESEADO
                            var subCategoriaEditar = _context.SubCategorias?.Find(subCategoriaId);
                            if(subCategoriaEditar != null){
                                subCategoriaEditar.SubDescripcion = descripcion;
                                subCategoriaEditar.CategoriaID = categoriaID;
                                subCategoriaEditar.CategoriaDescripcion = Categoria.Descripcion;
                                _context.SaveChanges();
                                resultado = true;
                            }
                        }
                    }
                }else{
                    resultado = true;
                }
            }
        }
        return Json(resultado);
    }

    public JsonResult desabilitarSubCategoria(int subCategoriaID ,int categoriaID){
        bool resultado = false;
            //VALIDACION DE SUBCATEGORIAEXISTENTE
            if(subCategoriaID != 0)
            {
                if (categoriaID != 0)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMO ID
                    var categoriaOriginal = _context.Categorias.Find(categoriaID);
                    if (categoriaOriginal?.Eliminado == false)
                    {
                        var subCategoriaOriginal = _context.SubCategorias.Find(subCategoriaID);
                        //SI LA CATEGORIA NO ESTE ELIMINADA PROCEDEMOS A HACERLO
                        if(subCategoriaOriginal?.SubEliminado == false)
                        {
                            subCategoriaOriginal.SubEliminado = true;
                            _context.SaveChanges();
                            resultado = true;
                        }else{
                            subCategoriaOriginal.SubEliminado = false;
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }
        return Json(resultado);
    }

    public JsonResult eliminarSubCategoria(int subCategoriaID ,int categoriaID){
        bool resultado = false;
            //VALIDACION DE SUBCATEGORIAEXISTENTE
            if(subCategoriaID != 0)
            {
                if (categoriaID != 0)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMO ID
                    var categoriaOriginal = _context.Categorias.Find(categoriaID);
                    if (categoriaOriginal?.Eliminado == false)
                    {
                        var subCategoriaOriginal = _context.SubCategorias.Find(subCategoriaID);
                        //SI LA CATEGORIA NO ESTE ELIMINADA PROCEDEMOS A HACERLO
                        if(subCategoriaOriginal?.SubEliminado == true)
                        {
                            _context.SubCategorias.Remove(subCategoriaOriginal);
                            _context.SaveChanges();
                            resultado = true;
                        }else{
                            resultado = false;
                        }
                    }
                }
            }
        return Json(resultado);
    }
}