$(document).ready(function () {
    //cargarSubCategorias()
})


function cargarSubCategorias() {
    let ddl = $("#ddlCategoria").val()
    $.ajax({
        url: ddlSubCategoriaUrl + "?idCategoria=" + ddl,
        type: "GET",
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                let ddlSubCategoria = $('#ddlSubCategoria')
                //ddl.empty()
                ddlSubCategoria.empty()

                let optionDefault = "<option> Selecciona una subcategoría</option>"
                ddlSubCategoria.append(optionDefault)

                $.each(result.Objects, function (index, value) {
                    let tagOption = `<option value='${value.IdSubCategoria}'>${value.Nombre}</option>`
                    ddlSubCategoria.append(tagOption)
                })
            }
        },
    })
}


function ValidarForm() {
    var isValid = true;

    $('form').find('input, select, textarea').each(function () {
        var $field = $(this);

        if ($field.attr('required') && $field.val().trim() === '') {
            $field.css('border-color', 'red');
            $field.addClass('error-input');
            isValid = false;
        }
        if ($field.hasClass('error-input')) {
            isValid = false;
        }

    });
    return isValid;
}

// Asignar el evento de clic en el botón de envío
/*document.getElementById("submitButton").addEventListener("click", ValidarForm);*/
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("submitButton").addEventListener("click", ValidarForm);
});

function SoloLetras(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = '';


    if (!(/[a-z A-Z]/.test(entrada))) {
        evt.preventDefault()
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Solo se aceptan letras';
    }
    else {
        console.log("Si es una letra")
        inputField.style.borderColor = 'green';
    }

}
