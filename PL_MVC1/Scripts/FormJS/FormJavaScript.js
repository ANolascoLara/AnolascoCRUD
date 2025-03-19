
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

function validarContrasena(evt) {
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';

    var password = inputField.value;
    var passwordRegex = /^(?=.*[A-Z])(?=.*[\W_]).{8}$/; // 1 mayúscula, 1 carácter especial, exactamente 8 caracteres

    if (password === '') {
        inputField.style.borderColor = ''; // Restablecer el color normal
    } else if (!passwordRegex.test(password)) {
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayuscula y un número';
    } else {
        inputField.style.borderColor = 'green';
    }
}

function confirmarContrasena(evt) {
    var confirmField = evt.target;
    var passwordField = document.getElementById('password');
    var ErrorMessage = confirmField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';


    if (confirmPassword.value !== passwordField.value) {
        evt.preventDefault();
        confirmField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Las contraseñas no coinciden';
    }
    else {
        confirmField.style.borderColor = 'green';
    }
}



function validarCURP(evt) {
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';

    var curp = inputField.value.toUpperCase();
    var curpRegex = /^[A-Z]{4}\d{6}[HM][A-Z]{5}[0-9A-Z]{2}$/;

    if (curp === '') {
        inputField.style.borderColor = ''; // Restablecer el color normal
    } else if (!curpRegex.test(curp)) {
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Ingrese una CURP válida';
    } else {
        inputField.style.borderColor = 'green';
    }
}

function validarCorreo(evt) {
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';

    var email = inputField.value;
    var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;

    if (email === '') {
        inputField.style.borderColor = '';
    } else if (!emailRegex.test(email)) {
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Ingrese un correo válido';
    } else {
        inputField.style.borderColor = 'green';
    }
}


function SoloNumeros(evt) {
    var entrada = String.fromCharCode(evt.which);
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');

    ErrorMessage.textContent = '';
    if (!/[0-9]/.test(entrada)) {
        evt.preventDefault();
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Solo se aceptan números';
    }
    else {
        console.log("Si es un Número");
        inputField.style.borderColor = 'green';
    }
}

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

function LimpiarBorde(evt) {
    var inputField = evt.target;
    if (inputField.value.trim() === '' || inputField.value) {
        inputField.style.borderColor = '';
        var ErrorMessage = inputField.parentNode.querySelector('.error')
        ErrorMessage.textContent = '';

    }
}

$("#Fecha").datepicker({
    dateFormat: "dd/mm/yy",
    showAnim: "fold",
    changeMonth: true,
    changeYear: true
})

$(document).ready(
    
)

function MunicipioGetBYIdEstado() {

    let ddl = $(`#ddlEstado`).val();
    $.ajax({
        url: urlMunicipio + "?IdEstado=" + ddl,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {

                let ddlMunicipio = $(`#ddlMunicipio`);
                $.each(result.Objects, function (i, valor) {
                    let option = `<option value= ${valor.IdMunicipio}>${valor.Nombre}</option>`;
                    ddlMunicipio.append(option)
                })


            }
        },
        error: function (xhr) {

            console.log(xhr)
        }
    }

    )

}
function ColoniaGetBYIdMunicipio() {

    let ddl = $(`#ddlMunicipio`).val();
    console.log(ddl)
    $.ajax({
        url: urlColonia + "?IdMunicipio=" + ddl,
        //data:MODELOS
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            console.log(ddl)
            if (result.Correct) {

                let ddlColonia = $(`#ddlColonia`);
                $.each(result.Objects, function (i, valor) {
                    let option = `<option value= ${valor.IdColonia}>${valor.Nombre}</option>`;
                    ddlColonia.append(option)
                })


            }
        },
        error: function (xhr) {

            console.log(xhr)
        }
    }

    )

}

function validarImagen() {
    var input = $('#inptFileImagen')[0].files[0].name.split('.').pop().toLowerCase()
    console.log(input)

    var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp']
    var banderaImg = false;

    for (var i = 0; i <= extensionesValidas.length; i++) {
        if (input == extensionesValidas[i]) {
            banderaImg = true
        }
    }
    if (!banderaImg) {
        alert(`Las extensiones permitidas son: ${extensionesValidas}`)
        $('#inptFileImagen').val("");
    }
}
function visualizarImagen(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#img').attr('src', elemento.target.result)
        }


        reader.readAsDataURL(input.files[0])
    }
}


