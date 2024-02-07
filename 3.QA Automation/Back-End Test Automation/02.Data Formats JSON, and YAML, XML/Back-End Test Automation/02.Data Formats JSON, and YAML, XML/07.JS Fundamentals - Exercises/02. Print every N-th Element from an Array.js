function printElement(input, number){

let result = [];
for(let i = 0; i < input.length; i += number){
    
    result.push(input[i]);
}

return result;
}

console.log(printElement(['5', 
'20', 
'31', 
'4', 
'20'], 
2
));