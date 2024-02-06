'use strict';



const product = {




    init: () => {
        
        product.obtenerProductos();
        product.cargarComboTipoProductos();

        product.idProductoSeleccionado = -1;

        $('#panelTabla').show();
        $('#panelForm').hide();
    },



    obtenerProductos() {


        let params = {};
        params.path = window.location.hostname;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Home/ObtenerProductos",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {


                let data = msg;


                let table = $('#table').DataTable({
                    "destroy": true,
                    "processing": true,
                    "order": [],
                    data: data,
                    columns: [
                        { data: 'IdProducto' },
                        { data: 'NombreProducto' },
                        { data: 'EstadoProducto' },
                        { data: 'FechaDeIngreso' },
                        { data: 'tipoProducto.TipoProductoNombre' },
                        { data: 'ValorCalculado' },
                        {
                            data: 'Accion',
                            render: function (data, type, row) {
                                return data;
                            }
                        }
                    ],
                    "columnDefs": [
                        {
                            "targets": [-1],
                            "orderable": false
                        },
                    ],
                    dom: 'frtiplB',
                    buttons: [
                        {
                            extend: 'excelHtml5',
                            title: "hola",
                            text: 'Xls', className: 'excelbtn'
                        },
                        {
                            extend: 'pdfHtml5',
                            title: "hola",
                            text: 'Pdf', className: 'pdfbtn'
                        }
                    ]

                });


            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
                console.log("Obtener productos fallido");


            }

        });

    },


    cargarComboTipoProductos() {

        var params = {};
        params.path = window.location.hostname;

        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Home/ObtenerTipoProductos",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                let items = msg;
                let opcion = '<option value="">Seleccione...</option>';

                for (let i = 0; i < items.length; i++) {
                    let item = items[i];

                    opcion += `<option value = '${item.IdTipoProducto}' > ${item.TipoProductoNombre}</option > `;

                }

                $(`#comboTipoProducto`).html(opcion);

            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
    },

    recuperarDatosProducto: (idProducto) => {

        var parametros = new Object();
        parametros.path = window.location.hostname;
        parametros.idProducto = idProducto;
        parametros = JSON.stringify(parametros);
        $.ajax({
            type: "POST",
            url: "../../Home/GetProducto",
            data: parametros,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                let item = msg;
                console.log(item);
                console.log("exito al obtener producto");

                product.idProductoSeleccionado = item.IdProducto;
                console.log("ID del producto seleccciona" + product.idProductoSeleccionado + "idqueSerecibe... " + item.IdProducto);

                $('#txtNombreProductoVenta').val(item.NombreProducto);
                $('#txtEstadoProductoVenta').val(item.EstadoProducto);
                $('#txtValorCalculadoVenta').val(item.ValorCalculado);


                $('#panelFormGenerarVenta').modal('show');


            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });

    },


    ponerProductoEnVenta: (idProducto) => {

        console.log("Entra a poner en venta");

        $('#frmVentas')[0].reset();

        $('.form-group').removeClass('has-error');
        $('.help-block').empty();

        let params = {};
        params.path = window.location.hostname;
        params.idProducto = idProducto;
        params = JSON.stringify(params);


        $.ajax({
            type: "POST",
            url: "../../Returns/ComprobarVigenciaDevolucion",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {



                if (msg == 1) {

                    product.recuperarDatosProducto(idProducto);
                   
                } else {
                    utils.toast(mensajesAlertas.vigenciaDevolucionActiva, 'fail');
                    return;
                }



            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
                console.log("Autenticación fallida");


            }

        });        
       
    },

    accionesBotones: () => {

        $('#btnNuevo').on('click', (e) => {
            e.preventDefault();

            $('#panelTabla').hide();
            $('#panelForm').show();

        });


        $('#btnCancelar').on('click', (e) => {
            e.preventDefault();

            $('#panelTabla').show();
            $('#panelForm').hide();

        });
        

        $('#btnGuardar').on('click', (e) => {
            e.preventDefault();



            var hasErrors = $('form[name="frm"]').validator('validate').has('.has-error').length;


            if (hasErrors) {
                return;
            }

            var item = new Object();


            item.NombreProducto = $('#txtNombreProducto').val();
            item.EstadoProducto = $('#txtEstadoProducto').val();
            item.ValorCalculado = parseFloat($('#txtValorCalculado').val());
            item.IdTipoProducto = parseInt($('#comboTipoProducto').val());
          

            console.log(item);
            let params = {};
            params.path = window.location.hostname;
            params.producto = item;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST",
                url: "../../Home/RegistrarProducto",
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {



                    if (msg > 0) {

                        utils.toast(mensajesAlertas.exitoGuardar, 'ok');
                        product.obtenerProductos();
                        $('#panelTabla').show();
                        $('#panelForm').hide();

                    } else {

                        utils.toast(mensajesAlertas.errorGuardar, 'fail');

                    }


                }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus + ": " + XMLHttpRequest.responseText);
                    console.log("Autenticación fallida");


                }

            });

        });


        $('#btnGuardarVenta').on('click', (e) => {
            e.preventDefault();

            var venta = new Object();

            venta.idProducto = product.idProductoSeleccionado;
            venta.PrecioVenta = $('#txtPrecioVenta').val();
            

            let params = {};
            params.path = window.location.hostname;
            params.venta = venta;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST",
                url: "../../Sales/PonerProductoEnVenta",
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {



                    if (msg > 0) {

                        utils.toast(mensajesAlertas.exitoGuardar, 'ok');
                        product.obtenerProductos();

                        $('#panelFormGenerarVenta').modal('hide');

                    } else {

                        utils.toast(mensajesAlertas.errorGuardar, 'fail');

                    }


                }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus + ": " + XMLHttpRequest.responseText);
                    console.log("Autenticación fallida");

                }

            });

        });


    }

}

window.addEventListener('load', () => {

    product.init();

    product.accionesBotones();
});
