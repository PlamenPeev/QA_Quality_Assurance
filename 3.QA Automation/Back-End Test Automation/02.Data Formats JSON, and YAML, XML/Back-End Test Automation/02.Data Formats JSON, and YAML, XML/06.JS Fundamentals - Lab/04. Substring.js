function substring( text, startIndex, countElements){

    let newText = text.substring(startIndex,  startIndex + countElements);
    console.log(newText);
}

substring('SkipWord', 4, 7);