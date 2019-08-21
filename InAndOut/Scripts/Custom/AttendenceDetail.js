function Trigger() {
    DateFilter();
    getList();

    $('input[name="daterange"]').daterangepicker({
        opens: 'right'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        $(document).trigger('datefilterevent');
    });

    $(document).on('datefilterevent', function () {
        getList();
    });

    $('input[name="daterange"]').on('showCalendar.daterangepicker', function (ev, picker, start, end) {
        debugger
        var startDate = picker.startDate.format('MM/DD/YYYY');
        var endDate = picker.endDate.format('MM/DD/YYYY');
        $('#attdaterange').val(startDate + ' - ' + endDate);
    });

}




var data = [];
var activeid;
var oTable;

function getList() {
    var myUrl = "/Attendance/getAttenDetails";
    var mydata = { empid: getParameterByName("id") };
    XHRPOSTRequest(myUrl, mydata, function (result) {
        data = result;
        if (oTable) { oTable.fnDestroy() }
        $('#listAttEmptb tbody').html('');
        if (!result || !result.length) { return; }
        for (var i = 0; i < result.length; i++) {
            var item = result[i];

            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.AttendanceId);
            $(tr).append('<td>' + (i + 1) + '</td>');
            $(tr).append('<td>' + item.EmpName + '</td>');
            $(tr).append('<td>' + moment(item.CheckinTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + moment(item.CheckoutTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + getEnumName(TimeOff, item.TimeOff) + '</td>');
            $(tr).append('<td>' + item.Status + '</td>');
            $(tr).append('<td>').find('td:last').append(ActiontdHtml(item));
            $('#listAttEmptb tbody').append(tr);
        }
        oTable = $('#listAttEmptb').dataTable({ "destroy": true });
    });
}

function ActiontdHtml(item) {
    var td = $('<td>');
    //  if (getCurrentPageRole("edit"))
    $(td).append('<span class="pad"><a href="#" class="tblView fa fa-eye" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return View(' + item.EmpID + ');" data-toggle="tooltip" data-placement="top" title="View"></a></span>');
    //   if (item.type != 1 && getCurrentPageRole("delete")) {
    $(td).append('<span class="pad"><a href="#" class="tblView fa fa-trash-o demo3" style="font-size:200%; padding: 0px 10px 5px 0px;" onclick="return Delete(' + item.EmpID + ');" data-toggle="tooltip" data-placement="top" title="Delete"></a></span>');
    //   }
    //   if (_.where(roleacess, { controller: 'Role', action: 'RoleAcess' }).length)
    //$(td).append(' <span class="pad"><a href="/Role/RoleAcess?id=' + item.roleid + '" class="tblView fa fa-lock demo3" style="font-size:200%; padding: 0px 10px 5px 0px;" data-toggle="tooltip" data-placement="top" title="Role Access"></a></span>');
    return $(td).html();
}

function DateFilter() { 
    var currentDate = moment().format("MM/DD/YYYY");
    var lastMonthDate = moment().subtract(31, "days").format("MM/DD/YYYY");

    $('#attdetailrange').val(lastMonthDate + ' - ' + currentDate);
}

