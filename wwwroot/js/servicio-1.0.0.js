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
        url: '../../Servicios/',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (servicio) {
            tBody.empty();
            $.each(subCategorias, function (index, servicios) {
                console.log(servicios);
                if (servicios.desabilitado) {
                    tBody.append(`
                    <tr class="table-danger">
                        <td> <a class="btn btn-warning btn-sm" onClick="EditarServicios(${servicios.serviciosID})" role="button">${subCategoria.subDescripcion}</a></td>
                        <td> <a class="btn btn-warning btn-sm" onClick="EditarSubCategoria(${servicios.serviciosID})" role="button">${subCategoria.SubDescripcion}</a></td>
                    </tr>`);
                } else {
                    tBody.append(`
                    <tr class="table-success">
                        <td> <a class="btn btn-primary btn-sm" onClick="EditarServicios(${servicios.serviciosID})" role="button">${subCategoria.subDescripcion}</a></td>
                        <td> <a class="btn btn-primary btn-sm" onClick="EditarSubCategoria(${servicios.serviciosID})" role="button">${subCategoria.SubDescripcion}</a></td>
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