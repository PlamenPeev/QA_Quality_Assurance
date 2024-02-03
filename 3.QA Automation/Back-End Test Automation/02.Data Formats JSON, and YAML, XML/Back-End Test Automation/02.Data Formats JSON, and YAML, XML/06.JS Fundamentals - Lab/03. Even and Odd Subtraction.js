function evenAndOddDifference(input){

    let evenSum = 0;
    let oddSum = 0;

    for (let i = 0; i < input.length; i++) {
        
        let currentNum = Number(input[i]);
        if(currentNum % 2 == 0){
            evenSum += currentNum;
        }else{
            oddSum += currentNum;
        }
        
    }
    console.log(evenSum - oddSum);
}

evenAndOddDifference([2,4,6,8,10]);