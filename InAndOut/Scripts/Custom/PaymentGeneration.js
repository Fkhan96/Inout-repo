function Trigger() {
    getList();
}

var data = [];
var activeid;
var oTable;

function getList() {
    var myUrl = "/PaymentGeneration/GetList";
    var myData = {};
    XHRGETRequest(myUrl, myData, function (result) {
        data = result;
        if (oTable) { oTable.fnDestroy() }
        $('#listtb tbody').html('');
        if (!result || !result.length) { return; }
        for (var i = 0; i < result.length; i++) {
            var item = result[i];
            if (item.UserPictureUrl != null) {
                item.UserPictureUrl = "data:image/png;base64," + item.UserPictureUrl;
            }
            else {
                item.UserPictureUrl = "../images/dummy.jpg";
            }
            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.EmpID);
            $(tr).append('<td>' + (i + 1) + '</td>');
            $(tr).append('<td>' + '<img src="' + item.UserPictureUrl + '" alt="" style="width:50px; height:auto;" />' + '</td>');
            $(tr).append('<td>' + item.Name + '</td>');
            $(tr).append('<td>' + item.SelfId + '</td>');
            $(tr).append('<td>').find('td:last').append(ActiontdHtml(item));
            $('#listtb tbody').append(tr);
        }
        oTable = $('#listtb').dataTable({ "destroy": true });
    });
}


function ActiontdHtml(item) {
    var td = $('<td>');
    $(td).append('<span class="pad"><a href="#" class="tblView fa fa-eye" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return View(' + item.EmpID + ');" data-toggle="tooltip" data-placement="top" title="View"></a></span>');
    $(td).append('<span class="pad"><a href="#" class="tblView fa fa-trash-o demo3" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return Delete(' + item.EmpID + ');" data-toggle="tooltip" data-placement="top" title="Delete"></a></span>');
    return $(td).html();
}

function View(e) {
    var id = e;
    window.location.href = "/Attendance/AttDetails?id=" + id;

}