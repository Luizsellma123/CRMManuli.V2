function validaUsuario() {
    var theForm = document.aspnetForm;

    if (theForm.ctl00_ContentPlaceHolder1_txtUsuario.value == "" || theForm.ctl00_ContentPlaceHolder1_txtUsuario.value == null) {
        alert("Usuário deve ser preenchido.");
        theForm.ctl00_ContentPlaceHolder1_txtUsuario.focus();
        return false;
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtSenha.value == "" || theForm.ctl00_ContentPlaceHolder1_txtSenha.value == null) {
        alert("Senha deve ser preenchida.");
        theForm.ctl00_ContentPlaceHolder1_txtSenha.focus();
        return false;
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtEmail.value == "" || theForm.ctl00_ContentPlaceHolder1_txtEmail.value == null) {
        alert("Email deve ser preenchido.");
        theForm.ctl00_ContentPlaceHolder1_txtEmail.focus();
        return false;
    }
     
    return true;
}