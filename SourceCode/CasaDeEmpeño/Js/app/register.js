'use strict';

const register = {
    init: () => {

    },

    registrarse() {

        var usuario = new Object();


        usuario.Correo = $('#txtCorreo').val();
        usuario.Password = $('#txtPassword').val();

        let params = {};
        params.path = window.location.hostname;
        params.Usuario = usuario;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Access/Registrarse",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

             

                if (msg > 0) {

                    utils.toast(mensajesAlertas.exitoGuardar, 'ok');
                    window.location.href = '/Access/Login'; 

                } else {

                    utils.toast(mensajesAlertas.errorGuardar, 'fail');

                }


            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
                console.log("Autenticación fallida");


            }

        });

    },

    accionesBotones: () => {

        $('#btnRegistrarse').on('click', (e) => {
            e.preventDefault();

            register.registrarse();

        });

    }

}

window.addEventListener('load', () => {

    register.init();

    register.accionesBotones();
});
