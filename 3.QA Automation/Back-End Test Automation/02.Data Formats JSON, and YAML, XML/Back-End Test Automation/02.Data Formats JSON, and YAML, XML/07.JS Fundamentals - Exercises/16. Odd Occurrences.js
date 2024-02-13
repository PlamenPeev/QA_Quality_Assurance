function extractOddOccurrences(input) {
    let words = input.toLowerCase().split(' ');
    let wordCount = {};

    
    for (let word of words) {
        wordCount[word] = (wordCount[word] || 0) + 1;
    }

    
    let oddOccurrences = Object.keys(wordCount).filter(word => wordCount[word] % 2 !== 0);

    
    let result = oddOccurrences.join(' ');

    console.log(result);
}
extractOddOccurrences('Cake IS SWEET is Soft CAKE sweet Food');