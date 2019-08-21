function Trigger() {

    DateFilter();
    getAttendance();

    $('input[name="daterange"]').daterangepicker({
        opens: 'right'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        $(document).trigger('datefilterevent');
    });

    $(document).on('datefilterevent', function () {
        getAttendance();
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



function ActiontdHtml(item) { debugger
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

function View(e) {
    var id = e;
    window.location.href = "/Attendance/AttDetails?id=" + id;
}

function DateFilter() {
    var currentDate = moment().format("MM/DD/YYYY");
    var yesterdayDate = moment().subtract(1, "days").format("MM/DD/YYYY");

    $('#attdaterange').val(yesterdayDate + ' - ' + currentDate);
}

function getAttendance() {

    var startDate = $('#attdaterange').val().split('-')[0].trim();
    var endDate = $('#attdaterange').val().split('-')[1].trim();

    var Parameters = {
        startDate: startDate,
        endDate: endDate
    };

    var myurl = "/Attendance/GetAttendance";
    var mydata = new Object();
    mydata = Parameters;

    XHRGETRequest(myurl, mydata, function (result) {
        data = result.Data;
        if (oTable) { oTable.fnDestroy() }
        $('#listAttendancetb tbody').html('');
        if (!data || !data.length) { return; }
        for (var i = 0; i < data.length; i++) {
            var item = data[i];

            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.AttDetailsID);
            $(tr).append('<td>' + (i + 1) + '</td>');
            $(tr).append('<td>' + item.Name + '</td>');
            $(tr).append('<td>' + moment(item.CheckinTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + moment(item.CheckoutTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + getEnumName(TimeOff, item.TimeOff) + '</td>');
            $(tr).append('<td>' + item.Status + '</td>');
            $(tr).append('<td>').find('td:last').append(ActiontdHtml(item));
            $('#listAttendancetb tbody').append(tr);
        }
        oTable = $('#listAttendancetb').dataTable({ "destroy": true });
    });
}

