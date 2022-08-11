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
    $(".navbar-toggler").addClass('toggle_menu');
});