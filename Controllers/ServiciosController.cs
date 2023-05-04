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
        var serviciosVista = new List<ServiciosVista>();
        foreach (var servicio in serviciosDB){
            var categoriaDescripcion = _context.SubCategorias?.Where(sc => sc.SubCategoriaID == servicio.SubCategoriaID && sc.SubEliminado == false).FirstOrDefault();
            var servicios = new ServiciosVista();
            servicios.ServicioID = servicio.ServicioID;
            servicios.SubCategoriaID = servicio.SubCategoriaID;
            servicios.CategoriaDescripcion = categoriaDescripcion?.CategoriaDescripcion;
            servicios.Telefono = servicio.Telefono;
            servicios.Direccion = servicio.Direccion;
            servicios.Descripcion = servicio.Descripcion;
            servicios.Desabilitado = servicio.Desabilitado;
            servicios.SubDescripcion = servicio.SubDescripcion;
            serviciosVista.Add(servicios);
        }
        if(servicioID > 0 ){
            serviciosVista = serviciosVista.Where(s => s.ServicioID == servicioID).OrderBy(s => s.Descripcion).ToList();
        }
        return Json(serviciosVista);
    }

    public JsonResult GuardarServicio(int servicioID, string descripcion, string direccion, int telefono, int subCategoriaId){
        var resultado = new ValidacionError();
        resultado.nonError = true;
        resultado.MsjError = "No se escribio ningun servicio";
        if(!string.IsNullOrEmpty(descripcion)){
            var subCategoria = _context.SubCategorias?.Where(sc => sc.SubCategoriaID == subCategoriaId).FirstOrDefault();
            //VERIFICAMOS QUE LA SUBCATEGORIA NO ESTE DESABILITADA
            if (subCategoria != null)
            {
                if (subCategoria.SubEliminado == false)
                {
                    //SI ES 0 QUIERE DECIR QUE ESTA CREANDO EL SERVICIO
                    if (servicioID == 0)
                    {
                        //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMA DESCRIPCION
                        var servicioOriginal = _context.Servicios?.Where(s => s.Descripcion == descripcion && s.Direccion == direccion && s.Telefono == telefono).FirstOrDefault();
                        if (servicioOriginal == null)
                        {
                            //DECLARAMOS EL OBJETO DADO EL VALOR
                            var servicio = new Servicios{
                                Descripcion = descripcion,
                                Direccion = direccion,
                                Telefono = telefono,
                                SubCategoriaID = subCategoriaId,
                                SubDescripcion = subCategoria.SubDescripcion
                            };
                            _context.Add(servicio);
                            _context.SaveChanges();
                            resultado.nonError = true;
                        }else{
                            resultado.nonError = true;
                            resultado.MsjError = "El nombre del servicio ya existe";
                        }
                    }else{
                        //BUSCAMOS EN LA TABLA SI EXISTE CON LA MISMA DESCRIPCION Y DISTINTO ID DE REGISTRO AL QUE ESTAMOS EDITANDO
                        var servicioOriginal = _context.Servicios?.Where(s => s.Descripcion == descripcion && s.Direccion == direccion && s.Telefono == telefono).FirstOrDefault();
                        if(servicioOriginal == null){
                            //CREAR VARIABLE QUE GUARDE EL OBJETO SEGUN EL ID DESEADO
                            var servicioEditar = _context.Servicios?.Find(servicioID);
                            if(servicioEditar != null){
                                servicioEditar.Descripcion = descripcion;
                                servicioEditar.SubCategoriaID = subCategoriaId;
                                servicioEditar.SubDescripcion = subCategoria.SubDescripcion;
                                servicioEditar.Direccion = direccion;
                                servicioEditar.Telefono = telefono;
                                _context.SaveChanges();
                                resultado.nonError = true;
                            }
                        }
                    }
                }else{
                    resultado.nonError = true;
                    resultado.MsjError = "La subcategoria seleccionada esta desabilitada";
                }
            }}else{
                resultado.nonError = true;
                resultado.MsjError = "La subcategoria seleccionada no exite";
            }

        return Json(resultado);
    }
    
    public JsonResult DesabilitarServicio(int servicioID, int subCategoriaID) {
        var resultado = new ValidacionError();
        resultado.nonError = false; 
        resultado.MsjError = "Servicio que desea eliminar no se encontro";
        //VALIDACION DE SERVICIO EXISTENTE
            if(servicioID != 0)
            {
                resultado.MsjError = "la subcategoria realicionada al servicio que desea eliminar no se encontro";
                //VALIDACION DE SUBCATEGORIA EXISTENTE
                if (subCategoriaID != 0)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UNA CON LA MISMO ID
                    var subCategoriaOriginal = _context.SubCategorias?.Find(subCategoriaID);
                    if (subCategoriaOriginal?.SubEliminado == false)
                    {
                        var servicioOriginal = _context.Servicios?.Find(servicioID);
                        //SI LA CATEGORIA NO ESTE ELIMINADA PROCEDEMOS A HACERLO
                        if(servicioOriginal?.Desabilitado == false)
                        {
                            servicioOriginal.Desabilitado = true;
                            _context.SaveChanges();
                            resultado.nonError = true;
                        }else{
                            servicioOriginal.Desabilitado = false;
                            _context.SaveChanges();
                            resultado.nonError = true;
                        }
                    }
                }
            }
        return Json(resultado);
    }
    
}
