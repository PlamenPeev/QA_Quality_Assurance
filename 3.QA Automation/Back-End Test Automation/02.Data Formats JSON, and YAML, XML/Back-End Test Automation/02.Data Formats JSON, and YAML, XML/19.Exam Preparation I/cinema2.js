function cinema(input) {
    let numberOfMovies = Number(input[0]);
    let movies = input.slice(1, numberOfMovies + 1);
    let commands = input.slice(numberOfMovies + 1);

    for (let command of commands) {
        let tokens = command.split(' ');

        if (tokens[0] === "Sell") {

            if (movies.length > 0) {
                console.log(`${movies.shift()} ticket sold!`);
                
            }
        }
        if (tokens[0] === "Add") {

            let movieToAdd = tokens.slice(1).join(' ');
            movies.push(movieToAdd);
            
        }

        if (tokens[0] === "Swap") {

            let startIndex = Number(tokens[1]);
            let endIndex = Number(tokens[2]);

            //-----validate indexces
            if (startIndex < 0 || startIndex >= movies.length || endIndex < 0 || endIndex >= movies.length) {
                continue;
            }
            else {
                //-------swap
                let movieOnFirstIndex = movies[startIndex];
                movies[startIndex] = movies[endIndex];
                movies[endIndex] = movieOnFirstIndex;
                console.log("Swapped!");
            }
            
        }

        if (tokens[0] === "End") {
            if (movies.length > 0) {
                console.log(`Tickets left: ${movies.join(', ')}`);
            } else {
                console.log("The box office is empty");
            }
        }
    }
}


//cinema(['3', 'Star Wars', 'Harry Potter', 'The Hunger Games', 'Sell', 'Sell', 'Sell', 'End'])

cinema(['3', 'Avatar', 'Titanic', 'Joker', 'Sell', 'Swap 0 1', 'End'])