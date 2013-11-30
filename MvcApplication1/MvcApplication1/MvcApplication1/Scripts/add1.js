$(document).ready(function () {
    $('#btnAddUser1').click(function () {
        var formData = {
            'chengfen': $('#chengfen').val(),
            'houdu': $('#houdu').val(),
            'touqixing': $('#touqixing').val(),
            'xuanchuixing': $('#xuanchuixing').val(),
            'yiling': $('#yiling').val(),
            'xiukou': $('#xiukou').val(),
            'dibai': $('#dibai').val(),
            'qitakaikou': $('#qitakaikou').val(),
            'haoxing': $('#haoxing').val(),
            'lingwei': $('#lingwei').val(),
            'xiukouwei': $('#xiukouwei').val(),
            'xiabaiwei': $('#xiabaiwei').val(),
            'qita': $('#qita').val()
        };
        $.post('/ClothInfo/Add', formData, function (data) {
            if (data == true) {
                //alert('');
                alert('save successfully');
                //$('#btnClose').click();
                // document.location.reload();
            }
            else {
                alert('wrong' + cloth);
            }
        }).fail(function () { alert('wrong'); });
        //bindTdCloseIcon();
    });
});