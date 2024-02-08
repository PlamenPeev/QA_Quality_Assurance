function stringSubstring(word, text){

    let text1 = text.toLowerCase();

    if (text1.includes(word)) {
        let words = text1.split(' '); 
        if (words.includes(word)) {
            console.log(word);
        } else {
            console.log(`${word} not found!`);
        }
    } else {
        console.log(`${word} not found!`);
    }
}

stringSubstring('best',
'JavaScript is the best programming language'
);

