function Trigger() {
    getList();

    $('#employeedetails').click(function () {
        var empId = parseInt($(this).attr('empId'));
        Details(empId);
    });

    $('.JoiningDate').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true
    });
}

var data = [];
var activeid;
var oTable;

function getList() {
    var myUrl = "/Employee/GetList";
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
            $(tr).append('<td>' + item.EmpBiometric + '</td>');
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

function Delete(e) {
    swal({
        title: "Are You Sure?",
        text: "The elimination will make it impossible to recover the content",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, proceed with the elimination !",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            var id = e;
            var myurl = "/Employee/Delete";
            var data = { id: id };
            XHRPOSTRequest(myurl, data, function (result) {
                getList();
                swal("Deleted!", "The Record has been eliminated", "success");
            });
        }
        $(".sweet-alert .sa-button-container .sa-custom-check").remove();
    });
    window.setTimeout(function () {
        $(".sweet-alert .sa-button-container").append('<div class="sa-custom-check"></div>');
    });
}

function Submit(e) { 
    var parent = $('#modalAdd');
    if (!isFillRequired($("#modalAdd"))) {
        swal('Please Fill Required Fields');
        return false;
    }
    var callback = function () {
        var data = {};
        $('[name]', parent).each(function () {
            if ($(this).data('type') == "date") {
                data[$(this).attr('name')] = moment($(this).val() || $(this).data('val'), 'DD/MM/YYYY').format('MM/DD/YYYY');
            }
            else if (this.name == "UserPictureUrl") {
                data[$(this).attr('name')] = this.src.split(',')[1];
            }
            else {
                data[$(this).attr('name')] = $(this).val() || $(this).data('val');
            }
        });
        var myurl = "/Employee/Add";
        if (activeid) {
            data["EmpDetailsID"] = activeid;
            myurl = "/Employee/Edit";
        }
        ShowAjaxLoader();
        XHRPOSTRequest(myurl, data, function (result) {
            $('#modalAdd').modal('hide');
            getList();
            HideAjaxLoader();
            showNotification("Saved!", "success");
            activeuserid = null;
            $("#modalAdd").modal('hide');
            ResetFields();
        });
    }
    var _id = activeid || 0;
    Validate([
        { table: "Employee", column: "SelfId", value: $('[name=SelfId]').val(), ignorecondition: "EmpID!=" + _id, error: "Employee  Already Exist" },
    ], callback);
    return false;
}

function isFillRequired(parentTelemnt) {
    var isfill = true;
    $(parentTelemnt).find('[required]').each(function (i) {
        if (!$(this).attr('disabled') && !$(this).val()) {
            isfill = false;
            return;
        }
    });
    return isfill;
}

function Add(e) { 
    $('.JoiningDate').datepicker().datepicker("setDate", new Date());
    $("#modalAdd").modal('show');
    return false;
}

function View(e) {
    var id = e;
    window.location.href = "/Employee/Details?id=" + id;

}

function imageUploader() {
    $('#imageUploader').modal('show');
}

function ResetFields() {
    $('#modalAdd input[type=text],input[type=password],select,textarea').val('');

}
