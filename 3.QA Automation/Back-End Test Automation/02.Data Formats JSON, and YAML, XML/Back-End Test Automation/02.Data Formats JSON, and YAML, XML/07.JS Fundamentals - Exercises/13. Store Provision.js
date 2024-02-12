function store(currentStock, ordered) {

    let allProducts = {};
    for (let i = 0; i < currentStock.length; i += 2) {
        let product = currentStock[i];
        let quantity = Number(currentStock[i + 1]);

        allProducts[product] = quantity;
    }

    for (let i = 0; i < ordered.length; i += 2) {
        let orderedProduct = ordered[i];
        let quantity = Number(ordered[i + 1]);

        
            if (allProducts.hasOwnProperty(orderedProduct)) {
                allProducts[orderedProduct] += quantity;
            } else {
                allProducts[orderedProduct] = quantity;
            }
    }

    for (let item in allProducts) {

        console.log(`${item} -> ${allProducts[item]}`);
    }

}

store([
    'Chips', '5', 'CocaCola', '9', 'Bananas', '14', 'Pasta', '4', 'Beer', '2'
],
    [
        'Flour', '44', 'Oil', '12', 'Pasta', '7', 'Tomatoes', '70', 'Bananas', '30'
    ]
);