function movies(data) {
    let result = [];

    for (let info of data) {
        let movieInfo = info.split(" ");

        if (movieInfo.includes("addMovie")) {
            let movieName = movieInfo.slice(1).join(' ');
            result.push({ name: movieName });
        // } else {
        //     let movieName = movieInfo.slice(0, movieInfo.indexOf("directedBy") !== -1 ? movieInfo.indexOf("directedBy") : movieInfo.indexOf("onDate")).join(' ');
        //     let movieIndex = result.findIndex(movie => movie.name === movieName);

        //     if (movieIndex !== -1) {
        //         if (movieInfo.includes("directedBy")) {
        //             let director = movieInfo.slice(movieInfo.indexOf("directedBy") + 1).join(' ');
        //             result[movieIndex].director = director;
        //         } else if (movieInfo.includes("onDate")) {
        //             let date = movieInfo.slice(movieInfo.indexOf("onDate") + 1).join(' ');
        //             result[movieIndex].date = date;
        //         }
        //     }
        // }
    } else {
        // Extract movieName by finding the index of "directedBy" or "onDate"
        let indexOfDirect = movieInfo.indexOf("directedBy");
        let indexOfOnDate = movieInfo.indexOf("onDate");
        let indexOfSeparator = (indexOfDirect !== -1) ? indexOfDirect : indexOfOnDate;
        let movieName = movieInfo.slice(0, indexOfSeparator).join(' ');
    
        // Find the index of the movie in the result array
        let movieIndex = result.findIndex(movie => movie.name === movieName);
    
        if (movieIndex !== -1) {
            // If "directedBy" is present, extract director and update the result
            if (movieInfo.includes("directedBy")) {
                let director = movieInfo.slice(indexOfDirect + 1).join(' ');
                result[movieIndex].director = director;
            }
            // If "onDate" is present, extract date and update the result
            else if (movieInfo.includes("onDate")) {
                let date = movieInfo.slice(indexOfOnDate + 1).join(' ');
                result[movieIndex].date = date;
            }
        }
    }
    }

    result = result.filter(movie => movie.name && movie.director && movie.date);

    for (let movie of result) {
        console.log(JSON.stringify(movie));
    }
}
movies([
    'addMovie The Avengers',
    'addMovie Superman',
    'The Avengers directedBy Anthony Russo',
    'The Avengers onDate 30.07.2010',
    'Captain America onDate 30.07.2010',
    'Captain America directedBy Joe Russo'
    ]
    );