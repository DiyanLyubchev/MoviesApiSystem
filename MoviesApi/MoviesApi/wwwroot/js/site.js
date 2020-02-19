//get movie to 
function AddMovie(id) {
    $.ajax({
        url: '/Home/AddMyFavoriteMovies',
        type: 'Get',
        data: { moviesId: id }
    })
}


function RefreshMovie() {
    window.location.href = '/Home/GetNewMovies'
}


//let myVar = setInterval(function () {

//    window.location.href = '/Home/GetNewMovies'

//}, 1000);

//function myStopFunction() {
//    clearInterval(myVar);
//}