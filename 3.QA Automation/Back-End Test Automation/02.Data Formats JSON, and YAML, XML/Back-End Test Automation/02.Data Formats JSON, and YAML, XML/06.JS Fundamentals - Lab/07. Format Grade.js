function formatGrade(number) {

    let description = '';
    if (number < 3.00) {
        description = 'Fail'
    }else if (number >= 3.00 && number < 3.50) {
        description = 'Poor'
    }if (number >= 3.50 && number < 4.50) {
        description = 'Good'
    }if (number >= 4.50 && number < 5.50) {
        description = 'Very good'
    }if (number >= 5.50) {
        description = 'Excellent'
    } 

    if(description === 'Fail'){
        console.log(`Fail (2)`);
    }else{
        console.log(`${description} (${number.toFixed(2)})`);
    }
    
}

formatGrade(2.99);