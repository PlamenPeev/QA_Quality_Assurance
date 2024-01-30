function printAndSum(startNum, endSum) {
    'use stric';

    let result = '';
    let sum = 0;
    for (let i = startNum; i <= endSum; i++) {
        sum += i;
        result += `${i} `;
    }
    console.log(result);
    console.log(`Sum: ${sum}`);
}

printAndSum(5, 10);