
function CambioEstatus(IdUsuario, input) {

    let estatus = input.checked
    $.ajax({
        url: CambioEstatus,
        data: { IdUsuario: IdUusario, Estatus: estatus },
        type: "POST",
        dataType: "json",
        sucess: function (result) {
            if (result.Correct) {

            } else {
                alert("Hubo un error")
            }
        },
        error: function (xhr) {

            console.log(xhr)
        }
    }

    )

}