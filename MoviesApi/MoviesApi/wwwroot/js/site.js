﻿//get movie to favorite 
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

$(':radio').change(function () {
    const rate = this.value;
    
    $.ajax({
        url: '/Movie/RateMovie?data=' + rate,
        type: 'Get',
    });
});