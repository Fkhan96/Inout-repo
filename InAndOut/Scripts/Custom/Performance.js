var documentID = null;
function Trigger() {
    getList();

    $('#txtUploadFile').on('change', function (e) {
        if ($("input[name='FK_ReportTypeID']:checked").val() == ReportType['Document']) {
            var files = e.target.files;
            var myID = 3; //uncomment this to make sure the ajax URL works
            if (files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < files.length; x++) {
                        //data.append("file" + x, files[x]);
                        data.append(files[x].name, files[x]);
                    }
                    $.ajax({
                        type: "POST",
                        url: '/Performance/UploadPerformanceDocument',
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            documentID = result;
                            //console.log(result);
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                        }
                    });
                }
                else {
                    showNotification("This browser doesn't support file uploads!", "error")
                }
            }
        } else {
            showNotification("Please Select The Document radio box!", "error")
        }
    });

    $(".ReportTypeID").change(function()
    {
        if ($("input[name='FK_ReportTypeID']:checked").val() == ReportType['Document']) {
            document.getElementById('UploadFile').classList.remove('hidden')
        }
        else { document.getElementById('UploadFile').classList.add('hidden')}
    });
}
var ReportType = {
    'Document':1,
    'Manual':2
};
var data = [];
var oTable;
function ModalPerformanceAdd(e) {
    $("#ModalPerformanceAdd").modal('show');
    documentID = null;
    var dataTranfer = e.split('_');
    var item = {
        EmpID: dataTranfer[0],
        Name: dataTranfer[1],
        SelfId: dataTranfer[2],
        FK_CompanyID: dataTranfer[3],
    };
    $("input[name='SelfId']").val(item.SelfId);
    $("input[name='FK_EmpID']").val(item.EmpID);
    $("input[name='Name']").val(item.Name);
    $("input[name='FK_CompanyID']").val(item.FK_CompanyID);
    return false;
}

function getList() {
    var myUrl = "/Performance/GetList";
    var myData = {};
    XHRGETRequest(myUrl, myData, function (result) {
        data = result;
        if (oTable) { oTable.fnDestroy() }
        $('#listtb tbody').html('');
        if (!result || !result.length) { return; }
        for (var i = 0; i < result.length; i++) {
            var item = result[i];
           
            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.EmpID);
            $(tr).append('<td>' + (i + 1) + '</td>');
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
    $(td).append('<span class="pad"><a href="/Performance/details?id=' + item.EmpID + '" target="_blank" class="tblView fa fa-eye" style="font-size:200%; padding: 0px 10px 5px 0px;" data-toggle="tooltip" data-placement="top" title="View"></a></span>');
    $(td).append('<span class="pad"><a href="#" class="tblAdd fa fa-plus demo3" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return ModalPerformanceAdd(\'' + item.EmpID + '_' + item.Name + '_' + item.SelfId + '_' + item.FK_CompanyID + '\');" data-toggle="tooltip" data-placement="top" title="Add"></a></span>');
    return $(td).html();
}
function Submit() {
    if ($("input[name='FK_ReportTypeID']:checked").val() == undefined) {
        showNotification("ReportType is required", "error");
        return;
    }
    if ($("input[name='FK_ExpectationID']:checked").val() == undefined) {
        showNotification("Expectation is required", "error");
        return;
    }
    if ($("input[name='FK_ReportTypeID']:checked").val() == ReportType['Document'] && documentID == null) {
        showNotification("Kindly Upload File", "error");
        return;
    }
    var PerformanceReportDTO = {
        FK_EmpID: $("input[name='FK_EmpID']").val(),
        FK_ReportTypeID: $("input[name='FK_ReportTypeID']:checked").val(),
        FK_QuarterID: $("select[name='FK_QuarterID']").val(),
        FK_DocumentUploadID: documentID,
        FK_ExpectationID: $("input[name='FK_ExpectationID']:checked").val(),
        notes: $("textarea[name='notes']").val(),
        year: $("select[name='year']").val()
    }
    XHRPOSTRequest('/Performance/PostPerformanceReport', PerformanceReportDTO, function (result) {
        HideAjaxLoader();
        showNotification("Save Successfully", "success");
    });
}