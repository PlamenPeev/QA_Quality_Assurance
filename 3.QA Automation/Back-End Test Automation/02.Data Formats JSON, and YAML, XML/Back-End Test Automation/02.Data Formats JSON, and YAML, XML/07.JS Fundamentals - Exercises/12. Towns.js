function townsInfo(towns){

    for(let town of towns){
        let cities = town.split(" | ");
        let city = cities[0];
        let latitude = Number(cities[1]);
        let longitude = Number(cities[2]);
        
       let result = {
            town: city,
            latitude: latitude.toFixed(2),
            longitude: longitude.toFixed(2)
        };

        console.log(result);
    }

}
(townsInfo(['Sofia | 42.696552 | 23.32601',
'Beijing | 39.913818 | 116.363625']
));
