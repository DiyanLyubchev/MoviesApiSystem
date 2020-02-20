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


//0.5
$('#starhalf').on('click', function () {
    const halfStar = $('#starhalf').val();
    console.log(halfStar);
});

//1
$('#star1').on('click', function () {
    const oneStar = $('#star1').val();
    console.log(oneStar);
});

//1.5
$('#star1half').on('click', function () {
    const oneAndHalfStar = $('#star1half').val();
    console.log(oneAndHalfStar);
});

//2.
$('#star2').on('click', function () {
    const twoStar = $('#star2').val();
    console.log(twoStar);
});

//2.5
$('#star2half').on('click', function () {
    const twoAndHalfStar = $('#star2half').val();
    console.log(twoAndHalfStar);
});


//3.
$('#star3').on('click', function () {
    const threeStar = $('#star3').val();
    console.log(threeStar);
});

//3.5
$('#star3half').on('click', function () {
    const threeAndHalfStar = $('#star3half').val();
    console.log(threeAndHalfStar);
});

//4.
$('#star4').on('click', function () {
    const fourStar = $('#star4').val();
    console.log(fourStar);
});

//4.5
$('#star4half').on('click', function () {
    const fiveAndHalfStar = $('#star4half').val();
    console.log(fiveAndHalfStar);
});

//5.
$('#star5').on('click', function () {
    const fiveStar = $('#star5').val();
    console.log(fiveStar);
});