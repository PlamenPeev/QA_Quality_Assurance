function oddAndEvenSum(number) {

    let evenSum = 0;
    let oddSum = 0;
    while (number > 0) {
        let currentDigit = number % 10;
        if (currentDigit % 2 === 0) {
            evenSum += currentDigit;
        } else {
            oddSum += currentDigit;
        }

        number = Math.floor(number / 10)

    }

    console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
}
oddAndEvenSum(3495892137259234);