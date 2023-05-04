window.onload = BuscarServicios();


function BuscarServicios() {
    $("#tbdoy-servicio").empty();
    $("#btnDesabilitar").hide();
    $("#btnEliminar").hide();
    $("#btnGuardar").show();
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
            // console.log(Servicios);
            $("#tbody-servicio").empty();
            $.each(Servicios, function (index, servicio) {
                console.log(servicio.desabilitado)
                if (servicio.desabilitado) {
                    $("#tbody-servicio").append(`
                    <tr class="table-danger">
                        <td> <a class="btn btn-warning btn-sm" onClick="BuscarServicio(${servicio.servicioID})" role="button">${servicio.descripcion}</a></td>
                        <td>${servicio.subDescripcion}</td>
                        <td> ${servicio.categoriaDescripcion}</td>
                        <td>${servicio.direccion}</td>
                        <td>${servicio.telefono}</td>
                    </tr>`);
                } else {
                    $("#tbody-servicio").append(`
                    <tr class="table-success">
                        <td> <a class="btn btn-primary btn-sm" onClick="BuscarServicio(${servicio.servicioID})" role="button">${servicio.descripcion}</a></td>
                        <td>${servicio.subDescripcion}</td>
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
    $("#btnDesabilitar").hide();
    $("#btnEliminar").hide();
    $("#btnHabilitar").hide();
    $("#Descripcion").prop('disabled', false);
    $("#subCategoriaID").prop('disabled', false);
    $("#btnGuardar").show();
}
function BuscarServicio(serviciosID) {
    $.ajax({
        //url para la peticion
        url: '../../Servicios/BuscarServicios',
        //info a enviar
        data: { servicioID: serviciosID },
        // especifica petición POST
        type: "POST",
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria
        success: function (servicios) {            
            if (servicios.length == 1) {
                let servicio = servicios[0];
                // $("#Descripcion").val(servicio.descripcion);
                $("#ServicioID").val(servicio.servicioID);
                $("#SubCategoriaID").val(servicio.subCategoriaID);
                if (servicio.desabilitado) {
                    $("#btnHabilitar").show();
                    $("#btnDesabilitar").hide();
                }else{
                    $("#Direccion").prop('disabled', false);
                    $("#Descripcion").prop('disabled', false);
                    $("#Telefono").prop('disabled', false);
                    $("#subCategoriaID").prop('disabled', false);
                    $("#btnHabilitar").hide();
                    $("#btnDesabilitar").show();
                    
                }
                $("#ModalServicio").modal("show");
            }
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert('Error al cargar categorias');
        },

        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('Petición realizada');
        }
    });
}
function GuardarServicio() {
    //creamos las variables para guardar los datos necesarios.
    let subCategoriaID = $("#subCategoriaID").val();
    let servicioID = $("#ServicioID").val();
    let descripcion = $("#Descripcion").val();
    let direccion = $("#Direccion").val();
    let telefono = $("#Telefono").val();
    $.ajax({
        //url para la peticion
        url: "../../Servicios/GuardarServicio",
        //info a enviar
        data: { servicioID: servicioID, descripcion: descripcion.toUpperCase(), direccion: direccion.toUpperCase(), telefono: telefono, subCategoriaID: subCategoriaID },
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
function desabilitarServicio() {
    let servicioID = $("#ServicioID").val();
    let subCategoriaID = $("#SubCategoriaID").val();
    console.log(servicioID, subCategoriaID);
    $.ajax({
        // la URL para la petición
        url: '../../Servicios/DesabilitarServicio',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { servicioID : servicioID, subCategoriaID: subCategoriaID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            console.log(resultado)
            if (resultado.nonError) {
                $("#ModalServicio").modal("hide");
                BuscarServicios();
            }
            else {
                $("#ModalServicio").modal("hide");
                BuscarServicios();
            }
        },
        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            $("#Label-Error").text('Disculpe, existió un problema');
        }
    });
}
