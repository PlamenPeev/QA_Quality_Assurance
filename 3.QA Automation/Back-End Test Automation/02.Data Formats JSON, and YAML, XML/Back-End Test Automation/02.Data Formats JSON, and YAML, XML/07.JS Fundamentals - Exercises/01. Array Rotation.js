function arrayRotation(input, number){
    'use strict;'

    for(let i = 0; i < number; i++){
        let element = input.shift();
        input.push(element);
    }

    console.log(input.join(" "));
}

arrayRotation([32, 21, 61, 1], 4);