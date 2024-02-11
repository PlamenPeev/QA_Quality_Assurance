function passwordValidator(password){

    let isLengthvalid = password.length >= 6 && password.length <= 10;
    let regex = /^[A-Za-z0-9]*$/;
    let isOnlyLettersAndDigits = regex.test(password);
    let regex2 = /\d{2,}/;
    let isTwoDigitsContains = regex2.test(password);

    if(isLengthvalid && isOnlyLettersAndDigits && isTwoDigitsContains){
        console.log(`Password is valid`);
    }if(!isLengthvalid){
        console.log(`Password must be between 6 and 10 characters`);
    }if(!isOnlyLettersAndDigits){
        console.log(`Password must consist only of letters and digits`);
    }if(!isTwoDigitsContains){
        console.log(`Password must have at least 2 digits`);
    }
}

passwordValidator('Pa$s$s');