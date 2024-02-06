function city(cityInfo){

    let result = '';

    for( const key in cityInfo){
        if(cityInfo.hasOwnProperty(key)){
            result += `${key} -> ${cityInfo[key]}\n`;
        }
    }
    return result.trim();
}
city({
    name: "Sofia",
    area: 492,
    population: 1238438,
    country: "Bulgaria",
    postCode: "1000"});

