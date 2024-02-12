function inventory(data){

    let result = [];

    for(let info of data){
        let parts = info.split(' / ');
        let heroName = parts[0];
        let heroLevel = Number(parts[1]);
        let items = parts[2].split(', ');
        //let items = parts[2] ? parts[2].split(', ') : []; // Handle the case when there are no items

        result.push({ heroName, heroLevel, items });
    }

    let sortedResult = result.sort((a,b) => a.heroLevel - b.heroLevel);
    for(let hero of sortedResult){
        console.log(`Hero: ${hero.heroName}`);
        console.log(`level => ${hero.heroLevel}`);
        console.log(`items => ${hero.items.join(', ')}`);
    }
}
inventory([
    'Batman / 2 / Banana, Gun',
    'Superman / 18 / Sword',
    'Poppy / 28 / Sentinel, Antara'
    ]
    );