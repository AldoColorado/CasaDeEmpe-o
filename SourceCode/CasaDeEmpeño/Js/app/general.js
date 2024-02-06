
var mensajesAlertas = {

    exitoLogin: 'Login exitoso',
    errorLogin: 'Usuario o contraseña incorrecta',
    solicitudCamposVacios: 'Existen campos sin llenar, por favor verifique.',
    errorGuardar: 'Se ha producido un error al almacenar los datos. Los datos no fueron almacenados.',
    exitoGuardar: 'Los datos se almacenaron correctamente.',
    exitoEliminar: 'El registro se eliminó correctamente.',
    errorEliminar: 'No se pudo eliminar el registro.',
    camposVacios: 'No se han completado los campos, favor de completar.',
    vigenciaDevolucionVencida: 'La vigencia de devolución del producto se ha vencido.',
    vigenciaDevolucionActiva: 'La devolución del producto sigue activa, no se puede vender por el momento.',
    exitoHacerOferta: 'Oferta guardada correctamente.',
    errorHacerOferta: 'Ocurrió un errro al guardar la oferta.',
    numeroLimiteDeOfertas: 'El producto ya cuenta con 3 ofertas, no puede hacer más.',
    exitoVenta: 'El producto se ha vendido a la oferta más alta',
    errorVenta: 'Error al registrar la venta, intentalo de nuevo'

};


var textosEsp =
{
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_  registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Registros _START_ al _END_ de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
};

$(document).ready(function () {

 
});


var utils = {
   
    toast: (mensaje, tipo) => {
        if (tipo === 'ok') {
            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 4000
                };
                toastr.success(mensaje);

            }, 500);
        } else {
            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 4000
                };
                toastr.error(mensaje);

            }, 500);
        }
    },


}