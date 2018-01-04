$('.modulePages,.btnsave').attr('disabled', true);

$('.parentModules').on('change', function () {

    var id = $('.parentModules').val();
    var moduleDdl = $('.modulePages');
    var viewAccess = $('.modulePages').val();
    if (id ==null || id == '') {
        $('.modulePages,.btnsave').attr('disabled', true);
    } else {
        $('.modulePages,.btnsave').removeAttr('disabled', true);
    }
    if (viewAccess != null || viewAccess != '' ) {
        $('#AllowView').attr('checked', true);
    }
    var roleAccId = $('.roleaccId').val();
    $.ajax({
        url: '/Configurations/GetModelByParentModel',
        type: 'GET',
        data: { id: id ,roleId:roleAccId },
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            moduleDdl.html('');
            $.each(data, function (Value, option) {
                moduleDdl.append($('<option>=----Select Pages-----</option>').val(option.Value).html(option.Text));

            });
        },
    });
});