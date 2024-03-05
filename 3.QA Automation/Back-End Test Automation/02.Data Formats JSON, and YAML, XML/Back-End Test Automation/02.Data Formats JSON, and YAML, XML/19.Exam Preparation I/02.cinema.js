function cinema(input) {
    let numberOfMovies = Number(input[0]);
    let movies = input.slice(1, numberOfMovies + 1);
    let commands = input.slice(numberOfMovies + 1);
  
    for (let command of commands) {
      let tokens = command.split(' ');
  
      switch (tokens[0]) {
        case 'Sell':
          if (movies.length > 0) {
            console.log(`${movies.shift()} ticket sold!`);
          }
          break;
  
        case 'Add':
          let movieToAdd = tokens.slice(1).join(' ');
          movies.push(movieToAdd);
          break;
  
        case 'Swap':
          let startIndex = Number(tokens[1]);
          let endIndex = Number(tokens[2]);

           //-----validate indexces
        //    if(startIndex < 0 || startIndex >= movies.length){
        //        continue;
        //    }
        //    if(endIndex < 0 || endIndex >= movies.length){
        //     continue;
        //    }

          //-------validate and swap  
          if (isValidIndex(startIndex, movies.length) && isValidIndex(endIndex, movies.length)) {
            [movies[startIndex], movies[endIndex]] = [movies[endIndex], movies[startIndex]];
            console.log("Swapped!");
          }
          break;
  
        case 'End':
          if (movies.length > 0) {
            console.log(`Tickets left: ${movies.join(', ')}`);
          } else {
            console.log("The box office is empty");
          }
          break;
  
        default:
          // Ignore unrecognized commands
          break;
      }
    }
  }
  
  function isValidIndex(index, arrayLength) {
    return index >= 0 && index < arrayLength;
  }

  //cinema(['3', 'Star Wars', 'Harry Potter', 'The Hunger Games', 'Sell', 'Sell', 'Sell', 'End'])

  cinema(['3','Avatar', 'Titanic', 'Joker', 'Sell', 'Swap 0 1','End' ])