function parkingLot(data) {

    let result = [];
    for (let info of data) {

        let park = info.split(", ");
        let direction = park[0];
        let carNumber = park[1];

        if (direction === "IN") {
            if (!result.includes(carNumber)) {
                result.push(carNumber);
            }
        } else if (direction === "OUT") {
            let numberIndex = result.indexOf(carNumber);
            if (numberIndex !== -1) {
                result.splice(numberIndex, 1);
            }
        }
    }

    if (result.length === 0) {
        console.log('Parking Lot is Empty');
    } else {
        result.sort();
        for (let car of result) {
            console.log(car);
        }
    }
}

parkingLot(['IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'IN, CA9999TT',
    'IN, CA2866HI',
    'OUT, CA1234TA',
    'IN, CA2844AA',
    'OUT, CA2866HI',
    'IN, CA9876HH',
    'IN, CA2822UU']
);