function replaceText(text, wordToReplace) {

    // let newWord = '';
    // for (let i = 0; i < wordToReplace.length; i++) {
    //     newWord = newWord.concat('*' + '');
    // }

    const newWord = '*'.repeat(wordToReplace.length);
    const regex = new RegExp(wordToReplace, 'gi');
    
    const replacedText = text.replace(regex, newWord);
    console.log(replacedText);

 }

replaceText('Find hidden the hidden word', 'hidden');