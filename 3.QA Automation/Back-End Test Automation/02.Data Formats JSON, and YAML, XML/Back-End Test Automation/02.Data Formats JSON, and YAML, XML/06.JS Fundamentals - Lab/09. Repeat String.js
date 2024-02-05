function repeatString(text, n) {

    let newText = [];
    for (let i = 0; i < n; i++) {
        newText.unshift(text);
    }

    console.log(newText.join(''));
}

repeatString("String", 2);