'use strict';

const access = {
    init: () => {

    },

    Loggearse() {

        var usuario = new Object();


        usuario.Correo = $('#txtCorreo').val();
        usuario.Password = $('#txtPassword').val();

        let params = {};
        params.path = window.location.hostname;
        params.Usuario = usuario;
        params = JSON.stringify(params);

        $.ajax({
            type: "POST",
            url: "../../Access/Loggearse",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {



                console.log(msg.IdUsuario);

                if (msg.IdUsuario == 0) { 

                    utils.toast(mensajesAlertas.errorLogin, 'fail');
                    

                } else {

                    utils.toast(mensajesAlertas.exitoLogin, 'ok');
                    window.location.href = '/Home/Productos'; 
                }
      
                    
                //console.log(data);


            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus + ": " + XMLHttpRequest.responseText);
                console.log("Autenticación fallida");


            }

        });

    },

    accionesBotones: () => {

        $('#btnLogin').on('click', (e) => {
            e.preventDefault();

            access.Loggearse();

        });

    }

}

window.addEventListener('load', () => {

    access.init();

    access.accionesBotones();
});
