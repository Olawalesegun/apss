showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
        }
    })
}

/*$(".sidebar ul li").on('click', function () {
    $(".sidebar ul li.active").removeClass('active');
    $(this).addClass('active');
});*/
$(".open-btn").on('click', function () {
    $(".sidebar").addClass('active');
});
$(".dashboard-content").on('click', function () {
    $('.sidebar').removeClass('active');
})

$(".close-btn").on('click', function () {
    $(".sidebar").removeClass('active');
});

//hiden sidebar when chooce any item from sidebar
$(".sidebar ul li").on('click', function () {
    $(".sidebar").removeClass('active');
});
//hiden sidebar when click on any other plays
$("content").on('click', function () {
    $(".sidebar").removeClass('active');
});
$(".navbar img").on('click', function () {
    $(".sidebar").removeClass('active');
});
$(".sidebar ul li").on('click', function () {
    $(".navbar-toggler").addClass('toggle_menu');
});

function searchTable() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];

        if (td) {
            txtValue = td.textContent || td.innerText;

            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            }

            else {
                tr[i].style.display = "none";
            }
        }
    }
}

var myVar;

function myFunction() {
    myVar = setTimeout(showPage, 1000);
}

function showPage() {
    document.getElementById("loader").style.display = "none";
    document.getElementById("table").style.display = "block";
}

$("#drops").on('click', function () {
    $('.rotate').toggleClass('down');
});

$(document).ready(function () {
    $('.toast').toast('show');
    $("#myBtn").click(function () {
        $('.toast').toast({
            delay: 10000,
            showNotification: true,
        });
    });
});