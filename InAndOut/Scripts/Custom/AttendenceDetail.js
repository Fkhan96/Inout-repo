function Trigger() {
    DateFilter();
    getAttendanceDetails();

    $('#attdetailrange').daterangepicker({
        opens: 'right'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        $(document).trigger('datefilterevent');
    });

    $(document).on('datefilterevent', function () {
        getAttendanceDetails();
    });

    $('#attdetailrange').on('showCalendar.daterangepicker', function (ev, picker, start, end) {
        var startDate = picker.startDate.format('MM/DD/YYYY');
        var endDate = picker.endDate.format('MM/DD/YYYY');
        $('#attdetailrange').val(startDate + ' - ' + endDate);
    });

}
var oTable;

function DateFilter() {
    var currentDate = moment().format("MM/DD/YYYY");
    var lastMonthDate = moment().subtract(31, "days").format("MM/DD/YYYY");

    $('#attdetailrange').val(lastMonthDate + ' - ' + currentDate);
}

function getAttendanceDetails() {
    var startDate = $('#attdetailrange').val().split('-')[0].trim();
    var endDate = $('#attdetailrange').val().split('-')[1].trim();
    var empId = getParameterByName("id");
    var Parameters = {
        EmpId: empId,
        startDate: startDate,
        endDate: endDate
    };

    var myurl = "/Attendance/GetAttendanceDetails";
    var mydata = new Object();
    mydata = Parameters;

    XHRGETRequest(myurl, mydata, function (result) {
        data = result.Data;
        if (oTable) { oTable.fnDestroy() }
        $('#listAttDetailtb tbody').html('');
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
            $('#listAttDetailtb tbody').append(tr);
        }
        oTable = $('#listAttDetailtb').dataTable({ "destroy": true });
    });
}
