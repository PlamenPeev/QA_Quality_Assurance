function revealWords(target, text) {

    let words = target.split(", ");
    
    for (let i = 0; i < words.length; i++) {
        let word = words[i];
        let astericks = "*".repeat(word.length);
        text = text.replace(astericks, word);
    }

    console.log(text);
}

revealWords('great, learning',
'softuni is ***** place for ******** new programming languages'
);