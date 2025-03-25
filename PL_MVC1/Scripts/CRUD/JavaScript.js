$(document).ready(function () {
    GetAll();
});

function GetAll() {
   
    $.ajax({
        url: ruta,
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            if (result.Correct) {
                var usuarios = result.Objects;
                var tabla = document.getElementById("UsuarioTabla");
                tabla.innerHTML = ""; // Limpiar tabla antes de llenarla

                $.each(usuarios, function (index, usuario) {

                    let imagenSrc = usuario.ImagenBase64 ? `data:image/png;base64,${usuario.ImagenBase64}`
                        : "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";

                    let checked = usuario.Estatus ? "checked" : "";
               
                    let direccionConcatenada = `Calle: ${usuario.Direccion.Calle}, Número Exterior: ${usuario.Direccion.NumeroExterior}, 
                                               Número Interior: ${usuario.Direccion.NumeroInterior}, Colonia: ${usuario.Direccion.Colonia.Nombre}, 
                                               Código Postal: ${usuario.Direccion.Colonia.CodigoPostal}, Municipio:                     ${usuario.Direccion.Colonia.Municipio.Nombre}, 
                                               Estado: ${usuario.Direccion.Colonia.Municipio.Estado.Nombre}`;

                    //var imagen = usuario.ImagenBase64
                    //    ? `<img src="data:image/jpeg;base64,${usuario.ImagenBase64}" width="50" height="50"/>`
                    //    : "Sin imagen";

                    var row = `
                   <td>
                                <div class="form-check form-switch">
                                    <input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault"
                                           ${checked} onchange="CambioEstatus(${usuario.IdUsuario}, this)">
                                </div>
                            </td>
                                    <td>
                                   <button class="btn btn-warning" onclick="CargarUsuario(${usuario.IdUsuario})">
                                    <i class="bi bi-pencil-square"></i> Actualizar
                                    </button>
                                    </td>

                                    <td> ${usuario.UserName}</td>
                                    <td> ${usuario.Nombre}</td>
                                    <td> ${usuario.ApellidoPaterno}</td>
                                    <td> ${usuario.ApellidoMaterno}</td>
                                    <td> ${usuario.Email}</td>
                                    <td> ${usuario.Password}</td>
                                    <td> ${usuario.FechaNacimiento} </td>
                                    <td> ${usuario.Sexo}</td>
                                    <td> ${usuario.Telefono}</td>
                                    <td> ${usuario.Celular}</td>
                                    <td> ${usuario.Estatus}</td>
                                    <td> ${usuario.Curp}</td>
                                    <td><img src="${imagenSrc}" style="width:100%;" /></td>
                                    <td> ${usuario.Rol.Nombre}</td>
                                    <td> ${direccionConcatenada} </td>
                                    
                                    
                                   


                                    <td>
                                           <button class="btn btn-danger" onclick="eliminarUsuario(${usuario.IdUsuario})">
                                            <i class="bi bi-trash"></i> Eliminar
                                            </button>


                                    </td> `;

                    tabla.innerHTML += row;
                });
            } else {
                console.error("Error al obtener usuarios:", result.ErrorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la petición AJAX:", error);
        }
    });
}
//Modal

function showModal() {
    $('#staticBackdrop').modal("show");
}
function cleanModal() {
    $("#IdUsuario").val("");
    $("#UserName").val("");
    $("#Nombre").val("");
    $("#ApellidoPaterno").val("");
    $("#ApellidoMaterno").val("");
    $("#Email").val("");
    $("#password").val("");
    $("#Fecha").val("");
    $("#Curp").val("");
    $("#Telefono").val("");
    $("#Celular").val("");
    $("#ddlRol").val("0");
    $("#Calle").val("");
    $("#NumeroExterior").val("");
    $("#NumeroInterior").val("");
    $("#ddlEstado").val("0");
    $("#ddlMunicipio").val("0");
    $("#ddlColonia").val("0")
    $("input[name='Sexo']").prop("checked", false);
    $("#inptFileImagen").val("");
    $("#img").attr("src", "").hide();
}
function Formulario() {
    cleanModal();
    showModal();
    LlenarRoles()
}


function eliminarUsuario(idUsuario) {
    
    if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
        $.ajax({
            url: rutaEliminar,
            type: 'POST',
            data: { IdUsuario: idUsuario }, 
            success: function (response) {
                if (response.Correct) {
                    alert("Usuario eliminado correctamente.");
                    
                    obtenerUsuarios(); 
                } else {
                    alert("Error al eliminar el usuario: " + response.ErrorMessage);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error en la solicitud de eliminación: ", error);
                alert("Hubo un problema al eliminar el usuario.");
            }
        });
    }
}

function GuardarUsuario() {
    var formData = new FormData(); 

    var idUsuario = $("#IdUsuario").val();
    if (idUsuario) {
        formData.append("IdUsuario", idUsuario);
    }
    formData.append("UserName", $("#UserName").val());
    formData.append("Nombre", $("#Nombre").val());
    formData.append("ApellidoPaterno", $("#ApellidoPaterno").val());
    formData.append("ApellidoMaterno", $("#ApellidoMaterno").val());
    formData.append("Email", $("#Email").val());
    formData.append("Password", $("#password").val());
    formData.append("FechaNacimiento", $("#Fecha").val());
    formData.append("Sexo", $("input[name='Sexo']:checked").val()); 
    formData.append("Curp", $("#Curp").val());
    formData.append("Telefono", $("#Telefono").val());
    formData.append("Celular", $("#Celular").val());
    formData.append("Rol.IdRol", $("#ddlRol").val() || 0);
    formData.append("Direccion.Calle", $("#Calle").val());
    formData.append("Direccion.NumeroExterior", $("#NumeroExterior").val());
    formData.append("Direccion.NumeroInterior", $("#NumeroInterior").val());
    formData.append("Direccion.Colonia.IdColonia", $("#ddlColonia").val() || 0);
    formData.append("Direccion.Colonia.Municipio.IdMunicipio", $("#ddlMunicipio").val() || 0);
    formData.append("Direccion.Colonia.Municipio.Estado.IdEstado", $("#ddlEstado").val() || 0);

    
    var fileInput = $("#inptFileImagen")[0]; 
    if (fileInput.files.length > 0) {
        formData.append("ImagenUsuario", fileInput.files[0]); 
    }

    $.ajax({
        url: rutaForm,
        type: 'POST',
        data: formData,
        processData: false, 
        contentType: false,
        success: function (response) {
            if (response.Correct) {
                alert("Usuario guardado correctamente.");
                location.reload(); // Recarga la página
            } else {
                alert("Error al guardar el usuario: " + response.Message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la solicitud:", error);
            alert("Hubo un problema al guardar el usuario.");
        }
    });
}


function CargarUsuario(IdUsuario) {
    Formulario()
    $.ajax({
        url: rutaForm,
        type: "GET",
        data: { IdUsuario: IdUsuario },
        success: function (response) {
            if (response.Correct) {
                $("#IdUsuario").val(response.Usuario.IdUsuario);
                $("#UserName").val(response.Usuario.UserName);
                $("#Nombre").val(response.Usuario.Nombre);
                $("#ApellidoPaterno").val(response.Usuario.ApellidoPaterno);
                $("#ApellidoMaterno").val(response.Usuario.ApellidoMaterno);
                $("#Email").val(response.Usuario.Email);
                $("#password").val(response.Usuario.Password);
                $("#Fecha").val(response.Usuario.FechaNacimiento);
                $("#Curp").val(response.Usuario.Curp);
                $("#Telefono").val(response.Usuario.Telefono);
                $("#Celular").val(response.Usuario.Celular);
                $("#ddlRol").val(response.Usuario.Rol.IdRol);
                $("#Calle").val(response.Usuario.Direccion.Calle);
                $("#NumeroExterior").val(response.Usuario.Direccion.NumeroExterior);
                $("#NumeroInterior").val(response.Usuario.Direccion.NumeroInterior);
                
                $("#IdRol").empty();
                $.each(response.Roles, function (index, rol) {
                    $("#ddlRol").append(`<option value="${rol.IdRol}">${rol.Nombre}</option>`);
                });
                $("#ddlRol").val(response.Usuario.Rol.IdRol || "");

                $("#ddlEstado").empty();
                $.each(response.Estados, function (index, estado) {
                    $("#ddlEstado").append(`<option value="${estado.IdEstado}">${estado.Nombre}</option>`);
                });
                $("#ddlMunicipio").val(response.Usuario.Direccion.Colonia.Municipio.Estado.IdEstado || "");


                $("#ddlMunicipio").empty();
                $.each(response.Municipios, function (index, municipio) {
                    $("#ddlMunicipio").append(`<option value="${municipio.IdMunicipio}">${municipio.Nombre}</option>`);
                });
                $("#ddlMunicipio").val(response.Usuario.Direccion.Colonia.Municipio.IdMunicipio || "");

                $("#ddlColonia").empty();
                $.each(response.Colonias, function (index, colonia) {
                    $("#ddlColonia").append(`<option value="${colonia.IdColonia}">${colonia.Nombre}</option>`);
                });
                $("#ddlColonia").val(response.Usuario.Direccion.Colonia.IdColonia || "");

                if (response.Sexo) {

                $("input[name='Sexo'][value='" + response.Usuario.Sexo + "']").prop("checked", true);
                }

                if (response.Usuario.Imagen) {
                    $("#img").attr("src", "data:image/jpeg;base64," + response.Usuario.Imagen);
                } else {
                    $("#img").attr("src", ""); // Limpiar si no hay imagen
                }

            } else {
                alert("Error al cargar el usuario: " + response.Message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error al obtener usuario:", error);
        }
    });
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
function arrayBufferToBase64(buffer) {
    let binary = '';
    let bytes = new Uint8Array(buffer);
    let len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return btoa(binary);
}

function cargarImagenDesdeBytes(byteArray) {
    let base64String = arrayBufferToBase64(byteArray);
    return `data:image/png;base64,${base64String}`;
}

//Funcionamiento ddl 
document.addEventListener("DOMContentLoaded", function () {
    CargarEstados();
    document.getElementById("ddlEstado").addEventListener("change", MunicipioGetBYIdEstado);
    document.getElementById("ddlMunicipio").addEventListener("change", ColoniaGetBYIdMunicipio);
});

// Obtener y llenar Estados
function CargarEstados() {
    fetch("/Usuario/EstadoGetAll")
        .then(response => response.json())
        .then(data => {
            let ddlEstado = document.getElementById("ddlEstado");
            limpiarOpciones("ddlEstado");

            if (data.Objects) {
                data.Objects.forEach(estado => {
                    let option = new Option(estado.Nombre, estado.IdEstado);
                    ddlEstado.appendChild(option);
                });
            }
        })
        .catch(error => console.error("Error al obtener estados:", error));
}

// Obtener y llenar Municipios al seleccionar un Estado
function MunicipioGetBYIdEstado() {
    let idEstado = document.getElementById("ddlEstado").value;
    limpiarOpciones("ddlMunicipio");
    limpiarOpciones("ddlColonia"); // Limpiar colonias al cambiar de estado

    if (idEstado) {
        fetch(`/Usuario/MunicipioGetByIdEstado?IdEstado=${idEstado}`)
            .then(response => response.json())
            .then(data => {
                let ddlMunicipio = document.getElementById("ddlMunicipio");

                if (data.Objects) {
                    data.Objects.forEach(municipio => {
                        let option = new Option(municipio.Nombre, municipio.IdMunicipio);
                        ddlMunicipio.appendChild(option);
                    });
                }
            })
            .catch(error => console.error("Error al obtener municipios:", error));
    }
}

// Obtener y llenar Colonias al seleccionar un Municipio
function ColoniaGetBYIdMunicipio() {
    let idMunicipio = document.getElementById("ddlMunicipio").value;
    limpiarOpciones("ddlColonia");

    if (idMunicipio) {
        fetch(`/Usuario/ColoniaGetByIdMunicipio?IdMunicipio=${idMunicipio}`)
            .then(response => response.json())
            .then(data => {
                let ddlColonia = document.getElementById("ddlColonia");

                if (data.Objects) {
                    data.Objects.forEach(colonia => {
                        let option = new Option(colonia.Nombre, colonia.IdColonia);
                        ddlColonia.appendChild(option);
                    });
                }
            })
            .catch(error => console.error("Error al obtener colonias:", error));
    }
}

// Función para limpiar opciones de un select
function limpiarOpciones(idSelect) {
    let ddl = document.getElementById(idSelect);
    ddl.innerHTML = '<option value="">Selecciona una opción</option>';
}
//Estatus
function CambioEstatus(IdUsuario, input) {
    let estatus = input.checked;

    $.ajax({
        url: CambioEstatus,
        type: "POST",
        data: { IdUsuario: IdUsuario, Estatus: estatus },
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                alert("Estatus actualizado correctamente");
            } else {
                alert("Hubo un error al actualizar el estatus");
                input.checked = !estatus; // Revertir cambio si falla
            }
        },
        error: function (xhr) {
            console.log(xhr);
            alert("Error en la petición");
            input.checked = !estatus; // Revertir cambio si hay error
        }
    });
}

function LlenarRoles() {
    $.ajax({
        url: ddlRol, 
        type: 'GET',
        success: function (response) {
            if (response.Correct) {
                
                $("#ddlRol").empty();
                $("#ddlRol").append('<option value="0">Seleccione un rol</option>');
                $.each(response.Objects, function (index, rol) {
                    $("#ddlRol").append('<option value="' + rol.IdRol + '">' + rol.Nombre + '</option>');
                });
            } else {
                alert("Error al cargar los roles: " + response.Message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error al obtener roles:", error);
            alert("Hubo un problema al cargar los roles.");
        }
    });
}



//Validaciones
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

$("#Fecha").datepicker({
    dateFormat: "dd/mm/yy",
    showAnim: "fold",
    changeMonth: true,
    changeYear: true
})

$(document).ready(

)

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