function phoneBook(input){

    let names = {};

    input.forEach(element => {
        let keyValuePair = element.split(' ');
        let name = keyValuePair[0];
        let phone = keyValuePair[1];

        names[name] = phone;
    });

    for(let key in names){
        console.log(`${key} -> ${names[key]}`);
    }
}
phoneBook(['George 0552554',
'Peter 087587',
'George 0453112',
'Bill 0845344']
);