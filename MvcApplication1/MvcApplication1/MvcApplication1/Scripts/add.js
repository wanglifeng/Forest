var count = 0;
var count1 = 1;

$(document).ready(function () {
    $('#addcol').click(function () {
        var html = '<tr><td>';
        count = count + 1;
        count1 = count1 + 1;
        var name = "第" + count1 + "组数据";
        var out = "out" + count;
        var fin = "in" + count;
        var flow = "flow" + count;

        html = html + '<input type="text" value=' + name + ' />';
        html = html + '</td><td><input type="text" id=' + out + ' value=0 />';
        html = html + '</td><td><input type="text" name=' + fin + '  value=0 />';
        html = html + '</td><td><input type="text" id=' + flow + '   value=0 />';
        $(html).appendTo($('#dataTable'));

        var a = dataTable.out0.value;
        //out_pingjun = $('#'+out+'').text() * $('#flow').text();
        out_pingjun = $('#out').text() * $('#flow').text();
        document.write("<h1>" + out_pingjun + "</h1>");
    });

    document.write("<h1>" + out_pingjun + "</h1>");
});


function bindTdCloseIcon() {
    $('input[name="name1"] :last').autocomplete({
        source: ['左臂', '前胸', '后背',],
        minLength: 0
    }).autocomplete('search', '');
}