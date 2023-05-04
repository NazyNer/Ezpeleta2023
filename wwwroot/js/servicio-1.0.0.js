window.onload = BuscarServicios();

let tBody = $("#tbdoy-servicio");
let btnDesabilitar = $("#btnDesabilitar");
let btnEliminar = $("#btnEliminar");
let btnGuardar = $("#btnGuardar");

function BuscarServicios() {
    tBody.empty();
    btnDesabilitar.hide();
    btnEliminar.hide();
    btnGuardar.show();
    $.ajax({
        // la URL para la petición
        url: '../../Servicios/BuscarServicios',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Servicios) {
            tBody.empty();
            $.each(servicios, function (index, servicio) {
                console.log(servicio);
                if (servicio.desabilitado) {
                    tBody.append(`
                    <tr class="table-danger">
                        <td> <a class="btn btn-warning btn-sm" onClick="EditarServicios(${servicio.serviciosID})" role="button">${servicio.descripcion}</a></td>
                        <td> <a class="btn btn-warning btn-sm" onClick="EditarSubCategoria(${servicio.serviciosID})" role="button">${servicio.subDescripcion}</a></td>
                        <td> ${servicio.categoriaDescripcion}</td>
                        <td>${servicio.direccion}</td>
                        <td>${servicio.telefono}</td>
                    </tr>`);
                } else {
                    tBody.append(`
                    <tr class="table-success">
                        <td> <a class="btn btn-primary btn-sm" onClick="EditarServicios(${servicios.serviciosID})" role="button">${servicio.descripcion}</a></td>
                        <td> <a class="btn btn-primary btn-sm" onClick="EditarSubCategoria(${servicios.serviciosID})" role="button">${servicio.subDescripcion}</a></td>
                        <td> ${servicio.categoriaDescripcion}</td>
                        <td>${servicio.direccion}</td>
                        <td>${servicio.telefono}</td>
                    </tr>
                    `);
                }
            })

        },
        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert('Error al cargar subcategorias');
        }
    });
}
function VaciarFormulario() {
    $("#Descripcion").val('');
    $("#SubCategoriaID").val(0);
    btnDesabilitar.hide();
    btnEliminar.hide();
    btnHabilitar.hide();
    $("#Descripcion").prop('disabled', false);
    $("#subCategoriaID").prop('disabled', false);
    btnGuardar.show();
}
function GuardarServicio() {
    //creamos las variables para guardar los datos necesarios.
    let subCategoriaID = $("#SubCategoriaID").val();
    let servicioID = $("#ServicioID").val();
    let descripcion = $("#Descripcion").val();
    $.ajax({
        //url para la peticion
        url: "../../Servicios/GuardarServicios",
        //info a enviar
        data: { servicioID: servicioID, descripcion: descripcion.toUpperCase(), subCategoriaID: subCategoriaID },
        // especifica petición POST
        type: "POST",
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria
        success: function (resultado) {
            if (resultado) {
                $("#ModalServicio").modal("hide");
                BuscarServicios();
            }
            else {
                $("#Label-Error").text("Existe un servicio con la misma descripción.")
            }
        }
    });
}