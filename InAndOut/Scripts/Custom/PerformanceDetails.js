var oTable;
function Trigger() {
    getPerformanceByEmployee();
}
var reportType = {
    1: 'Document',
    2: 'Manual',
}
var quarter = {
    1:'1st',
    2:'2nd',
    3:'3rd',
    4:'4th'
}
var expectation = {
    1:'High Expectation',
    2:'Average Expectation',
    3:'Below Expectation'
}
function getPerformanceByEmployee() {
    var empid = getParameterByName("id");
    var myUrl = "/Performance/getPerformanceByEmployee?id=" + empid;
    var myData = {};
    XHRGETRequest(myUrl, myData, function (result) {
        data = result;
        if (oTable) { oTable.fnDestroy() }
        $('#performanceTable tbody').html('');
        if (!result || !result.length) { return; }
        for (var i = 0; i < result.length; i++) {
            var item = result[i];

            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.PerformanceReportID);
            $(tr).append('<td>' + (i + 1) + '</td>');
            $(tr).append('<td>' + item.year + '</td>');
            $(tr).append('<td>' + quarter[item.FK_QuarterID] + '</td>');
            $(tr).append('<td>' + item.FK_EmpID + '</td>');
            $(tr).append('<td>' + reportType[item.FK_ReportTypeID] + '</td>');
            $(tr).append('<td>' + expectation[item.FK_ExpectationID] + '</td>');
            $(tr).append('<td>' + item.notes + '</td>');
            if (item.FK_DocumentUploadID != 0) {
                $(tr).append('<td><a href="#" onclick="return GetDocumentDownload(' + item.FK_DocumentUploadID + ')">Download Document</a></td>');
            }
            else {
                $(tr).append('<td> No Document Uploaded</td>');
            }
            $(tr).append('<td>').find('td:last').append(ActiontdHtml(item));
            $('#performanceTable tbody').append(tr);
        }
        oTable = $('#performanceTable').dataTable({ "destroy": true });
    });
}
function ActiontdHtml(item) {
    var td = $('<td>');
    //$(td).append('<span class="pad"><a href="#" class="tblView fa fa-eye" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return View(' + item.EmpID + ');" data-toggle="tooltip" data-placement="top" title="View"></a></span>');
    $(td).append('<span class="pad"><a href="#" class="tblView fa fa-trash-o demo3" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return Delete(' + item.PerformanceReportID + ');" data-toggle="tooltip" data-placement="top" title="Delete"></a></span>');

    //$(td).append('<span class="pad"><a href="#" class="tblAdd fa fa-plus demo3" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return ModalPerformanceAdd(\'' + item.EmpID + '_' + item.Name + '_' + item.SelfId + '_' + item.FK_CompanyID + '\');" data-toggle="tooltip" data-placement="top" title="Add"></a></span>');
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
            var myurl = "/performance/DeletePerformanceReport";
            var data = { id: id };
            XHRPOSTRequest(myurl, data, function (result) {
                if (result) {
                    window.location.reload();
                }
                swal("Deleted!", "The Record has been eliminated", "success");
            });
        }
        $(".sweet-alert .sa-button-container .sa-custom-check").remove();
    });
    window.setTimeout(function () {
        $(".sweet-alert .sa-button-container").append('<div class="sa-custom-check"></div>');
    });
}
function GetDocumentDownload(id) {
    $('#PreviewDocument')[0].src = '/Performance/GetPerformanceReportDocument?id=' + id;
}