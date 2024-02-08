function sortNumbers(numbers) {

    numbers.sort((a,b) => a-b);

    let sortedArray = [];
    let countelement = numbers.length;

    while (numbers.length > 0) {

        let firstElement = numbers.shift();
        let lastElement = numbers.pop();
        sortedArray.push(firstElement);

        
        if (lastElement) {
            sortedArray.push(lastElement);
        }
    }

    return sortedArray;
}

console.log(sortNumbers([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));