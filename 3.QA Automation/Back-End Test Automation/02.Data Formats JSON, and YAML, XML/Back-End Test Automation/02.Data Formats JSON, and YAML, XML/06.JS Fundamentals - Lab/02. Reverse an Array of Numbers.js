function reverseArray(n, input){

    let reverseArr = [];
   
    for (let i = 0; i < n; i++) {
        let currentElement = input[i];
        reverseArr.unshift(currentElement);
    }

    console.log(reverseArr.join(" "));
    
}

reverseArray(4, [-1, 20, 99, 5]);