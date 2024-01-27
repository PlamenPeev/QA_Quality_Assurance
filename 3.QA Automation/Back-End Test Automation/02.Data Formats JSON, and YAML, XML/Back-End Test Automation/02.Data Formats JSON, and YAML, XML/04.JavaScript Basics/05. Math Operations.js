function mathOperation(firstNum, secondNum, operand){

    let result = 0;
    if(operand === '+'){
        result = firstNum + secondNum;
    }else if(operand === '-'){
        result = firstNum - secondNum;
    }else if(operand === '*'){
        result = firstNum * secondNum;
    }else if(operand === '/'){
        result = firstNum / secondNum;
    }else if(operand === '%'){
        result = firstNum % secondNum;
    }else if(operand === '**'){
        result = firstNum ** secondNum;
    }

    console.log(result);
}

mathOperation(3, 5.5, '*');