﻿//get movie to 
function AddMovie(id) {
    $.ajax({
        url: '/Home/AddMyFavoriteMovies',
        type: 'Get',
        data: { moviesId: id },
        success: function () {
            alert("You added movie to favorite!");
        }
    })
}


function RefreshMovie() {
    window.location.href = '/Home/GetNewMovies'
}


function ShowMyMovie() {
    window.location.href = '/Home/ShowMyMovie'
}

function GoHome() {
    window.location.href = '/Home/Index'
}
