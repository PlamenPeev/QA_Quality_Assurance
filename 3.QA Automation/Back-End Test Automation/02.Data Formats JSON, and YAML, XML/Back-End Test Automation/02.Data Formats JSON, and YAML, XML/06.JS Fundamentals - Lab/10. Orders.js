function totalPrice( product, quantity){

    let price = 0;
    if(product === 'coffee'){
        price = 1.50;
    }else  if(product === 'water'){
        price = 1.00;
    } if(product === 'coke'){
        price = 1.40;
    } if(product === 'snacks'){
        price = 2.00;
    }

    let result = quantity * price;
    console.log(result.toFixed(2));
}

totalPrice("coffee", 2);