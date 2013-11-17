$(document).ready(function () {
    $('#btnAddUser1').click(function () {
        $.post('/ClothInfo/Add', { 'chengfen': $('#chengfen').val() }, function (cloth) {
            if (data == true) {
                alert('ok');
                //$('#btnClose').click();
               // document.location.reload();
            }
            else {
                alert('wrong' + cloth);
            }
        }).fail(function () { alert('wrong'); });
        bindTdCloseIcon();
    });
});