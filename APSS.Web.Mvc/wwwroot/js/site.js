// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".sidebar ul li").on('click', function () {
    $(".sidebar ul li.active").removeClass('active');
    $(this).addClass('active');
});
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
    $("navbar-toggler").addClass('toggle_menu');
});

$(document).ready(function () {
    $('#dtBasicExample').DataTable();
    $('.dataTables_length').addClass('bs-select');
});
document.getElementById("focus").focus();

$('#sandbox-container .input-group.date').datepicker({
    language: "ar"
});
$("#sandbox-container-p .input-group.date-p").datepicker({
    language: "ar"
});

document.getElementById("focus").focus();

const notchedOutline = new MDCNotchedOutline(document.querySelector('.mdc-notched-outline'));
function myFunction() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[2];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

// Function to format 1 in 01
const zeroFill = n => {
    return ('0' + n).slice(-2);
}

// Creates interval
const interval = setInterval(() => {
    // Get current time
    const now = new Date();

    // Format date as in mm/dd/aaaa hh:ii:ss
    const dateTime = zeroFill((now.getMonth() + 1)) + '/' + zeroFill(now.getUTCDate()) + '/' + now.getFullYear() + ' ' + zeroFill(now.getHours()) + ':' + zeroFill(now.getMinutes()) + ':' + zeroFill(now.getSeconds());

    // Display the date and time on the screen using div#date-time
    document.getElementById('date-time').innerHTML = dateTime;
}, 1000);

function otherSystem() {
    var othersystem = document.getElementById("system1");
    var iconro = document.getElementsByClassName("rotate");
    if (othersystem.style.display == "none") {
        othersystem.style.display = "block";
        iconro.classList.add("rotation");
    }
    else
        othersystem.style.display = "none";
    iconro.classList.remove("rotation");
}

function searchTable() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        td1 = tr[i].getElementsByTagName("td")[1];
        td2 = tr[i].getElementsByTagName("td")[2];
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