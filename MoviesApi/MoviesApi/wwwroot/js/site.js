//get movie to 
function AddMovie(id) {
    $.ajax({
        url: '/Movie/AddMyFavoriteMovies',
        type: 'Get',
        data: { moviesId: id },
        success: function () {
            alert("You added movie to favorite!");
        }
    })
}

function ReviewedMovie(id) {
    $.ajax({
        url: '/Movie/ReviewedMovie',
        type: 'Post',
        data: { reviewedMovieId: id },
        success: function () {
            window.location.href = "/movie/showMyMovie/";
        }
    });
}

function RefreshMovie() {
    window.location.href = '/Home/GetNewMovies'
}


function ShowMyMovie() {
    window.location.href = '/Movie/ShowMyMovie'
}

function GoHome() {
    window.location.href = '/Home/Index'
}



$(':radio').change(function () {
    const rate = this.value;
    
    $.ajax({
        url: '/Movie/RateMovie?data=' + rate,
        type: 'Get',
    });
});