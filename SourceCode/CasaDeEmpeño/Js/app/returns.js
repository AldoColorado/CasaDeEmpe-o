'use strict';



const returns = {




    init: () => {

        returns.obtenerProductos();
        returns.obtenerDevoluciones();
        returns.idProductoADevolver = -1;
        console.log("Si hace el init")
        $('#panelTabla').show();
        $('#panelForm').hide();
    },

   


    devolverProducto: (idProducto) => {

        //$('#txtNombreProducto').val(nombreProducto);
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
                    console.log("llega a comparobar la devolucion");
                    utils.toast(mensajesAlertas.vigenciaDevolucionVencida, 'fail');

                } else {
                    returns.idProductoADevolver = idProducto;

                    $('#panelTabla').hide();
                    $('#panelForm').show();
                }
                


            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
                console.log("Autenticación fallida");


            }

        });
        
    },


    obtenerProductos() {
        console.log("Si hace el obtener productos")
        let params = {};
        params.path = window.location.hostname;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Home/ObtenerProductosParaDevolucion",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {


                let data = msg;


                let table = $('#tableProductos').DataTable({
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


    obtenerDevoluciones() {


        let params = {};
        params.path = window.location.hostname;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Returns/ObtenerDevoluciones",
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
                        { data: 'IdDevolucion' },
                        { data: 'producto.NombreProducto' },
                        { data: 'ComentarioDevolucion' },
                        { data: 'FechaDevolucion' }
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
        

        $('#btnGuardar').on('click', (e) => {
            e.preventDefault();



            var hasErrors = $('form[name="frm"]').validator('validate').has('.has-error').length;


            if (hasErrors) {
                return;
            }

            var item = new Object();


            item.ComentarioDevolucion = $('#txtComentarioDevolucion').val();
            item.IdProducto = returns.idProductoADevolver;
            
          
            console.log(item);
            let params = {};
            params.path = window.location.hostname;
            params.devolucion = item;
            params = JSON.stringify(params);

            $.ajax({
                type: "POST",
                url: "../../Returns/GuardarDevolucion",
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {



                    if (msg > 0) {

                        utils.toast(mensajesAlertas.exitoGuardar, 'ok');
                        returns.obtenerDevoluciones();
                        returns.obtenerProductos();
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

    }

}

window.addEventListener('load', () => {

    returns.init();

    returns.accionesBotones();
});
