function GetAll() {
    $.ajax({
        url:,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                var usuarios = result.Objects;
                var tabla = document.getElementById("tbody");
                var row = '';

                $.each(usuarios, funtion(CRUDJavaScript, usuario))
                var imagen = usuario.ImagenBase64
                    ?'<img src '

            }

        }

    })
}