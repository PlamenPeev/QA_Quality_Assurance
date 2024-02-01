function sameNumbers(number) {
    'use strict';

    //let count = 0;
    let sum = 0;
    const firstDigit = number % 10;
    let isAllDigitsAreTheSame = true;

    while (number > 0) {
        //count++;
        // sum += number % 10;
        // number = Math.floor(number / 10);

        const currentDigit = number % 10

        if (firstDigit != currentDigit) {
            isAllDigitsAreTheSame = false;
        }

        sum += currentDigit;
        number = Math.floor(number / 10);
    }

    // if( (sum / count) === firstDigit){
    //     console.log(`true`);
    // }else{
    //     console.log(`false`);
    // }

    console.log(isAllDigitsAreTheSame);
    console.log(sum);
}

sameNumbers(2226222223);
sameNumbers(1234);