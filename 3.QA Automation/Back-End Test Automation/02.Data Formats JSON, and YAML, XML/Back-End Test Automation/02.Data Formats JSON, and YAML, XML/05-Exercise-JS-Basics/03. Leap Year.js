function leapYear(year) {
    'use strict';

    let isLeapYear = (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;

    if (isLeapYear) {
        console.log(`yes`);
    } else {
        console.log(`no`);
    }

    // const message = isLeapYear ? 'yes' : 'no';
    // console.log(message);
}

leapYear(1984);
leapYear(2003);