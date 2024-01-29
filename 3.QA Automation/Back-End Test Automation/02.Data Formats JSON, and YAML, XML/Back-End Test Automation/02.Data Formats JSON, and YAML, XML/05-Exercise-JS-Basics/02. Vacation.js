function vacation(numberOfPeople, typeOfGroup, dayOfWeek) {
    
    'use strict';
    let totalCost = 0;

    if (typeOfGroup === 'Students') {
        if (dayOfWeek === 'Friday') {
            totalCost = numberOfPeople * 8.45;
        } else if (dayOfWeek === 'Saturday') {
            totalCost = numberOfPeople * 9.80;
        } else if (dayOfWeek === 'Sunday') {
            totalCost = numberOfPeople * 10.46;
        }

        if(numberOfPeople >= 30){
            totalCost = totalCost * 0.85;
        }
    } else if (typeOfGroup === 'Business') {

        if(numberOfPeople >= 100){
            numberOfPeople -= 10;
        }

        if (dayOfWeek === 'Friday') {
            totalCost = numberOfPeople * 10.90;
        } else if (dayOfWeek === 'Saturday') {
            totalCost = numberOfPeople * 15.60;
        } else if (dayOfWeek === 'Sunday') {
            totalCost = numberOfPeople * 16;
        }
    } else if (typeOfGroup === 'Regular') {
        if (dayOfWeek === 'Friday') {
            totalCost = numberOfPeople * 15;
        } else if (dayOfWeek === 'Saturday') {
            totalCost = numberOfPeople * 20;
        } else if (dayOfWeek === 'Sunday') {
            totalCost = numberOfPeople * 22.50;
        }

        if(numberOfPeople >= 10 && numberOfPeople <= 20){
            totalCost = totalCost * 0.95;
        }
    }

    console.log(`Total price: ${totalCost.toFixed(2)}`);
}

vacation(30,
    "Students",
    "Sunday"
    );

    vacation(40,
        "Regular",
        "Saturday"
        );