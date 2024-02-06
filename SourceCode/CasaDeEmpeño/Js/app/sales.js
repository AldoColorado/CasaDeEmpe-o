'use strict';

const sales = {
    init: () => {

        sales.obtenerVentas();
        sales.idVentaSeleccionado = -1;
        
        $('#panelTabla').show();
        $('#panelForm').hide();
    },



    obtenerVentas() {


        let params = {};
        params.path = window.location.hostname;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Sales/ObtenerVentas",
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
                        { data: 'IdVenta' },
                        { data: 'producto.NombreProducto' },
                        { data: 'producto.EstadoProducto' },
                        { data: 'producto.tipoProducto.TipoProductoNombre' },
                        { data: 'PrecioVenta' },
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

    hacerOferta: (idVenta) => {

        let params = {};
        params.path = window.location.hostname;
        params.idVenta = idVenta;
        params = JSON.stringify(params);


        $.ajax({
            type: "POST",
            url: "../../Sales/ComprobarNumeroOfertas",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {



                if (msg == 1) {
                    console.log("llega a comparobar la devolucion");
                    utils.toast(mensajesAlertas.numeroLimiteDeOfertas, 'fail');

                } else {

                 


                    sales.idVentaSeleccionado = idVenta;

                    $('#frm')[0].reset();
                    $('#panelTabla').hide();
                    $('#panelForm').show();

                }



            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
                console.log("Autenticación fallida");


            }

        });

    },


    verOfertas: (idVenta) => {

        let params = {};
        params.path = window.location.hostname;
        sales.idVentaSeleccionado = idVenta;
        params.idVenta = idVenta;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Sales/ObtenerOfertas",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                $('#panelTablaOfertas').modal('show');

                let data = msg;


                let table = $('#tableOfertas').DataTable({
                    "destroy": true,
                    "processing": true,
                    "order": [],
                    data: data,
                    columns: [
                        { data: 'IdOferta' },
                        { data: 'NombrePersonaOferta' },
                        { data: 'NumeroCelular' },
                        { data: 'MontoOferta' }
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

        $('#btnVenderOfertaMasAlta').on('click', (e) => {
            e.preventDefault();


            console.log(sales.idVentaSeleccionado);
            let params = {};
            params.path = window.location.hostname;
            params.idVenta = sales.idVentaSeleccionado;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST",
                url: "../../Sales/VenderAOfertaMasAlta",
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {



                    if (msg > 0) {

                        utils.toast(mensajesAlertas.exitoVenta, 'ok');


                        $('#panelTablaOfertas').modal('hide');
                        sales.obtenerVentas();
                        $('#panelTabla').show();
                        $('#panelForm').hide();


                    } else {

                        utils.toast(mensajesAlertas.errorVenta, 'fail');

                    }


                }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus + ": " + XMLHttpRequest.responseText);
                    console.log("Autenticación fallida");


                }

            });

        });

        $('#btnGuardar').on('click', (e) => {
            e.preventDefault();



            var hasErrors = $('form[name="frm"]').validator('validate').has('.has-error').length;


            if (hasErrors) {
                return;
            }

            var item = new Object();


            item.NombrePersonaOferta = $('#txtNombrePersonaOferta').val();
            item.NumeroCelular = $('#txtNumeroCelular').val();
            item.MontoOferta = parseFloat($('#txtMontoOferta').val());
            item.IdVenta = sales.idVentaSeleccionado;
          

            console.log(item);
            let params = {};
            params.path = window.location.hostname;
            params.oferta = item;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST",
                url: "../../Sales/GuardarOferta",
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {



                    if (msg > 0) {

                        utils.toast(mensajesAlertas.exitoHacerOferta, 'ok');

                        $('#panelTabla').show();
                        $('#panelForm').hide();

                    } else {

                        utils.toast(mensajesAlertas.errorHacerOferta, 'fail');

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

    sales.init();

    sales.accionesBotones();
});
