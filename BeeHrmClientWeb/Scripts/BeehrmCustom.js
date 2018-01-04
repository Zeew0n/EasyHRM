/*************
Writtern By Shree

*************/

$(document).ready(function () {
    $(".confirm_delete").click(function () {
        if (confirm("Are you sure you want to delete this record?"))
            return true;
        else
            return false;
    });


});