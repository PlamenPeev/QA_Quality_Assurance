function moneyForFruit(typeOfFruit, grams, pricePerKilo) {
    'use strict';

    let kilogramsOfFruits = grams / 1000;
    let totalSum = kilogramsOfFruits * pricePerKilo;

    console.log(`I need $${totalSum.toFixed(2)} to buy ${kilogramsOfFruits.toFixed(2)} kilograms ${typeOfFruit}.`);
}

moneyForFruit('orange', 2500, 1.80);
moneyForFruit('apple', 1563, 2.35);