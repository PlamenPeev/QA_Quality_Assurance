function ciikingByNumbers(number, operation1, operation2, operation3, operation4, operation5) {
    'use strict';


          if(operation1 === 'chop'){
            number = number / 2;
            console.log(number);
          }if(operation1 === 'dice'){
            number = Math.sqrt(number,2);
            console.log(number);
          }if(operation1 === 'spice'){
            number = number + 1;
            console.log(number);
          }if(operation1 === 'bake'){
            number = number * 3;
            console.log(number);
          }if(operation1 === 'fillet'){
            number = number * 0.8;
            console.log(number);
          }if(operation2 === 'chop'){
            number = number / 2;
            console.log(number);
          }if(operation2 === 'dice'){
            number = Math.sqrt(number,2);
            console.log(number);
          }if(operation2 === 'spice'){
            number = number + 1;
            console.log(number);
          }if(operation2 === 'bake'){
            number = number * 3;
            console.log(number);
          }if(operation2 === 'fillet'){
            number = number * 0.8;
            console.log(number);
          }if(operation3 === 'chop'){
            number = number / 2;
            console.log(number);
          }if(operation3 === 'dice'){
            number = Math.sqrt(number,2);
            console.log(number);
          }if(operation3 === 'spice'){
            number = number + 1;
            console.log(number);
          }if(operation3 === 'bake'){
            number = number * 3;
            console.log(number);
          }if(operation3 === 'fillet'){
            number = number * 0.8;
            console.log(number);
          }if(operation4 === 'chop'){
            number = number / 2;
            console.log(number);
          }if(operation4 === 'dice'){
            number = Math.sqrt(number,2);
            console.log(number);
          }if(operation4 === 'spice'){
            number = number + 1;
            console.log(number);
          }if(operation4 === 'bake'){
            number = number * 3;
            console.log(number);
          }if(operation4 === 'fillet'){
            number = number * 0.8;
            console.log(number);
          }if(operation5 === 'chop'){
            number = number / 2;
            console.log(number);
          }if(operation5 === 'dice'){
            number = Math.sqrt(number,2);
            console.log(number);
          }if(operation5 === 'spice'){
            number = number + 1;
            console.log(number);
          }if(operation5 === 'bake'){
            number = number * 3;
            console.log(number);
          }if(operation5 === 'fillet'){
            number = number * 0.8;
            console.log(number);
          }
    
}


/////////////////////////////////////////////////////////////////////////////////////////

function ciikingByNumbers(number, ...operations) {
  

    const applyOperation = (operation, value) => {
      switch (operation) {
        case 'chop':
          return value / 2;
        case 'dice':
          return Math.sqrt(value, 2);
        case 'spice':
          return value + 1;
        case 'bake':
          return value * 3;
        case 'fillet':
          return value * 0.8;
        default:
          return value;
      }
    };
  
    for (const operation of operations) {
      number = applyOperation(operation, number);
      console.log(number);
    }
  }

////////////////////////////////////////////////////////////////////////////////////////////////////////////


function ciikingByNumbers(number, ...operations) {
 

    const operationsMap = {
      'chop': value => value / 2,
      'dice': value => Math.sqrt(value, 2),
      'spice': value => value + 1,
      'bake': value => value * 3,
      'fillet': value => value * 0.8,
    };
  
    operations.forEach(operation => {
      const operationFunction = operationsMap[operation];
      if (operationFunction) {
        number = operationFunction(number);
        console.log(number);
      }
    });
  }