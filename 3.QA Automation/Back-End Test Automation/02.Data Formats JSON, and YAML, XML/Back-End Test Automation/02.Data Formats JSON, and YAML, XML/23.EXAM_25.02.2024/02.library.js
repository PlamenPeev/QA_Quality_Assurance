function library(books) {

    let numberOfBooksInTheLibrary = Number(books[0]);
    let namesOfBooks = books.slice(1, numberOfBooksInTheLibrary + 1);
    let commandsForAction = books.slice(numberOfBooksInTheLibrary + 1);


    for (let command of commandsForAction) {
        let partOfCommand = command.split(' ');

        if (partOfCommand[0] === "Lend") {
            let bookToRemove = namesOfBooks.shift();
            console.log(`${bookToRemove} book lent!`);
        }
        if (partOfCommand[0] === "Return") {
            let putNewBookInTheLibrary = partOfCommand.slice(1).join(' ');
            namesOfBooks.unshift(putNewBookInTheLibrary);
        }
        if (partOfCommand[0] === "Exchange") {

            let startIndex = Number(partOfCommand[1]);
            let endIndex = Number(partOfCommand[2]);

            if (startIndex < 0 || startIndex >= namesOfBooks.length || endIndex < 0 || endIndex >= namesOfBooks.length) {
                continue;
            }
            else {
                let bookOnTheFirstIndex = namesOfBooks[startIndex];
                namesOfBooks[startIndex] = namesOfBooks[endIndex];
                namesOfBooks[endIndex] = bookOnTheFirstIndex;
                console.log("Exchanged!");
            }

        }
        if (partOfCommand[0] === "Stop") {
            if (namesOfBooks.length > 0) {
                console.log(`Books left: ${namesOfBooks.join(', ')}`);
            } else {
                console.log("The library is empty");
            }
            //break;
        }

    }

}

library(['3', 'Harry Potter', 'The Lord of the Rings', 'The Hunger Games', 'Lend', 'Stop', 'Exchange 0 1'])

//library(['5', 'The Catcher in the Rye', 'To Kill a Mockingbird', 'The Great Gatsby', '1984', 'Animal Farm', 'Return Brave New World', 'Exchange 1 4', 'Stop'])

//library(['3', 'The Da Vinci Code', 'The Girl with the Dragon Tattoo', 'The Kite Runner', 'Lend', 'Lend', 'Lend', 'Stop'])