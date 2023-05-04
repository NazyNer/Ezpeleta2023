window.onload = BuscarSubCategorias();

function BuscarSubCategorias() {
    $("#tbody-sub-categorias").empty();
    $("#btnDesabilitar").hide();
    $("#btnEliminar").hide();
    $("#btnGuardar").show();
    $.ajax({
        // la URL para la petición
        url: '../../SubCategorias/BuscarSubCategorias',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (subCategorias) {
            console.log(subCategorias);
            $("#tbody-sub-categorias").empty();
            $.each(subCategorias, function (index, subCategoria) {
                console.log(subCategoria);
                if (subCategoria.subEliminado) {
                    $("#tbody-sub-categorias").append(`
                    <tr class="table-danger">
                        <td> <a class="btn btn-warning btn-sm" onClick="EditarSubCategoria(${subCategoria.subCategoriaID})" role="button">${subCategoria.subDescripcion}</a></td>
                        <td> <a class="btn btn-warning btn-sm" onClick="EditarCategoria(${subCategoria.subCategoriaID})" role="button">${subCategoria.categoriaDescripcion}</a></td>
                    </tr>`);
                } else {
                    $("#tbody-sub-categorias").append(`
                    <tr class="table-success">
                        <td> <a class="btn btn-primary btn-sm" onClick="EditarSubCategoria(${subCategoria.subCategoriaID})" role="button">${subCategoria.subDescripcion}</a></td>
                        <td> <a class="btn btn-primary btn-sm" onClick="EditarCategoria(${subCategoria.subCategoriaID})" role="button">${subCategoria.categoriaDescripcion}</a></td>
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
        },
        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('Petición realizada');
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
    $("#CategoriaID").prop('disabled', false);
    $("#btnGuardar").show();
}
function BuscarSubCategoria(subCategoriaID) {
    $.ajax({
        //url para la peticion
        url: '../../SubCategorias/BuscarSubCategorias',
        //info a enviar
        data: { subCategoriaId: subCategoriaID },
        // especifica petición POST
        type: "POST",
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria
        success: function (subCategorias) {
            if (subCategorias.length == 1) {
                let SubCategoria = subCategorias[0];
                $("#Descripcion").val(SubCategoria.subDescripcion);
                $("#SubCategoriaID").val(SubCategoria.subCategoriaID);
                $("#categoriaID").val(SubCategoria.categoriaID);
                if (!SubCategoria.subEliminado) {
                    $("#btnEliminar").hide();
                    $("#btnDesabilitar").show();
                    $("#btnGuardar").show();
                    $("#btnHabilitar").hide();
                }
                else {
                    $("#CategoriaID").prop('disabled', true);
                    $("#Descripcion").prop('disabled', true);
                    $("#btnHabilitar").show();
                    $("#btnGuardar").hide();
                    $("#btnDesabilitar").hide();
                    $("#btnEliminar").show();
                }
                $("#ModalSubCategoria").modal("show");
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
function GuardarSubCategoria() {
    //creamos las variables para guardar los datos necesarios.
    let subCategoriaID = $("#SubCategoriaID").val();
    let categoriaID = $("#CategoriaID").val();
    let Descripcion = $("#Descripcion").val();
    $.ajax({
        //url para la peticion
        url: "../../SubCategorias/GuardarSubCategoria",
        //info a enviar
        data: { subCategoriaId: subCategoriaID, descripcion: Descripcion.toUpperCase(), categoriaID: categoriaID },
        // especifica petición POST
        type: "POST",
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria
        success: function (resultado) {
            if (resultado) {
                $("#ModalSubCategoria").modal("hide");
                BuscarSubCategorias();
            }
            else {
                $("#Label-Error").text("Existe una Categoría con la misma descripción.")
            }
        }
    });
}
function desabilitarSubCategoria() {
    //JAVASCRIPT
    let subCategoriaID = $("#SubCategoriaID").val();
    let categoriaID = $("#categoriaID").val();
    $.ajax({
        // la URL para la petición
        url: '../../SubCategorias/desabilitarSubCategoria',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { subCategoriaID : subCategoriaID, categoriaID: categoriaID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                $("#ModalSubCategoria").modal("hide");
                BuscarSubCategorias();
            }
            else {
                $("#ModalSubCategoria").modal("hide");
                BuscarSubCategorias();
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
function eliminarSubCategoria() {
    //JAVASCRIPT
    let subCategoriaID = $("#SubCategoriaID").val();
    let categoriaID = $("#categoriaID").val();
    $.ajax({
        // la URL para la petición
        url: '../../SubCategorias/eliminarSubCategoria',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { subCategoriaID : subCategoriaID, categoriaID: categoriaID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                $("#ModalSubCategoria").modal("hide");
                BuscarSubCategorias();
            }
            else {
                $("#ModalSubCategoria").modal("hide");
                BuscarSubCategorias();
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

function EditarSubCategoria(subCategoriaID) {
    $("#CategoriaID").prop('disabled', true);
    $("#Descripcion").prop('disabled', false);
    BuscarSubCategoria(subCategoriaID);
}
function EditarCategoria(subCategoriaID) {
    $("#Descripcion").prop('disabled', true);
    $("#CategoriaID").prop('disabled', false);
    BuscarSubCategoria(subCategoriaID);
}