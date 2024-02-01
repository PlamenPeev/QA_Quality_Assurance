function roadRadar(speed, area) {
    'use strict';

    let speedLimit = 0;
    if (area === 'motorway') {
        speedLimit = 130;
    } else if (area === 'interstate') {
        speedLimit = 90;
    } else if (area === 'city') {
        speedLimit = 50;
    } else if (area === 'residential') {
        speedLimit = 20;
    }

    let difference = speedLimit - speed;


    if (difference >= 0) {

        console.log(`Driving ${speed} km/h in a ${speedLimit} zone`);

    } else if(difference < 0){
        let status = '';
        let absDifference = Math.abs(difference);
        if (absDifference > 0 && absDifference <= 20) {
            status = 'speeding';
        } else if (absDifference > 20 && absDifference <= 40) {
            status = 'excessive speeding';
        } else {
            status = 'reckless driving';
        }
        console.log(`The speed is ${absDifference} km/h faster than the allowed speed of ${speedLimit} - ${status}`);
    }
}

roadRadar(78, 'interstate');