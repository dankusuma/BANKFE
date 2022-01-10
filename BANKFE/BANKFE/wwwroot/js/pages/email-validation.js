const isEmailValid = (email) => {
    const pattern = /^\w+([-+.'][^\s]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    const isValid = pattern.test(email);
    //console.log('is email ' + email + ' valid: ' + isValid);

    return isValid;
}