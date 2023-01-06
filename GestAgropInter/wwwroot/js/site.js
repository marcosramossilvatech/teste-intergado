function somenteNumeros(num) {
    var er = /[^0-9]/;
    er.lastIndex = 0;
    var campo = num;
    if (er.test(campo.value)) {
        campo.value = "";
    }
}

function verificaTagUnica(num) {

    event.preventDefault();
    if (num.value.length === 15) {
        $.ajax({
            url: "/Animal/VerificaTag?tag=" + num.value,
            method: 'GET',
            contentType: 'application/json',
            success: function (result) {
                if (result.mensagem!== "not")
                    swal({
                        title: "Erro",
                        text: "Já existe animal cadastrado com essa Tag",
                        icon: "warning"
                    });
            }
        });
    }
}

