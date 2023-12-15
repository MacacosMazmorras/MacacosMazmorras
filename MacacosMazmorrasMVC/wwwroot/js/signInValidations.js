//sign in validations 

const form = document.getElementById("signInValidation"); //acces to signin form with ID al formulario a través del id.
const input = document.querySelectorAll("#signInValidation input"); //Select all inputs inside form


//regular expressions for all inputs.
const validations = {
    UsuarioName: /^[a-zA-Z]{3,}$/,//minimo 3 caracteres y solamente se peuden letras.
    UsuarioMail: /^[a-zA-Z0-9._-]{3,}@[a-zA-Z0-9]{2,10}\.[a-zA-Z]{2,5}$/, //se pueden letras, número _ . - mínimo 3 caracteres. @ y minimo 2 caracteres + . + minimo2 caracteres.
    UsuarioPassword: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[^\s]{3,8}$/,//mínimo 1 minúscula, 1 mayúscula y un número. Máximo 8 caracteres.
}

//required
//marcar que todos son falsos en un principio porque estan incorrectos, através de "itemvalidation() se convertira, en true o false.".
let inputBoolean = {
    UsuarioName: false,
    UsuarioMail: false,
    UsuarioPassword: false,
}

const formValidation = (e) => { //identificamos en que input se ejecuta el evento (e). 
    switch (e.target.id) {
        case "UsuarioName":
            itemValidation(validations.UsuarioName, e.target, e.target.id); //se envia los siguientes parámetros.
            break;
        case "UsuarioMail":
            itemValidation(validations.UsuarioMail, e.target, e.target.id);
            break;
        case "UsuarioPassword":
            itemValidation(validations.UsuarioPassword, e.target, e.target.id);
            break;
    }
}

const itemValidation = (validations, input, id,) => { //le pasamos validation, el input (el valor del input) y la id).
    if (validations.test(input.value)) {
        document.getElementById(`${id}`).classList.remove("is-invalid"); //se quita la classe is-invalid.
        document.getElementById(`${id}`).classList.add("is-valid"); //se añade la clase is-valid.
        // document.querySelector(".invalid-feedback").classList.remove("d-block"); //se usa la classe invalid-feedback para quitar y añadir el texto.
        inputBoolean[id] = true;
    } else {
        document.getElementById(`${id}`).classList.add("is-invalid");
        document.getElementById(`${id}`).classList.remove("is-valid");
        // document.querySelector(".invalid-feedback").classList.add("d-block"); //se añade la clase display block de boostrap.
        inputBoolean[id] = false;
    }
}

input.forEach((input) => {
    input.addEventListener("keyup", formValidation) //cuando se levante la tecla, se ejecutará la función "formValidation" //keyup => es cuando se levanta la tecla.
    input.addEventListener("blur", formValidation); //blur, es que se ejecute si le da click fuera del input.
});

form.addEventListener("submit", (e) => {
    e.preventDefault(); //event para evitar que se envie el formulario antes de tiempo.

    if (inputBoolean.UsuarioName && inputBoolean.UsuarioMail && inputBoolean.UsuarioPassword) {

        // Realizar la redirección al controlador en el servidor
        try {
            const response = await fetch('/usuario/signin', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    UsuarioName: input.UsuarioName,
                    UsuarioMail: input.UsuarioMail,
                    UsuarioPassword: input.UsuarioPassword,
                }),
            });

            if (response.ok) {
                // Si la respuesta del servidor es exitosa, resetea el formulario
                form.reset();
                //Lo de abajo, es para eliminar la clase is-valid para que desaparezca también en cuanto depararezca toda la información.
                document.querySelectorAll(".is-valid").forEach((input) => { //seleccionamos la classes en All. indicamos que es del input.
                    input.classList.remove("is-valid");//y cogemos todas las classlits, dentro de input
                });

                document.getElementById("validMessage").innerHTML = "Your form was submitted successfully!" //aparece si esta todo correcto
                document.getElementById("validMessage").classList.add("text-success"); //letras verdes
                document.getElementById("validMessage").classList.remove("invisible"); ////hacemos que desaparexca la classe invisible para que aparezca el mensaje
                setTimeout(() => { //función flecha para hacer aparecer un mensaje durante un tiemmpo determinado
                    document.getElementById("validMessage").classList.add("invisible");//hacemos que se elimine la classse sinvisble para que vuelva a aparecer a los 5 segundos respués del mensaje.
                }, 5000); //después de la coma va el tiempo que durará el mensaje en pantalla.
            } else {
                // Manejar el caso en que la respuesta del servidor no sea exitosa
                console.error('Error en el servidor:', response.status);
            }
        } catch (error) {
            console.error('Error de red:', error);
        }

        //form.reset(); //se resetea todo el formulario en cuanto se envia la información
        ////Lo de abajo, es para eliminar la clase is-valid para que desaparezca también en cuanto depararezca toda la información.
        //document.querySelectorAll(".is-valid").forEach((input) => { //seleccionamos la classes en All. indicamos que es del input.
        //    input.classList.remove("is-valid");//y cogemos todas las classlits, dentro de input
        //});

        //document.getElementById("validMessage").innerHTML = "Your form was submitted successfully!" //aparece si esta todo correcto
        //document.getElementById("validMessage").classList.add("text-success"); //letras verdes
        //document.getElementById("validMessage").classList.remove("invisible"); ////hacemos que desaparexca la classe invisible para que aparezca el mensaje
        //setTimeout(() => { //función flecha para hacer aparecer un mensaje durante un tiemmpo determinado
        //    document.getElementById("validMessage").classList.add("invisible");//hacemos que se elimine la classse sinvisble para que vuelva a aparecer a los 5 segundos respués del mensaje.
        //}, 5000); //después de la coma va el tiempo que durará el mensaje en pantalla.

    } else {
        document.getElementById("invalidMessage").innerHTML = "You have to fill in all the fields of the form "; //aparece si hay algun campo que falta por rellenar
        document.getElementById("invalidMessage").classList.add("text-danger");
        document.getElementById("invalidMessage").classList.remove("invisible");//hacemos que desaparexca la classe invisible para que aparezca el mensaje
        setTimeout(() => {//función flecha para hacer aparecer un mensaje durante un tiemmpo determinado
            document.getElementById("invalidMessage").classList.add("invisible"); //hacemos que se elimine la classse sinvisble para que vuelva a aparecer a los 5 segundos respués del mensaje.
        }, 5000); //después de la coma va el tiempo que durará el mensaje en pantalla.
    }
});