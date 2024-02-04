function foundOccurences(text, word){

    let count = 0;
    let index = text.indexOf(word);

    while(index !== -1){
        count++;
        index = text.indexOf(word, index + 1);
    }

    console.log(count);
}

foundOccurences('This is a word and it also is a sentence','is');