function shop(input) {
    let numberOfProduct = Number(input[0]);
    let products = input.slice(1, numberOfProduct + 1);
    let commands = input.slice(numberOfProduct + 1);

    for (let command of commands) {
        let parts = command.split(" ");

        if (parts[0] === "Sell") {
            let sellForstProduct = products.shift();
            console.log(`${sellForstProduct} product sold!`);
        }
        if (parts[0] === "Add") {
            let addProduct = parts[1];
            products.push(addProduct);
        }
        if (parts[0] === "Swap") {
            let startIndex = Number(parts[1]);
            let endIndex = Number(parts[2]);

              if(isValidIndex(startIndex,products.length) && isValidIndex(endIndex,products.length)){
                  let firstToSwapProduct = products[startIndex];
                  products[startIndex] = products[endIndex];
                  products[endIndex] = firstToSwapProduct;
                  console.log("Swapped!");
              }

        }
        if (parts[0] === "End") {
            if (products.length > 0) {
                console.log(`Products left: ${products.join(", ")}`);
            } else {
                console.log(`The shop is empty`);
            }
        }


    }

    function isValidIndex(index, arrayLength){
        return index >=0 && index < arrayLength;
    }


}

//shop(['3', 'Apple', 'Banana', 'Orange', 'Sell', 'Swap 0 1', 'End' ]);

shop(['5', 'Milk', 'Eggs', 'Bread', 'Cheese', 'Butter', 'Add Yogurt', 'Swap 1 4', 'End']) 