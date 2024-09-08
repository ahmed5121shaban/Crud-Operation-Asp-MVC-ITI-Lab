// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(() => {

    $('#open-sidebar').click(() => {

        // add class active on #sidebar
        $('#sidebar').addClass('active');

        // show sidebar overlay
        $('#sidebar-overlay').removeClass('d-none');

    });


    $('#sidebar-overlay').click(function () {

        // add class active on #sidebar
        $('#sidebar').removeClass('active');

        // show sidebar overlay
        $(this).addClass('d-none');

    });

});